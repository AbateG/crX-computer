# Test Strategy

## Unit Tests
- Located in `Tests/Unit`, `Domain.Tests`, etc.
- Test individual methods and classes in isolation.

## Integration Tests
- Located in `Tests/Integration`, `Infrastructure.Tests`.
- Test interactions between components (e.g., API, database, external services).

## E2E Tests
- Located in `Tests/E2E`.
- Use Cypress to test user flows in the web UI.

## Running Tests
- Unit/Integration: `dotnet test`
- E2E: `npx cypress open` or `npx cypress run`
