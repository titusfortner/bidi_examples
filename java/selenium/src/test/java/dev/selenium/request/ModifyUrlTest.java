package dev.selenium.request;

import dev.selenium.TestBase;
import java.net.URI;
import java.util.function.Predicate;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.remote.http.Contents;

public class ModifyUrlTest extends TestBase {

  @Test
  public void redirectUrl() {
    startFirefox();
    driver.get("https://httpbin.org/forms/post");

    Predicate<URI> filter =
        uri -> uri.getHost().equals("httpbin.org") && uri.getPath().equals("/post");
    String newContent = "custname=&custtel=&custemail=fake@example.com&delivery=&comments=";

    ((FirefoxDriver) driver)
        .network()
        .addRequestHandler(
            filter,
            req ->
                req.setContent(Contents.utf8String(newContent))
                    .setHeader("Content-Length", String.valueOf(newContent.length())));
    driver.findElement(By.name("custemail")).sendKeys("real@example.com");
    driver.findElement(By.tagName("button")).click();

    Assertions.assertEquals(
        "custemail \"fake@example.com\"", driver.findElement(By.id("/form/custemail")).getText());
  }
}
