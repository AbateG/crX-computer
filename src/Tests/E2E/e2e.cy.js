describe('E2E Test Suite', () => {
  it('should load the main page and display Projects', () => {
    cy.visit('/');
    cy.contains('Projects').should('be.visible');
  });
});
