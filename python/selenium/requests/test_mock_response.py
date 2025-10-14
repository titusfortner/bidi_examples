from selenium.webdriver.common.by import By


def test_mock_response_body(driver):
    # mock_body = "<h1>Mock Response</h1>"
    # filter = "https://selenium.dev/"
    # driver.network.add_request_handler(filters=[filter], collectBody=False, intercept=True, handler=lambda req: req.respond(mock_body))

    driver.get("https://selenium.dev/")
    h1 = driver.find_element(By.TAG_NAME, "h1").text
    assert h1 == "Mock Response", "Does not yet support mocks"