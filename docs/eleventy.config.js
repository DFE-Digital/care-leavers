import { govukEleventyPlugin } from '@x-govuk/govuk-eleventy-plugin'
import { pathPrefix } from "./path-prefix.js";

export default function(eleventyConfig) {
  eleventyConfig.addPassthroughCopy("content/assets/images");
  eleventyConfig.addPassthroughCopy("content/assets/js")

  eleventyConfig.addPlugin(govukEleventyPlugin, {
    stylesheets: ['/assets/styles.css'],
    titleSuffix: 'Care Leavers',
    templates: {
      sitemap: true,
      searchIndex: true
    },
    header: {
      logotype: {
        html: '<img src="/assets/images/department-for-education_white.png" alt="Department for Education">'
      },
      search: {
        indexPath: `${pathPrefix}search-index.json`,
        sitemapPath: '/sitemap'
      }
    },
    serviceNavigation: {
      serviceName: 'Care Leavers',
      navigation: [
        {
          text: 'Architecture',
          href: '/architecture/'
        },
        {
          text: 'Developers',
          href: '/developers/'
        },
        {
          text: 'Decisions',
          href: '/decisions/'
        },
        {
          text: 'Testing',
          href: '/testing/'
        },
        {
          text: 'Operational',
          href: '/operational/'
        },
        {
          text: 'Scans',
          href: '/scans/'
        }
      ]
    },
    footer: {
      logo: false,
      meta: {
        items: [
          {
            href: 'https://github.com/DFE-Digital/care-leavers',
            text: 'GitHub repository'
          }
        ],
        html: '<script src="/assets/js/mermaid.js" type="module"></script>'
      }
    }
  })

  return {
    pathPrefix: pathPrefix,
    dataTemplateEngine: 'njk',
    htmlTemplateEngine: 'njk',
    markdownTemplateEngine: 'njk',
    dir: {
      input: 'content',
    }
  }
};