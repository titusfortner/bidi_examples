from playwright.sync_api import BrowserContext, Page


def test_log_requests(context: BrowserContext, page: Page):
    """Test logging all requests made during page navigation."""
    requests = []
    context.on("request", lambda request: requests.append(request.url))
    
    page.goto("https://demo.playwright.dev/todomvc/")
    
    assert len(requests) == 5