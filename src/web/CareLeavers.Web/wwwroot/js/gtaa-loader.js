(function() {
    'use strict';
    let loaderElements = {
        loader: null,
        frame: null,
        wrapper: null
    };
    let isNavigating = false;
    function initializeElements() {
        loaderElements.loader = document.getElementById('gtaaLoader');
        loaderElements.frame = document.getElementById('gtaaFrame');
        loaderElements.wrapper = document.querySelector('.gtaa-wrapper');
    }
    function showLoader() {
        if (loaderElements.loader) {
            loaderElements.loader.style.display = 'block';
        }
    }
    function hideLoader() {
        if (loaderElements.loader) {
            loaderElements.loader.style.display = 'none';
        }
    }
    function showError(message) {
        if (loaderElements.loader) {
            loaderElements.loader.innerHTML = '<p style="color: #d32f2f;">' +
                (message || 'Failed to load questionnaire. Please try again.') +
                '</p>';
        }
    }
    function showUnloadMessage(message) {
        if (loaderElements.loader) {
            loaderElements.loader.innerHTML = '<p style="text-align: center;">' +
                (message || 'Results are getting ready...') +
                '</p>';
            loaderElements.loader.style.display = 'block';
        }
    }
    function handleFrameLoad() {
        hideLoader();
        if (loaderElements.wrapper) {
            loaderElements.wrapper.classList.remove('loading');
        }
    }
    function handleFrameError() {
        showError('Failed to load questionnaire. Please try again.');
    }
    function handlePostMessage(event) {
        if (event.data && event.data.type === 'gtaa-navigation-start') {
            if (!isNavigating) {
                isNavigating = true;
                showUnloadMessage(event.data.message || 'Results are being generated...');
            }
        }
    }
    function handleBeforeUnload() {
        if (!isNavigating) {
            isNavigating = true;
            showUnloadMessage('Redirecting to results...');
            return new Promise((resolve) => {
                setTimeout(resolve, 100);
            });
        }
    }
    function setupTimeoutSafety(timeoutMs) {
        setTimeout(() => {
            if (loaderElements.loader && loaderElements.loader.style.display !== 'none') {
                hideLoader();
                if (loaderElements.wrapper) {
                    loaderElements.wrapper.classList.remove('loading');
                }
            }
        }, timeoutMs);
    }
    function init() {
        initializeElements();
        showLoader();
        if (loaderElements.frame) {
            loaderElements.frame.style.display = 'block';
            loaderElements.frame.addEventListener('load', handleFrameLoad, { once: true });
            loaderElements.frame.addEventListener('error', handleFrameError, { once: true });
            window.addEventListener('message', handlePostMessage);
            window.addEventListener('beforeunload', handleBeforeUnload);
            setupTimeoutSafety(30000);
        }
    }
    window.GTAALoader = {
        init: init,
        showLoader: showLoader,
        hideLoader: hideLoader,
        showError: showError,
        showUnloadMessage: showUnloadMessage,
        isNavigating: function() { return isNavigating; }
    };
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
