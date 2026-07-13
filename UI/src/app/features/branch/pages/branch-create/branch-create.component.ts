import { Component, inject } from '@angular/core';
import { BranchFormComponent } from '../../components/branch-form/branch-form.component';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { BranchStore } from '../../state/branch.store';
import { CreateBranchRequest, UpdateBranchRequest } from '../../model/branch-request.model';

@Component({
  selector: 'app-branch-create',
  imports: [BranchFormComponent],
  templateUrl: './branch-create.component.html',
  styleUrl: './branch-create.component.scss',
})
export class BranchCreateComponent {
  private branchStore = inject(BranchStore);
  private ref = inject(DynamicDialogRef);

  handleUpdate(formData: CreateBranchRequest | UpdateBranchRequest) {
    this.branchStore.createBranch(formData as CreateBranchRequest);
    this.ref.close();
  }
}
