from selenium.webdriver.common.by import By
from selenium.webdriver.remote.http import Contents


def test_modify_post_body(driver):
    new_content = "custname=&custtel=&custemail=fake@example.com&delivery=&comments=";

    def modify_body(request):
    request.continue_request(body=new_content, method="POST", headers=request.headers)

    driver.network.add_request_handler("before_request", callback=modify_body)

    # filter = "https://httpbin.org/post
    # driver.network.add_request_handler(filters=[filter], handler=lambda req: req.content = new_content)))

    driver.get("https://httpbin.org/forms/post")

    driver.find_element(By.NAME, "custemail").send_keys("real@example.com")
    driver.find_element(By.TAG_NAME, "button").click()

    assert driver.find_element(By.ID, "/form/custemail").text == 'custemail "fake@example.com"'

def test_collect_body_and_modify(driver):
