import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Game } from 'src/app/models/game';
import { Player } from 'src/app/models/player';
import { CardService } from 'src/app/services/card.service';
import { GameService } from 'src/app/services/game.service';
import { SharedDataService } from 'src/app/services/shared-data.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css'],
})
export class TableComponent implements OnInit, OnDestroy {
  game: Game | undefined;
  player: Player | undefined;

  private subscriptions: Subscription[] = [];

  constructor(
    private route: ActivatedRoute,
    private gameService: GameService,
    private cardService: CardService,
    private dataService: SharedDataService
  ) {}

  async ngOnInit(): Promise<void> {
    const gameId = this.route.snapshot.paramMap.get('game-id') ?? '';
    const game = await this.dataService.getGame(gameId);
    this.subscriptions.push(game.subscribe((x: Game) => (this.game = x)));

    this.subscriptions.push(
      this.dataService.getPlayer().subscribe((x: Player) => (this.player = x))
    );
  }

  async drawCard(): Promise<void> {
    if (this.game && this.player) {
      await this.gameService.drawCard(this.game.id, this.player.id);
    }
  }

  getImageSrc(fileName: string): string {
    return this.cardService.getImageSrc(fileName);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((x) => x.unsubscribe());
  }
}
