# ZAP Scanning Report

ZAP by [Checkmarx](https://checkmarx.com/).


## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 0 |
| Medium | 4 |
| Low | 5 |
| Informational | 13 |




## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- |
| Backup File Disclosure | Medium | 11 |
| CSP: style-src unsafe-inline | Medium | 12 |
| Proxy Disclosure | Medium | 21 |
| Sub Resource Integrity Attribute Missing | Medium | 11 |
| Cross-Domain JavaScript Source File Inclusion | Low | 10 |
| Insufficient Site Isolation Against Spectre Vulnerability | Low | 9 |
| Permissions Policy Header Not Set | Low | 9 |
| Private IP Disclosure | Low | 1 |
| Timestamp Disclosure - Unix | Low | 1 |
| Base64 Disclosure | Informational | 12 |
| Cookie Slack Detector | Informational | 39 |
| GET for POST | Informational | 1 |
| Non-Storable Content | Informational | 5 |
| Re-examine Cache-control Directives | Informational | 9 |
| Sec-Fetch-Dest Header is Missing | Informational | 3 |
| Sec-Fetch-Mode Header is Missing | Informational | 3 |
| Sec-Fetch-Site Header is Missing | Informational | 3 |
| Sec-Fetch-User Header is Missing | Informational | 3 |
| Session Management Response Identified | Informational | 1 |
| Storable and Cacheable Content | Informational | 5 |
| User Agent Fuzzer | Informational | 570 |
| User Controllable HTML Element Attribute (Potential XSS) | Informational | 3 |




## Alert Detail



### [ Backup File Disclosure ](https://www.zaproxy.org/docs/alerts/10095/)



##### Medium (Medium)

### Description

A backup of the file was disclosed by the web server.

* URL: https://staging.support-for-care-leavers.education.gov.uk/Copy%2520of%2520en/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/Copy%20of%20en/cookie-policy`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy] is available at [https://staging.support-for-care-leavers.education.gov.uk/Copy%20of%20en/cookie-policy]`
* URL: https://staging.support-for-care-leavers.education.gov.uk/enbackup/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/enbackup/cookie-policy`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy] is available at [https://staging.support-for-care-leavers.education.gov.uk/enbackup/cookie-policy]`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page%2520-%2520Copy
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page%20-%20Copy`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page] is available at [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page%20-%20Copy]`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page%2520-%2520Copy%2520(2&29
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page%20-%20Copy%20(2)`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page] is available at [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page%20-%20Copy%20(2)]`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page%2520-%2520Copy%2520(3&29
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page%20-%20Copy%20(3)`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page] is available at [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page%20-%20Copy%20(3)]`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.jar
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.jar`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page] is available at [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.jar]`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.log
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.log`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page] is available at [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.log]`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.old
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.old`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page] is available at [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.old]`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.~bk
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.~bk`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page] is available at [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page.~bk]`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-pagebackup
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-pagebackup`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page] is available at [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-pagebackup]`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page~
  * Method: `GET`
  * Parameter: ``
  * Attack: `https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page~`
  * Evidence: ``
  * Other Info: `A backup of [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page] is available at [https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page~]`

Instances: 11

### Solution

Do not edit files in-situ on the web server, and ensure that un-necessary files (including hidden files) are removed from the web server.

### Reference


* [ https://cwe.mitre.org/data/definitions/530.html ](https://cwe.mitre.org/data/definitions/530.html)
* [ https://owasp.org/www-project-web-security-testing-guide/v41/4-Web_Application_Security_Testing/02-Configuration_and_Deployment_Management_Testing/04-Review_Old_Backup_and_Unreferenced_Files_for_Sensitive_Information.html ](https://owasp.org/www-project-web-security-testing-guide/v41/4-Web_Application_Security_Testing/02-Configuration_and_Deployment_Management_Testing/04-Review_Old_Backup_and_Unreferenced_Files_for_Sensitive_Information.html)


#### CWE Id: [ 530 ](https://cwe.mitre.org/data/definitions/530.html)


#### WASC Id: 34

#### Source ID: 1

### [ CSP: style-src unsafe-inline ](https://www.zaproxy.org/docs/alerts/10055/)



##### Medium (High)

### Description

Content Security Policy (CSP) is an added layer of security that helps to detect and mitigate certain types of attacks. Including (but not limited to) Cross Site Scripting (XSS), and data injection attacks. These attacks are used for everything from data theft to site defacement or distribution of malware. CSP provides a set of standard HTTP headers that allow website owners to declare approved sources of content that browsers should be allowed to load on that page — covered types are JavaScript, CSS, HTML frames, fonts, images and embeddable objects such as Java applets, ActiveX, audio and video files.

* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-E/H4y8tSwnHJvqjmHFHGo4sMybGy5DVIs8raAABbh4I=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-l/P65nKiIAAQrDT/x8VsnCCe2TRPPEnbzM5o+1ihL/Q=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-acFlfUSkt9/S6jj3x0zlxWC5Ohb4d0SWNZpJxB3HOk0=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-ujABO9Bv4JSA1SR8CjSFKOtpqXtrnQQoyj96u0U5UT8=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-C17km0Qpi8RFiueaH8/v3Gi5bRdwXKruH54fnr0zpLI=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-hmiIgbqpuN/rn7MV1QaD60uWtxhfg8y3zorhBvb76Nk=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-Rsp+Q4CsTHSv/pBxSt+B48FXjF517/ioQjE5o8PzMig=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-YUvI3OH4g4cqGzYfdn7keqvOF3TACnVqnUdOPtsAAFk=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-73UWKP2tJsGtEpFRZiKiKUggAzQ8lHKnBvTYnzhei0E=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-MtMNNTrGJZw+NyMj3Hlbp/UqQ3LV4eWtR51UoWHYX5o=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-rVaE3D5Ob3NeQgXpMFoFDEbM1qaJeJXeGNMM7ZsHHCM=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' cdn.openshareweb.com cdn.openshareweb.net cdn.shareaholic.net partner.shareaholic.com *.googletagmanager.com *.google-analytics.com *.clarity.ms c.bing.com 'sha256-xrAXrOY+vbRBqDmgZaRaAkCXcYa6i7XrdPSlOPQiY2E=' 'nonce-yyf4QVaCdVk83Iy9aMiFwYZrvPQQeNbF+TZVi7DXWak=';style-src 'self' cdn.openshareweb.com rsms.me 'unsafe-inline';connect-src 'self' *.shareaholic.com *.shareaholic.net cdn.openshareweb.com *.clarity.ms c.bing.com *.googletagmanager.com *.google-analytics.com *.analytics.google.com;font-src 'self' data: rsms.me cdn.openshareweb.net cdn.openshareweb.com;form-action 'self';img-src 'self' data: cdn.openshareweb.com cdn.openshareweb.net images.ctfassets.net *.googletagmanager.com *.google-analytics.com;frame-ancestors 'self' *.googletagmanager.com app.contentful.com *.riddle.com;frame-src *.googletagmanager.com app.contentful.com *.riddle.com`
  * Other Info: `style-src includes unsafe-inline.`

Instances: 12

### Solution

Ensure that your web server, application server, load balancer, etc. is properly configured to set the Content-Security-Policy header.

### Reference


* [ https://www.w3.org/TR/CSP/ ](https://www.w3.org/TR/CSP/)
* [ https://caniuse.com/#search=content+security+policy ](https://caniuse.com/#search=content+security+policy)
* [ https://content-security-policy.com/ ](https://content-security-policy.com/)
* [ https://github.com/HtmlUnit/htmlunit-csp ](https://github.com/HtmlUnit/htmlunit-csp)
* [ https://developers.google.com/web/fundamentals/security/csp#policy_applies_to_a_wide_variety_of_resources ](https://developers.google.com/web/fundamentals/security/csp#policy_applies_to_a_wide_variety_of_resources)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Proxy Disclosure ](https://www.zaproxy.org/docs/alerts/40025/)



##### Medium (Medium)

### Description

2 proxy server(s) were detected or fingerprinted. This information helps a potential attacker to determine
- A list of targets for an attack against the application.
 - Potential vulnerabilities on the proxy servers that service the application.
 - The presence or absence of any proxy-based components that might cause attacks against the application to be detected, prevented, or mitigated.

* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/dfe-logo-alt.png
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/dfe-logo.png
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images/favicon.ico%3Fv=04PFFEabSV4DnYUjW2dFPt0SP9F8-oEwjaac26G8bsI
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images/favicon.svg%3Fv=BY_XOpoc_9SAi-Nt7O42KebBXEsOb2Fu1GnBttHVEcU
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images/govuk-icon-180.png%3Fv=SA-cZsEiik4F8BmKKh_vfU3kYC-4oe6ZUbvxokSh-QM
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images/govuk-icon-mask.svg%3Fv=N0Z3XZRPHy7GhBSmbzJ0FMxChzQ6CsBnL3LwjbH9xHQ
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/css/application.css%3Fv=Bl5QJlZQVMxI_TNMPxVjaXzPxiu6UJBQ8ZoBnq5Sc90
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/css/print.css%3Fv=qGMYIg8zPLpHlU6WziUggN6hsNAo2-XrDoeK-_ztkOA
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/js/dfefrontend.min.js
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: `TRACE, OPTIONS methods with 'Max-Forwards' header. TRACK method.`
  * Evidence: ``
  * Other Info: `Using the TRACE, OPTIONS, and TRACK methods, the following proxy servers have been identified between ZAP and the application/web server:
- Unknown
- Unknown
The following web/application server has been identified:
- Unknown
`

Instances: 21

### Solution

Disable the 'TRACE' method on the proxy servers, as well as the origin web/application server.
Disable the 'OPTIONS' method on the proxy servers, as well as the origin web/application server, if it is not required for other purposes, such as 'CORS' (Cross Origin Resource Sharing).
Configure the web and application servers with custom error pages, to prevent 'fingerprintable' product-specific error pages being leaked to the user in the event of HTTP errors, such as 'TRACK' requests for non-existent pages.
Configure all proxies, application servers, and web servers to prevent disclosure of the technology and version information in the 'Server' and 'X-Powered-By' HTTP response headers.


### Reference


* [ https://tools.ietf.org/html/rfc7231#section-5.1.2 ](https://tools.ietf.org/html/rfc7231#section-5.1.2)


#### CWE Id: [ 204 ](https://cwe.mitre.org/data/definitions/204.html)


#### WASC Id: 45

#### Source ID: 1

### [ Sub Resource Integrity Attribute Missing ](https://www.zaproxy.org/docs/alerts/90003/)



##### Medium (High)

### Description

The integrity attribute is missing on a script or link tag served by an external server. The integrity tag prevents an attacker who have gained access to this server from injecting a malicious content.

* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link rel="preload" href="https://cdn.shareaholic.net/assets/pub/shareaholic.js" as="script"/>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="E/H4y8tSwnHJvqjmHFHGo4sMybGy5DVIs8raAABbh4I="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link rel="preload" href="https://cdn.shareaholic.net/assets/pub/shareaholic.js" as="script"/>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="acFlfUSkt9/S6jj3x0zlxWC5Ohb4d0SWNZpJxB3HOk0="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link rel="preload" href="https://cdn.shareaholic.net/assets/pub/shareaholic.js" as="script"/>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="C17km0Qpi8RFiueaH8/v3Gi5bRdwXKruH54fnr0zpLI="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link rel="preload" href="https://cdn.shareaholic.net/assets/pub/shareaholic.js" as="script"/>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="hmiIgbqpuN/rn7MV1QaD60uWtxhfg8y3zorhBvb76Nk="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<link rel="preload" href="https://cdn.shareaholic.net/assets/pub/shareaholic.js" as="script"/>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="MtMNNTrGJZw&#x2B;NyMj3Hlbp/UqQ3LV4eWtR51UoWHYX5o="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="yyf4QVaCdVk83Iy9aMiFwYZrvPQQeNbF&#x2B;TZVi7DXWak="></script>`
  * Other Info: ``

Instances: 11

### Solution

Provide a valid integrity attribute to the tag.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/Security/Subresource_Integrity ](https://developer.mozilla.org/en-US/docs/Web/Security/Subresource_Integrity)


#### CWE Id: [ 345 ](https://cwe.mitre.org/data/definitions/345.html)


#### WASC Id: 15

#### Source ID: 3

### [ Cross-Domain JavaScript Source File Inclusion ](https://www.zaproxy.org/docs/alerts/10017/)



##### Low (Medium)

### Description

The page includes one or more script files from a third-party domain.

* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `https://cdn.shareaholic.net/assets/pub/shareaholic.js`
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="E/H4y8tSwnHJvqjmHFHGo4sMybGy5DVIs8raAABbh4I="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `https://cdn.shareaholic.net/assets/pub/shareaholic.js`
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="l/P65nKiIAAQrDT/x8VsnCCe2TRPPEnbzM5o&#x2B;1ihL/Q="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `https://cdn.shareaholic.net/assets/pub/shareaholic.js`
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="acFlfUSkt9/S6jj3x0zlxWC5Ohb4d0SWNZpJxB3HOk0="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `https://cdn.shareaholic.net/assets/pub/shareaholic.js`
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="C17km0Qpi8RFiueaH8/v3Gi5bRdwXKruH54fnr0zpLI="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `https://cdn.shareaholic.net/assets/pub/shareaholic.js`
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="hmiIgbqpuN/rn7MV1QaD60uWtxhfg8y3zorhBvb76Nk="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `https://cdn.shareaholic.net/assets/pub/shareaholic.js`
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="eKf//9ue0sfo1gB0yqev35omJkVVhOK0/yKfnlzSg2w="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `https://cdn.shareaholic.net/assets/pub/shareaholic.js`
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="73UWKP2tJsGtEpFRZiKiKUggAzQ8lHKnBvTYnzhei0E="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `https://cdn.shareaholic.net/assets/pub/shareaholic.js`
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="MtMNNTrGJZw&#x2B;NyMj3Hlbp/UqQ3LV4eWtR51UoWHYX5o="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `https://cdn.shareaholic.net/assets/pub/shareaholic.js`
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="rVaE3D5Ob3NeQgXpMFoFDEbM1qaJeJXeGNMM7ZsHHCM="></script>`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `https://cdn.shareaholic.net/assets/pub/shareaholic.js`
  * Attack: ``
  * Evidence: `<script data-cfasync="false" async src="https://cdn.shareaholic.net/assets/pub/shareaholic.js" nonce="yyf4QVaCdVk83Iy9aMiFwYZrvPQQeNbF&#x2B;TZVi7DXWak="></script>`
  * Other Info: ``

Instances: 10

### Solution

Ensure JavaScript source files are loaded from only trusted sources, and the sources can't be controlled by end users of the application.

### Reference



#### CWE Id: [ 829 ](https://cwe.mitre.org/data/definitions/829.html)


#### WASC Id: 15

#### Source ID: 3

### [ Insufficient Site Isolation Against Spectre Vulnerability ](https://www.zaproxy.org/docs/alerts/90004/)



##### Low (Medium)

### Description

Cross-Origin-Resource-Policy header is an opt-in header designed to counter side-channels attacks like Spectre. Resource should be specifically set as shareable amongst different origins.

* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images/favicon.svg%3Fv=BY_XOpoc_9SAi-Nt7O42KebBXEsOb2Fu1GnBttHVEcU
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images/govuk-icon-mask.svg%3Fv=N0Z3XZRPHy7GhBSmbzJ0FMxChzQ6CsBnL3LwjbH9xHQ
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/robots.txt
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/sitemap.xml
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Cross-Origin-Embedder-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/sitemap.xml
  * Method: `GET`
  * Parameter: `Cross-Origin-Embedder-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Cross-Origin-Opener-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/sitemap.xml
  * Method: `GET`
  * Parameter: `Cross-Origin-Opener-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 9

### Solution

Ensure that the application/web server sets the Cross-Origin-Resource-Policy header appropriately, and that it sets the Cross-Origin-Resource-Policy header to 'same-origin' for all web pages.
'same-site' is considered as less secured and should be avoided.
If resources must be shared, set the header to 'cross-origin'.
If possible, ensure that the end user uses a standards-compliant and modern web browser that supports the Cross-Origin-Resource-Policy header (https://caniuse.com/mdn-http_headers_cross-origin-resource-policy).

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Cross-Origin_Resource_Policy ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Cross-Origin_Resource_Policy)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 14

#### Source ID: 3

### [ Permissions Policy Header Not Set ](https://www.zaproxy.org/docs/alerts/10063/)



##### Low (Medium)

### Description

Permissions Policy Header is an added layer of security that helps to restrict from unauthorized access or usage of browser/client features by web resources. This policy ensures the user privacy by limiting or specifying the features of the browsers can be used by the web resources. Permissions Policy provides a set of standard HTTP headers that allow website owners to limit which features of browsers can be used by the page such as camera, microphone, location, full screen etc.

* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js/dfefrontend.min.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 9

### Solution

Ensure that your web server, application server, load balancer, etc. is configured to set the Permissions-Policy header.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Permissions-Policy ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Permissions-Policy)
* [ https://developer.chrome.com/blog/feature-policy/ ](https://developer.chrome.com/blog/feature-policy/)
* [ https://scotthelme.co.uk/a-new-security-header-feature-policy/ ](https://scotthelme.co.uk/a-new-security-header-feature-policy/)
* [ https://w3c.github.io/webappsec-feature-policy/ ](https://w3c.github.io/webappsec-feature-policy/)
* [ https://www.smashingmagazine.com/2018/12/feature-policy/ ](https://www.smashingmagazine.com/2018/12/feature-policy/)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Private IP Disclosure ](https://www.zaproxy.org/docs/alerts/2/)



##### Low (Medium)

### Description

A private IP (such as 10.x.x.x, 172.x.x.x, 192.168.x.x) or an Amazon EC2 private hostname (for example, ip-10-0-56-78) has been found in the HTTP response body. This information might be helpful for further attacks targeting internal systems.

* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images/favicon.svg%3Fv=BY_XOpoc_9SAi-Nt7O42KebBXEsOb2Fu1GnBttHVEcU
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `10.02.68.86`
  * Other Info: `10.02.68.86
`

Instances: 1

### Solution

Remove the private IP address from the HTTP response body. For comments, use JSP/ASP/PHP comment instead of HTML/JavaScript comment which can be seen by client browsers.

### Reference


* [ https://tools.ietf.org/html/rfc1918 ](https://tools.ietf.org/html/rfc1918)


#### CWE Id: [ 497 ](https://cwe.mitre.org/data/definitions/497.html)


#### WASC Id: 13

#### Source ID: 3

### [ Timestamp Disclosure - Unix ](https://www.zaproxy.org/docs/alerts/10096/)



##### Low (Low)

### Description

A timestamp was disclosed by the application/web server. - Unix

* URL: https://staging.support-for-care-leavers.education.gov.uk/css/application.css%3Fv=Bl5QJlZQVMxI_TNMPxVjaXzPxiu6UJBQ8ZoBnq5Sc90
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1428571429`
  * Other Info: `1428571429, which evaluates to: 2015-04-09 09:23:49.`

Instances: 1

### Solution

Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.

### Reference


* [ https://cwe.mitre.org/data/definitions/200.html ](https://cwe.mitre.org/data/definitions/200.html)


#### CWE Id: [ 497 ](https://cwe.mitre.org/data/definitions/497.html)


#### WASC Id: 13

#### Source ID: 3

### [ Base64 Disclosure ](https://www.zaproxy.org/docs/alerts/10094/)



##### Informational (Medium)

### Description

Base64 encoded data was disclosed by the application/web server. Note: in the interests of performance not all base64 strings in the response were analyzed individually, the entire response should be looked at by the analyst/security team/developer(s).

* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `E/H4y8tSwnHJvqjmHFHGo4sMybGy5DVIs8raAABbh4I=`
  * Other Info: `����R�qɾ��Qƣ�ɱ��5H���  [��`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `l/P65nKiIAAQrDT/x8VsnCCe2TRPPEnbzM5o`
  * Other Info: `����r�  �4���l� ��4O<I���h`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `CfDJ8NVHRbWMHiJBs9kdGgtuDuA4oVAvteQDDZqDwMesYzWSrsDXgeKzo3epRFTU8dNMqF4NYZY5LgoB1uiETlL07wGnkcn-cbDVMfxo-8OJdyMtDaCaZ_P-JvQAeT3WaFCCj99peu4ys-CidP8-Cc6RDYU`
  * Other Info: `	����GE��"A��n�8�P/�����Ǭc5���ׁⳣw�DT���L�^a�9.
��NR������q��1�h�Éw#-��g��&� y=�hP���iz�2��t�>	Α�`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `BY_XOpoc_9SAi-Nt7O42KebBXEsOb2Fu1GnBttHVEcU`
  * Other Info: `��:��Ԁ��m��6)��\Koan�i�����`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `C17km0Qpi8RFiueaH8/v3Gi5bRdwXKruH54fnr0zpLI=`
  * Other Info: `^�D)��E�����h�mp\�����3��`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `hmiIgbqpuN/rn7MV1QaD60uWtxhfg8y3zorhBvb76Nk=`
  * Other Info: `�h������럳���K��_�̷Ί�����`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `BY_XOpoc_9SAi-Nt7O42KebBXEsOb2Fu1GnBttHVEcU`
  * Other Info: `��:��Ԁ��m��6)��\Koan�i�����`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `YUvI3OH4g4cqGzYfdn7keqvOF3TACnVqnUdOPtsAAFk=`
  * Other Info: `aK������*6v~�z��t�
uj�GN>�  Y`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `NyMj3Hlbp/UqQ3LV4eWtR51UoWHYX5o=`
  * Other Info: `7##�y[��*Cr���G�T�a�_�`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `yyf4QVaCdVk83Iy9aMiFwYZrvPQQeNbF`
  * Other Info: `�'�AV�uY<܌�hȅ��k��x��`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `73UWKP2tJsGtEpFRZiKiKUggAzQ8lHKnBvTYnzhei0E=`
  * Other Info: `�u(��&���Qf"�)H 4<�r��؟8^�A`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `rVaE3D5Ob3NeQgXpMFoFDEbM1qaJeJXeGNMM7ZsHHCM=`
  * Other Info: `�V��>Nos^B�0ZF�֦�x����#`

Instances: 12

### Solution

Manually confirm that the Base64 data does not leak sensitive information, and that the data cannot be aggregated/used to exploit other vulnerabilities.

### Reference


* [ https://projects.webappsec.org/w/page/13246936/Information%20Leakage ](https://projects.webappsec.org/w/page/13246936/Information%20Leakage)


#### CWE Id: [ 319 ](https://cwe.mitre.org/data/definitions/319.html)


#### WASC Id: 13

#### Source ID: 3

### [ Cookie Slack Detector ](https://www.zaproxy.org/docs/alerts/90027/)



##### Informational (Low)

### Description

Repeated GET requests: drop a different cookie each time, followed by normal request with all cookies to stabilize session, compare responses against original baseline GET. This can reveal areas where cookie based authentication/attributes are not actually enforced.

* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/dfe-logo-alt.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/dfe-logo.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/css/print.css%3Fv=qGMYIg8zPLpHlU6WziUggN6hsNAo2-XrDoeK-_ztkOA
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Dropping this cookie appears to have invalidated the session: [.AspNetCore.Antiforgery.VyLW6ORzMgk] A follow-on request with all original cookies still had a different response than the original request.
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Dropping this cookie appears to have invalidated the session: [.AspNetCore.Antiforgery.VyLW6ORzMgk] A follow-on request with all original cookies still had a different response than the original request.
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Dropping this cookie appears to have invalidated the session: [.AspNetCore.Antiforgery.VyLW6ORzMgk] A follow-on request with all original cookies still had a different response than the original request.
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/js/dfefrontend.min.js
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Dropping this cookie appears to have invalidated the session: [.AspNet.Consent] A follow-on request with all original cookies still had a different response than the original request.
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNet.Consent,.AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: .AspNetCore.Antiforgery.VyLW6ORzMgk
`

Instances: 39

### Solution



### Reference


* [ https://cwe.mitre.org/data/definitions/205.html ](https://cwe.mitre.org/data/definitions/205.html)


#### CWE Id: [ 205 ](https://cwe.mitre.org/data/definitions/205.html)


#### WASC Id: 45

#### Source ID: 1

### [ GET for POST ](https://www.zaproxy.org/docs/alerts/10058/)



##### Informational (High)

### Description

A request that was originally observed as a POST was also accepted as a GET. This issue does not represent a security weakness unto itself, however, it may facilitate simplification of other attacks. For example if the original POST is subject to Cross-Site Scripting (XSS), then this finding may indicate that a simplified (GET based) XSS may also be possible.

* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `GET https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy?AcceptCookies=True&__RequestVerificationToken=CfDJ8NVHRbWMHiJBs9kdGgtuDuDdqBLKeLv44g6G0bR-Jjaq64pZcK_YXQgHjmm4kwkMhQAT6a6ElcgDOVt-KdiXKMOoG5LjVRwiLaZGqNlzgMjpyioGwrmwsxEnrjQ9Jlk1tdNVvzAkwQIKxiycVCllkbU HTTP/1.1`
  * Other Info: ``

Instances: 1

### Solution

Ensure that only POST is accepted where POST is expected.

### Reference



#### CWE Id: [ 16 ](https://cwe.mitre.org/data/definitions/16.html)


#### WASC Id: 20

#### Source ID: 1

### [ Non-Storable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are not storable by caching components such as proxy servers. If the response does not contain sensitive, personal or user-specific information, it may benefit from being stored and cached, to improve performance.

* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `302`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `302`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `302`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `302`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `302`
  * Other Info: ``

Instances: 5

### Solution

The content may be marked as storable by ensuring that the following conditions are satisfied:
The request method must be understood by the cache and defined as being cacheable ("GET", "HEAD", and "POST" are currently defined as cacheable)
The response status code must be understood by the cache (one of the 1XX, 2XX, 3XX, 4XX, or 5XX response classes are generally understood)
The "no-store" cache directive must not appear in the request or response header fields
For caching by "shared" caches such as "proxy" caches, the "private" response directive must not appear in the response
For caching by "shared" caches such as "proxy" caches, the "Authorization" header field must not appear in the request, unless the response explicitly allows it (using one of the "must-revalidate", "public", or "s-maxage" Cache-Control response directives)
In addition to the conditions above, at least one of the following conditions must also be satisfied by the response:
It must contain an "Expires" header field
It must contain a "max-age" response directive
For "shared" caches such as "proxy" caches, it must contain a "s-maxage" response directive
It must contain a "Cache Control Extension" that allows it to be cached
It must have a status code that is defined as cacheable by default (200, 203, 204, 206, 300, 301, 404, 405, 410, 414, 501).

### Reference


* [ https://datatracker.ietf.org/doc/html/rfc7234 ](https://datatracker.ietf.org/doc/html/rfc7234)
* [ https://datatracker.ietf.org/doc/html/rfc7231 ](https://datatracker.ietf.org/doc/html/rfc7231)
* [ https://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html ](https://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html)


#### CWE Id: [ 524 ](https://cwe.mitre.org/data/definitions/524.html)


#### WASC Id: 13

#### Source ID: 3

### [ Re-examine Cache-control Directives ](https://www.zaproxy.org/docs/alerts/10015/)



##### Informational (Low)

### Description

The cache-control header has not been set properly or is missing, allowing the browser and proxies to cache content. For static assets like css, js, or image files this might be intended, however, the resources should be reviewed to ensure that no sensitive content will be cached.

* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: `no-cache, no-store`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/robots.txt
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/sitemap.xml
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 9

### Solution

For secure content, ensure the cache-control HTTP header is set with "no-cache, no-store, must-revalidate". If an asset should be cached consider setting the directives "public, max-age, immutable".

### Reference


* [ https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching ](https://cheatsheetseries.owasp.org/cheatsheets/Session_Management_Cheat_Sheet.html#web-content-caching)
* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control)
* [ https://grayduck.mn/2021/09/13/cache-control-recommendations/ ](https://grayduck.mn/2021/09/13/cache-control-recommendations/)


#### CWE Id: [ 525 ](https://cwe.mitre.org/data/definitions/525.html)


#### WASC Id: 13

#### Source ID: 3

### [ Sec-Fetch-Dest Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Specifies how and where the data would be used. For instance, if the value is audio, then the requested resource must be audio data and not any other type of resource.

* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 3

### Solution

Ensure that Sec-Fetch-Dest header is included in request headers.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Dest ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Dest)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Sec-Fetch-Mode Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Allows to differentiate between requests for navigating between HTML pages and requests for loading resources like images, audio etc.

* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 3

### Solution

Ensure that Sec-Fetch-Mode header is included in request headers.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Mode ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Mode)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Sec-Fetch-Site Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Specifies the relationship between request initiator's origin and target's origin.

* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 3

### Solution

Ensure that Sec-Fetch-Site header is included in request headers.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Site ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-Site)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Sec-Fetch-User Header is Missing ](https://www.zaproxy.org/docs/alerts/90005/)



##### Informational (High)

### Description

Specifies if a navigation request was initiated by a user.

* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 3

### Solution

Ensure that Sec-Fetch-User header is included in user initiated requests.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-User ](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Sec-Fetch-User)


#### CWE Id: [ 352 ](https://cwe.mitre.org/data/definitions/352.html)


#### WASC Id: 9

#### Source ID: 3

### [ Session Management Response Identified ](https://www.zaproxy.org/docs/alerts/10112/)



##### Informational (Medium)

### Description

The given response has been identified as containing a session management token. The 'Other Info' field contains a set of header tokens that can be used in the Header Based Session Management Method. If the request is in a context which has a Session Management Method set to "Auto-Detect" then this rule will change the session management to use the tokens identified.

* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `.AspNetCore.Antiforgery.VyLW6ORzMgk`
  * Attack: ``
  * Evidence: `CfDJ8NVHRbWMHiJBs9kdGgtuDuA4oVAvteQDDZqDwMesYzWSrsDXgeKzo3epRFTU8dNMqF4NYZY5LgoB1uiETlL07wGnkcn-cbDVMfxo-8OJdyMtDaCaZ_P-JvQAeT3WaFCCj99peu4ys-CidP8-Cc6RDYU`
  * Other Info: `
cookie:.AspNetCore.Antiforgery.VyLW6ORzMgk`

Instances: 1

### Solution

This is an informational alert rather than a vulnerability and so there is nothing to fix.

### Reference


* [ https://www.zaproxy.org/docs/desktop/addons/authentication-helper/session-mgmt-id ](https://www.zaproxy.org/docs/desktop/addons/authentication-helper/session-mgmt-id)



#### Source ID: 3

### [ Storable and Cacheable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are storable by caching components such as proxy servers, and may be retrieved directly from the cache, rather than from the origin server by the caching servers, in response to similar requests from other users. If the response data is sensitive, personal or user-specific, this may result in sensitive information being leaked. In some cases, this may even result in a user gaining complete control of the session of another user, depending on the configuration of the caching components in use in their environment. This is primarily an issue where "shared" caching servers such as "proxy" caches are configured on the local network. This configuration is typically found in corporate or educational environments, for instance.

* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images/favicon.svg%3Fv=BY_XOpoc_9SAi-Nt7O42KebBXEsOb2Fu1GnBttHVEcU
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `max-age=2678400`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images/govuk-icon-mask.svg%3Fv=N0Z3XZRPHy7GhBSmbzJ0FMxChzQ6CsBnL3LwjbH9xHQ
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `max-age=2678400`
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://staging.support-for-care-leavers.education.gov.uk/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`

Instances: 5

### Solution

Validate that the response does not contain sensitive, personal or user-specific information. If it does, consider the use of the following HTTP response headers, to limit, or prevent the content being stored and retrieved from the cache by another user:
Cache-Control: no-cache, no-store, must-revalidate, private
Pragma: no-cache
Expires: 0
This configuration directs both HTTP 1.0 and HTTP 1.1 compliant caching servers to not store the response, and to not retrieve the response (without validation) from the cache, in response to a similar request.

### Reference


* [ https://datatracker.ietf.org/doc/html/rfc7234 ](https://datatracker.ietf.org/doc/html/rfc7234)
* [ https://datatracker.ietf.org/doc/html/rfc7231 ](https://datatracker.ietf.org/doc/html/rfc7231)
* [ https://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html ](https://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html)


#### CWE Id: [ 524 ](https://cwe.mitre.org/data/definitions/524.html)


#### WASC Id: 13

#### Source ID: 3

### [ User Agent Fuzzer ](https://www.zaproxy.org/docs/alerts/10104/)



##### Informational (Medium)

### Description

Check for differences in response based on fuzzed User Agent (eg. mobile sites, access as a Search Engine Crawler). Compares the response statuscode and the hashcode of the response body with the original response.

* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/assets/images
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/css
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/all-support
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/care-terms-explained
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/education-and-training
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/eligible-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/error
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/former-relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/health-and-wellbeing
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/help-with-education-and-training-costs
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/helplines
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/higher-education-bursary
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/housing-and-accommodation
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-allowance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/leaving-care-guides
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/local-offer-for-care-leavers
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/money-and-benefits
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/page-not-found
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/pathway-plan
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/person-qualifying-for-advice-and-assistance
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/personal-adviser
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/privacy-policies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/relevant-child
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/service-unavailable
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/unaccompanied-asylum-seeking-young-people
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/what-happens-when-you-leave-care
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/work-and-employment
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/your-rights
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/js
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/robots.txt
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/sitemap.xml
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/accessibility-statement-page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/AccessibilityStatement
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/CookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PageNotFound
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PostCookiePolicy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/translate-this-website/page/PrivacyPolicies
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``

Instances: 570

### Solution



### Reference


* [ https://owasp.org/wstg ](https://owasp.org/wstg)



#### Source ID: 1

### [ User Controllable HTML Element Attribute (Potential XSS) ](https://www.zaproxy.org/docs/alerts/10031/)



##### Informational (Low)

### Description

This check looks at user-supplied input in query string parameters and POST data to identify where certain HTML attribute values might be controlled. This provides hot-spot detection for XSS (cross-site scripting) that will require further review by a security analyst to determine exploitability.

* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `AcceptCookies`
  * Attack: ``
  * Evidence: ``
  * Other Info: `User-controlled HTML attribute values were found. Try injecting special characters to see if XSS might be possible. The page at the following URL:

https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy

appears to include user input in:
a(n) [input] tag [data-val] attribute

The user input found was:
AcceptCookies=True

The user-controlled value was:
true`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `AcceptCookies`
  * Attack: ``
  * Evidence: ``
  * Other Info: `User-controlled HTML attribute values were found. Try injecting special characters to see if XSS might be possible. The page at the following URL:

https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy

appears to include user input in:
a(n) [input] tag [value] attribute

The user input found was:
AcceptCookies=True

The user-controlled value was:
true`
* URL: https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy
  * Method: `POST`
  * Parameter: `AcceptCookies`
  * Attack: ``
  * Evidence: ``
  * Other Info: `User-controlled HTML attribute values were found. Try injecting special characters to see if XSS might be possible. The page at the following URL:

https://staging.support-for-care-leavers.education.gov.uk/en/cookie-policy

appears to include user input in:
a(n) [svg] tag [aria-hidden] attribute

The user input found was:
AcceptCookies=True

The user-controlled value was:
true`

Instances: 3

### Solution

Validate all input and sanitize output it before writing to any HTML attributes.

### Reference


* [ https://cheatsheetseries.owasp.org/cheatsheets/Input_Validation_Cheat_Sheet.html ](https://cheatsheetseries.owasp.org/cheatsheets/Input_Validation_Cheat_Sheet.html)


#### CWE Id: [ 20 ](https://cwe.mitre.org/data/definitions/20.html)


#### WASC Id: 20

#### Source ID: 3


