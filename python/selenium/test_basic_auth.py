from selenium.webdriver.common.by import By


def test_basic_auth(driver):
    driver.network.add_auth_handler("admin", "admin")

    # driver.network.add_auth_handler(username="admin", password="admin")

    driver.get("https://the-internet.herokuapp.com/basic_auth")

    message = driver.find_element(By.TAG_NAME, "p").text
    assert message == "Congratulations! You must have the proper credentials."
