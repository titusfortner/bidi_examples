from playwright.sync_api import Browser, BrowserContext, Page


def test_basic_auth_context(browser: Browser):
    """Test basic authentication using browser context credentials."""
    context = browser.new_context(http_credentials={"username": "admin", "password": "admin"})
    page = context.new_page()
    
    page.goto("https://the-internet.herokuapp.com/basic_auth")
    
    assert page.locator("p").inner_text() == "Congratulations! You must have the proper credentials."
    
    context.close()