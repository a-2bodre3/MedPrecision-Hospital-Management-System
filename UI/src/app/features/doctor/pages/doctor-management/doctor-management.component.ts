import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButton } from '@spartan-ng/helm/button';
import { LucideStethoscope } from '@lucide/angular';

import { DepartmentStore } from '../../../Department/state/department.store';
import { RoleStore } from '../../../Role&Permission/state/role.store';
import { toast } from '@spartan-ng/brain/sonner';
import { DoctorStore } from '../../store/doctor.store';
import { DoctorTableComponent } from '../../components/doctor-table/doctor-table.component';
import { LookupsState } from '../../../../core/state/Lookups.store';

@Component({
  selector: 'app-doctor-management',
  standalone: true,
  imports: [HeaderComponent, HlmButton, LucideStethoscope, DoctorTableComponent],
  templateUrl: './doctor-management.component.html',
})
export class DoctorManagementComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected doctorStore = inject(DoctorStore);
  protected departmentStore = inject(DepartmentStore);
  protected lookupsStore = inject(LookupsState);
  protected roleStore = inject(RoleStore);
  private router = inject(Router);

  //=========================================
  //==============life cycle=================
  //=========================================
  ngOnInit() {}

  //=========================================
  //==============method=====================
  //=========================================
  goToCreate() {
    this.router.navigate(['/staff/doctor/create']);
  }

  onSearchChange(event: Event) {
    const searchTerm = (event.target as HTMLInputElement).value;
    this.doctorStore.updateQueryParameters({ searchTerm, pageNumber: 1 });
    this.fetchDoctors();
  }

  onDepartmentChange(event: Event) {
    const value = (event.target as HTMLSelectElement).value;
    this.doctorStore.updateQueryParameters({
      pageNumber: 1,
      departmentId: Number(value),
    });
    this.fetchDoctors();
  }

  onIncludeInactiveChange(event: Event) {
    const includeInactive = (event.target as HTMLInputElement).checked;
    this.doctorStore.loadDoctors({
      params: { ...this.doctorStore.queryParameters(), pageNumber: 1, isActive: includeInactive },
    });
  }

  private fetchDoctors() {
    this.doctorStore.loadDoctors({
      params: this.doctorStore.queryParameters(),
    });
  }
}
