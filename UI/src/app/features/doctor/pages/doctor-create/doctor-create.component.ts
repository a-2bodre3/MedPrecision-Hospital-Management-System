import { Component, inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { form, FormField, required, email } from '@angular/forms/signals';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButton } from '@spartan-ng/helm/button';
import { LucideArrowRight, LucideSave } from '@lucide/angular';
import { DoctorStore } from '../../store/doctor.store';
import { BranchStore } from '../../../branch/state/branch.store';
import { DepartmentStore } from '../../../Department/state/department.store';
import { RoleStore } from '../../../Role&Permission/state/role.store';
import { toast } from '@spartan-ng/brain/sonner';
import { CreateDoctorRequest } from '../../model/doctor-request.model';
import { LookupsState } from '../../../../core/state/Lookups.store';
import { AcademicDegree } from '../../../../core/enum/AcademicDegree.enum';

@Component({
  selector: 'app-doctor-create',
  standalone: true,
  imports: [HeaderComponent, HlmButton, LucideArrowRight, LucideSave, FormField],
  templateUrl: './doctor-create.component.html',
  styleUrl: './doctor-create.component.scss',
})
export class DoctorCreateComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected doctorStore = inject(DoctorStore);
  protected branchStore = inject(BranchStore);
  protected departmentStore = inject(DepartmentStore);
  protected roleStore = inject(RoleStore);
  protected lookupsStore = inject(LookupsState);
  private router = inject(Router);

  //=========================================
  //==============form=======================
  //=========================================
  protected doctorModel = signal<Omit<CreateDoctorRequest, 'degree'> & { degree: string }>({
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    roleId: '',
    branchId: '',
    jobTitle: '',
    imageFile: null,
    departmentId: '',
    salary: '',
    dateOfBirth: '',
    gender: '0',
    address: {
      city: '',
      country: '',
      street: '',
    },
    licenseNumber: '',
    consultationFee: 0,
    degree: '1',
    subSpecialtyId: '',
  });

  protected doctorForm = form(this.doctorModel, (schemaPath) => {
    required(schemaPath.email);
    email(schemaPath.email);
    required(schemaPath.password);
    required(schemaPath.firstName);
    required(schemaPath.lastName);
    required(schemaPath.phoneNumber);
    required(schemaPath.roleId);
    required(schemaPath.branchId);
    required(schemaPath.jobTitle);
    required(schemaPath.departmentId);
    required(schemaPath.salary);
    required(schemaPath.dateOfBirth);
    required(schemaPath.address.street);
    required(schemaPath.address.city);
    required(schemaPath.address.country);
    required(schemaPath.licenseNumber);
    required(schemaPath.consultationFee);
    required(schemaPath.degree);
    required(schemaPath.subSpecialtyId);
  });

  //=========================================
  //==============life cycle=================
  //=========================================
  ngOnInit() {
    this.branchStore.loadBranch({});
    this.departmentStore.fetchDepartments();
  }

  //=========================================
  //==============method=====================
  //=========================================
  goBack() {
    this.router.navigate(['/staff/doctor']);
  }

  onFileChange(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0] ?? null;
    this.doctorModel.update((model) => ({ ...model, imageFile: file }));
  }

  onBranchChange() {
    this.doctorModel.update((model) => ({ ...model, departmentId: '' }));
  }

  onSubmit(event: Event) {
    event.preventDefault();
    if (this.doctorForm().invalid()) {
      toast.error('الرجاء تعبئة كافة الحقول المطلوبة وإرفاق صورة الطبيب');
      return;
    }

    const modelValue = this.doctorModel();
    const data: CreateDoctorRequest = {
      ...modelValue,
      degree: Number(modelValue.degree) as AcademicDegree,
    };
    this.doctorStore.createDoctor({ data });
    toast.success('تم إنشاء الطبيب بنجاح');
    this.goBack();
  }

  protected filteredDepartments() {
    const branchId = Number(this.doctorModel().branchId);
    if (!branchId) {
      return this.departmentStore.departments();
    }

    const branch = this.branchStore.branches().find((item) => item.id === branchId);
    if (!branch) {
      return this.departmentStore.departments();
    }

    return this.departmentStore.departments().filter((item) => item.branchName === branch.name);
  }
}
