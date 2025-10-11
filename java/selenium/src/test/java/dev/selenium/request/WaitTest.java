package dev.selenium.request;

import dev.selenium.TestBase;
import java.time.Duration;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.support.ui.WebDriverWait;

public class WaitTest extends TestBase {
  @Test
  public void waitAndGetRequest() {
    startFirefox();

    // Not implemented

    driver.get("https://httpbin.org/forms/post");
    driver.findElement(By.name("custemail")).sendKeys("real@example.com");
    driver.findElement(By.tagName("button")).click();

    WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));
    Assertions.fail("Does not yet support Waits and Returns");
  }
}
