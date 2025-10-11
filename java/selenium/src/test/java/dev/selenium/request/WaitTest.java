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

    //    UrlPattern filter = UrlPatternBuilder.setHost("httpbin.org").build();
    //    Handler handler = ((FirefoxDriver) driver)
    //            .network()
    //            .addRequestHandler(filter, route -> {
    //                if (route.request.resourceType() == ResourceType.IMAGE) {
    //                    return route.complete();
    //                } else {
    //                    return route.next();
    //            });

    driver.get("https://httpbin.org/forms/post");
    driver.findElement(By.name("custemail")).sendKeys("real@example.com");
    driver.findElement(By.tagName("button")).click();

    WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));
    Assertions.fail("Does not yet support Waits and Returns");
  }
}
