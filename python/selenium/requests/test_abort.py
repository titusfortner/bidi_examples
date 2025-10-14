from selenium.webdriver.common.by import By


def test_unconditional_abort(driver):
    # driver.network.add_request_handler(filters=[], handler=lambda route: route.fail())

    driver.get("https://selenium.dev")

    svg = driver.find_element(By.CSS_SELECTOR, "svg")
    assert svg.size['height'] > 30


def test_conditional_abort(driver):
    # def fail_images(req):
    #     if req.resource_type == "image":
    #         req.fail()

    # driver.network.add_request_handler(filters=[], collectBody=False, intercept=True, handler=fail_images)

    driver.get("https://selenium.dev")

    img = driver.find_element(By.CSS_SELECTOR, "img")
    natural_height = driver.execute_script("return arguments[0].naturalHeight", img)
    assert natural_height == 0