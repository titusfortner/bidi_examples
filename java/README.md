# Java BiDi Examples

This repo is designed to help develop the High Level API for Selenium 5.
There is an example project of real-world use cases for both Playwright and Selenium.

## Proposed API

1. `addRequestHandler()` returns a `Registration` instance instead of just an id that has:
    * `remove()`
    * `getResult()`
    * `isComplete()` (maybe?)
    * `dispose()` (maybe?)

2. Overload `addRequestHandler()` with these 2 parameter signatures:
    ```java
    public Registration addRequestHandler(ObserveOptions options, Consumer<ObservedRequest> handler);
    public Registration addRequestHandler(RequestInterceptOptions options, Consumer<PausedRequset> handler);
    ```
    * `Consumer` means no return values, everything is from side effects.
    * ObserveOptions should work for Requests and Responses, just contains collectRequestBody
    * RequestInterceptOptions: collectRequestBody, urlPatterns, likely eventually will include timeouts, audit settings, priorities, etc
    * ObservedRequest - a read-only wrapper of the HttpRequest object
    * PausedRequest - a wrapper of HttpRequest object that delegates to HttpRequest class as necessary, but has additional
        methods change and record state for the Dispatcher code 

3. The Dispatcher code (however implemented) iterates over the intercept handlers (LIFO), 
    before each handler it collects the body if the options for that handler says to do so,
    after the handler runs, it checks for terminal state, to exit, else continue iterating over intercept handlers
    after last intercept handler with no terminal conditions, calls continueRequest, then iterates over observers

4. `complete()` is called if the purpose of the handler was to find a specific matching request. So the first 
    match calls complete() and the handler is now completely unnecessary. The Dispatch code will check for it as
    a terminal state and update the Registration instance with the result, 
    and have it remove() itself in whatever atomic/idempotent way,

5. Examples are mostly one-liners:
    ```java
    Network nw = driver.network();
    RequestInterceptOptions options = RequestInterceptOptions.createDefaultOptions();
    ```
    nw.addRequestHandler(options, req -> req.removeHeader("upgrade-insecure-requests"));
    nw.addRequestHandler(options, req -> req.setMethod("HEAD"));
    nw.addRequestHandler(options, req -> req.fail());
    ```

6. Waiting and Returning values
    ```java
    Registration registration = driver
            .network()
            .addRequestHandler(RequestInterceptOptions.createDefaultOptions(), 
                              req -> {
                                  if (req.resourceType() == ResourceType.IMAGE) {
                                    req.complete();
                                  }});
    driver.get("https://selenium.dev");
    HttpRequest request = wait.until(() -> registration.getResult());
    ```

   This is the equivalent code in Playwright, which is obviously more succinct:
   ```java
    Request request =
        page.waitForRequest(
            r -> "image".equals(r.resourceType()), () -> page.navigate("https://selenium.dev"));
    ```
    
    Could potentially tidy it up with this, but it's essentially a wrapper with a lot of assumptions:
   ```java
     HttpRequest request = driver.network().waitForRequest(
                                                req -> req.resourceType() == ResourceType.IMAGE,
                                                () -> driver.get("https://selenium.dev"));
    ```

## Current Implementation Status

Working with current API:

* Basic Auth support
* Logging Requests
* Modifying header, post, url

Needs Fixing:

* HttpRequest does not support modifying method
* Intercept is currently always turned on globally regardless of whether there are handlers that need to be intercepted
* We are filtering client side instead of with BiDi UrlPattern
* We are intercepting all network traffic; we should default to just http/https/ws/wss unless the user specifies otherwise

Not Working:

* Multiple Handler Support
* Aborting requests
* Mocking requests

