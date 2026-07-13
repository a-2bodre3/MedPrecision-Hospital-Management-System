// import { Component, inject, OnInit, signal } from '@angular/core';
// import { EmployeeStore } from '../../../employee/store/employee.store';
// import { BranchStore } from '../../../branch/state/branch.store';
// import { DepartmentStore } from '../../../Department/state/department.store';
// import { RoleStore } from '../../../Role&Permission/state/role.store';
// import { Router } from '@angular/router';
// import { CreateDoctorDto } from '../../model/doctor.model';
// import { email, form, FormField, required } from '@angular/forms/signals';
// import { toast } from '@spartan-ng/brain/sonner';
// import { HeaderComponent } from '../../../../shared/components/header/header.component';
// import { HlmButton } from '@spartan-ng/helm/button';
// import { LucideArrowRight, LucideSave } from '@lucide/angular';
//
// @Component({
//   selector: 'app-doctor-create',
//   imports: [HeaderComponent, HlmButton, LucideArrowRight, LucideSave, FormField],
//   templateUrl: './doctor-create.component.html',
//   styleUrl: './doctor-create.component.scss',
// })
// export class DoctorCreateComponent implements OnInit {
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
//   protected doctorModel = signal<CreateDoctorDto>({
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
//     licenseNumber: '',
//     academicDegree: '',
//     consultationFee: 0,
//     subSpecialtyId: '',
//   });
//
//   protected doctorForm = form(this.doctorModel, (schemaPath) => {
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
//     required(schemaPath.licenseNumber);
//     required(schemaPath.academicDegree);
//     required(schemaPath.consultationFee);
//     required(schemaPath.subSpecialtyId);
//   });
//   //=========================================
//   //==============life cycle=================
//   //=========================================
//   ngOnInit() {
//     this.branchStore.loadBranch({});
//     this.departmentStore.fetchDepartments();
//     this.roleStore.loadRoles();
//     this.employeeStore.loadSpecializations();
//   }
//   //=========================================
//   //==============method=====================
//   //=========================================
//   goBack() {
//     this.router.navigate(['/staff/employee']);
//   }
//   onFileChange(event: Event) {
//     const file = (event.target as HTMLInputElement).files?.[0] ?? null;
//     this.doctorModel.update((model) => ({ ...model, imageFile: file }));
//   }
//
//   onBranchChange() {
//     this.doctorModel.update((model) => ({ ...model, departmentId: '' }));
//   }
//   protected filteredDepartments() {
//     const branchId = Number(this.doctorModel().branchId);
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
//   onSubmit(event: Event) {
//     event.preventDefault();
//     if (this.doctorForm().invalid() || !this.doctorModel().imageFile) {
//       toast.error('الرجاء تعبئة كافة الحقول المطلوبة وإرفاق صورة الموظف');
//       return;
//     }
//     const formData = new FormData();
//
//     formData.append('Email', this.doctorModel().email);
//     formData.append('Password', this.doctorModel().password);
//     formData.append('FirstName', this.doctorModel().firstName);
//     formData.append('LastName', this.doctorModel().lastName);
//     formData.append('PhoneNumber', this.doctorModel().phoneNumber);
//     formData.append('RoleId', this.doctorModel().roleId);
//     formData.append('BranchId', this.doctorModel().branchId);
//     formData.append('DepartmentId', this.doctorModel().departmentId);
//     formData.append('JobTitle', this.doctorModel().jobTitle);
//     formData.append('Salary', this.doctorModel().salary.toString());
//     formData.append('DateOfBirth', this.doctorModel().dateOfBirth);
//     formData.append('Gender', this.doctorModel().gender);
//     formData.append('Street', this.doctorModel().street);
//     formData.append('City', this.doctorModel().city);
//     formData.append('Country', this.doctorModel().country);
//     formData.append('IsActive', this.doctorModel().isActive);
//     formData.append('LicenseNumber', this.doctorModel().licenseNumber);
//     formData.append('AcademicDegree', this.doctorModel().academicDegree);
//     formData.append('ConsultationFee', this.doctorModel().consultationFee.toString());
//     formData.append('SubSpecialtyId', this.doctorModel().subSpecialtyId.toString());
//
//     const file = this.doctorModel().imageFile;
//     if (file) {
//       formData.append('ImageFile', file, file.name);
//     }
//     this.employeeStore.createDoctor(formData as any);
//     toast.success('تم إنشاء الموظف بنجاح');
//     this.goBack();
//   }
// }
