package dev.selenium;

import com.titusfortner.logging.SeleniumLogger;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.firefox.FirefoxOptions;

public class TestBase {
  protected WebDriver driver;

  @BeforeAll
  static void logStuff() {
    SeleniumLogger.enable().disable();
  }

  @BeforeEach
  void startDriver() {
      startFirefox();
  }

  protected void startFirefox() {
    FirefoxOptions options = new FirefoxOptions();
    options.enableBiDi();
    driver = new FirefoxDriver(options);
  }

  @AfterEach
  void closeContext() {
    driver.quit();
  }
}
