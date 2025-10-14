from playwright.sync_api import Page


def test_modify_http_method(page: Page):
    """Test modifying the HTTP method of a request."""
    page.route("https://selenium.dev/**", lambda route: route.continue_(method="HEAD"))
    
    page.goto("https://selenium.dev")
    assert page.text_content("body") == ""