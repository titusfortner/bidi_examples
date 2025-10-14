def test_log_requests(driver):
    requests = []

    def log_requests(request):
        requests.append(request)
        request.continue_request()

    driver.network.add_request_handler("before_request", log_requests)

    # driver.network.add_request_handler(collect_body=False, handler=lambda req: requests.append(req.uri))

    URL = "https://demo.playwright.dev/todomvc/"

    driver.get(URL)

    assert len(requests) == 5
