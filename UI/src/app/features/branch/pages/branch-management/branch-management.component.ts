import { Component, inject, OnInit } from '@angular/core';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButtonImports } from '@spartan-ng/helm/button';
import { LucideGitBranchPlus } from '@lucide/angular';
import { BranchListComponent } from '../branch-list/branch-list.component';
import { BranchStore } from '../../state/branch.store';
import { DialogService } from 'primeng/dynamicdialog';
import { BranchCreateComponent } from '../branch-create/branch-create.component';

@Component({
  selector: 'app-branch-management',
  imports: [HeaderComponent, HlmButtonImports, LucideGitBranchPlus, BranchListComponent],
  templateUrl: './branch-management.component.html',
  styleUrl: './branch-management.component.scss',
})
export class BranchManagementComponent {
  //=========================================
  //==============inject=====================
  //=========================================
  public dialogService = inject(DialogService);

  //=========================================
  //==============method=====================
  //=========================================
  showDialogCreate() {
    this.dialogService.open(BranchCreateComponent, {
      width: '500px',
      closable: false,
      showHeader: false,
    });
  }
}
