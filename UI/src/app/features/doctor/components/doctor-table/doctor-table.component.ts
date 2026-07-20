import { Component, inject, OnInit } from '@angular/core';
import {
  LucideEye,
  LucidePencil,
  LucideUserRound,
  LucideChevronLeft,
  LucideChevronRight,
} from '@lucide/angular';
import { Router } from '@angular/router';
import { DoctorStore } from '../../store/doctor.store';

@Component({
  selector: 'app-doctor-table',
  standalone: true,
  imports: [
    LucideEye,
    LucidePencil,
    LucideUserRound,
    LucideChevronLeft,
    LucideChevronRight,
  ],
  templateUrl: './doctor-table.component.html',
})
export class DoctorTableComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected doctorStore = inject(DoctorStore);
  private router = inject(Router);

  //=========================================
  //==============life cycle=================
  //=========================================
  ngOnInit() {
    this.fetchDoctors();
  }

  //=========================================
  //==============computed helpers===========
  //=========================================
  get totalPages() {
    const query = this.doctorStore.queryParameters();
    return Math.max(Math.ceil(this.doctorStore.totalCount() / query.pageSize), 1);
  }

  get startRecord() {
    const query = this.doctorStore.queryParameters();
    if (!this.doctorStore.totalCount()) {
      return 0;
    }
    return (query.pageNumber - 1) * query.pageSize + 1;
  }

  get endRecord() {
    const query = this.doctorStore.queryParameters();
    return Math.min(query.pageNumber * query.pageSize, this.doctorStore.totalCount());
  }

  //=========================================
  //==============method=====================
  //=========================================
  fetchDoctors() {
    this.doctorStore.loadDoctors({
      params: this.doctorStore.queryParameters(),
    });
  }

  goToDetails(id: number) {
    this.router.navigate(['/staff/doctor', id]);
  }

  goToEdit(id: number) {
    this.doctorStore.loadDoctorById(id);
    this.router.navigate(['/staff/doctor', id, 'edit']);
  }

  changePage(pageNumber: number) {
    if (pageNumber < 1 || pageNumber > this.totalPages) {
      return;
    }

    this.doctorStore.updateQueryParameters({ pageNumber });
    this.fetchDoctors();
  }

  changePageSize(event: Event) {
    const pageSize = Number((event.target as HTMLSelectElement).value);
    this.doctorStore.updateQueryParameters({ pageSize, pageNumber: 1 });
    this.fetchDoctors();
  }
}
