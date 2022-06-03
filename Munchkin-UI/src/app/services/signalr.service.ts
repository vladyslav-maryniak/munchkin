import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private hubConnection!: signalR.HubConnection;
  private _eventHub: Subject<string> = new Subject<string>();

  private methodName!: string;

  get gameEvents(): Observable<string> {
    return this._eventHub.asObservable();
  }

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.hostUrl}/event`)
      .build();
  }

  public connect(): Promise<void> {
    return this.hubConnection.start();
  }

  public follow(methodName: string): void {
    if (this.methodName) {
      this.hubConnection.off(methodName);
    }
    this.methodName = methodName;
    this.hubConnection.on(methodName, (event) => {
      this._eventHub.next(event);
    });
  }

  public disconnect(): Promise<void> {
    this.hubConnection.off(this.methodName);
    return this.hubConnection.stop();
  }
}
