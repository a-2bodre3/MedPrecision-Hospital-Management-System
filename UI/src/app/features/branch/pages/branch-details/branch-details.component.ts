import { Component, inject } from '@angular/core';

import { LucideCircleX, LucideHospital } from '@lucide/angular';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { BranchStore } from '../../state/branch.store';
import { HlmEmptyImports } from '@spartan-ng/helm/empty';
import { HlmSpinnerImports } from '@spartan-ng/helm/spinner';

@Component({
  selector: 'app-branch-details',
  imports: [LucideHospital, LucideCircleX, HlmEmptyImports, HlmSpinnerImports],
  templateUrl: './branch-details.component.html',
  styleUrl: './branch-details.component.scss',
})
export class BranchDetailsComponent {
  protected ref = inject(DynamicDialogRef);
  protected branchStore = inject(BranchStore);
  closeDialog() {
    this.ref.close();
  }
}
