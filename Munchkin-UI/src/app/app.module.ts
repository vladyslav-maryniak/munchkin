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
import { ActionButtonComponent } from './components/action-button/action-button.component';
import { ActionControlAreaDirective } from './directives/action-control-area.directive';
import { MetadataDialogComponent } from './components/metadata-dialog/metadata-dialog.component';

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
    ActionButtonComponent,
    ActionControlAreaDirective,
    MetadataDialogComponent,
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
