from selenium.webdriver.common.by import By


def test_redirect_url(driver):
    # url = "https://deploy-preview-2198--selenium-dev.netlify.app"
    # driver.network.add_request_handler(filters=["https://selenium.dev"], handler=lambda req: req.url = url)

    driver.get("https://selenium.dev")

    message = "Registrations Open for SeleniumConf 2025 | March 26–28 | Join Us In-Person! Register now!"
    assert driver.find_element(By.TAG_NAME, "h4").text == message