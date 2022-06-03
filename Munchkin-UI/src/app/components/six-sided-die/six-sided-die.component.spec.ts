import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SixSidedDieComponent } from './six-sided-die.component';

describe('SixSidedDieComponent', () => {
  let component: SixSidedDieComponent;
  let fixture: ComponentFixture<SixSidedDieComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SixSidedDieComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SixSidedDieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
