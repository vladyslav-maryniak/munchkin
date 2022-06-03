import { Component } from '@angular/core';
import { GameStateControl } from '../base/game-state-control';

@Component({
  selector: 'app-six-sided-die',
  templateUrl: './six-sided-die.component.html',
  styleUrls: ['./six-sided-die.component.css'],
})
export class SixSidedDieComponent implements GameStateControl {
  data: any;

  async action(): Promise<void> {}
}
