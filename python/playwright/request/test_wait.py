from playwright.sync_api import Page


def test_wait_and_get_result(page: Page):
    """Test waiting for a specific request and getting its result."""
    with page.expect_request(lambda request: request.resource_type == "image") as request_info:
        page.goto("https://selenium.dev")
    
    request = request_info.value
    assert "sponsors" in request.url