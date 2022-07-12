import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameCompletionComponent } from './game-completion.component';

describe('GameCompletionComponent', () => {
  let component: GameCompletionComponent;
  let fixture: ComponentFixture<GameCompletionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameCompletionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GameCompletionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
