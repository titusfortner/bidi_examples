from selenium.webdriver.support.ui import WebDriverWait


def test_wait_and_get_request(driver):
    # def first_png(req):
    #     if "png" in req.uri:
    #         req.complete()
    # registration = driver.network.add_request_handler(filters=[], handler=first_png)

    driver.get("https://selenium.dev")

    wait = WebDriverWait(driver, 10)

    # wait.until(lambda d: registration.result is not None)
    # assert "sponsors" in registration.result.uri