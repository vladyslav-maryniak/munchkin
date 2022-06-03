import { Component, Input } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { lastValueFrom } from 'rxjs';
import { Game } from 'src/app/models/game';
import { MunchkinCard } from 'src/app/models/munchkin-card';
import { Player } from 'src/app/models/player';
import { GameService } from 'src/app/services/game.service';
import { MetadataDialogComponent } from '../metadata-dialog/metadata-dialog.component';

@Component({
  selector: 'app-in-hand-card-panel',
  templateUrl: './in-hand-card-panel.component.html',
  styleUrls: ['./in-hand-card-panel.component.css'],
})
export class InHandCardPanelComponent {
  @Input() game!: Game;
  @Input() player!: Player;

  get inHandCards(): MunchkinCard[] {
    const place = this.game.table.places.find(
      (x) => x.player.id === this.player.id
    );
    return place?.inHandCards ?? [];
  }

  constructor(private gameService: GameService, public dialog: MatDialog) {}

  async playCard(card: MunchkinCard): Promise<void> {
    let metadata = card.metadata;
    if (card.metadata) {
      metadata = new Map<string, string>(Object.entries(card.metadata));
      const dialogConfig = new MatDialogConfig();

      for (var key of metadata.keys()) {
        dialogConfig.data = { title: key };
        const dialogRef = this.dialog.open(
          MetadataDialogComponent,
          dialogConfig
        );
        const value = await lastValueFrom(dialogRef.afterClosed());
        metadata.set(key, value);
      }
    }

    await this.gameService.playCard(
      this.game.id,
      this.player.id,
      card.id,
      metadata
    );
  }

  getCardDescription(c: MunchkinCard): string {
    const description: string[] = [];
    if (c.description) {
      description.push(c.description);
    }
    if (c.badStuff) {
      description.push(`Bad stuff: ${c.badStuff}`);
    }
    if (c.goldPieces) {
      description.push(`${c.goldPieces} Gold Pieces`);
    }
    return description.join('\n');
  }
}
