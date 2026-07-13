// import { Component, inject, OnInit, signal } from '@angular/core';
// import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
// import { LucideX } from '@lucide/angular';
// import { HlmFieldImports } from '@spartan-ng/helm/field';
// import { HlmInputImports } from '@spartan-ng/helm/input';
// import { DAYS_LIST } from '../../../../core/constants/days.constants';
// import { DayOfWeek } from '../../../../core/enum/day-of-week.enum';
// import { EmployeeStore } from '../../../employee/store/employee.store';
// import { RoomStore } from '../../../Room/state/room.store';
// import { HlmSelectImports } from '@spartan-ng/helm/select';
// import { HlmButtonImports } from '@spartan-ng/helm/button';
// import { DoctorScheduleDisplayDTO, DoctorScheduleFormDTO } from '../../model/doctorSchedule.model';
// import { form, FormField, required } from '@angular/forms/signals';
// import { doctorScheduleState } from '../../state/doctorSchedule.store';
//
// interface IDays {
//   value: DayOfWeek;
//   label: string;
//   selected: boolean;
// }
//
// @Component({
//   selector: 'app-doctor-schedule-form',
//   imports: [
//     LucideX,
//     HlmFieldImports,
//     HlmInputImports,
//     HlmSelectImports,
//     HlmButtonImports,
//     FormField,
//   ],
//   templateUrl: './doctor-schedule-form.component.html',
//   styleUrl: './doctor-schedule-form.component.scss',
// })
// export class DoctorScheduleFormComponent implements OnInit {
//   //=========================================
//   //==============inject=====================
//   //=========================================
//   private dialogRef = inject(DynamicDialogRef);
//   private dialogConfig = inject(DynamicDialogConfig);
//   protected employeeStore = inject(EmployeeStore);
//   protected roomStore = inject(RoomStore);
//   private doctorScheduleStore = inject(doctorScheduleState);
//
//   //=========================================
//   //==============variable===================
//   //=========================================
//   protected dialogData = signal<any | undefined>(undefined);
//   protected type = signal<'add' | 'edit' | undefined>(undefined);
//   protected header = signal<string | undefined>(undefined);
//   protected description = signal<string | undefined>(undefined);
//   protected days = signal<IDays[]>([]);
//   dayList = DAYS_LIST;
//   private selectedSchedule = signal<DoctorScheduleDisplayDTO | null>(null);
//
//   //=========================================
//   //==============form=======================
//   //=========================================
//   protected scheduleModel = signal<DoctorScheduleFormDTO>({
//     dayOfWeeks: this.days().map((d) => d.value),
//     startTime: '',
//     endTime: '',
//     maxPatients: 1,
//     doctorId: 1,
//     roomId: 1,
//   });
//
//   protected scheduleForm = form(this.scheduleModel, (schemaPath) => {
//     required(schemaPath.startTime);
//     required(schemaPath.endTime);
//     required(schemaPath.maxPatients);
//     required(schemaPath.doctorId);
//     required(schemaPath.roomId);
//   });
//
//   //=========================================
//   //==============life cycle=================
//   //=========================================
//   async ngOnInit() {
//     this.dialogData.set(this.dialogConfig.data);
//     if (this.dialogData()) {
//       this.type.set(this.dialogData().type);
//       this.header.set(this.dialogData().header);
//       this.description.set(this.dialogData().description);
//     }
//     if (this.type() === 'edit') {
//       this.selectedSchedule.set(this.dialogData().data);
//       this.days.update((days) => {
//         const selectedDays = this.selectedSchedule()?.dayOfWeeks ?? [];
//
//         return days.map((d) => ({
//           ...d,
//           selected: selectedDays.includes(d.value),
//         }));
//       });
//     }
//     console.log(this.selectedSchedule());
//     this.employeeStore.loadEmployees({
//       params: {
//         pageNumber: 1,
//         pageSize: 50,
//       },
//       includeInactive: false,
//     });
//
//     await this.roomStore.loadRooms();
//   }
//   constructor() {
//     this.dayList.forEach((day) => {
//       this.days.update((prev) => [
//         ...prev,
//         { value: day.value, label: day.label, selected: false },
//       ]);
//     });
//   }
//
//   //=========================================
//   //==============method=====================
//   //=========================================
//   onClose() {
//     this.dialogRef.close();
//   }
//
//   onSubmit(event: Event) {
//     event.preventDefault();
//     const data: DoctorScheduleFormDTO = {
//       dayOfWeeks: this.days()
//         .filter((d) => d.selected)
//         .map((d) => d.value),
//       startTime: this.scheduleModel().startTime,
//       endTime: this.scheduleModel().endTime,
//       maxPatients: this.scheduleModel().maxPatients.toString(),
//       doctorId: this.scheduleModel().doctorId.toString(),
//       roomId: this.scheduleModel().roomId.toString(),
//     };
//     if (this.type() === 'add') {
//       this.doctorScheduleStore.createDoctorSchedule(data);
//       this.onClose();
//     }
//     if (this.type() === 'edit') {
//       this.doctorScheduleStore.updateDoctorSchedule({
//         id: this.selectedSchedule()?.id!,
//         data: data,
//       });
//       this.onClose();
//     }
//   }
//
//   selectedDay(index: number) {
//     this.days.update((prev) =>
//       prev.map((d, i) => (i === index ? { ...d, selected: !d.selected } : d)),
//     );
//   }
// }
