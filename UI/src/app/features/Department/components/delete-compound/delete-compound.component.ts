import { Component, inject } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DepartmentStore } from '../../state/department.store';
import { toast } from '@spartan-ng/brain/sonner';

@Component({
  selector: 'app-delete-compound',
  standalone: true,
  imports: [],
  templateUrl: './delete-compound.component.html',
  styleUrl: './delete-compound.component.scss',
})
export class DeleteCompoundComponent {
  protected ref = inject(DynamicDialogRef);
  private config = inject(DynamicDialogConfig);
  private departmentStore = inject(DepartmentStore);

  get departmentId(): number {
    return this.config.data.id;
  }

  get departmentName(): string {
    return this.config.data.name;
  }

  cancel() {
    this.ref.close();
  }

  async confirm() {
    const isDeleted = await this.departmentStore.deleteDepartment(this.departmentId);
    if (isDeleted) {
      toast.success('تم حذف القسم بنجاح');
      this.ref.close();
    } else {
      toast.error(this.departmentStore.error() || 'حدث خطأ أثناء حذف القسم');
    }
  }
}
