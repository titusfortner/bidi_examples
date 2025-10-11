package dev.selenium.request;

import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;

public class MockResponseTest extends TestBase {

  @Test
  public void mockResponseBody() {
    startFirefox();

    //    String mockBody = "<h1>Mock Response</h1>";
    //    UrlPattern filter = UrlPattern.string(List.of("https://selenium.dev/"));
    //    ((FirefoxDriver) driver).network().addRequestHandler(filter, route ->
    // route.response(mockBody));

    driver.get("https://selenium.dev/");
    Assertions.assertEquals("Mock Response", driver.findElement(By.tagName("h1")).getText());
  }
}
