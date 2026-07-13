import { Component, computed, inject, input, OnInit } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { DepartmentStore } from '../../state/department.store';
import { LucideHospital, LucidePencil, LucideTrash2 } from '@lucide/angular';
import { DepartmentUpdateComponent } from '../department-update/department-update.component';
import { DeleteCompoundComponent } from '../../components/delete-compound/delete-compound.component';

@Component({
  selector: 'app-department-list',
  imports: [LucideHospital, LucidePencil, LucideTrash2],
  templateUrl: './department-list.component.html',
  styleUrl: './department-list.component.scss',
})
export class DepartmentListComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  public dialogService = inject(DialogService);
  protected departmentStore = inject(DepartmentStore);
  branchName = input<string | null>(null);
  protected filteredDepartments = computed(() => {
    const branchName = this.branchName();
    if (!branchName) {
      return this.departmentStore.departments();
    }

    return this.departmentStore.departments().filter((department) => department.branchName === branchName);
  });

  //=========================================
  //==============life cycle=================
  //=========================================
  async ngOnInit() {
    await this.departmentStore.fetchDepartments();
  }

  //=========================================
  //==============method=====================
  //=========================================
  async showDialogEdit(id: number) {
    this.departmentStore.loadDepartmentById(id);
    this.dialogService.open(DepartmentUpdateComponent, {
      width: '500px',
      closable: false,
      showHeader: false,
    });
  }

  showDeleteConfirmation(id: number, name: string) {
    this.dialogService.open(DeleteCompoundComponent, {
      width: '400px',
      closable: false,
      showHeader: false,
      data: { id, name }
    });
  }
}
