# ZAP Scanning Report

ZAP by [Checkmarx](https://checkmarx.com/).


## Summary of Alerts

| Risk Level | Number of Alerts |
| --- | --- |
| High | 0 |
| Medium | 3 |
| Low | 11 |
| Informational | 15 |




## Alerts

| Name | Risk Level | Number of Instances |
| --- | --- | --- |
| CSP: style-src unsafe-inline | Medium | 7 |
| Proxy Disclosure | Medium | 22 |
| Sub Resource Integrity Attribute Missing | Medium | 7 |
| Cookie No HttpOnly Flag | Low | 1 |
| Cookie Without Secure Flag | Low | 2 |
| Cookie with SameSite Attribute None | Low | 3 |
| Cookie without SameSite Attribute | Low | 3 |
| Cross-Domain JavaScript Source File Inclusion | Low | 7 |
| Insufficient Site Isolation Against Spectre Vulnerability | Low | 11 |
| Permissions Policy Header Not Set | Low | 4 |
| Private IP Disclosure | Low | 1 |
| Strict-Transport-Security Header Not Set | Low | 11 |
| Timestamp Disclosure - Unix | Low | 2 |
| X-Content-Type-Options Header Missing | Low | 10 |
| Base64 Disclosure | Informational | 6 |
| Cookie Slack Detector | Informational | 19 |
| GET for POST | Informational | 1 |
| Information Disclosure - Suspicious Comments | Informational | 8 |
| Modern Web Application | Informational | 3 |
| Non-Storable Content | Informational | 2 |
| Re-examine Cache-control Directives | Informational | 3 |
| Sec-Fetch-Dest Header is Missing | Informational | 3 |
| Sec-Fetch-Mode Header is Missing | Informational | 3 |
| Sec-Fetch-Site Header is Missing | Informational | 3 |
| Sec-Fetch-User Header is Missing | Informational | 3 |
| Session Management Response Identified | Informational | 4 |
| Storable and Cacheable Content | Informational | 8 |
| User Agent Fuzzer | Informational | 60 |
| User Controllable HTML Element Attribute (Potential XSS) | Informational | 3 |




## Alert Detail



### [ CSP: style-src unsafe-inline ](https://www.zaproxy.org/docs/alerts/10055/)



##### Medium (High)

### Description

Content Security Policy (CSP) is an added layer of security that helps to detect and mitigate certain types of attacks. Including (but not limited to) Cross Site Scripting (XSS), and data injection attacks. These attacks are used for everything from data theft to site defacement or distribution of malware. CSP provides a set of standard HTTP headers that allow website owners to declare approved sources of content that browsers should be allowed to load on that page — covered types are JavaScript, CSS, HTML frames, fonts, images and embeddable objects such as Java applets, ActiveX, audio and video files.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' https://*.sharethis.com https://*.googletagmanager.com https://*.clarity.ms https://c.bing.com 'nonce-LX4JCcI5bk0/ipG+S+3hHzP2yALoZLTpOzhIIqzxJfQ=';style-src 'self' https://*.sharethis.com https://rsms.me 'unsafe-inline';connect-src 'self' https://*.sharethis.com https://*.clarity.ms https://c.bing.com https://*.googletagmanager.com https://*.google-analytics.com https://*.analytics.google.com;font-src 'self' data: https://rsms.me;form-action 'self';img-src 'self' data: https://*.sharethis.com https://images.ctfassets.net https://*.googletagmanager.com https://*.google-analytics.com;frame-ancestors 'none';frame-src https://*.googletagmanager.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' https://*.sharethis.com https://*.googletagmanager.com https://*.clarity.ms https://c.bing.com 'nonce-vHxI/ivhuRKoOnMDTZSNjMn67DhMumX82/hIUdNz7wk=';style-src 'self' https://*.sharethis.com https://rsms.me 'unsafe-inline';connect-src 'self' https://*.sharethis.com https://*.clarity.ms https://c.bing.com https://*.googletagmanager.com https://*.google-analytics.com https://*.analytics.google.com;font-src 'self' data: https://rsms.me;form-action 'self';img-src 'self' data: https://*.sharethis.com https://images.ctfassets.net https://*.googletagmanager.com https://*.google-analytics.com;frame-ancestors 'none';frame-src https://*.googletagmanager.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' https://*.sharethis.com https://*.googletagmanager.com https://*.clarity.ms https://c.bing.com 'nonce-bORKZ56q2lxhHPHcZ3OLuO/o5pr2PODafrniL/BwOuM=';style-src 'self' https://*.sharethis.com https://rsms.me 'unsafe-inline';connect-src 'self' https://*.sharethis.com https://*.clarity.ms https://c.bing.com https://*.googletagmanager.com https://*.google-analytics.com https://*.analytics.google.com;font-src 'self' data: https://rsms.me;form-action 'self';img-src 'self' data: https://*.sharethis.com https://images.ctfassets.net https://*.googletagmanager.com https://*.google-analytics.com;frame-ancestors 'none';frame-src https://*.googletagmanager.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' https://*.sharethis.com https://*.googletagmanager.com https://*.clarity.ms https://c.bing.com 'nonce-Kjph5kzeK2i3U4R9+B6qKfK2zO7wSvFyQUWZJTvx6uY=';style-src 'self' https://*.sharethis.com https://rsms.me 'unsafe-inline';connect-src 'self' https://*.sharethis.com https://*.clarity.ms https://c.bing.com https://*.googletagmanager.com https://*.google-analytics.com https://*.analytics.google.com;font-src 'self' data: https://rsms.me;form-action 'self';img-src 'self' data: https://*.sharethis.com https://images.ctfassets.net https://*.googletagmanager.com https://*.google-analytics.com;frame-ancestors 'none';frame-src https://*.googletagmanager.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' https://*.sharethis.com https://*.googletagmanager.com https://*.clarity.ms https://c.bing.com 'nonce-MtJKXHDZkvtNcF7iHF8rA2cQOI+nrMLcdyhr0duoyrg=';style-src 'self' https://*.sharethis.com https://rsms.me 'unsafe-inline';connect-src 'self' https://*.sharethis.com https://*.clarity.ms https://c.bing.com https://*.googletagmanager.com https://*.google-analytics.com https://*.analytics.google.com;font-src 'self' data: https://rsms.me;form-action 'self';img-src 'self' data: https://*.sharethis.com https://images.ctfassets.net https://*.googletagmanager.com https://*.google-analytics.com;frame-ancestors 'none';frame-src https://*.googletagmanager.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' https://*.sharethis.com https://*.googletagmanager.com https://*.clarity.ms https://c.bing.com 'nonce-Sme8phdkk/GwExBis4d0cPZeDU1eMfMvSXoAOmZKoMg=';style-src 'self' https://*.sharethis.com https://rsms.me 'unsafe-inline';connect-src 'self' https://*.sharethis.com https://*.clarity.ms https://c.bing.com https://*.googletagmanager.com https://*.google-analytics.com https://*.analytics.google.com;font-src 'self' data: https://rsms.me;form-action 'self';img-src 'self' data: https://*.sharethis.com https://images.ctfassets.net https://*.googletagmanager.com https://*.google-analytics.com;frame-ancestors 'none';frame-src https://*.googletagmanager.com`
  * Other Info: `style-src includes unsafe-inline.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Content-Security-Policy`
  * Attack: ``
  * Evidence: `default-src 'none';script-src 'self' https://*.sharethis.com https://*.googletagmanager.com https://*.clarity.ms https://c.bing.com 'nonce-uFZBJgjyuVXme7MciacvygA2sEAinKOeCTM756oCU2Y=';style-src 'self' https://*.sharethis.com https://rsms.me 'unsafe-inline';connect-src 'self' https://*.sharethis.com https://*.clarity.ms https://c.bing.com https://*.googletagmanager.com https://*.google-analytics.com https://*.analytics.google.com;font-src 'self' data: https://rsms.me;form-action 'self';img-src 'self' data: https://*.sharethis.com https://images.ctfassets.net https://*.googletagmanager.com https://*.google-analytics.com;frame-ancestors 'none';frame-src https://*.googletagmanager.com`
  * Other Info: `style-src includes unsafe-inline.`

Instances: 7

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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/dfe-logo-alt.png
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/dfe-logo.png
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/images
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/images/favicon.svg
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/application.css%3Fv=I3neivNJISN7aaM9BjjTaMSAGV3ibvoNLtwa2oq7JpY
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/cta.css%3Fv=xclHjduLeoknWVTbhPL4tVIADjz_wSmD4_i8rB9OLak
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/dfefrontend.css%3Fv=WGvq-LWGl1pJq4gg3aT3WY8ZfNc8VC9IO17Do-VNQhE
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/metadata.css%3Fv=R7DoI6WesxgOJXfjGZG8KqwKp-T-YbRuxy5344pR9r8
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/navigation.css%3Fv=59wXNhiFU-aQaRdFpQLGqhzWHAe4AxpOz3vPpL52yog
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk/all.min.css%3Fv=1zk5-99q-gHitumTgjIgN7iX7dHY_sxfmG9CzkpR58o
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk/all.min.js%3Fv=RHpXsNmLgFsVb0_A7CLeltntRsu0LVT9kE8cBjem4vY
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/robots.txt
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/sitemap.xml
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
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
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

Instances: 22

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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="LX4JCcI5bk0/ipG&#x2B;S&#x2B;3hHzP2yALoZLTpOzhIIqzxJfQ="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="vHxI/ivhuRKoOnMDTZSNjMn67DhMumX82/hIUdNz7wk="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="bORKZ56q2lxhHPHcZ3OLuO/o5pr2PODafrniL/BwOuM="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="Kjph5kzeK2i3U4R9&#x2B;B6qKfK2zO7wSvFyQUWZJTvx6uY="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="MtJKXHDZkvtNcF7iHF8rA2cQOI&#x2B;nrMLcdyhr0duoyrg="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="Sme8phdkk/GwExBis4d0cPZeDU1eMfMvSXoAOmZKoMg="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="uFZBJgjyuVXme7MciacvygA2sEAinKOeCTM756oCU2Y="></script>`
  * Other Info: ``

Instances: 7

### Solution

Provide a valid integrity attribute to the tag.

### Reference


* [ https://developer.mozilla.org/en-US/docs/Web/Security/Subresource_Integrity ](https://developer.mozilla.org/en-US/docs/Web/Security/Subresource_Integrity)


#### CWE Id: [ 345 ](https://cwe.mitre.org/data/definitions/345.html)


#### WASC Id: 15

#### Source ID: 3

### [ Cookie No HttpOnly Flag ](https://www.zaproxy.org/docs/alerts/10010/)



##### Low (Medium)

### Description

A cookie has been set without the HttpOnly flag, which means that the cookie can be accessed by JavaScript. If a malicious script can be run on this page then the cookie will be accessible and can be transmitted to another site. If this is a session cookie then session hijacking may be possible.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `.AspNet.Consent`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNet.Consent`
  * Other Info: ``

Instances: 1

### Solution

Ensure that the HttpOnly flag is set for all cookies.

### Reference


* [ https://owasp.org/www-community/HttpOnly ](https://owasp.org/www-community/HttpOnly)


#### CWE Id: [ 1004 ](https://cwe.mitre.org/data/definitions/1004.html)


#### WASC Id: 13

#### Source ID: 3

### [ Cookie Without Secure Flag ](https://www.zaproxy.org/docs/alerts/10011/)



##### Low (Medium)

### Description

A cookie has been set without the secure flag, which means that the cookie can be accessed via unencrypted connections.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `.AspNetCore.Antiforgery.VyLW6ORzMgk`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNetCore.Antiforgery.VyLW6ORzMgk`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `.AspNet.Consent`
  * Attack: ``
  * Evidence: `Set-Cookie: .AspNet.Consent`
  * Other Info: ``

Instances: 2

### Solution

Whenever a cookie contains sensitive information or is a session token, then it should always be passed using an encrypted channel. Ensure that the secure flag is set for cookies containing such sensitive information.

### Reference


* [ https://owasp.org/www-project-web-security-testing-guide/v41/4-Web_Application_Security_Testing/06-Session_Management_Testing/02-Testing_for_Cookies_Attributes.html ](https://owasp.org/www-project-web-security-testing-guide/v41/4-Web_Application_Security_Testing/06-Session_Management_Testing/02-Testing_for_Cookies_Attributes.html)


#### CWE Id: [ 614 ](https://cwe.mitre.org/data/definitions/614.html)


#### WASC Id: 13

#### Source ID: 3

### [ Cookie with SameSite Attribute None ](https://www.zaproxy.org/docs/alerts/10054/)



##### Low (Medium)

### Description

A cookie has been set with its SameSite attribute set to "none", which means that the cookie can be sent as a result of a 'cross-site' request. The SameSite attribute is an effective counter measure to cross-site request forgery, cross-site script inclusion, and timing attacks.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `ASLBSACORS`
  * Attack: ``
  * Evidence: `set-cookie: ASLBSACORS`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `ASLBSACORS`
  * Attack: ``
  * Evidence: `set-cookie: ASLBSACORS`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `ASLBSACORS`
  * Attack: ``
  * Evidence: `set-cookie: ASLBSACORS`
  * Other Info: ``

Instances: 3

### Solution

Ensure that the SameSite attribute is set to either 'lax' or ideally 'strict' for all cookies.

### Reference


* [ https://tools.ietf.org/html/draft-ietf-httpbis-cookie-same-site ](https://tools.ietf.org/html/draft-ietf-httpbis-cookie-same-site)


#### CWE Id: [ 1275 ](https://cwe.mitre.org/data/definitions/1275.html)


#### WASC Id: 13

#### Source ID: 3

### [ Cookie without SameSite Attribute ](https://www.zaproxy.org/docs/alerts/10054/)



##### Low (Medium)

### Description

A cookie has been set without the SameSite attribute, which means that the cookie can be sent as a result of a 'cross-site' request. The SameSite attribute is an effective counter measure to cross-site request forgery, cross-site script inclusion, and timing attacks.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `ASLBSA`
  * Attack: ``
  * Evidence: `set-cookie: ASLBSA`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `ASLBSA`
  * Attack: ``
  * Evidence: `set-cookie: ASLBSA`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `ASLBSA`
  * Attack: ``
  * Evidence: `set-cookie: ASLBSA`
  * Other Info: ``

Instances: 3

### Solution

Ensure that the SameSite attribute is set to either 'lax' or ideally 'strict' for all cookies.

### Reference


* [ https://tools.ietf.org/html/draft-ietf-httpbis-cookie-same-site ](https://tools.ietf.org/html/draft-ietf-httpbis-cookie-same-site)


#### CWE Id: [ 1275 ](https://cwe.mitre.org/data/definitions/1275.html)


#### WASC Id: 13

#### Source ID: 3

### [ Cross-Domain JavaScript Source File Inclusion ](https://www.zaproxy.org/docs/alerts/10017/)



##### Low (Medium)

### Description

The page includes one or more script files from a third-party domain.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="LX4JCcI5bk0/ipG&#x2B;S&#x2B;3hHzP2yALoZLTpOzhIIqzxJfQ="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="vHxI/ivhuRKoOnMDTZSNjMn67DhMumX82/hIUdNz7wk="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="bORKZ56q2lxhHPHcZ3OLuO/o5pr2PODafrniL/BwOuM="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="Kjph5kzeK2i3U4R9&#x2B;B6qKfK2zO7wSvFyQUWZJTvx6uY="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="MtJKXHDZkvtNcF7iHF8rA2cQOI&#x2B;nrMLcdyhr0duoyrg="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="Sme8phdkk/GwExBis4d0cPZeDU1eMfMvSXoAOmZKoMg="></script>`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform`
  * Attack: ``
  * Evidence: `<script type="text/javascript" src="https://platform-api.sharethis.com/js/sharethis.js#property=67a9d0adc7696f001258a34a&product=inline-share-buttons&source=platform" async="async" nonce="uFZBJgjyuVXme7MciacvygA2sEAinKOeCTM756oCU2Y="></script>`
  * Other Info: ``

Instances: 7

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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/dfe-logo.png
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/images/favicon.svg
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/cta.css%3Fv=xclHjduLeoknWVTbhPL4tVIADjz_wSmD4_i8rB9OLak
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/metadata.css%3Fv=R7DoI6WesxgOJXfjGZG8KqwKp-T-YbRuxy5344pR9r8
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/navigation.css%3Fv=59wXNhiFU-aQaRdFpQLGqhzWHAe4AxpOz3vPpL52yog
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Cross-Origin-Resource-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Cross-Origin-Embedder-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Cross-Origin-Embedder-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Cross-Origin-Opener-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `Cross-Origin-Opener-Policy`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 11

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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk/all.min.js%3Fv=RHpXsNmLgFsVb0_A7CLeltntRsu0LVT9kE8cBjem4vY
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 4

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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/images/favicon.svg
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


#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 13

#### Source ID: 3

### [ Strict-Transport-Security Header Not Set ](https://www.zaproxy.org/docs/alerts/10035/)



##### Low (High)

### Description

HTTP Strict Transport Security (HSTS) is a web security policy mechanism whereby a web server declares that complying user agents (such as a web browser) are to interact with it using only secure HTTPS connections (i.e. HTTP layered over TLS/SSL). HSTS is an IETF standards track protocol and is specified in RFC 6797.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/dfe-logo.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/images/favicon.svg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/cta.css%3Fv=xclHjduLeoknWVTbhPL4tVIADjz_wSmD4_i8rB9OLak
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/metadata.css%3Fv=R7DoI6WesxgOJXfjGZG8KqwKp-T-YbRuxy5344pR9r8
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/navigation.css%3Fv=59wXNhiFU-aQaRdFpQLGqhzWHAe4AxpOz3vPpL52yog
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk/all.min.js%3Fv=RHpXsNmLgFsVb0_A7CLeltntRsu0LVT9kE8cBjem4vY
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 11

### Solution

Ensure that your web server, application server, load balancer, etc. is configured to enforce Strict-Transport-Security.

### Reference


* [ https://cheatsheetseries.owasp.org/cheatsheets/HTTP_Strict_Transport_Security_Cheat_Sheet.html ](https://cheatsheetseries.owasp.org/cheatsheets/HTTP_Strict_Transport_Security_Cheat_Sheet.html)
* [ https://owasp.org/www-community/Security_Headers ](https://owasp.org/www-community/Security_Headers)
* [ https://en.wikipedia.org/wiki/HTTP_Strict_Transport_Security ](https://en.wikipedia.org/wiki/HTTP_Strict_Transport_Security)
* [ https://caniuse.com/stricttransportsecurity ](https://caniuse.com/stricttransportsecurity)
* [ https://datatracker.ietf.org/doc/html/rfc6797 ](https://datatracker.ietf.org/doc/html/rfc6797)


#### CWE Id: [ 319 ](https://cwe.mitre.org/data/definitions/319.html)


#### WASC Id: 15

#### Source ID: 3

### [ Timestamp Disclosure - Unix ](https://www.zaproxy.org/docs/alerts/10096/)



##### Low (Low)

### Description

A timestamp was disclosed by the application/web server. - Unix

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/application.css%3Fv=I3neivNJISN7aaM9BjjTaMSAGV3ibvoNLtwa2oq7JpY
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1428571429`
  * Other Info: `1428571429, which evaluates to: 2015-04-09 09:23:49.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk/all.min.css%3Fv=1zk5-99q-gHitumTgjIgN7iX7dHY_sxfmG9CzkpR58o
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1428571429`
  * Other Info: `1428571429, which evaluates to: 2015-04-09 09:23:49.`

Instances: 2

### Solution

Manually confirm that the timestamp data is not sensitive, and that the data cannot be aggregated to disclose exploitable patterns.

### Reference


* [ https://cwe.mitre.org/data/definitions/200.html ](https://cwe.mitre.org/data/definitions/200.html)


#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 13

#### Source ID: 3

### [ X-Content-Type-Options Header Missing ](https://www.zaproxy.org/docs/alerts/10021/)



##### Low (Medium)

### Description

The Anti-MIME-Sniffing header X-Content-Type-Options was not set to 'nosniff'. This allows older versions of Internet Explorer and Chrome to perform MIME-sniffing on the response body, potentially causing the response body to be interpreted and displayed as a content type other than the declared content type. Current (early 2014) and legacy versions of Firefox will use the declared content type (if one is set), rather than performing MIME-sniffing.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/dfe-logo-alt.png
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/dfe-logo.png
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/images/favicon.svg
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/application.css%3Fv=I3neivNJISN7aaM9BjjTaMSAGV3ibvoNLtwa2oq7JpY
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/cta.css%3Fv=xclHjduLeoknWVTbhPL4tVIADjz_wSmD4_i8rB9OLak
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/dfefrontend.css%3Fv=WGvq-LWGl1pJq4gg3aT3WY8ZfNc8VC9IO17Do-VNQhE
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/metadata.css%3Fv=R7DoI6WesxgOJXfjGZG8KqwKp-T-YbRuxy5344pR9r8
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/navigation.css%3Fv=59wXNhiFU-aQaRdFpQLGqhzWHAe4AxpOz3vPpL52yog
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk/all.min.css%3Fv=1zk5-99q-gHitumTgjIgN7iX7dHY_sxfmG9CzkpR58o
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk/all.min.js%3Fv=RHpXsNmLgFsVb0_A7CLeltntRsu0LVT9kE8cBjem4vY
  * Method: `GET`
  * Parameter: `x-content-type-options`
  * Attack: ``
  * Evidence: ``
  * Other Info: `This issue still applies to error type pages (401, 403, 500, etc.) as those pages are often still affected by injection issues, in which case there is still concern for browsers sniffing pages away from their actual content type.
At "High" threshold this scan rule will not alert on client or server error responses.`

Instances: 10

### Solution

Ensure that the application/web server sets the Content-Type header appropriately, and that it sets the X-Content-Type-Options header to 'nosniff' for all web pages.
If possible, ensure that the end user uses a standards-compliant and modern web browser that does not perform MIME-sniffing at all, or that can be directed by the web application/web server to not perform MIME-sniffing.

### Reference


* [ https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/compatibility/gg622941(v=vs.85) ](https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/compatibility/gg622941(v=vs.85))
* [ https://owasp.org/www-community/Security_Headers ](https://owasp.org/www-community/Security_Headers)


#### CWE Id: [ 693 ](https://cwe.mitre.org/data/definitions/693.html)


#### WASC Id: 15

#### Source ID: 3

### [ Base64 Disclosure ](https://www.zaproxy.org/docs/alerts/10094/)



##### Informational (Medium)

### Description

Base64 encoded data was disclosed by the application/web server. Note: in the interests of performance not all base64 strings in the response were analyzed individually, the entire response should be looked at by the analyst/security team/developer(s).

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1zk5-99q-gHitumTgjIgN7iX7dHY_sxfmG9CzkpR58o`
  * Other Info: `�99��j��铂2 7�������_�oB�JQ��`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `vHxI/ivhuRKoOnMDTZSNjMn67DhMumX82/hIUdNz7wk=`
  * Other Info: `�|H�+��:sM������8L�e���HQ�s�	`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `CfDJ8GeQqKB5achLnVhFTuXmZT0QrzPHgQ4ZMBcLmG0-a9f71twtXOfUY_Fa9efneG8IAe9llIQAIGUTDfZ3xpYLoDYMcQW9zPFgaQvIK6wNtYPZrkR0f12bB3Q8sFF4HBX99cxw9S1Hcc0n8NOyS9c1IfM`
  * Other Info: `	���g���yi�K�XEN��e=�3ǁ0�m>k����-\��c�Z���xo�e��  e�wƖ�6q���`i�+���ٮDt]�t<�Qx���p�-Gq�'�ӲK�5!�`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `Sme8phdkk/GwExBis4d0cPZeDU1eMfMvSXoAOmZKoMg=`
  * Other Info: `Jg��d��b��tp�^M^1�/Iz :fJ��`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `1zk5-99q-gHitumTgjIgN7iX7dHY_sxfmG9CzkpR58o`
  * Other Info: `�99��j��铂2 7�������_�oB�JQ��`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `uFZBJgjyuVXme7MciacvygA2sEAinKOeCTM756oCU2Y=`
  * Other Info: `�VA&�U�{���/� 6�@"���	3;�Sf`

Instances: 6

### Solution

Manually confirm that the Base64 data does not leak sensitive information, and that the data cannot be aggregated/used to exploit other vulnerabilities.

### Reference


* [ https://projects.webappsec.org/w/page/13246936/Information%20Leakage ](https://projects.webappsec.org/w/page/13246936/Information%20Leakage)


#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 13

#### Source ID: 3

### [ Cookie Slack Detector ](https://www.zaproxy.org/docs/alerts/90027/)



##### Informational (Low)

### Description

Repeated GET requests: drop a different cookie each time, followed by normal request with all cookies to stabilize session, compare responses against original baseline GET. This can reveal areas where cookie based authentication/attributes are not actually enforced.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/dfe-logo-alt.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA,.AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/dfe-logo.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA,.AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/images
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/images/favicon.svg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/application.css%3Fv=I3neivNJISN7aaM9BjjTaMSAGV3ibvoNLtwa2oq7JpY
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/cta.css%3Fv=xclHjduLeoknWVTbhPL4tVIADjz_wSmD4_i8rB9OLak
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/dfefrontend.css%3Fv=WGvq-LWGl1pJq4gg3aT3WY8ZfNc8VC9IO17Do-VNQhE
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA,.AspNet.Consent,.AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/metadata.css%3Fv=R7DoI6WesxgOJXfjGZG8KqwKp-T-YbRuxy5344pR9r8
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/navigation.css%3Fv=59wXNhiFU-aQaRdFpQLGqhzWHAe4AxpOz3vPpL52yog
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk/all.min.css%3Fv=1zk5-99q-gHitumTgjIgN7iX7dHY_sxfmG9CzkpR58o
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA,.AspNet.Consent,.AspNetCore.Antiforgery.VyLW6ORzMgk
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk/all.min.js%3Fv=RHpXsNmLgFsVb0_A7CLeltntRsu0LVT9kE8cBjem4vY
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Cookies that don't have expected effects can reveal flaws in application logic. In the worst case, this can reveal where authentication via cookie token(s) is not actually enforced.
These cookies affected the response: 
These cookies did NOT affect the response: ASLBSACORS,ASLBSA
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Dropping this cookie appears to have invalidated the session: [ASLBSA] A follow-on request with all original cookies still had a different response than the original request.
`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `Dropping this cookie appears to have invalidated the session: [.AspNet.Consent] A follow-on request with all original cookies still had a different response than the original request.
`

Instances: 19

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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `GET https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy?AcceptCookies=True&__RequestVerificationToken=CfDJ8GeQqKB5achLnVhFTuXmZT1FH3knh5YVYTm741FtYhISNSHEFqrQOqfsIskBn9kVuSupZSgxCB_lv2JV89k-oipHNimyXkTgbLv1YIXandlp5SqtqaUZUw_bLzzA9AQphrTFIRhbvH3GqNunEkA98A4 HTTP/1.1`
  * Other Info: ``

Instances: 1

### Solution

Ensure that only POST is accepted where POST is expected.

### Reference



#### CWE Id: [ 16 ](https://cwe.mitre.org/data/definitions/16.html)


#### WASC Id: 20

#### Source ID: 1

### [ Information Disclosure - Suspicious Comments ](https://www.zaproxy.org/docs/alerts/10027/)



##### Informational (Low)

### Description

The response appears to contain suspicious comments which may help an attacker. Note: Matches made within script blocks or files are against the entire content not only comments.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/govuk/all.min.js%3Fv=RHpXsNmLgFsVb0_A7CLeltntRsu0LVT9kE8cBjem4vY
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `select`
  * Other Info: `The following pattern was used: \bSELECT\b and was detected in the element starting with: "const version="5.8.0";function getFragmentFromUrl(t){if(t.includes("#"))return t.split("#").pop()}function getBreakpoint(t){cons", see evidence field for the suspicious comment/snippet.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<script nonce="LX4JCcI5bk0/ipG&#x2B;S&#x2B;3hHzP2yALoZLTpOzhIIqzxJfQ=" type="module">
import { initAll } from '/govuk/all.min.js", see evidence field for the suspicious comment/snippet.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<script nonce="vHxI/ivhuRKoOnMDTZSNjMn67DhMumX82/hIUdNz7wk=" type="module">
import { initAll } from '/govuk/all.min.js'
initAll(", see evidence field for the suspicious comment/snippet.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<script nonce="bORKZ56q2lxhHPHcZ3OLuO/o5pr2PODafrniL/BwOuM=" type="module">
import { initAll } from '/govuk/all.min.js'
initAll(", see evidence field for the suspicious comment/snippet.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<script nonce="Kjph5kzeK2i3U4R9&#x2B;B6qKfK2zO7wSvFyQUWZJTvx6uY=" type="module">
import { initAll } from '/govuk/all.min.js'
ini", see evidence field for the suspicious comment/snippet.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<script nonce="MtJKXHDZkvtNcF7iHF8rA2cQOI&#x2B;nrMLcdyhr0duoyrg=" type="module">
import { initAll } from '/govuk/all.min.js'
ini", see evidence field for the suspicious comment/snippet.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<script nonce="Sme8phdkk/GwExBis4d0cPZeDU1eMfMvSXoAOmZKoMg=" type="module">
import { initAll } from '/govuk/all.min.js'
initAll(", see evidence field for the suspicious comment/snippet.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `from`
  * Other Info: `The following pattern was used: \bFROM\b and was detected in the element starting with: "<script nonce="uFZBJgjyuVXme7MciacvygA2sEAinKOeCTM756oCU2Y=" type="module">
import { initAll } from '/govuk/all.min.js'
initAll(", see evidence field for the suspicious comment/snippet.`

Instances: 8

### Solution

Remove all comments that return information that may help an attacker and fix any underlying problems they refer to.

### Reference



#### CWE Id: [ 200 ](https://cwe.mitre.org/data/definitions/200.html)


#### WASC Id: 13

#### Source ID: 3

### [ Modern Web Application ](https://www.zaproxy.org/docs/alerts/10109/)



##### Informational (Medium)

### Description

The application appears to be a modern web application. If you need to explore it automatically then the Ajax Spider may well be more effective than the standard one.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<a class="govuk-link" href="#">give your feedback by email</a>`
  * Other Info: `Links have been found that do not have traditional href attributes, which is an indication that this is a modern web application.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<a class="govuk-link" href="#">give your feedback by email</a>`
  * Other Info: `Links have been found that do not have traditional href attributes, which is an indication that this is a modern web application.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: ``
  * Attack: ``
  * Evidence: `<a class="govuk-link" href="#">give your feedback by email</a>`
  * Other Info: `Links have been found that do not have traditional href attributes, which is an indication that this is a modern web application.`

Instances: 3

### Solution

This is an informational alert and so no changes are required.

### Reference




#### Source ID: 3

### [ Non-Storable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are not storable by caching components such as proxy servers. If the response does not contain sensitive, personal or user-specific information, it may benefit from being stored and cached, to improve performance.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `302`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: `no-store`
  * Other Info: ``

Instances: 2

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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: `no-cache, no-store`
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: `cache-control`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``

Instances: 3

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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Sec-Fetch-Dest`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/robots.txt
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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Sec-Fetch-Mode`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/robots.txt
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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Sec-Fetch-Site`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/robots.txt
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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Sec-Fetch-User`
  * Attack: ``
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/robots.txt
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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `ASLBSA`
  * Attack: ``
  * Evidence: `000329fdac6aad2db95ab9be0591bf51de6946d323cdb87666f9cc3490770ae6c4c7`
  * Other Info: `
cookie:ASLBSA
cookie:ASLBSACORS`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `ASLBSA`
  * Attack: ``
  * Evidence: `000329fdac6aad2db95ab9be0591bf51de6946d323cdb87666f9cc3490770ae6c4c7`
  * Other Info: `
cookie:ASLBSA
cookie:ASLBSACORS`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `.AspNetCore.Antiforgery.VyLW6ORzMgk`
  * Attack: ``
  * Evidence: `CfDJ8GeQqKB5achLnVhFTuXmZT0QrzPHgQ4ZMBcLmG0-a9f71twtXOfUY_Fa9efneG8IAe9llIQAIGUTDfZ3xpYLoDYMcQW9zPFgaQvIK6wNtYPZrkR0f12bB3Q8sFF4HBX99cxw9S1Hcc0n8NOyS9c1IfM`
  * Other Info: `
cookie:.AspNetCore.Antiforgery.VyLW6ORzMgk
cookie:ASLBSA
cookie:ASLBSACORS`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `ASLBSA`
  * Attack: ``
  * Evidence: `000329fdac6aad2db95ab9be0591bf51de6946d323cdb87666f9cc3490770ae6c4c7`
  * Other Info: `
cookie:ASLBSA
cookie:ASLBSACORS`

Instances: 4

### Solution

This is an informational alert rather than a vulnerability and so there is nothing to fix.

### Reference


* [ https://www.zaproxy.org/docs/desktop/addons/authentication-helper/session-mgmt-id ](https://www.zaproxy.org/docs/desktop/addons/authentication-helper/session-mgmt-id)



#### Source ID: 3

### [ Storable and Cacheable Content ](https://www.zaproxy.org/docs/alerts/10049/)



##### Informational (Medium)

### Description

The response contents are storable by caching components such as proxy servers, and may be retrieved directly from the cache, rather than from the origin server by the caching servers, in response to similar requests from other users. If the response data is sensitive, personal or user-specific, this may result in sensitive information being leaked. In some cases, this may even result in a user gaining complete control of the session of another user, depending on the configuration of the caching components in use in their environment. This is primarily an issue where "shared" caching servers such as "proxy" caches are configured on the local network. This configuration is typically found in corporate or educational environments, for instance.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/dfe-logo.png
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/assets/images/favicon.svg
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/cta.css%3Fv=xclHjduLeoknWVTbhPL4tVIADjz_wSmD4_i8rB9OLak
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/metadata.css%3Fv=R7DoI6WesxgOJXfjGZG8KqwKp-T-YbRuxy5344pR9r8
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/css/navigation.css%3Fv=59wXNhiFU-aQaRdFpQLGqhzWHAe4AxpOz3vPpL52yog
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/robots.txt
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/sitemap.xml
  * Method: `GET`
  * Parameter: ``
  * Attack: ``
  * Evidence: ``
  * Other Info: `In the absence of an explicitly specified caching lifetime directive in the response, a liberal lifetime heuristic of 1 year was assumed. This is permitted by rfc7234.`

Instances: 8

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

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/home
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `GET`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Trident/7.0; rv:11.0) like Gecko`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3739.0 Safari/537.36 Edg/75.0.109.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:93.0) Gecko/20100101 Firefox/91.0`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; CPU iPhone OS 8_0_2 like Mac OS X) AppleWebKit/600.1.4 (KHTML, like Gecko) Version/8.0 Mobile/12A366 Safari/600.1.4`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `Mozilla/5.0 (iPhone; U; CPU iPhone OS 3_0 like Mac OS X; en-us) AppleWebKit/528.18 (KHTML, like Gecko) Version/4.0 Mobile/7A341 Safari/528.16`
  * Evidence: ``
  * Other Info: ``
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `Header User-Agent`
  * Attack: `msnbot/1.1 (+http://search.msn.com/msnbot.htm)`
  * Evidence: ``
  * Other Info: ``

Instances: 60

### Solution



### Reference


* [ https://owasp.org/wstg ](https://owasp.org/wstg)



#### Source ID: 1

### [ User Controllable HTML Element Attribute (Potential XSS) ](https://www.zaproxy.org/docs/alerts/10031/)



##### Informational (Low)

### Description

This check looks at user-supplied input in query string parameters and POST data to identify where certain HTML attribute values might be controlled. This provides hot-spot detection for XSS (cross-site scripting) that will require further review by a security analyst to determine exploitability.

* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `AcceptCookies`
  * Attack: ``
  * Evidence: ``
  * Other Info: `User-controlled HTML attribute values were found. Try injecting special characters to see if XSS might be possible. The page at the following URL:

https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy

appears to include user input in:
a(n) [input] tag [data-val] attribute

The user input found was:
AcceptCookies=True

The user-controlled value was:
true`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `AcceptCookies`
  * Attack: ``
  * Evidence: ``
  * Other Info: `User-controlled HTML attribute values were found. Try injecting special characters to see if XSS might be possible. The page at the following URL:

https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy

appears to include user input in:
a(n) [input] tag [value] attribute

The user input found was:
AcceptCookies=True

The user-controlled value was:
true`
* URL: https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy
  * Method: `POST`
  * Parameter: `AcceptCookies`
  * Attack: ``
  * Evidence: ``
  * Other Info: `User-controlled HTML attribute values were found. Try injecting special characters to see if XSS might be possible. The page at the following URL:

https://s186d01-cl-web-fd-endpoint-gsace3a3hddqf0g6.a02.azurefd.net/pages/cookie-policy

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


