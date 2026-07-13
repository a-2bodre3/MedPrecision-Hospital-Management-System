// import { Component, inject, OnInit, signal } from '@angular/core';
// import { Router } from '@angular/router';
// import { form, FormField, required, email } from '@angular/forms/signals';
// import { HeaderComponent } from '../../../../shared/components/header/header.component';
// import { HlmButton } from '@spartan-ng/helm/button';
// import { LucideArrowRight, LucideSave } from '@lucide/angular';
// import { EmployeeFormModel } from '../../model/employee.model';
// import { EmployeeStore } from '../../store/employee.store';
// import { BranchStore } from '../../../branch/state/branch.store';
// import { DepartmentStore } from '../../../Department/state/department.store';
// import { RoleStore } from '../../../Role&Permission/state/role.store';
// import { toast } from '@spartan-ng/brain/sonner';
//
// @Component({
//   selector: 'app-employee-create',
//   standalone: true,
//   imports: [HeaderComponent, HlmButton, LucideArrowRight, LucideSave, FormField],
//   templateUrl: './employee-create.component.html',
//   styleUrl: './employee-create.component.scss',
// })
// export class EmployeeCreateComponent implements OnInit {
//   //=========================================
//   //==============inject=====================
//   //=========================================
//   protected employeeStore = inject(EmployeeStore);
//   protected branchStore = inject(BranchStore);
//   protected departmentStore = inject(DepartmentStore);
//   protected roleStore = inject(RoleStore);
//   private router = inject(Router);
//
//   //=========================================
//   //==============form=======================
//   //=========================================
//   protected employeeModel = signal<EmployeeFormModel>({
//     email: '',
//     password: '',
//     firstName: '',
//     lastName: '',
//     phoneNumber: '',
//     imageFile: null,
//     roleId: '',
//     branchId: '',
//     jobTitle: '',
//     departmentId: '',
//     salary: '',
//     dateOfBirth: '',
//     gender: '1',
//     street: '',
//     city: '',
//     country: '',
//     isActive: 'true',
//   });
//
//   protected employeeForm = form(this.employeeModel, (schemaPath) => {
//     required(schemaPath.email);
//     email(schemaPath.email);
//     required(schemaPath.password);
//     required(schemaPath.firstName);
//     required(schemaPath.lastName);
//     required(schemaPath.phoneNumber);
//     required(schemaPath.roleId);
//     required(schemaPath.branchId);
//     required(schemaPath.jobTitle);
//     required(schemaPath.departmentId);
//     required(schemaPath.salary);
//     required(schemaPath.dateOfBirth);
//     required(schemaPath.gender);
//     required(schemaPath.street);
//     required(schemaPath.city);
//     required(schemaPath.country);
//   });
//
//   //=========================================
//   //==============life cycle=================
//   //=========================================
//   ngOnInit() {
//     this.branchStore.loadBranch({});
//     this.departmentStore.fetchDepartments();
//     this.roleStore.loadRoles();
//   }
//
//   //=========================================
//   //==============method=====================
//   //=========================================
//   goBack() {
//     this.router.navigate(['/staff/employee']);
//   }
//
//   onFileChange(event: Event) {
//     const file = (event.target as HTMLInputElement).files?.[0] ?? null;
//     this.employeeModel.update((model) => ({ ...model, imageFile: file }));
//   }
//
//   onBranchChange() {
//     this.employeeModel.update((model) => ({ ...model, departmentId: '' }));
//   }
//
//   onSubmit(event: Event) {
//     event.preventDefault();
//     if (this.employeeForm().invalid() || !this.employeeModel().imageFile) {
//       toast.error('الرجاء تعبئة كافة الحقول المطلوبة وإرفاق صورة الموظف');
//       return;
//     }
//
//     const formData = this.toFormData();
//     this.employeeStore.createEmployee(formData);
//     toast.success('تم إنشاء الموظف بنجاح');
//     this.goBack();
//   }
//
//   protected filteredDepartments() {
//     const branchId = Number(this.employeeModel().branchId);
//     if (!branchId) {
//       return this.departmentStore.departments();
//     }
//
//     const branch = this.branchStore.branches().find((item) => item.id === branchId);
//     if (!branch) {
//       return this.departmentStore.departments();
//     }
//
//     return this.departmentStore.departments().filter((item) => item.branchName === branch.name);
//   }
//
//   private toFormData() {
//     const value = this.employeeModel();
//     const data = new FormData();
//     data.append('Email', value.email);
//     data.append('Password', value.password);
//     data.append('FirstName', value.firstName);
//     data.append('LastName', value.lastName);
//     data.append('PhoneNumber', value.phoneNumber);
//     data.append('ImageFile', value.imageFile as File);
//     data.append('RoleId', value.roleId);
//     data.append('BranchId', value.branchId);
//     data.append('JobTitle', value.jobTitle);
//     data.append('DepartmentId', value.departmentId);
//     data.append('Salary', value.salary);
//     data.append('DateOfBirth', value.dateOfBirth);
//     data.append('Gender', value.gender);
//     data.append('Street', value.street);
//     data.append('City', value.city);
//     data.append('Country', value.country);
//     return data;
//   }
// }
