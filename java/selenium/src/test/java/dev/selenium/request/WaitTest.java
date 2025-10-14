package dev.selenium.request;

import dev.selenium.TestBase;

import java.net.URI;
import java.time.Duration;
import java.util.concurrent.atomic.AtomicReference;
import java.util.function.Predicate;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.remote.http.HttpMethod;
import org.openqa.selenium.remote.http.HttpRequest;
import org.openqa.selenium.support.ui.WebDriverWait;

public class WaitTest extends TestBase {
  @Test
  public void waitAndGetRequest() {
    Predicate<URI> filter = uri -> true;
    AtomicReference<HttpRequest> request = new AtomicReference<>();

    ((FirefoxDriver) driver)
        .network()
        .addRequestHandler(
            filter,
            req -> {
              if (req.getUri().contains("png")) {
                request.set(req);
              }
              return req;
            });

    // .  RequestInterceptOptions options =
    // RequestInterceptOptionsBuilder.addFilter(Map.of("host", "httpbin.org").build();
    //    Handler registration = driver
    //            .network()
    //            .addRequestHandler(options, req -> {
    //                if (req.resourceType() == ResourceType.IMAGE) {
    //                    req.finish()
    //            });

    driver.get("https://selenium.dev");

    WebDriverWait wait = new WebDriverWait(driver, Duration.ofSeconds(10));

    // It returned something
    wait.until(d -> request.get() != null);
    // It returned the first thing
    Assertions.assertTrue(
        request.get().getUri().contains("sponsors"), "It did not return the first thing");

    // HttpRequest request = wait.until(d -> registration.getResult());
    // Assertions.assertTrue(request.getUri().contains("sponsors"));
  }
}
