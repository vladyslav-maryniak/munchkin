import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CombatFieldComponent } from 'src/app/components/combat-field/combat-field.component';
import {
  GameCompletionComponent,
  GameCompletionDialogData,
} from 'src/app/components/game-completion/game-completion.component';
import { GameState } from '../base/game-state';

export class GameCompletion extends GameState<CombatFieldComponent> {
  constructor(private dialog: MatDialog) {
    super();
  }

  public occurs(): void {
    const container = this.context.actionControlAreaDirective.viewContainerRef;
    container.clear();

    const dialogConfig = {
      disableClose: true,
    } as MatDialogConfig<GameCompletionDialogData>;

    const winnerIds = this.context.game.table.places
      .filter((x) => x.character.level == 10)
      .map((x) => x.player.id);

    if (winnerIds.includes(this.context.player.id)) {
      dialogConfig.data = {
        title: 'Victory',
        text: 'Congratulations! You win!',
      } as GameCompletionDialogData;
    } else {
      dialogConfig.data = {
        title: 'Defeat',
        text: 'You lose. Better luck next time!',
      } as GameCompletionDialogData;
    }
    this.dialog.open(GameCompletionComponent, dialogConfig);
  }
}
