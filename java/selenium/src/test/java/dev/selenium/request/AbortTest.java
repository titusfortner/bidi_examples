package dev.selenium.request;

import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebElement;

public class AbortTest extends TestBase {
  @BeforeEach
  public void startDriver() {
    startFirefox();
  }

  @Test
  public void unconditionalAbort() {
    //    ((FirefoxDriver) driver).network().addRequestHandler(UrlPattern.ALL, route ->
    // route.fail());

    driver.get("https://selenium.dev");

    WebElement svg = driver.findElement(By.cssSelector("svg"));
    Assertions.assertTrue(svg.getSize().getHeight() > 30, "Not implemented yet");
  }

  @Test
  public void conditionalAbort() {
    //    ((FirefoxDriver) driver)
    //        .network()
    //        .addRequestHandler(
    //            UrlPattern.ALL,
    //            route -> {
    //              if (route.request().resourceType() == ResourceType.IMAGE) {
    //                return route.fail();
    //              }
    //              return route.next();
    //            });

    driver.get("https://selenium.dev");

    WebElement img = driver.findElement(By.cssSelector("img"));
    Long naturalHeight =
        (Long)
            ((JavascriptExecutor) driver).executeScript("return arguments[0].naturalHeight", img);
    Assertions.assertEquals(0L, naturalHeight, "Not implemented yet");
  }
}
