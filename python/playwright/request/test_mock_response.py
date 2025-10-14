from playwright.sync_api import Page


def test_mock_response_body(page: Page):
    """Test mocking a response body."""
    mock_body = "<h1>Mock Response</h1>"
    
    page.route("https://selenium.dev/", lambda route: route.fulfill(body=mock_body))
    
    page.goto("https://selenium.dev")
    assert page.text_content("h1") == "Mock Response"