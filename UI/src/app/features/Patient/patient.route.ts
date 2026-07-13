import { Routes } from '@angular/router';

export const PATIENT_ROUTES: Routes = [
  // {
  //   path: 'add',
  //   loadComponent: () =>
  //     import('./pages/patient-create/patient-create.component').then(
  //       (m) => m.PatientCreateComponent,
  //     ),
  // },
  {
    path: 'list',
    loadComponent: () =>
      import('./pages/patient-list/patient-list.component').then((m) => m.PatientListComponent),
  },
  // {
  //   path: ':id/edit',
  //   loadComponent: () =>
  //     import('./pages/patient-edit/patient-edit.component').then((m) => m.PatientEditComponent),
  // },
  {
    path: ':id',
    loadComponent: () =>
      import('./pages/patient-details/patient-details.component').then(
        (m) => m.PatientDetailsComponent,
      ),
  },
];
