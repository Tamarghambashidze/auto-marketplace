import { TestBed } from '@angular/core/testing';

import { SortCarsService } from './sort-cars.service';

describe('SortCarsService', () => {
  let service: SortCarsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SortCarsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
