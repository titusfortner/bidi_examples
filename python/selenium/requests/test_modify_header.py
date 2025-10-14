from selenium.webdriver.common.by import By


def test_add_header(driver):
    def add_header(request):
        headers = request.headers
        headers.append({'name': 'X-Test', 'value': {'type': 'string', 'value': 'true'}})
        request.continue_request(method=request.method, headers=headers)

    driver.network.add_request_handler("before_request", callback=add_header)

    # filter = {"host": "httpbin.org"}
    # driver.network.add_request_handler(filters=[filter], handler=lambda req: req.add_header("X-Test", True))

    driver.get("http://httpbin.org/headers")
    assert driver.find_element(By.ID, "/headers/X-Test").text == 'X-Test "true"'


def test_remove_header(driver):
    # filter = {"host": "httpbin.org"}
    # driver.network.add_request_handler(filters=[filter], handler=lambda req: req.remove_header("upgrade-insecure-requests"))

    driver.get("http://httpbin.org/headers")

    assert len(driver.find_elements(By.ID, "/headers/Upgrade-Insecure-Requests")) == 0