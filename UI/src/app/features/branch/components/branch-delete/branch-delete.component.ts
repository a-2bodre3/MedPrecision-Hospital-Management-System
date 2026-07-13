import { Component, inject } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { toast } from '@spartan-ng/brain/sonner';
import { BranchStore } from '../../state/branch.store';

@Component({
  selector: 'app-branch-delete',
  standalone: true,
  imports: [],
  templateUrl: './branch-delete.component.html',
  styleUrl: './branch-delete.component.scss',
})
export class BranchDeleteComponent {
  protected ref = inject(DynamicDialogRef);
  private config = inject(DynamicDialogConfig);
  private branchStore = inject(BranchStore);

  get branchId(): number {
    return this.config.data.id;
  }

  get branchName(): string {
    return this.config.data.name;
  }

  cancel() {
    this.ref.close();
  }

  confirm() {
    this.branchStore.deleteBranch(this.branchId);
    toast.success('تم حذف الفرع بنجاح');
    this.ref.close();
  }
}
