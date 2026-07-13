import { Component, inject } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { toast } from '@spartan-ng/brain/sonner';
import { RoomStore } from '../../state/room.store';

@Component({
  selector: 'app-room-delete',
  standalone: true,
  templateUrl: './room-delete.component.html',
})
export class RoomDeleteComponent {
  protected ref = inject(DynamicDialogRef);
  private config = inject(DynamicDialogConfig);
  private roomStore = inject(RoomStore);

  get roomId(): number {
    return this.config.data.id;
  }

  get roomNumber(): string {
    return this.config.data.roomNumber;
  }

  cancel() {
    this.ref.close();
  }

  confirm() {
    try {
      this.roomStore.DeleteRoom(this.roomId);
      toast.success('تم حذف الغرفة بنجاح');
    } catch (error) {
      toast.error('حدث خطأ أثناء حذف الغرفة');
    }
    this.ref.close();
  }
}
