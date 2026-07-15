import { Component, inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { form, FormField, required, email } from '@angular/forms/signals';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButton } from '@spartan-ng/helm/button';
import { LucideArrowRight, LucideSave } from '@lucide/angular';
import { EmployeeStore } from '../../store/employee.store';
import { BranchStore } from '../../../branch/state/branch.store';
import { DepartmentStore } from '../../../Department/state/department.store';
import { RoleStore } from '../../../Role&Permission/state/role.store';
import { toast } from '@spartan-ng/brain/sonner';
import { CreateEmployeeRequest } from '../../model/employee-request.model';
import { LookupsState } from '../../../../core/state/Lookups.store';

@Component({
  selector: 'app-employee-create',
  standalone: true,
  imports: [HeaderComponent, HlmButton, LucideArrowRight, LucideSave, FormField],
  templateUrl: './employee-create.component.html',
  styleUrl: './employee-create.component.scss',
})
export class EmployeeCreateComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected employeeStore = inject(EmployeeStore);
  protected branchStore = inject(BranchStore);
  protected departmentStore = inject(DepartmentStore);
  protected roleStore = inject(RoleStore);
  protected lookupsStore = inject(LookupsState);
  private router = inject(Router);

  //=========================================
  //==============form=======================
  //=========================================
  protected employeeModel = signal<CreateEmployeeRequest>({
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
  });

  protected employeeForm = form(this.employeeModel, (schemaPath) => {
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
    this.router.navigate(['/staff/employee']);
  }

  onFileChange(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0] ?? null;
    this.employeeModel.update((model) => ({ ...model, imageFile: file }));
  }

  onBranchChange() {
    this.employeeModel.update((model) => ({ ...model, departmentId: '' }));
  }

  onSubmit(event: Event) {
    event.preventDefault();
    if (this.employeeForm().invalid() || !this.employeeModel().imageFile) {
      toast.error('الرجاء تعبئة كافة الحقول المطلوبة وإرفاق صورة الموظف');
      return;
    }

    const data: CreateEmployeeRequest = this.employeeModel();
    this.employeeStore.createEmployee({ data });
    toast.success('تم إنشاء الموظف بنجاح');
    this.goBack();
  }

  protected filteredDepartments() {
    const branchId = Number(this.employeeModel().branchId);
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
