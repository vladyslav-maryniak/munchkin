import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Equipment } from 'src/app/models/equipment';
import { Game } from 'src/app/models/game';
import { Place } from 'src/app/models/place';
import { Player } from 'src/app/models/player';
import { CardService } from 'src/app/services/card.service';
import { GameService } from 'src/app/services/game.service';
import { SharedDataService } from 'src/app/services/shared-data.service';

@Component({
  selector: 'app-player-sidebar',
  templateUrl: './player-sidebar.component.html',
  styleUrls: ['./player-sidebar.component.css'],
})
export class PlayerSidebarComponent implements OnInit, OnDestroy {
  game!: Game;

  private subscription!: Subscription;

  get places(): Place[] {
    return this.game?.table.places;
  }

  constructor(
    private route: ActivatedRoute,
    private cardService: CardService,
    private dataService: SharedDataService,
    private gameService: GameService
  ) {}

  async ngOnInit(): Promise<void> {
    const gameId = this.route.snapshot.paramMap.get('game-id') ?? '';
    const game = await this.dataService.getGame(gameId);
    this.subscription = game.subscribe((x: Game) => (this.game = x));
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

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
