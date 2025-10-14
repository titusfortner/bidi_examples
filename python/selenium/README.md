# Python Selenium BiDi Examples

This directory contains Python examples demonstrating BiDirectional (BiDi) protocol usage with Selenium WebDriver.

## Prerequisites

- Python 3.8 or higher
- Firefox browser installed
- GeckoDriver (automatically managed by Selenium Manager)

## Setup

1. Create a virtual environment (recommended):
```bash
python -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate
```

2. Install dependencies:
```bash
pip install -r requirements.txt
```

## Running Tests

Run all tests:
```bash
pytest
```

Run a specific test file:
```bash
pytest test_basic_auth.py
```

Run with verbose output:
```bash
pytest -v
```

## Project Structure

- `test_base.py` - Base test class with WebDriver setup and teardown
- `test_basic_auth.py` - Example test demonstrating basic authentication
- `pytest.ini` - Pytest configuration
- `requirements.txt` - Python dependencies

## Base Test Class

The `TestBase` class provides:
- Automatic WebDriver setup before each test
- Automatic WebDriver teardown after each test
- Firefox with BiDi enabled by default
- Easy extension for custom test classes

### Usage Example

```python
from test_base import TestBase

class TestMyFeature(TestBase):
    def test_something(self):
        self.driver.get("https://example.com")
        # Your test code here
```

## Notes

- All tests use Firefox with BiDi enabled by default
- The base class uses pytest fixtures for setup/teardown
- Each test gets a fresh browser instance