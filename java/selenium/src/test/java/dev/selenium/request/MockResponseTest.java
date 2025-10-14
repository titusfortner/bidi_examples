package dev.selenium.request;

import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;

public class MockResponseTest extends TestBase {

  @Test
  public void mockResponseBody() {
    //    String mockBody = "<h1>Mock Response</h1>";
    // .  RequestInterceptOptions options =
    //       RequestInterceptOptionsBuilder.addFilter("https://selenium.dev/").build();
    //    driver.network().addRequestHandler(options, req -> req.respond(mockBody));

    driver.get("https://selenium.dev/");
    String h1 = driver.findElement(By.tagName("h1")).getText();
    Assertions.assertEquals("Mock Response", h1, "Does not yet support mocks");
  }
}
