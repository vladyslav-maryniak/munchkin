import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, firstValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CheckSignIn } from '../models/identity/check-sign-in';
import { IdentityResult } from '../models/identity/identity-result';
import { SignInResult } from '../models/identity/sign-in-result';
import { Player } from '../models/player';
import { StorageItems } from '../models/storage-items';

const options = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  observe: 'response' as const,
  responseType: 'json' as const,
  withCredentials: true,
};

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private identityControllerUrl = `${environment.hostUrl}/api/identity`;

  signedPlayer: BehaviorSubject<Player | null>;

  constructor(private httpClient: HttpClient) {
    const json = localStorage.getItem(StorageItems.Player);
    let player: Player | null = null;
    if (json) {
      player = JSON.parse(json);
    }
    this.signedPlayer = new BehaviorSubject(player);
  }

  async createUser(
    nickname: string,
    email: string,
    password: string
  ): Promise<IdentityResult> {
    const endpoint = `${this.identityControllerUrl}/create`;
    const body = { nickname, email, password };
    const observable = this.httpClient.post<IdentityResult>(
      endpoint,
      body,
      options
    );
    const response = await firstValueFrom(observable);

    return response.body ?? ({} as IdentityResult);
  }

  async signInUser(
    nickname: string,
    email: string,
    password: string,
    isPersistent: boolean
  ): Promise<SignInResult> {
    const endpoint = `${this.identityControllerUrl}/sign-in`;
    const body = { nickname, email, password, isPersistent };
    const observable = this.httpClient.post<SignInResult>(
      endpoint,
      body,
      options
    );
    const response = await firstValueFrom(observable);

    if (response.body?.succeeded) {
      const player = await this.getUser();
      localStorage.setItem(StorageItems.Player, JSON.stringify(player));
      this.signedPlayer.next(player);
    }
    return response.body ?? ({} as SignInResult);
  }

  async signOutUser(): Promise<void> {
    const endpoint = `${this.identityControllerUrl}/sign-out`;
    const observable = this.httpClient.post<void>(endpoint, {}, options);
    await firstValueFrom(observable);

    this.clearUserData();
  }

  async checkSignIn(): Promise<CheckSignIn> {
    const endpoint = `${this.identityControllerUrl}/check-sign-in`;
    const observable = this.httpClient.post<CheckSignIn>(endpoint, {}, options);
    const response = await firstValueFrom(observable);

    return response.body ?? ({} as CheckSignIn);
  }

  async getUser(): Promise<Player | null> {
    const endpoint = `${this.identityControllerUrl}/user`;
    const observable = this.httpClient.get<Player>(endpoint, options);
    const response = await firstValueFrom(observable);

    return response.body;
  }

  clearUserData() {
    localStorage.clear();
    this.signedPlayer.next(null);
  }
}
