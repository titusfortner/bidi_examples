package dev.selenium.request;

import dev.selenium.TestBase;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Predicate;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.firefox.FirefoxDriver;

public class LogTest extends TestBase {

  @Test
  public void logRequests() {
    List<String> requests = new ArrayList<>();

    Predicate<URI> filter = uri -> true;
    ((FirefoxDriver) driver)
        .network()
        .addRequestHandler(
            filter,
            req -> {
              requests.add(req.getUri());
              return req;
            });

    //    driver.network().addRequestHandler(ObserverOptions.createDefaultOptions(), req ->
    // requests.add(req.getUri());

    driver.get("https://demo.playwright.dev/todomvc/");

    Assertions.assertEquals(5, requests.size());
  }
}
