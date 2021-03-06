import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MaterialImportsModule } from './material-imports/material-imports.module';
import { ClipboardModule } from 'ngx-clipboard';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from './guards/authentication.guard';
import { AuthenticationInterceptor } from './interceptors/authentication.interceptor';
import { HomeComponent } from './pages/home/home.component';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { SignUpComponent } from './pages/sign-up/sign-up.component';
import { GameLobbyComponent } from './pages/game-lobby/game-lobby.component';
import { GameComponent } from './pages/game/game.component';
import { ActionButtonComponent } from './components/action-button/action-button.component';
import { ActionControlAreaDirective } from './directives/action-control-area.directive';
import { MetadataDialogComponent } from './components/metadata-dialog/metadata-dialog.component';
import { CombatFieldComponent } from './components/combat-field/combat-field.component';
import { TableComponent } from './components/table/table.component';
import { PlayerSidebarComponent } from './components/player-sidebar/player-sidebar.component';
import { InHandCardPanelComponent } from './components/in-hand-card-panel/in-hand-card-panel.component';
import { SixSidedDieComponent } from './components/six-sided-die/six-sided-die.component';
import { BribeDialogComponent } from './components/bribe-dialog/bribe-dialog.component';
import { TradeDialogComponent } from './components/trade-dialog/trade-dialog.component';
import { RewardDialogComponent } from './components/reward-dialog/reward-dialog.component';
import { NicknameValidatorDirective } from './directives/validation/nickname-validator.directive';
import { EmailValidatorDirective } from './directives/validation/email-validator.directive';
import { PasswordMatchingValidatorDirective } from './directives/validation/password-matching-validator.directive';
import { GameCompletionComponent } from './components/game-completion/game-completion.component';

const appRoutes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'sign-in',
    component: SignInComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'sign-up',
    component: SignUpComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'game/:game-id',
    component: GameComponent,
    canActivate: [AuthenticationGuard],
  },
  {
    path: 'game/:game-id/lobby',
    component: GameLobbyComponent,
    canActivate: [AuthenticationGuard],
  },
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SignInComponent,
    SignUpComponent,
    GameLobbyComponent,
    GameComponent,
    ActionButtonComponent,
    ActionControlAreaDirective,
    MetadataDialogComponent,
    CombatFieldComponent,
    TableComponent,
    PlayerSidebarComponent,
    InHandCardPanelComponent,
    SixSidedDieComponent,
    BribeDialogComponent,
    TradeDialogComponent,
    RewardDialogComponent,
    NicknameValidatorDirective,
    EmailValidatorDirective,
    PasswordMatchingValidatorDirective,
    GameCompletionComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    MaterialImportsModule,
    ClipboardModule,
    RouterModule.forRoot(appRoutes),
  ],
  providers: [
    AuthenticationGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
