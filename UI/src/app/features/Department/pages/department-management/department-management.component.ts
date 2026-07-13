import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { DepartmentCreateComponent } from '../department-create/department-create.component';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButton } from '@spartan-ng/helm/button';
import { LucideWaypoints } from '@lucide/angular';
import { DepartmentListComponent } from '../department-list/department-list.component';
import { BranchStore } from '../../../branch/state/branch.store';
import { DepartmentStore } from '../../state/department.store';

@Component({
  selector: 'app-department-management',
  imports: [HeaderComponent, HlmButton, LucideWaypoints, DepartmentListComponent],
  templateUrl: './department-management.component.html',
  styleUrl: './department-management.component.scss',
})
export class DepartmentManagementComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  public dialogService = inject(DialogService);
  protected branchStore = inject(BranchStore);
  protected selectedBranchId = signal<number | null>(null);
  protected selectedBranchName = computed(() => {
    const selectedBranchId = this.selectedBranchId();
    if (!selectedBranchId) {
      return null;
    }

    return this.branchStore.branches().find((branch) => branch.id === selectedBranchId)?.name ?? null;
  });

  //=========================================
  //==============life cycle=================
  //=========================================
  ngOnInit() {
    this.branchStore.loadBranch({});
  }

  //=========================================
  //==============method=====================
  //=========================================
  showDialogCreate() {
    this.dialogService.open(DepartmentCreateComponent, {
      width: '500px',
      closable: false,
      showHeader: false,
    });
  }

  onBranchFilterChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const value = target.value;
    this.selectedBranchId.set(value ? Number(value) : null);
  }
}
