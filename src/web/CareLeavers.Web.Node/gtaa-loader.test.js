// Load the gtaa-loader module
const {
    init,
    showLoader,
    hideLoader,
    showError,
    showUnloadMessage,
    handleFrameLoad,
    handleFrameError,
    handlePostMessage,
    handleBeforeUnload,
    handlePageShow,
    setupTimeoutSafety,
    initializeElements,
    getLoaderElements,
    setLoaderElements,
    getIsNavigating,
    setIsNavigating
} = require('../CareLeavers.Web/wwwroot/js/gtaa-loader.js');

describe('GTAA Loader', () => {
    let mockLoader, mockFrame, mockWrapper;

    beforeEach(() => {
        // Clear all timers
        jest.clearAllTimers();
        jest.useFakeTimers();

        // Reset navigation state
        setIsNavigating(false);

        // Create mock DOM elements
        mockLoader = document.createElement('div');
        mockLoader.id = 'gtaaLoader';
        mockLoader.style.display = 'none';

        mockFrame = document.createElement('iframe');
        mockFrame.id = 'gtaaFrame';
        mockFrame.style.display = 'none';

        mockWrapper = document.createElement('div');
        mockWrapper.className = 'gtaa-wrapper';
        mockWrapper.classList.add('loading');

        document.body.appendChild(mockLoader);
        document.body.appendChild(mockFrame);
        document.body.appendChild(mockWrapper);

        // Initialize elements
        initializeElements();
    });

    afterEach(() => {
        jest.runOnlyPendingTimers();
        jest.useRealTimers();
        document.body.innerHTML = '';
    });

    describe('initializeElements', () => {
        it('should initialize loader elements from DOM', () => {
            const loaderElements = getLoaderElements();
            expect(loaderElements.loader).toBe(mockLoader);
            expect(loaderElements.frame).toBe(mockFrame);
            expect(loaderElements.wrapper).toBe(mockWrapper);
        });

        it('should handle missing elements gracefully', () => {
            document.body.innerHTML = '';
            initializeElements();
            const loaderElements = getLoaderElements();
            expect(loaderElements.loader).toBeNull();
            expect(loaderElements.frame).toBeNull();
            expect(loaderElements.wrapper).toBeNull();
        });
    });

    describe('showLoader', () => {
        it('should set loader display to block', () => {
            mockLoader.style.display = 'none';
            showLoader();
            expect(mockLoader.style.display).toBe('block');
        });

        it('should not throw if loader element is missing', () => {
            document.body.innerHTML = '';
            initializeElements();
            expect(() => showLoader()).not.toThrow();
        });
    });

    describe('hideLoader', () => {
        it('should set loader display to none', () => {
            mockLoader.style.display = 'block';
            hideLoader();
            expect(mockLoader.style.display).toBe('none');
        });

        it('should not throw if loader element is missing', () => {
            document.body.innerHTML = '';
            initializeElements();
            expect(() => hideLoader()).not.toThrow();
        });
    });

    describe('showError', () => {
        it('should display error message with red color', () => {
            const errorMessage = 'Custom error message';
            showError(errorMessage);

            const errorParagraph = mockLoader.querySelector('p');
            expect(errorParagraph).toBeTruthy();
            expect(errorParagraph.textContent).toBe(errorMessage);
            // jsdom converts hex to rgb, so check for either format
            expect(errorParagraph.style.color).toMatch(/^(#d32f2f|rgb\(211, 47, 47\))$/);
        });

        it('should display default error message if none provided', () => {
            showError();

            const errorParagraph = mockLoader.querySelector('p');
            expect(errorParagraph.textContent).toBe('Failed to load questionnaire. Please try again.');
        });

        it('should clear previous content before showing error', () => {
            mockLoader.innerHTML = '<div>Previous content</div>';
            showError('New error');

            expect(mockLoader.querySelector('div')).toBeNull();
            expect(mockLoader.querySelector('p')).toBeTruthy();
        });

        it('should not throw if loader element is missing', () => {
            document.body.innerHTML = '';
            initializeElements();
            expect(() => showError('Error')).not.toThrow();
        });
    });

    describe('showUnloadMessage', () => {
        it('should update heading text and display loader', () => {
            const heading = document.createElement('h1');
            heading.className = 'govuk-heading-m';
            heading.textContent = 'Old message';
            mockLoader.appendChild(heading);
            mockLoader.style.display = 'none';

            showUnloadMessage('New message');

            expect(heading.textContent).toBe('New message');
            expect(mockLoader.style.display).toBe('block');
        });

        it('should display loader without heading if heading not found', () => {
            mockLoader.style.display = 'none';
            showUnloadMessage('Message without heading');

            expect(mockLoader.style.display).toBe('block');
        });

        it('should use default message if none provided', () => {
            const heading = document.createElement('h1');
            heading.className = 'govuk-heading-m';
            heading.textContent = 'Old';
            mockLoader.appendChild(heading);

            showUnloadMessage();

            expect(heading.textContent).toBe('Loading...');
        });

        it('should not throw if loader element is missing', () => {
            document.body.innerHTML = '';
            initializeElements();
            expect(() => showUnloadMessage('Message')).not.toThrow();
        });
    });

    describe('handleFrameLoad', () => {
        it('should hide loader and remove loading class from wrapper', () => {
            mockLoader.style.display = 'block';
            mockWrapper.classList.add('loading');

            handleFrameLoad();

            expect(mockLoader.style.display).toBe('none');
            expect(mockWrapper.classList.contains('loading')).toBe(false);
        });

        it('should handle missing wrapper gracefully', () => {
            document.body.innerHTML = '';
            mockLoader = document.createElement('div');
            mockLoader.id = 'gtaaLoader';
            mockLoader.style.display = 'block';
            document.body.appendChild(mockLoader);
            initializeElements();

            expect(() => handleFrameLoad()).not.toThrow();
            expect(mockLoader.style.display).toBe('none');
        });
    });

    describe('handleFrameError', () => {
        it('should display error message', () => {
            handleFrameError();

            const errorParagraph = mockLoader.querySelector('p');
            expect(errorParagraph).toBeTruthy();
            expect(errorParagraph.textContent).toBe('Failed to load questionnaire. Please try again.');
        });
    });

    describe('handlePostMessage', () => {
        it('should set isNavigating to true on gtaa-navigation-start message', () => {
            setIsNavigating(false);

            const event = new MessageEvent('message', {
                data: { type: 'gtaa-navigation-start', message: 'Navigating...' },
                origin: window.location.origin
            });

            handlePostMessage(event);

            expect(getIsNavigating()).toBe(true);
        });

        it('should ignore messages from different origins', () => {
            setIsNavigating(false);

            const event = new MessageEvent('message', {
                data: { type: 'gtaa-navigation-start' },
                origin: 'https://different-origin.com'
            });

            handlePostMessage(event);

            expect(getIsNavigating()).toBe(false);
        });

        it('should use default message if none provided', () => {
            mockLoader.innerHTML = '<h1 class="govuk-heading-m">Old message</h1>';
            setIsNavigating(false);

            const event = new MessageEvent('message', {
                data: { type: 'gtaa-navigation-start' },
                origin: window.location.origin
            });

            handlePostMessage(event);

            const heading = mockLoader.querySelector('.govuk-heading-m');
            expect(heading.textContent).toBe('Loading...');
        });

        it('should not set isNavigating again if already navigating', () => {
            setIsNavigating(true);
            mockLoader.innerHTML = '<h1 class="govuk-heading-m">First message</h1>';

            const event = new MessageEvent('message', {
                data: { type: 'gtaa-navigation-start', message: 'Second message' },
                origin: window.location.origin
            });

            handlePostMessage(event);

            const heading = mockLoader.querySelector('.govuk-heading-m');
            expect(heading.textContent).toBe('First message');
        });

        it('should ignore messages without gtaa-navigation-start type', () => {
            setIsNavigating(false);

            const event = new MessageEvent('message', {
                data: { type: 'other-type' },
                origin: window.location.origin
            });

            handlePostMessage(event);

            expect(getIsNavigating()).toBe(false);
        });

        it('should handle messages without data gracefully', () => {
            setIsNavigating(false);

            const event = new MessageEvent('message', {
                origin: window.location.origin
            });

            expect(() => handlePostMessage(event)).not.toThrow();
            expect(getIsNavigating()).toBe(false);
        });
    });

    describe('handleBeforeUnload', () => {
        it('should set isNavigating to true and show unload message', () => {
            setIsNavigating(false);
            mockLoader.innerHTML = '<h1 class="govuk-heading-m">Old</h1>';

            const result = handleBeforeUnload();

            expect(getIsNavigating()).toBe(true);
            const heading = mockLoader.querySelector('.govuk-heading-m');
            expect(heading.textContent).toBe('Loading...');
            expect(result instanceof Promise).toBe(true);
        });

        it('should not do anything if already navigating', () => {
            setIsNavigating(true);
            mockLoader.innerHTML = '<h1 class="govuk-heading-m">Original</h1>';

            const result = handleBeforeUnload();

            expect(result).toBeUndefined();
            const heading = mockLoader.querySelector('.govuk-heading-m');
            expect(heading.textContent).toBe('Original');
        });

        it('should return a promise that resolves after 100ms', async () => {
            setIsNavigating(false);
            mockLoader.innerHTML = '<h1 class="govuk-heading-m"></h1>';

            const promise = handleBeforeUnload();

            jest.advanceTimersByTime(100);
            await expect(promise).resolves.toBeUndefined();
        });
    });

    describe('handlePageShow', () => {
        it('should reset isNavigating and hide loader', () => {
            setIsNavigating(true);
            mockLoader.style.display = 'block';

            const event = new Event('pageshow');
            handlePageShow(event);

            expect(getIsNavigating()).toBe(false);
            expect(mockLoader.style.display).toBe('none');
        });

        it('should remove loading class from wrapper', () => {
            setIsNavigating(true);
            mockWrapper.classList.add('loading');

            const event = new Event('pageshow');
            handlePageShow(event);

            expect(mockWrapper.classList.contains('loading')).toBe(false);
        });

        it('should handle missing elements gracefully', () => {
            document.body.innerHTML = '';
            initializeElements();
            setIsNavigating(true);

            const event = new Event('pageshow');
            expect(() => handlePageShow(event)).not.toThrow();
            expect(getIsNavigating()).toBe(false);
        });
    });

    describe('setupTimeoutSafety', () => {
        it('should hide loader and remove loading class after timeout', () => {
            mockLoader.style.display = 'block';
            mockWrapper.classList.add('loading');

            setupTimeoutSafety(5000);

            jest.advanceTimersByTime(5000);

            expect(mockLoader.style.display).toBe('none');
            expect(mockWrapper.classList.contains('loading')).toBe(false);
        });

        it('should not hide loader if already hidden', () => {
            mockLoader.style.display = 'none';
            mockWrapper.classList.add('loading');

            setupTimeoutSafety(5000);

            jest.advanceTimersByTime(5000);

            expect(mockLoader.style.display).toBe('none');
            expect(mockWrapper.classList.contains('loading')).toBe(true);
        });

        it('should handle missing wrapper gracefully', () => {
            mockWrapper.remove();
            mockLoader.style.display = 'block';

            setupTimeoutSafety(5000);

            jest.advanceTimersByTime(5000);

            expect(mockLoader.style.display).toBe('none');
        });

        it('should use configurable timeout value', () => {
            mockLoader.style.display = 'block';

            setupTimeoutSafety(2000);

            jest.advanceTimersByTime(1999);
            expect(mockLoader.style.display).toBe('block');

            jest.advanceTimersByTime(1);
            expect(mockLoader.style.display).toBe('none');
        });
    });

    describe('init function', () => {
        it('should initialize elements, show loader, and set frame display', () => {
            mockLoader.style.display = 'none';
            mockFrame.style.display = 'none';

            init();

            expect(mockLoader.style.display).toBe('block');
            expect(mockFrame.style.display).toBe('block');
        });

        it('should not throw if frame element is missing', () => {
            mockFrame.remove();

            expect(() => init()).not.toThrow();
            expect(mockLoader.style.display).toBe('block');
        });

        it('should setup event listeners on frame', () => {
            const addEventListenerSpy = jest.spyOn(mockFrame, 'addEventListener');

            init();

            expect(addEventListenerSpy).toHaveBeenCalledWith('load', expect.any(Function), { once: true });
            expect(addEventListenerSpy).toHaveBeenCalledWith('error', expect.any(Function), { once: true });
            expect(addEventListenerSpy).toHaveBeenCalledWith('pageshow', expect.any(Function), { once: true });

            addEventListenerSpy.mockRestore();
        });

        it('should setup window event listeners', () => {
            const addEventListenerSpy = jest.spyOn(window, 'addEventListener');

            init();

            expect(addEventListenerSpy).toHaveBeenCalledWith('message', expect.any(Function));
            expect(addEventListenerSpy).toHaveBeenCalledWith('beforeunload', expect.any(Function));
            expect(addEventListenerSpy).toHaveBeenCalledWith('pageshow', expect.any(Function));

            addEventListenerSpy.mockRestore();
        });

        it('should setup timeout safety', () => {
            const setTimeoutSpy = jest.spyOn(global, 'setTimeout');

            init();

            expect(setTimeoutSpy).toHaveBeenCalled();
            const calls = setTimeoutSpy.mock.calls;
            const timeoutCall = calls.find(call => call[1] === 30000);
            expect(timeoutCall).toBeDefined();

            setTimeoutSpy.mockRestore();
        });
    });

    describe('Integration tests', () => {
        it('should handle full frame load sequence', () => {
            init();
            expect(mockLoader.style.display).toBe('block');
            expect(mockFrame.style.display).toBe('block');

            handleFrameLoad();
            expect(mockLoader.style.display).toBe('none');
            expect(mockWrapper.classList.contains('loading')).toBe(false);
        });

        it('should handle frame error sequence', () => {
            init();
            expect(mockLoader.style.display).toBe('block');

            handleFrameError();
            const errorMessage = mockLoader.querySelector('p');
            expect(errorMessage).toBeTruthy();
            expect(errorMessage.textContent).toBe('Failed to load questionnaire. Please try again.');
        });

        it('should handle navigation and page show sequence', () => {
            init();
            setIsNavigating(false);
            const postMessageEvent = new MessageEvent('message', {
                data: { type: 'gtaa-navigation-start', message: 'Navigating to next question...' },
                origin: window.location.origin
            });

            handlePostMessage(postMessageEvent);
            expect(getIsNavigating()).toBe(true);

            const pageShowEvent = new Event('pageshow');
            handlePageShow(pageShowEvent);
            expect(getIsNavigating()).toBe(false);
            expect(mockLoader.style.display).toBe('none');
        });

        it('should handle before unload and page show sequence', () => {
            init();
            setIsNavigating(false);
            mockLoader.innerHTML = '<h1 class="govuk-heading-m">Original</h1>';

            handleBeforeUnload();
            expect(getIsNavigating()).toBe(true);

            const pageShowEvent = new Event('pageshow');
            handlePageShow(pageShowEvent);
            expect(getIsNavigating()).toBe(false);
        });
    });
});
