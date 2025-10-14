import re
from playwright.sync_api import Page, Route


def test_modify_multiple(page: Page):
    """Test using multiple route handlers to modify requests."""
    def add_header(route: Route):
        headers = route.request.headers
        headers["x-test"] = "true"
        route.continue_(headers=headers)
    
    def remove_header(route: Route):
        headers = route.request.headers.copy()
        headers.pop("upgrade-insecure-requests", None)
        route.fallback(headers=headers)
    
    page.route("https://httpbin.org/**", add_header)
    page.route("https://httpbin.org/**", remove_header)
    
    page.goto("https://httpbin.org/headers")
    
    body_text = re.sub(r"\s+", "", page.inner_text("body"))
    assert "Upgrade-Insecure-Requests" not in body_text
    assert '"X-Test":"true"' in body_text