# Contentful Webhooks

This folder contains handlers for webhooks that fire off from Contentful, each of them handling a different event type.

### PublishAssetWebhook

Handles the "Asset" event type, which is triggered when an Asset is created, modified or deleted in Contentful. These are things such as images or video.

The purpose of this handler is to clear the cache of any pages that use the asset that was modified, so that on the next page load the updated entry is fetched instead of using the now stale cached version.

### PublishContentfulWebhook

Handles the "Entry" event type, which is triggered when an Entry is created, modified or deleted in Contentful. These are things such as Pages, Banners, and other Content types.

The primary purpose of this handler is to clear caches depending on the type of Content that was modified, so that the latest version of it can be fetched instead of using the stale cached version.

At the end it will also republish anything that was linked to the content that was modified, as they would otherwise be in a "Changed" state on the CMS.

Specifically what happens to each type:

#### Redirection Rules

If this has been modified (by the page slug changing or a manual change on the CMS) then the cache for the redirection rules is cleared.

#### Configuration

If the Contentful Configuration Entity is modified, then the cache for the configuration is cleared.

#### Page

If a page is changed a few things happen:

- The page is cleared from the cache
- At the same time, the slug for the page that was stored in the cache is compared to the slug of the updated page
- If the slugs don't match, the redirection rules are updated to redirect the old slug to the new slug
- The old slug references are removed from the cache for good measure
- If the redirection rules were changed, it is republished on Contentful

#### Printable Collection

If a printable collection is changed, it's cleared from the cache.

#### Other..

Any other types are always embedded within a Page, so similarly to the Asset clearing this will handle clearing the cache of all pages that use the updated Content, so the latest version can be fetched from the CMS.