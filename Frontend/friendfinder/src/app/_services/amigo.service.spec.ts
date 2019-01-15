import { TestBed } from '@angular/core/testing';

import { AmigoService } from './amigo.service';

describe('AmigoService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AmigoService = TestBed.get(AmigoService);
    expect(service).toBeTruthy();
  });
});
