import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameStatus } from 'src/app/models/game-status';
import { Player } from 'src/app/models/player';
import { PlayerGame } from 'src/app/models/player-game';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GameService } from 'src/app/services/game.service';
import { PlayerService } from 'src/app/services/player.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  playerGames: PlayerGame[] = [];
  player: Player | null = null;
  gameStatus = GameStatus;

  constructor(
    private authService: AuthenticationService,
    private gameService: GameService,
    private playerService: PlayerService,
    private router: Router
  ) {}

  async ngOnInit(): Promise<void> {
    this.player = await this.authService.signedPlayer.getValue();
    this.playerGames = await this.playerService.getPlayerGames(
      this.player?.id ?? ''
    );
  }

  async createGame(): Promise<void> {
    const game = await this.gameService.createGame();

    await this.gameService.joinPlayer(game.id);
    await this.router.navigate(['game', game.id, 'lobby']);
  }

  async goTo(game: PlayerGame): Promise<void> {
    switch (game.status) {
      case GameStatus.NotStarted:
        await this.router.navigate(['game', game.id, 'lobby']);
        break;

      case GameStatus.Started:
        await this.router.navigate(['game', game.id]);
        break;

      default:
        break;
    }
  }

  getPlayerNicknames(game: PlayerGame): string[] {
    return game.lobby.concat(game.table).map((x) => x.nickname);
  }
}
