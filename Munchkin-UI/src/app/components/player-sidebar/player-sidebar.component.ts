import { Component, Input } from '@angular/core';
import { Equipment } from 'src/app/models/equipment';
import { Game } from 'src/app/models/game';
import { Place } from 'src/app/models/place';
import { Player } from 'src/app/models/player';
import { CardService } from 'src/app/services/card.service';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-player-sidebar',
  templateUrl: './player-sidebar.component.html',
  styleUrls: ['./player-sidebar.component.css'],
})
export class PlayerSidebarComponent {
  @Input() game!: Game;

  get places(): Place[] {
    return this.game?.table.places;
  }

  constructor(
    private cardService: CardService,
    private gameService: GameService
  ) {}
  }

  isPlayerTurn(player: Player): boolean {
    return this.gameService.isPlayerTurn(this.game, player);
  }

  getNicknameAbbreviation(player: Player): string {
    return this.cardService.getNicknameAbbreviation(player);
  }

  getCharacterEquipmentDescription(equipment: Equipment): string {
    return this.cardService.getCharacterEquipmentDescription(equipment);
  }

  }
}
