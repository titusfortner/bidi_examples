import pytest
from playwright.sync_api import Playwright, Browser, BrowserContext, Page


@pytest.fixture(scope="session")
def playwright_instance(playwright: Playwright) -> Playwright:
    return playwright


@pytest.fixture(scope="session")
def browser(playwright: Playwright) -> Browser:
    browser = playwright.chromium.launch(headless=False)
    yield browser
    browser.close()


@pytest.fixture
def context(browser: Browser) -> BrowserContext:
    context = browser.new_context()
    yield context
    context.close()


@pytest.fixture
def page(context: BrowserContext) -> Page:
    page = context.new_page()
    yield page
    page.close()