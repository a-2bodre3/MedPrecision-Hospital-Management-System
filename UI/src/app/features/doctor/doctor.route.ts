import { Routes } from '@angular/router';

export const DOCTOR_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./pages/doctor-management/doctor-management.component').then(
        (m) => m.DoctorManagementComponent,
      ),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./pages/doctor-create/doctor-create.component').then((m) => m.DoctorCreateComponent),
  },
  {
    path: ':id/edit',
    loadComponent: () =>
      import('./pages/doctor-edit/doctor-edit.component').then((m) => m.DoctorEditComponent),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./pages/doctor-details/doctor-details.component').then(
        (m) => m.DoctorDetailsComponent,
      ),
  },
];
