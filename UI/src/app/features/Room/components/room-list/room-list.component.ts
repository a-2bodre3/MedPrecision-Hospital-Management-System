import { Component, computed, inject, input, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { RoomStore } from '../../state/room.store';
import { LucideEye, LucideHospital, LucidePencil, LucideTrash2 } from '@lucide/angular';
import { RoomUpdateComponent } from '../room-update/room-update.component';
import { RoomDeleteComponent } from '../room-delete/room-delete.component';
import { RoomViewComponent } from '../room-view/room-view.component';
import { getRoomTypeLabel } from '../../model/room-type-options';
import { RoomType } from '../../../../core/enum/RoomType.enum';

@Component({
  selector: 'app-room-list',
  standalone: true,
  imports: [LucideHospital, LucidePencil, LucideTrash2, LucideEye],
  templateUrl: './room-list.component.html',
})
export class RoomListComponent implements OnInit {
  public dialogService = inject(DialogService);
  protected roomStore = inject(RoomStore);
  protected getRoomTypeLabel = getRoomTypeLabel;

  roomType = input<RoomType | null>(null);

  filteredRooms = computed(() => {
    let list = this.roomStore.rooms();
    const type = this.roomType();

    if (type) {
      list = list.filter((room) => room.roomType === type);
    }

    return list;
  });

  ngOnInit() {
    this.roomStore.loadRooms();
  }

  showDialogView(id: number) {
    this.roomStore.GetRoomDetails(id);
    this.dialogService.open(RoomViewComponent, {
      width: '600px',
      closable: false,
      showHeader: false,
    });
  }

  showDialogEdit(id: number) {
    this.roomStore.GetRoomDetails(id);
    this.dialogService.open(RoomUpdateComponent, {
      width: '600px',
      closable: false,
      showHeader: false,
    });
  }

  showDeleteConfirmation(id: number, roomNumber: string) {
    this.dialogService.open(RoomDeleteComponent, {
      width: '400px',
      closable: false,
      showHeader: false,
      data: { id, roomNumber },
    });
  }
}
