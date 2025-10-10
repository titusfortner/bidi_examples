package dev.selenium.request;

import com.microsoft.playwright.Route;
import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class MockResponseTest extends TestBase {

  @Test
  public void mockResponseBody() {
    String mockBody = "<h1>Mock Response</h1>";
    page.route(
        "https://selenium.dev/",
        route -> {
          route.fulfill(new Route.FulfillOptions().setBody(mockBody));
        });

    page.navigate("https://selenium.dev");
    Assertions.assertEquals("Mock Response", page.textContent("h1"));
  }
}
