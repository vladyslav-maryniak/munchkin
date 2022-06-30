import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Equipment } from '../models/equipment';
import { ItemCard } from '../models/item-card';
import { MunchkinCard } from '../models/munchkin-card';
import { Player } from '../models/player';

@Injectable({
  providedIn: 'root',
})
export class CardService {
  private hostUrl = environment.hostUrl;

  getCardDescription(card: MunchkinCard): string {
    const description: string[] = [];
    if (card.description) {
      description.push(card.description);
    }
    if (card.badStuff) {
      description.push(`Bad stuff: ${card.badStuff}`);
    }
    if (card.goldPieces) {
      description.push(`${card.goldPieces} Gold Pieces`);
    }
    return description.join('\n');
  }

  getCharacterEquipmentDescription(equipment: Equipment): string {
    const total =
      (equipment.headgear?.bonus ?? 0) +
      (equipment.armor?.bonus ?? 0) +
      (equipment.footgear?.bonus ?? 0) +
      (equipment.leftHand?.bonus ?? 0) +
      (equipment.leftHand?.id !== equipment.rightHand?.id
        ? equipment.rightHand?.bonus ?? 0
        : 0);

    const description: string[] = [
      `Headgear: ${this.getItemCardDescription(equipment.headgear)}`,
      `Armor: ${this.getItemCardDescription(equipment.armor)}`,
      `Footgear: ${this.getItemCardDescription(equipment.footgear)}`,
      `Left hand: ${this.getItemCardDescription(equipment.leftHand)}`,
      `Right hand: ${this.getItemCardDescription(equipment.rightHand)}`,
      `Total: +${total}`,
    ];

    return description.join('\n');
  }

  getItemCardDescription(card: ItemCard): string {
    return card ? `${card.name} +${card.bonus}` : 'none';
  }

  getNicknameAbbreviation(player: Player): string {
    return player.nickname.trim().charAt(0).toUpperCase() ?? '';
  }

  getCardImageSrc(card: MunchkinCard): string {
    const fileName = this.getImageFileName(card);
    return `${this.hostUrl}/api/images/${fileName}`;
  }

  getImageSrc(fileName: string): string {
    return `${this.hostUrl}/api/images/${fileName}`;
  }

  getImageFileName(card: MunchkinCard): string {
    return card.name.toLowerCase().split(' ').join('_') + '.avif';
  }

  isTreasureCard(card: MunchkinCard): boolean {
    return card.goldPieces !== undefined;
  }

  isItemCard(card: MunchkinCard): boolean {
    return card.goldPieces !== undefined && card.bonus !== undefined;
  }
}
