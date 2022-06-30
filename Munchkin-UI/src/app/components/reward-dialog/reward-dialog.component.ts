import { Component, Inject } from '@angular/core';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MunchkinCard } from 'src/app/models/munchkin-card';

export interface RewardDialogData {
  itemCards: MunchkinCard[];
  cardsForPlay: MunchkinCard[];
  numberOfTreasures: number;
  selectedItemCards: MunchkinCard[];
  selectedCardsForPlay: MunchkinCard[];
  selectedNumberOfTreasures: number;
  helperPicksFirst: boolean;
}

@Component({
  selector: 'app-reward-dialog',
  templateUrl: './reward-dialog.component.html',
  styleUrls: ['./reward-dialog.component.css'],
})
export class RewardDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<RewardDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: RewardDialogData
  ) {}

  onConfirm(): void {
    this.dialogRef.close(this.data);
  }

  onItemCardsChange(card: MunchkinCard, event: MatCheckboxChange) {
    this.onChange(card, this.data.selectedItemCards, event);
  }

  onCardsForPlayChange(card: MunchkinCard, event: MatCheckboxChange) {
    this.onChange(card, this.data.selectedCardsForPlay, event);
  }

  private onChange(
    card: MunchkinCard,
    collection: MunchkinCard[],
    event: MatCheckboxChange
  ) {
    event.checked
      ? collection.push(card)
      : collection.splice(collection.indexOf(card), 1);
  }
}
