import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Player } from './models/player';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'Munchkin-UI';
  signedPlayer!: Player | null;
  subscription!: Subscription;

  constructor(
    private router: Router,
    private authService: AuthenticationService
  ) {}

  async ngOnInit() {
    this.subscription = this.authService.signedPlayer.subscribe(
      (signedPlayer: Player | null) => (this.signedPlayer = signedPlayer)
    );
  }

  async signOut(): Promise<void> {
    await this.authService.signOutUser();
    await this.router.navigate(['/sign-in']);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
