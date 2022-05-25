import { Component, Input } from '@angular/core';
import { Equipment } from 'src/app/models/equipment';
import { Game } from 'src/app/models/game';
import { ItemCard } from 'src/app/models/item-card';
import { Place } from 'src/app/models/place';
import { Player } from 'src/app/models/player';

@Component({
  selector: 'app-player-sidebar',
  templateUrl: './player-sidebar.component.html',
  styleUrls: ['./player-sidebar.component.css'],
})
export class PlayerSidebarComponent {
  @Input() game!: Game;

  get places(): Place[] {
    return this.game.table.places;
  }

  isPlayerTurn(player: Player): boolean {
    const index = this.game.turnIndex % this.game.table.places.length;
    return this.game.table.places[index].player.id == player.id;
  }

  getNicknameLabel(player: Player): string {
    return player.nickname.trim().charAt(0).toUpperCase();
  }

  getEquipmentDescription(e: Equipment): string {
    const total =
      (e.headgear?.bonus ?? 0) +
      (e.armor?.bonus ?? 0) +
      (e.footgear?.bonus ?? 0) +
      (e.leftHand?.bonus ?? 0) +
      (e.leftHand?.id != e.rightHand?.id ? e.rightHand?.bonus ?? 0 : 0);

    const description: string[] = [
      `Headgear: ${this.getItemDescription(e.headgear)}`,
      `Armor: ${this.getItemDescription(e.armor)}`,
      `Footgear: ${this.getItemDescription(e.footgear)}`,
      `Left hand: ${this.getItemDescription(e.leftHand)}`,
      `Right hand: ${this.getItemDescription(e.rightHand)}`,
      `Total: +${total}`,
    ];

    return description.join('\n');
  }

  private getItemDescription(card: ItemCard): string {
    return card ? `${card.name} +${card.bonus}` : 'none';
  }
}
