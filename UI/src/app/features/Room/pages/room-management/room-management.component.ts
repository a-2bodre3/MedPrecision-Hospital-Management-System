import { Component, inject, signal } from '@angular/core';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButton } from '@spartan-ng/helm/button';
import { LucideBed } from '@lucide/angular';
import { DialogService } from 'primeng/dynamicdialog';
import { RoomCreateComponent } from '../../components/room-create/room-create.component';
import { RoomListComponent } from '../../components/room-list/room-list.component';
import { RoomType } from '../../../../core/enum/RoomType.enum';

@Component({
  selector: 'app-room-management',
  imports: [HeaderComponent, HlmButton, LucideBed, RoomListComponent],
  templateUrl: './room-management.component.html',
  styleUrl: './room-management.component.scss',
})
export class RoomManagementComponent {
  public dialogService = inject(DialogService);

  protected selectedRoomType = signal<RoomType | null>(null);

  protected roomTypes = [
    { value: RoomType.Clinic, label: 'عيادة خارجية' },
    { value: RoomType.Ward, label: 'عنبر / غرفة إقامة مرضى' },
    { value: RoomType.OperationRoom, label: 'غرفة عمليات' },
    { value: RoomType.Office, label: 'مكتب إداري' },
    { value: RoomType.Laboratory, label: 'معمل تحاليل أو أشعة' },
  ];

  showDialogCreate() {
    this.dialogService.open(RoomCreateComponent, {
      width: '560px',
      closable: false,
      showHeader: false,
    });
  }

  onRoomTypeFilterChange(event: Event) {
    const value = (event.target as HTMLSelectElement).value;
    this.selectedRoomType.set(value ? (value as RoomType) : null);
  }
}
