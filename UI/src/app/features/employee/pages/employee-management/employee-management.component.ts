// import { Component, inject, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { HeaderComponent } from '../../../../shared/components/header/header.component';
// import { HlmButton } from '@spartan-ng/helm/button';
// import { LucideStethoscope, LucideUserPlus } from '@lucide/angular';
// import { EmployeeTableComponent } from '../../components/employee-table/employee-table.component';
// import { EmployeeStore } from '../../store/employee.store';
// import { DepartmentStore } from '../../../Department/state/department.store';
// import { RoleStore } from '../../../Role&Permission/state/role.store';
// import { toast } from '@spartan-ng/brain/sonner';
//
// @Component({
//   selector: 'app-employee-management',
//   standalone: true,
//   imports: [HeaderComponent, HlmButton, LucideUserPlus, LucideStethoscope, EmployeeTableComponent],
//   templateUrl: './employee-management.component.html',
// })
// export class EmployeeManagementComponent implements OnInit {
//   //=========================================
//   //==============inject=====================
//   //=========================================
//   protected employeeStore = inject(EmployeeStore);
//   protected departmentStore = inject(DepartmentStore);
//   protected roleStore = inject(RoleStore);
//   private router = inject(Router);
//
//   //=========================================
//   //==============life cycle=================
//   //=========================================
//   ngOnInit() {
//     this.departmentStore.fetchDepartments();
//     this.roleStore.loadRoles();
//   }
//
//   //=========================================
//   //==============method=====================
//   //=========================================
//   goToCreate() {
//     this.router.navigate(['/staff/employee/create']);
//   }
//
//   addDoctorPlaceholder() {
//     this.router.navigate(['/staff/doctor/create']);
//   }
//
//   onSearchChange(event: Event) {
//     const searchTerm = (event.target as HTMLInputElement).value;
//     this.employeeStore.updateQueryParameters({ searchTerm, pageNumber: 1 });
//     this.fetchEmployees();
//   }
//
//   onRoleChange(event: Event) {
//     const value = (event.target as HTMLSelectElement).value;
//     this.employeeStore.updateQueryParameters({
//       roleId: value ? Number(value) : null,
//       pageNumber: 1,
//     });
//     this.fetchEmployees();
//   }
//
//   onDepartmentChange(event: Event) {
//     const value = (event.target as HTMLSelectElement).value;
//     this.employeeStore.updateQueryParameters({
//       departmentId: value ? Number(value) : null,
//       pageNumber: 1,
//     });
//     this.fetchEmployees();
//   }
//
//   onIncludeInactiveChange(event: Event) {
//     const includeInactive = (event.target as HTMLInputElement).checked;
//     this.employeeStore.loadEmployees({
//       params: { ...this.employeeStore.queryParameters(), pageNumber: 1 },
//       includeInactive,
//     });
//   }
//
//   private fetchEmployees() {
//     this.employeeStore.loadEmployees({
//       params: this.employeeStore.queryParameters(),
//       includeInactive: this.employeeStore.includeInactive(),
//     });
//   }
// }
