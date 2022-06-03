import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InHandCardPanelComponent } from './in-hand-card-panel.component';

describe('InHandCardPanelComponent', () => {
  let component: InHandCardPanelComponent;
  let fixture: ComponentFixture<InHandCardPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InHandCardPanelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InHandCardPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
