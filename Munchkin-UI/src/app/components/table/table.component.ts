import { Component, Input } from '@angular/core';
import { Game } from 'src/app/models/game';
import { Player } from 'src/app/models/player';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
})
export class TableComponent {
  @Input() game!: Game;
  @Input() player!: Player;

  constructor(private gameService: GameService) {}

  async drawCard(): Promise<void> {
    await this.gameService.drawCard(this.game.id, this.player.id);
  }
}
