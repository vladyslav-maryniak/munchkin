import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface MetaDialogData {
  key: string;
  value: string;
}

@Component({
  selector: 'app-metadata-dialog',
  templateUrl: './metadata-dialog.component.html',
  styleUrls: ['./metadata-dialog.component.css'],
})
export class MetadataDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<MetadataDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: MetaDialogData
  ) {}

  onSubmit(): void {
    this.dialogRef.close(this.data.value);
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  get title(): string {
    return this.data.key.replace(/([a-z0-9])([A-Z])/g, '$1 $2');
  }
}
