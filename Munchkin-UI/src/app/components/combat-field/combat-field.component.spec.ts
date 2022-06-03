import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CombatFieldComponent } from './combat-field.component';

describe('CombatFieldComponent', () => {
  let component: CombatFieldComponent;
  let fixture: ComponentFixture<CombatFieldComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CombatFieldComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CombatFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
