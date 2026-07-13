import { Component, inject, OnInit, signal } from '@angular/core';
import { LucideCircleX, LucideWaypoints } from '@lucide/angular';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { BranchStore } from '../../../branch/state/branch.store';
import { DepartmentStore } from '../../../Department/state/department.store';
import { RoomStore } from '../../state/room.store';
import { CreateRoomCommand } from '../../model/RoomCommand.model';
import { form, FormField } from '@angular/forms/signals';
import { toast } from '@spartan-ng/brain/sonner';
import { ROOM_TYPE_OPTIONS } from '../../model/room-type-options';
import { RoomType } from '../../../../core/enum/RoomType.enum';

@Component({
  selector: 'app-room-create',
  standalone: true,
  imports: [LucideWaypoints, LucideCircleX, FormField],
  templateUrl: './room-create.component.html',
})
export class RoomCreateComponent implements OnInit {
  protected ref = inject(DynamicDialogRef);
  protected branchStore = inject(BranchStore);
  protected departmentStore = inject(DepartmentStore);
  private roomStore = inject(RoomStore);
  protected roomTypeOptions = ROOM_TYPE_OPTIONS;

  protected roomModel = signal({
    roomNumber: '',
    floor: '',
    roomType: '1',
    branchId: '',
    departmentId: '',
    capacity: '1',
  });

  protected roomForm = form(this.roomModel);

  async ngOnInit() {
    await this.branchStore.loadBranch({});
    await this.departmentStore.fetchDepartments();
  }

  getFilteredDepartments() {
    const branchIdStr = this.roomModel().branchId;
    if (!branchIdStr) {
      return this.departmentStore.departments();
    }
    const branch = this.branchStore.branches().find((b) => b.id === Number(branchIdStr));
    if (!branch) {
      return this.departmentStore.departments();
    }
    return this.departmentStore.departments().filter((d) => d.branchName === branch.name);
  }

  closeDialog() {
    this.ref.close();
  }

  onBranchChange() {
    this.roomModel.update((model) => ({
      ...model,
      departmentId: '',
    }));
  }

  onSubmit(event: Event) {
    event.preventDefault();
    if (this.roomForm().invalid()) {
      toast.error('الرجاء تعبئة كافة الحقول المطلوبة بشكل صحيح');
      return;
    }
    const formValue = this.roomModel();

    const payload: CreateRoomCommand = {
      roomNumber: formValue.roomNumber,
      floor: Number(formValue.floor),
      roomType: formValue.roomType as RoomType,
      branchId: Number(formValue.branchId),
      departmentId: Number(formValue.departmentId),
      capacity: Number(formValue.capacity),
    };

    this.roomStore.CreateRoom(payload);
    toast.success('تم إنشاء الغرفة بنجاح');
    this.closeDialog();
  }
}
