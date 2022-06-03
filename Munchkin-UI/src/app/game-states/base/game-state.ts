import { ViewContainerRef } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActionButtonComponent } from 'src/app/components/action-button/action-button.component';
import { GameStateControl } from 'src/app/components/base/game-state-control';

export abstract class GameState<T> {
  protected context!: T;

  public setContext(context: T) {
    this.context = context;
  }

  public abstract occurs(): void;

  protected setActionButton(
    name: string,
    action: () => Promise<void>,
    container: ViewContainerRef
  ): void {
    const component = container.createComponent<GameStateControl>(
      ActionButtonComponent
    );

    component.instance.data = name;
    component.instance.action = action;
  }

  protected showSnackBar(
    forPlayer: string,
    forOthers: string,
    isCurrentPlayerTurn: boolean,
    snackBar: MatSnackBar
  ): void {
    snackBar.open(isCurrentPlayerTurn ? forPlayer : forOthers, '', {
      duration: 3000,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }
}
