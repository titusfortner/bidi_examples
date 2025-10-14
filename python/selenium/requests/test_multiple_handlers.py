import re
from selenium.webdriver.common.by import By


def test_modify_multiple(driver):
    # filter = {"host": "httpbin.org"}
    # driver.network.add_request_handler(filters=[filter], handler=lambda req: req.add_header("X-Test", True))
    # driver.network.add_request_handler(filters=[filter], handler=lambda req: req.remove_header("upgrade-insecure-requests"))

    driver.get("https://httpbin.org/headers")

    body_text = re.sub(r'\s+', '', driver.find_element(By.TAG_NAME, "body").text)
    assert "Upgrade-Insecure-Requests" not in body_text
    assert '"X-Test":"true"' in body_text, "Does not yet support multiple handlers"