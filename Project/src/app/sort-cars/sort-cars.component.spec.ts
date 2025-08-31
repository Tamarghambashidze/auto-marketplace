import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SortCarsComponent } from './sort-cars.component';

describe('SortCarsComponent', () => {
  let component: SortCarsComponent;
  let fixture: ComponentFixture<SortCarsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SortCarsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SortCarsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
