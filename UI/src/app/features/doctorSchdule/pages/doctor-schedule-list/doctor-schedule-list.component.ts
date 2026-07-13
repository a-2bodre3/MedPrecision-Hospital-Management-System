// import { Component, inject, OnInit } from '@angular/core';
// import { doctorScheduleState } from '../../state/doctorSchedule.store';
// import { LucideSquarePen, LucideTrash2 } from '@lucide/angular';
// import { DatePipe } from '@angular/common';
// import { DialogService } from 'primeng/dynamicdialog';
// import { DoctorScheduleFormComponent } from '../../component/doctor-schedule-form/doctor-schedule-form.component';
// import { DoctorScheduleDisplayDTO } from '../../model/doctorSchedule.model';
// import { DoctorScheduleDeleteComponent } from '../doctor-schedule-delete/doctor-schedule-delete.component';
//
// @Component({
//   selector: 'app-doctor-schedule-list',
//   imports: [LucideTrash2, LucideSquarePen, DatePipe],
//   templateUrl: './doctor-schedule-list.component.html',
//   styleUrl: './doctor-schedule-list.component.scss',
// })
// export class DoctorScheduleListComponent implements OnInit {
//   //=================================
//   //==========inject=================
//   //=================================
//   protected doctorScheduleStore = inject(doctorScheduleState);
//   public dialogService = inject(DialogService);
//
//   //=================================
//   //==========life cycle=============
//   //=================================
//   ngOnInit() {
//     this.doctorScheduleStore.loadAllDoctorSchedule();
//   }
//   //=================================
//   //==========method=================
//   //=================================
//   formatTime(time: string) {
//     return new Date(`1970-01-01T${time}`);
//   }
//
//   showDialogUpdate(
//     type: 'add' | 'edit',
//     header: string,
//     description: string,
//     doctor: DoctorScheduleDisplayDTO,
//   ) {
//     this.dialogService.open(DoctorScheduleFormComponent, {
//       width: '500px',
//       closable: false,
//       showHeader: false,
//       data: {
//         type: type,
//         header: header,
//         description: description,
//         data: doctor,
//       },
//     });
//   }
//
//   showDialogDelete(doctor: DoctorScheduleDisplayDTO) {
//     this.dialogService.open(DoctorScheduleDeleteComponent, {
//       width: '500px',
//       closable: false,
//       showHeader: false,
//       data: {
//         id: doctor.id,
//       },
//     });
//   }
// }
