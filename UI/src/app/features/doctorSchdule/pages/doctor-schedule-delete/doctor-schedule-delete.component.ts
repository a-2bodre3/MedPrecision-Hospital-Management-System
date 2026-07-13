import { Component, inject, OnInit, signal } from '@angular/core';
import { doctorScheduleState } from '../../state/doctorSchedule.store';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DoctorScheduleDisplayDTO } from '../../model/doctorSchedule.model';

@Component({
  selector: 'app-doctor-schedule-delete',
  imports: [],
  templateUrl: './doctor-schedule-delete.component.html',
  styleUrl: './doctor-schedule-delete.component.scss',
})
export class DoctorScheduleDeleteComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  private dialogRef = inject(DynamicDialogRef);
  private dialogConfig = inject(DynamicDialogConfig);
  private doctorScheduleStore = inject(doctorScheduleState);

  //=========================================
  //==============variable===================
  //=========================================
  private id = signal<number | null>(null);
  protected dialogData = signal<any | undefined>(undefined);

  //=========================================
  //==============life cycle=================
  //=========================================
  ngOnInit() {
    this.dialogData.set(this.dialogConfig.data);
    if (this.dialogData) {
      this.id.set(this.dialogData().id);
    }
  }

  //=========================================
  //==============method=====================
  //=========================================
  onClose() {
    this.dialogRef.close();
  }

  onDelete() {
    this.doctorScheduleStore.deleteDoctorSchedule(this.id()!);
    this.dialogRef.close();
  }
}
