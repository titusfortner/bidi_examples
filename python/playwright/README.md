# Python Playwright BiDi Examples

This directory contains Python examples demonstrating BiDirectional (BiDi) protocol capabilities using Playwright.

## Setup

### Prerequisites
- Python 3.8 or higher
- pip (Python package manager)

### Installation

1. Install dependencies:
```bash
pip install -r requirements.txt
```

2. Install Playwright browsers:
```bash
playwright install chromium
```

## Running Tests

### Run all tests:
```bash
pytest
```

### Run specific test file:
```bash
pytest test_basic_auth.py
```

### Run tests in a specific directory:
```bash
pytest request/
```

### Run a specific test:
```bash
pytest test_basic_auth.py::test_basic_auth_context
```

### Run with verbose output:
```bash
pytest -v
```

## Test Structure

- `conftest.py` - Pytest fixtures for browser, context, and page setup
- `test_basic_auth.py` - Basic authentication examples
- `request/` - Request interception and modification examples
  - `test_abort.py` - Aborting requests (conditional and unconditional)
  - `test_log.py` - Logging network requests
  - `test_mock_response.py` - Mocking response bodies
  - `test_modify_header.py` - Adding and removing request headers
  - `test_modify_method.py` - Modifying HTTP methods
  - `test_modify_post.py` - Modifying POST request data
  - `test_modify_url.py` - Redirecting URLs and adding query parameters
  - `test_wait.py` - Waiting for specific requests
  - `test_multiple_handler.py` - Using multiple route handlers

## Features Demonstrated

- **Network Interception**: Intercept and modify network requests
- **Request Routing**: Route requests based on URL patterns
- **Response Mocking**: Mock responses with custom content
- **Header Manipulation**: Add, modify, or remove request headers
- **Request Modification**: Change HTTP methods, URLs, and POST data
- **Request Logging**: Monitor and log network activity
- **Authentication**: Handle basic HTTP authentication