import pytest
from selenium import webdriver

@pytest.fixture(scope='function')
def driver():
    options = webdriver.FirefoxOptions()
    options.enable_bidi = True

    driver = webdriver.Firefox(options=options)

    yield driver

    driver.network.clear_request_handlers()
    driver.quit()
