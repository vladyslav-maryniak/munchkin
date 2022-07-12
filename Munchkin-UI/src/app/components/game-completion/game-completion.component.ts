import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';

export interface GameCompletionDialogData {
  title: string;
  text: string;
}

@Component({
  selector: 'app-game-completion',
  templateUrl: './game-completion.component.html',
  styleUrls: ['./game-completion.component.css'],
})
export class GameCompletionComponent {
  constructor(
    public dialogRef: MatDialogRef<GameCompletionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: GameCompletionDialogData,
    private router: Router
  ) {}

  async goHome() {
    await this.router.navigate(['home']);
    this.dialogRef.close();
  }
}
