package dev.selenium.request;

import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;

public class MockResponseTest extends TestBase {

  @Test
  public void mockResponseBody() {
    startFirefox();

    // Not implemented

    driver.get("https://selenium.dev/");
    Assertions.assertEquals("Mock Response", driver.findElement(By.tagName("h1")).getText());
  }
}
