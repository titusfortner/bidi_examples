import re
from playwright.sync_api import BrowserContext, Page


def test_modify_post_body(context: BrowserContext, page: Page):
    """Test modifying POST request body."""
    page.goto("https://httpbin.org/forms/post")
    page.fill("input[name=custemail]", "real@example.com")
    
    override_body = "custname=&custtel=&custemail=fake@example.com&delivery=&comments="
    
    context.route("**/post", lambda route: route.continue_(post_data=override_body))
    page.click("button")
    
    body_text = re.sub(r"\s+", "", page.inner_text("body"))
    expected = '"custemail":"fake@example.com"'
    assert expected in body_text