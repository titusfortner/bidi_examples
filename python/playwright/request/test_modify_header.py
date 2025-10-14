import re
from playwright.sync_api import Page, Route


def test_add_header(page: Page):
    """Test adding a custom header to requests."""
    def handle_route(route: Route):
        headers = route.request.headers
        headers["x-test"] = "true"
        route.continue_(headers=headers)
    
    page.route("https://httpbin.org/**", handle_route)
    
    page.goto("https://httpbin.org/headers")
    
    body_text = re.sub(r"\s+", "", page.inner_text("body"))
    assert '"X-Test":"true"' in body_text


def test_remove_header(page: Page):
    """Test removing a header from requests."""
    def handle_route(route: Route):
        headers = route.request.headers.copy()
        headers.pop("upgrade-insecure-requests", None)
        route.continue_(headers=headers)
    
    page.route("https://httpbin.org/**", handle_route)
    
    page.goto("https://httpbin.org/headers")
    
    assert "Upgrade-Insecure-Requests" not in page.inner_text("body")