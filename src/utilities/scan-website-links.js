import chalk from 'chalk';
import * as core from '@actions/core';
import axios from 'axios';
import * as cheerio from 'cheerio';

const red = chalk.bold.red;

let websiteRoot = process.env.WEBSITE_ROOT;
let scannedPages = [],
    externalLinks = [],
    brokenLinks = {
        internal: [],
        external: []
    };


if (!websiteRoot) {
    core.setFailed(red('Environment variable WEBSITE_ROOT not set'));
    process.exit();
}

const setup = () => {

    // trim trailing / from website root if exists
    if (websiteRoot.slice(-1) == '/') {
        websiteRoot = websiteRoot.slice(0, -1)
    }
};

const transformInternalUrl = (url) => {

    if (url.startsWith('/')) {
        url = websiteRoot + url;
    }

    if (url.slice(-1) == '/') {
        url = url.slice(0, -1)
    }

    if (url.includes('?')) {
        url = url.slice(0, url.indexOf('?'));
    }
    return url;
};

const internalPageToScan = (url) => {
    
    if (url.startsWith('//assets.ctfassets.net')) return false;
    if (url.includes('translate-this-website')) return true;
    if (url.includes('pdf')) return true;

    if (url.startsWith('/') || url.startsWith(websiteRoot)) {
        return transformInternalUrl(url);
    }
    return false;
};

const ignoreUrl = (url) => {
    
    if (url.startsWith('//assets.ctfassets.net')) return true;
    if (url.startsWith('#')) return true;
    if (url.startsWith('mailto:')) return true;
    let ignoreRelativeUrls = [ 'home', 'contents' ];
    if (ignoreRelativeUrls.includes(url)) return true;

    return false;
};

const addBrokenLink = (url, pages, type) => {
    if (!Array.isArray(pages)) {
        pages = [pages];
    }
    
    let idx = brokenLinks[type].findIndex(x => x.url === url);
    if (idx === -1) {
        brokenLinks[type].push({
            url: url,
            pages: pages
        });
    }
    else {
        for (let page of pages) {
            brokenLinks[type][idx].pages.push(page);
        }
    }
};

const delay = ms => new Promise(res => setTimeout(res, ms));

const scanPage = async (url, parent = '') => {
    try {
        await delay(5000);

        const { data } = await axios.get(url, { "User-Agent": "CL Link Checker" });
        const $ = cheerio.load(data);
        const links = $('a[href]').map((_, el) => $(el).attr('href')).get();

        const childPagesToScan = processLinks(links, url);

        for (const childUrl of childPagesToScan) {
            await scanPage(childUrl, url);
        }
    } catch (error) {
        handleScanError(error, url, parent);
    }
};

const processLinks = (links, currentUrl) => {
    const internalToScan = [];

    links.forEach(href => {
        const internalHref = internalPageToScan(href);

        if (internalHref) {
            if (!scannedPages.includes(internalHref)) {
                scannedPages.push(internalHref);
                internalToScan.push(internalHref);
            }
            return;
        }

        // It's an external link
        if (!ignoreUrl(href)) {
            trackExternalLink(href, currentUrl);
        }
    });

    return internalToScan;
};

const trackExternalLink = (url, sourcePage) => {
    const existing = externalLinks.find(x => x.url === url);
    if (existing) {
        existing.pages.push(sourcePage);
    } else {
        externalLinks.push({ url, pages: [sourcePage] });
    }
};

const handleScanError = (error, url, parent) => {
    if (error.response?.status === 404) {
        addBrokenLink(url, parent, 'internal');
    } else {
        console.error(`Error scanning ${url}: ${error.message}`);
    }
};

const checkLink = async (link) => {
    let responseStatus;
    try {
        // wait 10 seconds - we don't want to flood servers with loads of requests
        await delay (10000);
    
        let headers = {
              "Accept": "*/*, application/json, text/plain",
              "Referer": "https://www.support-for-care-leavers.education.gov.uk",
              "Referrer-Policy": "strict-origin-when-cross-origin"
            };

        try {
            const parsedUrl = new URL(link);
            if (parsedUrl.host === 'support-for-care-leavers.education.gov.uk' || 
                parsedUrl.host.endsWith('.support-for-care-leavers.education.gov.uk')) {
                headers['User-Agent'] = 'CL Link Checker';
            }
        } catch (e) {
            // If the URL is invalid, we skip adding special headers.
        }

        const response = await axios.get(link, {
            headers: headers
        });

        responseStatus = response.status;
    }
    catch (err) {
        if (err.status) {
            responseStatus = err.status;
        }
    }

    return responseStatus;
};

const checkLinks = async (externalLinks) => {

    externalLinks.sort((a,b) => {
        if (a.url < b.url) return -1;
        return 1;
    });

    for (const externalLink of externalLinks) {
    const status = await checkLink(externalLink.url);

    if (status === 404) {
        addBrokenLink(externalLink.url, externalLink.pages, 'external');
    }
}
};

const reportOutput = () => {
    const outputLink = (link) => {
        console.log('   ' + link.url);
        console.log('   on page(s):');

        if (link.pages.length >= 5) {
            for (let count=0; count<=5; count++) {
                console.log('      ' + link.pages[count]);

            }
            console.log('      ... and ' + (link.pages.length - 5) + ' other pages');
        }
        else {
            for (let page of link.pages) {
                console.log('      ' + page);
            }    
        }
        console.log('  ');
    };

    if (brokenLinks.internal.length > 0) {
        console.log('Broken Internal Link(s):');
        for (let link of brokenLinks.internal) {
            outputLink(link);
        }
        console.log('----------------------');
    }
    if (brokenLinks.external.length > 0) {
        console.log('Broken External Link(s):');
        for (let link of brokenLinks.external) {
            outputLink(link);
        }
        console.log('----------------------');
    }
};

setup();
await scanPage(websiteRoot);
await checkLinks(externalLinks);
reportOutput();

if (brokenLinks.internal.length > 0 || brokenLinks.external.length > 0) {
    core.setFailed('Broken links found');
}