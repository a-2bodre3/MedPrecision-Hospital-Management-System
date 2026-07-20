import { Routes } from '@angular/router';

export const EMPLOYEE_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/employee-management/employee-management.component').then(
        (m) => m.EmployeeManagementComponent,
      ),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./pages/employee-create/employee-create.component').then(
        (m) => m.EmployeeCreateComponent,
      ),
  },
  {
    path: ':id/edit',
    loadComponent: () =>
      import('./pages/employee-edit/employee-edit.component').then((m) => m.EmployeeEditComponent),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./pages/employee-details/employee-details.component').then(
        (m) => m.EmployeeDetailsComponent,
      ),
  },
];
