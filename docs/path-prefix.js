export const pathPrefix =
    process.env.ELEVENTY_ENV === "production"
        ? "/care-leavers/"
        : "/";