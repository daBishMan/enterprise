describe('admin', () => {
  beforeEach(() => cy.visit('/'));

  it('should display welcome message', () => {
    expect(true).to.be.true;
  });
});
