import { Component, inject, OnInit, signal } from '@angular/core';
import { LucideCircleX, LucideWaypoints } from '@lucide/angular';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { BranchStore } from '../../../branch/state/branch.store';
import { UpdateDepartmentDtoModel } from '../../model/update-department-dto.model';
import { form, FormField } from '@angular/forms/signals';
import { toast } from '@spartan-ng/brain/sonner';
import { DepartmentStore } from '../../state/department.store';

@Component({
  selector: 'app-department-update',
  imports: [LucideWaypoints, LucideCircleX, FormField],
  templateUrl: './department-update.component.html',
  styleUrl: './department-update.component.scss',
})
export class DepartmentUpdateComponent implements OnInit {
  //====================================
  //===========inject===================
  //====================================
  protected ref = inject(DynamicDialogRef);
  protected branchStore = inject(BranchStore);
  private departmentStore = inject(DepartmentStore);

  //====================================
  //===========form=====================
  //====================================
  protected departmentModel = signal({
    name: '',
    description: '',
    branchId: '',
    isActive: 'true',
  });

  protected departmentForm = form(this.departmentModel);

  //====================================
  //===========life cycle===============
  //====================================
  async ngOnInit() {
    await this.branchStore.loadBranch({});
    const selected = this.departmentStore.departmentSelected();
    if (selected) {
      const branch = this.branchStore.branches().find((b) => b.name === selected.branchName);
      const branchId = branch ? String(branch.id) : '';

      this.departmentModel.set({
        name: selected.name,
        description: selected.description || '',
        branchId: branchId,
        isActive: selected.isActive ? 'true' : 'false',
      });
    }
  }

  //====================================
  //===========method===================
  //====================================
  closeDialog() {
    this.ref.close();
  }

  async onSubmit(event: Event) {
    event.preventDefault();
    if (this.departmentForm().invalid()) {
      toast.error('بيانات القسم غير صالحة');
      return;
    }
    const selected = this.departmentStore.departmentSelected();
    if (!selected) {
      toast.error('لم يتم تحديد قسم لتعديله');
      return;
    }

    const formValue = this.departmentModel();

    const payload: UpdateDepartmentDtoModel = {
      name: formValue.name,
      description: formValue.description,
      branchId: Number(formValue.branchId),
      isActive: formValue.isActive === 'true',
    };

    const isUpdated = await this.departmentStore.updateDepartment(selected.id, payload);
    if (isUpdated) {
      toast.success('تم تعديل القسم بنجاح');
      this.closeDialog();
    } else {
      toast.error(this.departmentStore.error() || 'حدث خطأ أثناء تعديل القسم');
    }
  }
}
