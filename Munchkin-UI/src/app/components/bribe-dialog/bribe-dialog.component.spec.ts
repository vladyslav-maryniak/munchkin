import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BribeDialogComponent } from './bribe-dialog.component';

describe('BribeDialogComponent', () => {
  let component: BribeDialogComponent;
  let fixture: ComponentFixture<BribeDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BribeDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BribeDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
