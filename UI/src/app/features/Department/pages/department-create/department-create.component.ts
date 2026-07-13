import { Component, inject, OnInit, signal } from '@angular/core';
import { LucideCircleX, LucideWaypoints } from '@lucide/angular';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { BranchStore } from '../../../branch/state/branch.store';
import { CreateDepartmentDtoModel } from '../../model/create-department-dto.model';
import { form, FormField } from '@angular/forms/signals';
import { toast } from '@spartan-ng/brain/sonner';
import { DepartmentStore } from '../../state/department.store';

@Component({
  selector: 'app-department-create',
  imports: [LucideWaypoints, LucideCircleX, FormField],
  templateUrl: './department-create.component.html',
  styleUrl: './department-create.component.scss',
})
export class DepartmentCreateComponent implements OnInit {
  //====================================
  //===========inject===================
  //====================================
  protected ref = inject(DynamicDialogRef);
  protected branchStore = inject(BranchStore);
  private departmentStore = inject(DepartmentStore);

  //====================================
  //===========life cycle===============
  //====================================
  ngOnInit(): void {
    this.branchStore.loadBranch({});
  }
  //====================================
  //===========form=====================
  //====================================
  protected departmentModel = signal({
    name: '',
    description: '',
    branchId: '',
  });

  protected departmentForm = form(this.departmentModel);
  //====================================
  //===========method===================
  //====================================
  closeDialog() {
    this.ref.close();
  }
  async onSubmit(event: Event) {
    event.preventDefault();
    if (this.departmentForm().invalid()) {
      toast.error('invalid department');
      return;
    }
    const formValue = this.departmentModel();

    const payload: CreateDepartmentDtoModel = {
      name: formValue.name,
      description: formValue.description,
      branchId: Number(formValue.branchId),
    };

    const isCreated = await this.departmentStore.createDepartment(payload);
    if (isCreated) {
      toast.success('تم انشاء القسم بنجاح');
      this.closeDialog();
    } else {
      toast.error(this.departmentStore.error() || 'حدث خطأ أثناء إضافة القسم');
    }
  }
}
