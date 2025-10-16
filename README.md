# BiDi Examples

This repo is designed to help develop the High Level API for Selenium 5.
Each language has a folder with projects for both Playwright and Selenium.

Where Selenium code or WebDriver BiDi browser functionality
has not been implemented, suggestions for what the API to use will be 
added as comments in the code.

## Network Class
All bindings will create a Network class accessible directly from the driver object to manage these features.

### Observers

#### Events
Users can add an observer to obtain the network traffic at each of the following events:
* Before Request Sent
* Authentication Required
* Response Started
* Response Completed
* Error Received

#### SubscriptionRegistration
Observers can be registered using the language idiomatic form of: `register<Type>Observer`
Where Type can be: `Request`, `Authentication`, `BeforeResponse`, `AfterResponse`, `ErrorReceived` 

### Interception
A user can add a handler to intercept and make changes to network traffic at each of the following stages:
* Before Request Sent
* Authentication Required
* Response Started

Multiple handlers may apply to the same request.
These requests:
* Need to be processed in reverse order of their registration
* Need to be applied to only http/https traffic by default

#### Request Interception

##### Properties
These are the values that can be changed within a request handler:
* url: `String` - Request URL
* method: `String` - HTTP method
* headers: `List[Map<String, String>]` - Request headers
* body: `String` - Request body (optional)
* cookies: `List[String]`- Request cookie headers

##### Actions
The following actions are possible when handling a request:
1. **Fail**: These requests will get a response of network.
2. **Respond**: These requests are provided a specific response rather than being sent to the original location
3. **Modify**: These requests have their modifications sent to the original location to get that response
4. **Continue**: These requests continue on to the next handler without modification
5. **Complete**: This exits interception entirely and the handler removed from future processing

* Requests will be processed by all handlers unless a request is put in a terminal state
* Terminal states include: Fail, Respond, and Complete
* User does not need to indicate in their handler code the end state of the request, 
Selenium will infer the end state based on the action taken within the handler

##### Handler Registration
Register an interception with the language idiomatic form of: `addRequestHandler`
The parameters that can be provided with this handler include:
* `stringFilters`: `List[String]` and/or `List[Map<String, String]` that apply to the request hanlder.
* `patternFilters`: `List[Map<String, String>]` with key values of: `protocol`, `host`, `port`, `path`, `query` 
* `collectBody`: `Boolean` indicating whether to store body information about matching requests and provide it to the handler

Additional parameters we may want to consider:
* `timeouts`
* `priority`
* `auditing`

Because of the number of options available, we should provide an options object in static languages:
`RequestInterceptOptions`, and optional keyword arguments for each parameter in dynamic languages.

Handler registration will return a `Registration` object that will contain: the registration id,
* `id`: registration id
* `status`: whether the handler is active or inactive
* `result`: the value of the request that was designated as complete within the handler
* `remove`: remove the handler from further processing

Handlers can be removed by either:
`removeHandler(id)` or `clearSubscriptions()` which will remove all observer and interceptions managed by Network class 

##### Waiting on Request 

Selenium will not create special waiting methods, and will continue to use the Selenium Wait objects.
To wait for and obtain the desired request object, use the complete method above to designate
that the processing of the handler is complete and register the result in the Registration object.

Users can wrap this behavior in their own custom functions if they wish to do so.

