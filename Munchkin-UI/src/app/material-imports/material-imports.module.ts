import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatChipsModule } from '@angular/material/chips';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDialogModule } from '@angular/material/dialog';
import { OverlayModule } from '@angular/cdk/overlay';
import { MatStepperModule } from '@angular/material/stepper';
import { MatRadioModule } from '@angular/material/radio';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatButtonModule,
    MatInputModule,
    MatChipsModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatListModule,
    MatIconModule,
    MatBadgeModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatDialogModule,
    OverlayModule,
    MatStepperModule,
    MatRadioModule,
  ],
  exports: [
    MatButtonModule,
    MatInputModule,
    MatChipsModule,
    MatToolbarModule,
    MatCheckboxModule,
    MatListModule,
    MatIconModule,
    MatBadgeModule,
    MatTooltipModule,
    MatSnackBarModule,
    MatDialogModule,
    OverlayModule,
    MatStepperModule,
    MatRadioModule,
  ],
})
export class MaterialImportsModule {}
