from playwright.sync_api import Page, Route


def test_unconditional_abort(page: Page):
    """Test unconditionally aborting CSS requests."""
    page.route("**/*.css", lambda route: route.abort())
    
    page.goto("https://selenium.dev")
    height = page.locator("svg").first.bounding_box()["height"]
    assert height > 30


def test_conditional_abort(page: Page):
    """Test conditionally aborting image requests."""
    def handle_route(route: Route):
        if route.request.resource_type == "image":
            route.abort()
        else:
            route.continue_()
    
    page.route("**/*", handle_route)
    
    page.goto("https://selenium.dev")
    
    height = page.locator("img").first.evaluate("img => img.naturalHeight")
    assert height == 0