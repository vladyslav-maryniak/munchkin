import { Component, Inject } from '@angular/core';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MunchkinCard } from 'src/app/models/munchkin-card';

export interface BribeDialogData {
  agreement: string;
  inHandCards: MunchkinCard[];
  selectedCards: MunchkinCard[];
}

@Component({
  selector: 'app-bribe-dialog',
  templateUrl: './bribe-dialog.component.html',
  styleUrls: ['./bribe-dialog.component.css'],
})
export class BribeDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<BribeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: BribeDialogData
  ) {}

  onChange(card: MunchkinCard, event: MatCheckboxChange) {
    if (event.checked) {
      this.data.selectedCards.push(card);
    } else {
      const index = this.data.selectedCards.indexOf(card);
      this.data.selectedCards.splice(index, 1);
    }
  }

  onConfirm(): void {
    this.dialogRef.close(this.data);
  }
}
