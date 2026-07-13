import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PatientStore } from '../../store/patient.store';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { toast } from '@spartan-ng/brain/sonner';
import {
  LucideEye,
  LucidePencil,
  LucideTrash2,
  LucideUserRound,
  LucideChevronLeft,
  LucideChevronRight,
  LucideSearch,
  LucidePlus,
} from '@lucide/angular';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [
    HeaderComponent,
    LucideEye,
    LucidePencil,
    LucideTrash2,
    LucideUserRound,
    LucideChevronLeft,
    LucideChevronRight,
    LucideSearch,
    LucidePlus,
  ],
  templateUrl: './patient-list.component.html',
  styleUrl: './patient-list.component.scss',
})
export class PatientListComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected patientStore = inject(PatientStore);
  private router = inject(Router);

  //=========================================
  //==============life cycle=================
  //=========================================
  ngOnInit() {
    this.fetchPatients();
  }


  //=========================================
  //==============computed helpers===========
  //=========================================
  get totalPages() {
    const total = this.patientStore.totalCount();
    const size = this.patientStore.queryParams().pageSize;
    return Math.max(Math.ceil(total / size), 1);
  }

  get startRecord() {
    const query = this.patientStore.queryParams();
    if (!this.patientStore.totalCount()) {
      return 0;
    }
    return (query.pageNumber - 1) * query.pageSize + 1;
  }

  get endRecord() {
    const query = this.patientStore.queryParams();
    return Math.min(query.pageNumber * query.pageSize, this.patientStore.totalCount());
  }

  //=========================================
  //==============methods====================
  //=========================================
  fetchPatients() {
    this.patientStore.loadPatient({
      params: this.patientStore.queryParams(),
    });
  }

  onSearchChange(event: Event) {
    const searchTerm = (event.target as HTMLInputElement).value;
    const currentParams = this.patientStore.queryParams();
    this.patientStore.loadPatient({
      params: {
        ...currentParams,
        searchTerm: searchTerm || '',
        pageNumber: 1,
      },
    });
  }

  changePage(pageNumber: number) {
    if (pageNumber < 1 || pageNumber > this.totalPages) {
      return;
    }
    const currentParams = this.patientStore.queryParams();
    this.patientStore.loadPatient({
      params: {
        ...currentParams,
        pageNumber,
      },
    });
  }

  changePageSize(event: Event) {
    const pageSize = Number((event.target as HTMLSelectElement).value);
    const currentParams = this.patientStore.queryParams();
    this.patientStore.loadPatient({
      params: {
        ...currentParams,
        pageSize,
        pageNumber: 1,
      },
    });
  }

  goToCreate() {
    this.router.navigate(['/staff/patient/add']);
  }

  goToDetails(id: number) {
    this.router.navigate(['/staff/patient', id]);
  }

  goToEdit(id: number) {
    this.router.navigate(['/staff/patient', id, 'edit']);
  }

  deletePatient(id: number, fullName: string) {
    const confirmed = window.confirm(`هل أنت متأكد من حذف المريض "${fullName}"؟`);
    if (confirmed) {
      this.patientStore.deletePatient(id);
      toast.success(`تم حذف المريض "${fullName}" بنجاح`);
    }
  }
}
