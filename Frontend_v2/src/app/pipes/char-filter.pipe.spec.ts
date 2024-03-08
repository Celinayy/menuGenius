import { CharFilterPipe } from './char-filter.pipe';

describe('FilterPipe', () => {
  it('create an instance', () => {
    const pipe = new CharFilterPipe();
    expect(pipe).toBeTruthy();
  });
});
