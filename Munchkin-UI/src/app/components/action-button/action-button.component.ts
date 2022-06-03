import { Component, Input } from '@angular/core';
import { GameStateControl } from '../base/game-state-control';

@Component({
  selector: 'app-action-button',
  templateUrl: './action-button.component.html',
  styleUrls: ['./action-button.component.css'],
})
export class ActionButtonComponent implements GameStateControl {
  @Input() data: any;

  action!: () => Promise<void>;
}
