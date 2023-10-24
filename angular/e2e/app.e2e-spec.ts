import { projectTemplatePage } from './app.po';

describe('project App', function() {
  let page: projectTemplatePage;

  beforeEach(() => {
    page = new projectTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
