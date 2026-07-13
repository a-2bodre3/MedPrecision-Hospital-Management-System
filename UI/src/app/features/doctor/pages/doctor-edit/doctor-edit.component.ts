// import { Component, effect, inject, OnInit, signal } from '@angular/core';
// import { EmployeeStore } from '../../../employee/store/employee.store';
// import { BranchStore } from '../../../branch/state/branch.store';
// import { DepartmentStore } from '../../../Department/state/department.store';
// import { RoleStore } from '../../../Role&Permission/state/role.store';
// import { ActivatedRoute, Router } from '@angular/router';
// import { UpdateDoctorDto } from '../../model/doctor.model';
// import { form, FormField, required } from '@angular/forms/signals';
// import { toast } from '@spartan-ng/brain/sonner';
// import { HeaderComponent } from '../../../../shared/components/header/header.component';
// import { HlmButton } from '@spartan-ng/helm/button';
// import { LucideArrowRight, LucideSave } from '@lucide/angular';
//
// @Component({
//   selector: 'app-doctor-edit',
//   imports: [HeaderComponent, HlmButton, LucideArrowRight, LucideSave, FormField],
//   templateUrl: './doctor-edit.component.html',
//   styleUrl: './doctor-edit.component.scss',
// })
// export class DoctorEditComponent implements OnInit {
//   //=========================================
//   //==============inject=====================
//   //=========================================
//   protected employeeStore = inject(EmployeeStore);
//   protected branchStore = inject(BranchStore);
//   protected departmentStore = inject(DepartmentStore);
//   protected roleStore = inject(RoleStore);
//   private route = inject(ActivatedRoute);
//   private router = inject(Router);
//
//   protected doctorId = 0;
//   private formPatched = signal(false);
//
//   //=========================================
//   //==============form=======================
//   //=========================================
//
//   protected doctorModel = signal<UpdateDoctorDto>({
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
//     consultationFee: 0,
//   });
//   protected doctorForm = form(this.doctorModel, (schemaPath) => {
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
//   constructor() {
//     effect(() => {
//       const doctor = this.employeeStore.selectedDoctor();
//       if (doctor && !this.formPatched()) {
//         this.patchFormFromSelected();
//         this.formPatched.set(true);
//       }
//     });
//   }
//
//   async ngOnInit() {
//     this.doctorId = Number(this.route.snapshot.paramMap.get('id'));
//     await this.branchStore.loadBranch({});
//     await this.departmentStore.fetchDepartments();
//     await this.roleStore.loadRoles();
//     this.employeeStore.loadDoctorById(this.doctorId);
//   }
//   //=========================================
//   //==============method=====================
//   //=========================================
//   goBack() {
//     this.employeeStore.clearSelectedDoctor();
//     this.formPatched.set(false);
//     this.router.navigate(['/staff/employee']);
//   }
//
//   onFileChange(event: Event) {
//     const file = (event.target as HTMLInputElement).files?.[0] ?? null;
//     this.doctorModel.update((model) => ({ ...model, imageFile: file }));
//   }
//
//   onBranchChange() {
//     this.doctorModel.update((model) => ({ ...model, departmentId: '' }));
//   }
//   onSubmit(event: Event) {
//     event.preventDefault();
//     if (this.doctorForm().invalid()) {
//       toast.error('الرجاء تعبئة كافة الحقول المطلوبة بشكل صحيح');
//       return;
//     }
//     this.employeeStore.updateDoctor({ id: this.doctorId, data: this.toFormData() });
//     toast.success('تم تعديل بيانات الدكتور بنجاح');
//     this.goBack();
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
//   private patchFormFromSelected() {
//     const doctor = this.employeeStore.selectedDoctor();
//     if (!doctor) {
//       return;
//     }
//     const [firstName, ...rest] = doctor.fullName.split(' ');
//     const role = this.roleStore.roles().find((item) => item.name === doctor.roleName);
//     const branch = this.branchStore.branches().find((item) => item.name === doctor.branchName);
//     const department = this.departmentStore
//       .departments()
//       .find((item) => item.name === doctor.departmentName);
//
//     this.doctorModel.update((model) => ({
//       ...model,
//       email: doctor.email,
//       firstName: firstName || '',
//       lastName: rest.join(' '),
//       phoneNumber: doctor.phoneNumber,
//       roleId: role ? String(role.id) : '',
//       branchId: branch ? String(branch.id) : '',
//       departmentId: department ? String(department.id) : '',
//       jobTitle: doctor.jobTitle,
//       salary: String(doctor.salary),
//       dateOfBirth: doctor.birthDate ? doctor.birthDate.substring(0, 10) : '',
//       street: doctor.street,
//       city: doctor.city,
//       country: doctor.country,
//       isActive: doctor.isActive ? 'true' : 'false',
//       consultationFee: 0,
//     }));
//   }
//   private toFormData() {
//     const value = this.doctorModel();
//     const data = new FormData();
//     data.append('FirstName', value.firstName);
//     data.append('LastName', value.lastName);
//     data.append('PhoneNumber', value.phoneNumber);
//     if (value.imageFile) {
//       data.append('ImageFile', value.imageFile);
//     }
//     data.append('RoleId', value.roleId);
//     data.append('BranchId', value.branchId);
//     data.append('JobTitle', value.jobTitle);
//     data.append('DepartmentId', value.departmentId);
//     data.append('IsActive', value.isActive);
//     data.append('Salary', value.salary);
//     data.append('DateOfBirth', value.dateOfBirth);
//     data.append('Gender', value.gender.toString());
//     data.append('Street', value.street);
//     data.append('City', value.city);
//     data.append('Country', value.country);
//     data.append('ConsultationFee', String(value.consultationFee));
//     return data;
//   }
// }
