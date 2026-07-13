// import { Component, inject } from '@angular/core';
// import { HeaderComponent } from '../../../../shared/components/header/header.component';
// import { HlmButtonImports } from '@spartan-ng/helm/button';
// import { LucideClipboardPlus } from '@lucide/angular';
// import { DoctorScheduleListComponent } from '../doctor-schedule-list/doctor-schedule-list.component';
// import { DialogService } from 'primeng/dynamicdialog';
// import { DoctorScheduleFormComponent } from '../../component/doctor-schedule-form/doctor-schedule-form.component';
//
// @Component({
//   selector: 'app-doctor-schedule-management',
//   imports: [HeaderComponent, HlmButtonImports, LucideClipboardPlus, DoctorScheduleListComponent],
//   templateUrl: './doctor-schedule-management.component.html',
//   styleUrl: './doctor-schedule-management.component.scss',
// })
// export class DoctorScheduleManagementComponent {
//   //=========================================
//   //==============inject=====================
//   //=========================================
//   public dialogService = inject(DialogService);
//
//   //=========================================
//   //==============method=====================
//   //=========================================
//   showDialogCreate(type: 'add' | 'edit', header: string, description: string) {
//     this.dialogService.open(DoctorScheduleFormComponent, {
//       width: '500px',
//       closable: false,
//       showHeader: false,
//       data: {
//         type: type,
//         header: header,
//         description: description,
//       },
//     });
//   }
// }
