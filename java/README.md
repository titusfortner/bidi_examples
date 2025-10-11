# Java BiDi Examples

This repo is designed to help develop the High Level API for Selenium 5.
There is an example project of real-world use cases for both Playwright and Selenium.

## Proposed API

1. `addRequestHandler()` returns a `Registration` instance instead of just an id that has:
    * `remove()`
    * `isComplete()`
    * `getResult()`
    * `dispose()`

2. Overload `addRequestHandler()` with these parameter signatures:
    ```java
    public Registration addRequestHandler(Consumer<HttpRequest> handler);
    public Registration addRequestHandler(List<UrlPattern> filters, UnaryOperator<RequestRoute> handler);
    ```
    * `Consumer` does not allow a return value and is only for observation
    * `UnaryOperator` requires users to explicitly call a method on request route to return a `RequestRoute` instance
    * BiDi filters are only supported for Interception, so this is an easy way to disambiguate the overloads
    * Any client-side filtering can be done inside the function body

3. Multiple handlers are managed LIFO (Last In First Out), similar to Playwright
    * Interceptors are evaluated in reverse registration order (newest first).
    * `RequestRoute` methods manages how the requests terminates or continues

4. `RequestRoute` manages how individual requests are routed, this is also what playwright does; 
    the request is stored in the route and the user must pick which action to return where

5. `RequestRoute` Methods:
    * `fail()` same as playwright `abort()` but matches bidi spec "failRequest"
    * `respond()` same as playwright `fulfill()` but matches bidi spec "provideResponse"
    * `next()` same as playwright `fallback()`, the bidi "continueRequest" will be an implementation detail of this
    * `complete()` - this will remove the handler from bidi handlers list entirely and store the request for retrieval
    * `continueRequest()` - (optional); it's the original playwright behavior that will skip other handlers and call the
      bidi "continueRequest" immediately
    * default: returning without calling any of those methods is the same as calling `next()` with the original request

6. The `complete()` implementation is tricky. The idea is for when you need the first matching of a thing 
    and after that you don't need to keep looking. So `RequestRoute` stores the instance of the 
    `Registration` managing the handler, has it remove() itself in whatever atomic/idempotent way,
    then store the request as the result in the `Registration` instance


7. Examples are mostly one-liners:
    ```java
    Network nw = driver.network();
    nw.addRequestHandler(filter, route -> route.next(route.request().removeHeader("upgrade-insecure-requests")));
    nw.addRequestHandler(filter, route -> route.next(route.request().setMethod("HEAD"));
    nw.addRequestHandler(filter, route -> route.fail());
    ```

8. Waiting and Returning values
    ```java
    Registration registration = driver
            .network()
            .addRequestHandler(UrlPattern.ALL, route -> {
              if (route.request().resourceType() == ResourceType.IMAGE) {
                return route.complete();
              }
              return;
            });
    driver.get("https://selenium.dev");
    HttpRequest request = wait.until(() -> registration.getResult());
    ```

   This is "more selenium" by allowing the use of existing waits and existing handler signatures, but much more verbose than the
   equivalent playwright:
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

Not Working:

* Multiple Handler Support
* Aborting requests
* Mocking requests

