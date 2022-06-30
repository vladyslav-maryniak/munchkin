import { Component, Inject, OnInit } from '@angular/core';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Equipment } from 'src/app/models/equipment';
import { ItemCard } from 'src/app/models/item-card';

export interface TradeDialogData {
  offerorEquipment: Equipment;
  offereeEquipment: Equipment;
  demand: ItemCard[];
  supply: ItemCard[];
}

@Component({
  selector: 'app-trade-dialog',
  templateUrl: './trade-dialog.component.html',
  styleUrls: ['./trade-dialog.component.css'],
})
export class TradeDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<TradeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TradeDialogData
  ) {}

  get offerorEquipment(): ItemCard[] {
    return this.toArray(this.data.offerorEquipment);
  }

  get offereeEquipment(): ItemCard[] {
    return this.toArray(this.data.offereeEquipment);
  }

  private toArray(equipment: Equipment): ItemCard[] {
    const array = [
      equipment.headgear,
      equipment.armor,
      equipment.footgear,
      equipment.leftHand,
    ];
    if (equipment.rightHand?.id !== equipment.leftHand?.id) {
      array.push(equipment.rightHand);
    }
    return array.filter((x) => x !== null);
  }

  onConfirm(): void {
    this.dialogRef.close(this.data);
  }

  onSupplyChange(card: ItemCard, event: MatCheckboxChange) {
    this.onChange(card, this.data.supply, event);
  }

  onDemandChange(card: ItemCard, event: MatCheckboxChange) {
    this.onChange(card, this.data.demand, event);
  }

  private onChange(
    card: ItemCard,
    collection: ItemCard[],
    event: MatCheckboxChange
  ) {
    event.checked
      ? collection.push(card)
      : collection.splice(collection.indexOf(card), 1);
  }
}
