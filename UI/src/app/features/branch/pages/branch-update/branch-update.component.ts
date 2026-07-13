import { Component, inject } from '@angular/core';
import { BranchFormComponent } from '../../components/branch-form/branch-form.component';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { BranchStore } from '../../state/branch.store';
import { toast } from '@spartan-ng/brain/sonner';
import { CreateBranchRequest, UpdateBranchRequest } from '../../model/branch-request.model';

@Component({
  selector: 'app-branch-update',
  imports: [BranchFormComponent],
  templateUrl: './branch-update.component.html',
  styleUrl: './branch-update.component.scss',
})
export class BranchUpdateComponent {
  private branchStore = inject(BranchStore);
  private ref = inject(DynamicDialogRef);

  handleUpdate(formData: CreateBranchRequest | UpdateBranchRequest) {
    const id = this.branchStore.branchDetails()?.id;
    if (!id) {
      return;
    }
    toast.success('تم تعديل الفرع بنجاح');
    this.branchStore.updateBranch({ id, data: formData as UpdateBranchRequest });
    this.ref.close();
  }
}
