import re
from playwright.sync_api import Page, Route


def test_redirect_url(page: Page):
    """Test redirecting a URL to a different domain."""
    def handle_route(route: Route):
        replaced = route.request.url.replace(
            "https://selenium.dev",
            "https://deploy-preview-2198--selenium-dev.netlify.app"
        )
        route.continue_(url=replaced)
    
    page.route("https://selenium.dev/**", handle_route)
    
    page.goto("https://selenium.dev")
    
    message = "Registrations Open for SeleniumConf 2025 | March 26–28 | Join Us In-Person! Register now!"
    assert page.locator("h4").first.inner_text() == message


def test_add_args(page: Page):
    """Test adding query parameters to a URL."""
    def handle_route(route: Route):
        appended = route.request.url + "?foo=bar"
        route.continue_(url=appended)
    
    page.route("https://httpbin.org/**", handle_route)
    
    page.goto("https://httpbin.org/anything")
    
    body_text = re.sub(r"\s+", "", page.inner_text("body"))
    assert '"args":{"foo":"bar"' in body_text