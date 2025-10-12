package dev.selenium;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.UsernameAndPassword;
import org.openqa.selenium.firefox.FirefoxDriver;

public class BasicAuthTest extends TestBase {
  @Test
  public void basicAuthTest() {
    ((FirefoxDriver) driver)
        .network()
        .addAuthenticationHandler(new UsernameAndPassword("admin", "admin"));

    //    driver.network().addAuthenticationHandler(UrlPattern.WEB, new UsernameAndPassword("admin", "admin"));

    driver.navigate().to("https://the-internet.herokuapp.com/basic_auth");

    Assertions.assertEquals(
        "Congratulations! You must have the proper credentials.",
        driver.findElement(By.tagName("p")).getText());
  }
}
