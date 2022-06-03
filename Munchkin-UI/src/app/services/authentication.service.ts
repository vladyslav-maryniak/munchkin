import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, firstValueFrom } from 'rxjs';
import { environment } from 'src/environments/environment';
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
  ): Promise<boolean> {
    const endpoint = `${this.identityControllerUrl}/create`;
    const body = { nickname, email, password };
    const observable = this.httpClient.post<boolean>(endpoint, body, options);
    const response = await firstValueFrom(observable);

    return response.body ?? false;
  }

  async signInUser(
    nickname: string,
    email: string,
    password: string,
    isPersistent: boolean
  ): Promise<boolean> {
    const endpoint = `${this.identityControllerUrl}/sign-in`;
    const body = { nickname, email, password, isPersistent };
    const observable = this.httpClient.post<boolean>(endpoint, body, options);
    const response = await firstValueFrom(observable);

    if (response.body) {
      const player = await this.getUser();
      localStorage.setItem(StorageItems.Player, JSON.stringify(player));
      this.signedPlayer.next(player);
      return true;
    }
    return false;
  }

  async signOutUser(): Promise<void> {
    const endpoint = `${this.identityControllerUrl}/sign-out`;
    const observable = this.httpClient.post<void>(endpoint, {}, options);
    await firstValueFrom(observable);

    this.clearUserData();
  }

  async checkSignIn(): Promise<boolean> {
    const endpoint = `${this.identityControllerUrl}/check-sign-in`;
    const observable = this.httpClient.post<boolean>(endpoint, {}, options);
    const response = await firstValueFrom(observable);

    return response.body ?? false;
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
