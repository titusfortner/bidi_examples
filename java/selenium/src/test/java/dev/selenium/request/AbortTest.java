package dev.selenium.request;

import dev.selenium.TestBase;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebElement;

public class AbortTest extends TestBase {

  @Test
  public void unconditionalAbort() {
    //    driver.network().addRequestHandler(RequestInterceptOptions.createDefaultOptions(), req -> req.fail());

    driver.get("https://selenium.dev");

    WebElement svg = driver.findElement(By.cssSelector("svg"));
    Assertions.assertTrue(svg.getSize().getHeight() > 30, "Abort not implemented yet");
  }

  @Test
  public void conditionalAbort() {
    //    driver
    //        .network()
    //        .addRequestHandler(
    //            RequestInterceptOptions.createDefaultOptions(),
    //            req -> {
    //              if (req.resourceType() == ResourceType.IMAGE) {
    //                req.fail();
    //              }
    //            });

    driver.get("https://selenium.dev");

    WebElement img = driver.findElement(By.cssSelector("img"));
    Long naturalHeight =
        (Long)
            ((JavascriptExecutor) driver).executeScript("return arguments[0].naturalHeight", img);
    Assertions.assertEquals(0L, naturalHeight, "Abort not implemented yet");
  }
}
