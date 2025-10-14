from selenium.webdriver.common.by import By


def test_modify_http_method(driver):
    def modify_method(request):
        request.continue_request(method="HEAD", headers=request.headers)

    driver.network.add_request_handler("before_request", callback=modify_method)

    # driver.network.add_request_handler(filters=[], handler=lambda req: req.method = "HEAD")

    driver.get("https://selenium.dev")

    assert driver.find_element(By.TAG_NAME, "body").text == ""