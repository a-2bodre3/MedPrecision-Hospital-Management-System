import { Component, inject, OnInit } from '@angular/core';
import { LucideEye, LucideHospital, LucidePencil, LucideTrash2 } from '@lucide/angular';
import { BranchStore } from '../../state/branch.store';
import { BranchDetailsComponent } from '../branch-details/branch-details.component';
import { DialogService } from 'primeng/dynamicdialog';
import { BranchUpdateComponent } from '../branch-update/branch-update.component';
import { BranchDeleteComponent } from '../../components/branch-delete/branch-delete.component';

@Component({
  selector: 'app-branch-list',
  imports: [LucideHospital, LucideEye, LucidePencil, LucideTrash2],
  templateUrl: './branch-list.component.html',
  styleUrl: './branch-list.component.scss',
})
export class BranchListComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected branchStore = inject(BranchStore);
  public dialogService = inject(DialogService);

  //=========================================
  //==============life cycle=================
  //=========================================
  async ngOnInit() {
    await this.branchStore.loadBranch({});
  }

  //=========================================
  //==============method=====================
  //=========================================
  showDialogDetails(id: number) {
    this.branchStore.loadBranchById(id);
    this.dialogService.open(BranchDetailsComponent, {
      width: '500px',
      closable: false,
      showHeader: false,
    });
  }

  async showDialogEdit(id: number) {
    await this.branchStore.loadBranchById(id);
    this.dialogService.open(BranchUpdateComponent, {
      width: '500px',
      closable: false,
      showHeader: false,
    });
  }

  showDeleteConfirmation(id: number, name: string) {
    this.dialogService.open(BranchDeleteComponent, {
      width: '400px',
      closable: false,
      showHeader: false,
      data: { id, name },
    });
  }
}
