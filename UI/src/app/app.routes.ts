import { Routes } from '@angular/router';
import { guestGuard } from './core/guards/guest.guard';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/auth',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    loadComponent: () =>
      import('./layout/auth-layout/auth-layout.component').then((m) => m.AuthLayoutComponent),
    canActivate: [guestGuard],
    children: [
      {
        path: '',
        loadChildren: () => import('./features/auth/auth-routing.route').then((m) => m.AUTH_ROUTES),
      },
    ],
  },
  {
    path: 'staff',
    loadComponent: () =>
      import('./layout/staff-layout/staff-layout.component').then((m) => m.StaffLayoutComponent),
    canActivate: [authGuard, roleGuard],
    data: { portal: 'staff' },
    children: [
      {
        path: 'branch',
        loadComponent: () =>
          import('./features/branch/pages/branch-management/branch-management.component').then(
            (m) => m.BranchManagementComponent,
          ),
      },
      {
        path: 'department',
        loadComponent: () =>
          import('./features/Department/pages/department-management/department-management.component').then(
            (m) => m.DepartmentManagementComponent,
          ),
      },
      {
        path: 'room',
        loadComponent: () =>
          import('./features/Room/pages/room-management/room-management.component').then(
            (m) => m.RoomManagementComponent,
          ),
      },
      {
        path: 'employee',
        loadChildren: () => import('./features/employee/routes').then((m) => m.EMPLOYEE_ROUTES),
      },
      // {
      //   path: 'doctor',
      //   loadChildren: () => import('./features/doctor/doctor.route').then((m) => m.DOCTOR_ROUTES),
      // },
      {
        path: 'role',
        loadComponent: () =>
          import('./features/Role&Permission/pages/role-management/role-management.component').then(
            (m) => m.RoleManagementComponent,
          ),
      },
      {
        path: 'patient',
        loadChildren: () =>
          import('./features/Patient/patient.route').then((p) => p.PATIENT_ROUTES),
      },
      // {
      //   path:'doctorSchedule',
      //   loadComponent:() =>
      //     import('./features/doctorSchdule/pages/doctor-schedule-management/doctor-schedule-management.component')
      //       .then((p) => p.DoctorScheduleManagementComponent)
      // }
    ],
  },
  {
    path: 'Patient',
    loadComponent: () =>
      import('./layout/patient-layout/patient-layout.component').then(
        (m) => m.PatientLayoutComponent,
      ),
    canActivate: [authGuard, roleGuard],
    data: { portal: 'Patient' },
  },
];
