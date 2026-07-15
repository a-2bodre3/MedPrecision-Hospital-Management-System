import { Component, inject, OnInit } from '@angular/core';
import {
  LucideEye,
  LucidePencil,
  LucideTrash2,
  LucideUserRound,
  LucideChevronLeft,
  LucideChevronRight,
} from '@lucide/angular';
import { Router } from '@angular/router';
import { EmployeeStore } from '../../store/employee.store';

@Component({
  selector: 'app-employee-table',
  standalone: true,
  imports: [
    LucideEye,
    LucidePencil,
    LucideTrash2,
    LucideUserRound,
    LucideChevronLeft,
    LucideChevronRight,
  ],
  templateUrl: './employee-table.component.html',
})
export class EmployeeTableComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected employeeStore = inject(EmployeeStore);
  private router = inject(Router);

  //=========================================
  //==============life cycle=================
  //=========================================
  ngOnInit() {
    this.fetchEmployees();
  }

  //=========================================
  //==============computed helpers===========
  //=========================================
  get totalPages() {
    const query = this.employeeStore.queryParameters();
    return Math.max(Math.ceil(this.employeeStore.totalCount() / query.pageSize), 1);
  }

  get startRecord() {
    const query = this.employeeStore.queryParameters();
    if (!this.employeeStore.totalCount()) {
      return 0;
    }
    return (query.pageNumber - 1) * query.pageSize + 1;
  }

  get endRecord() {
    const query = this.employeeStore.queryParameters();
    return Math.min(query.pageNumber * query.pageSize, this.employeeStore.totalCount());
  }

  //=========================================
  //==============method=====================
  //=========================================
  fetchEmployees() {
    this.employeeStore.loadEmployees({
      params: this.employeeStore.queryParameters(),
    });
  }

  goToDetails(id: number, jopTitle: string) {
    if (jopTitle === 'دكتور') {
      this.router.navigate(['/staff/doctor', id]);
    } else {
      this.router.navigate(['/staff/employee', id]);
    }
  }

  goToEdit(id: number, jopTitle: string) {
    if (jopTitle === 'دكتور') {
      this.router.navigate(['/staff/doctor', id, 'edit']);
    } else {
      this.employeeStore.loadEmployeeById(id);
      this.router.navigate(['/staff/employee', id, 'edit']);
    }
  }

  changePage(pageNumber: number) {
    if (pageNumber < 1 || pageNumber > this.totalPages) {
      return;
    }

    this.employeeStore.updateQueryParameters({ pageNumber });
    this.fetchEmployees();
  }

  changePageSize(event: Event) {
    const pageSize = Number((event.target as HTMLSelectElement).value);
    this.employeeStore.updateQueryParameters({ pageSize, pageNumber: 1 });
    this.fetchEmployees();
  }

  deleteEmployee(id: number, fullName: string) {
    const confirmed = window.confirm(`هل أنت متأكد من حذف الموظف "${fullName}"؟`);
    if (confirmed) {
      this.employeeStore.deleteEmployee(id);
    }
  }
}
