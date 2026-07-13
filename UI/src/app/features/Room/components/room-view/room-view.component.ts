import { Component, inject } from '@angular/core';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { RoomStore } from '../../state/room.store';
import { getRoomTypeLabel } from '../../model/room-type-options';

@Component({
  selector: 'app-room-view',
  standalone: true,
  templateUrl: './room-view.component.html',
  styleUrl: './room-view.component.scss',
})
export class RoomViewComponent {
  protected ref = inject(DynamicDialogRef);
  protected roomStore = inject(RoomStore);
  protected getRoomTypeLabel = getRoomTypeLabel;

  closeDialog() {
    this.ref.close();
    this.roomStore.removeRoomDetails();
  }
}
