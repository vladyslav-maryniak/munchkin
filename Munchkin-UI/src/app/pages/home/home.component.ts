import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/services/game.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(private gameService: GameService, private router: Router) {}

  ngOnInit(): void {}

  async createGame(): Promise<void> {
    const game = await this.gameService.createGame();

    await this.gameService.joinPlayer(game.id);
    await this.router.navigate(['game', game.id, 'lobby']);
  }
}
