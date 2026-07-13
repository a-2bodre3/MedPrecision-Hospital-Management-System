// import { Component, effect, inject, OnInit, signal } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { form, FormField, required } from '@angular/forms/signals';
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
//   selector: 'app-employee-edit',
//   standalone: true,
//   imports: [HeaderComponent, HlmButton, LucideArrowRight, LucideSave, FormField],
//   templateUrl: './employee-edit.component.html',
//   styleUrl: '../employee-create/employee-create.component.scss',
// })
// export class EmployeeEditComponent implements OnInit {
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
//   protected employeeId = 0;
//   private formPatched = signal(false);
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
//
//   //=========================================
//   //==============life cycle=================
//   //=========================================
//   constructor() {
//     effect(() => {
//       const employee = this.employeeStore.selectedEmployee();
//       if (employee && !this.formPatched()) {
//         this.patchFormFromSelected();
//         this.formPatched.set(true);
//       }
//     });
//   }
//
//   async ngOnInit() {
//     this.employeeId = Number(this.route.snapshot.paramMap.get('id'));
//     await this.branchStore.loadBranch({});
//     await this.departmentStore.fetchDepartments();
//     await this.roleStore.loadRoles();
//     this.employeeStore.loadEmployeeById(this.employeeId);
//   }
//
//   //=========================================
//   //==============method=====================
//   //=========================================
//   goBack() {
//     this.employeeStore.clearSelectedEmployee();
//     this.formPatched.set(false);
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
//     if (this.employeeForm().invalid()) {
//       toast.error('الرجاء تعبئة كافة الحقول المطلوبة بشكل صحيح');
//       return;
//     }
//
//     this.employeeStore.updateEmployee({ id: this.employeeId, data: this.toFormData() });
//     toast.success('تم تعديل بيانات الموظف بنجاح');
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
//   private patchFormFromSelected() {
//     const employee = this.employeeStore.selectedEmployee();
//     if (!employee) {
//       return;
//     }
//
//     const [firstName, ...rest] = employee.fullName.split(' ');
//     const role = this.roleStore.roles().find((item) => item.name === employee.roleName);
//     const branch = this.branchStore.branches().find((item) => item.name === employee.branchName);
//     const department = this.departmentStore
//       .departments()
//       .find((item) => item.name === employee.departmentName);
//
//     this.employeeModel.update((model) => ({
//       ...model,
//       email: employee.email,
//       firstName: firstName || '',
//       lastName: rest.join(' '),
//       phoneNumber: employee.phoneNumber,
//       roleId: role ? String(role.id) : '',
//       branchId: branch ? String(branch.id) : '',
//       departmentId: department ? String(department.id) : '',
//       jobTitle: employee.jobTitle,
//       salary: String(employee.salary),
//       dateOfBirth: employee.birthDate ? employee.birthDate.substring(0, 10) : '',
//       street: employee.street,
//       city: employee.city,
//       country: employee.country,
//       isActive: employee.isActive ? 'true' : 'false',
//     }));
//   }
//
//   private toFormData() {
//     const value = this.employeeModel();
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
//     return data;
//   }
// }
