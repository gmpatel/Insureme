import { InsuremePage } from './app.po';

describe('insureme App', () => {
  let page: InsuremePage;

  beforeEach(() => {
    page = new InsuremePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
