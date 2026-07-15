import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { form, FormField, required } from '@angular/forms/signals';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButton } from '@spartan-ng/helm/button';
import { LucideArrowRight, LucideSave } from '@lucide/angular';
import { EmployeeStore } from '../../store/employee.store';
import { BranchStore } from '../../../branch/state/branch.store';
import { DepartmentStore } from '../../../Department/state/department.store';
import { RoleStore } from '../../../Role&Permission/state/role.store';
import { toast } from '@spartan-ng/brain/sonner';
import { UpdateEmployeeRequest } from '../../model/employee-request.model';
import { LookupsState } from '../../../../core/state/Lookups.store';
import { EmployeeDetailsResponse } from '../../model/employee-response.model';

@Component({
  selector: 'app-employee-edit',
  standalone: true,
  imports: [HeaderComponent, HlmButton, LucideArrowRight, LucideSave, FormField],
  templateUrl: './employee-edit.component.html',
  styleUrl: '../employee-create/employee-create.component.scss',
})
export class EmployeeEditComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected employeeStore = inject(EmployeeStore);
  protected branchStore = inject(BranchStore);
  protected departmentStore = inject(DepartmentStore);
  protected roleStore = inject(RoleStore);
  protected lookupsStore = inject(LookupsState);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  protected employeeId = 0;
  private formPatched = signal(false);
  private employee = signal<EmployeeDetailsResponse | null>(null);

  //=========================================
  //==============form=======================
  //=========================================
  protected employeeModel = signal<UpdateEmployeeRequest>({
    firstName: '',
    lastName: '',
    phoneNumber: '',
    imageFile: null,
    roleId: '',
    branchId: '',
    jobTitle: '',
    departmentId: '',
    salary: '',
    dateOfBirth: '',
    gender: '0',
    address: {
      street: '',
      city: '',
      country: '',
    },
    isActive: '',
  });

  protected employeeForm = form(this.employeeModel, (schemaPath) => {
    required(schemaPath.firstName);
    required(schemaPath.lastName);
    required(schemaPath.phoneNumber);
    required(schemaPath.roleId);
    required(schemaPath.branchId);
    required(schemaPath.jobTitle);
    required(schemaPath.departmentId);
    required(schemaPath.salary);
    required(schemaPath.dateOfBirth);
  });

  //=========================================
  //==============life cycle=================
  //=========================================
  constructor() {
    effect(() => {
      const employee = this.employeeStore.employeeDetails();
      const roles = this.roleStore.roles();
      const branches = this.lookupsStore.branches().length
        ? this.lookupsStore.branches()
        : this.branchStore.branches();
      const departments = this.departmentStore.departments();

      const isLoading = this.lookupsStore.loading() || this.roleStore.isLoading() || this.departmentStore.isLoading();

      if (employee && !this.formPatched() && !isLoading) {
        this.patchFormFromSelected();
        this.formPatched.set(true);
      }
    });
  }

  ngOnInit() {
    this.branchStore.loadBranch({});
    this.departmentStore.fetchDepartments();
    this.roleStore.getRoles();
    this.lookupsStore.getLookups();
    this.employeeId = Number(this.route.snapshot.paramMap.get('id'));
    this.employeeStore.loadEmployeeById(this.employeeId);
    this.employee.set(this.employeeStore.employeeDetails());
  }

  //=========================================
  //==============method=====================
  //=========================================
  goBack() {
    this.employeeStore.clearSelectedEmployee();
    this.formPatched.set(false);
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
    if (this.employeeForm().invalid()) {
      toast.error('الرجاء تعبئة كافة الحقول المطلوبة بشكل صحيح');
      return;
    }

    this.employeeStore.updateEmployee({ id: this.employeeId, data: this.employeeModel() });
    toast.success('تم تعديل بيانات الموظف بنجاح');
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

  private patchFormFromSelected() {
    const employee = this.employeeStore.employeeDetails();
    if (!employee) {
      return;
    }

    const anyEmp = employee as any;
    const nameParts = employee.fullName ? employee.fullName.split(' ') : [];
    const firstName = anyEmp.firstName || nameParts[0] || '';
    const lastName = anyEmp.lastName || nameParts.slice(1).join(' ') || '';

    const role = this.roleStore
      .roles()
      .find((item) => item.name === employee.roleName || item.id === anyEmp.roleId);
    const branches = this.lookupsStore.branches().length
      ? this.lookupsStore.branches()
      : this.branchStore.branches();
    const branch = branches.find(
      (item) => item.name === employee.branchName || item.id === anyEmp.branchId,
    );
    const department = this.departmentStore
      .departments()
      .find((item) => item.name === employee.departmentName || item.id === anyEmp.departmentId);

    const gender = anyEmp.gender !== undefined ? String(anyEmp.gender) : '0';

    this.employeeModel.update((model) => ({
      ...model,
      firstName: firstName,
      lastName: lastName,
      phoneNumber: employee.phoneNumber,
      roleId: role ? String(role.id) : '',
      branchId: branch ? String(branch.id) : '',
      departmentId: department ? String(department.id) : '',
      jobTitle: employee.jobTitle,
      salary: String(employee.salary),
      dateOfBirth: employee.birthDate ? employee.birthDate.substring(0, 10) : '',
      gender: gender,
      address: {
        street: employee.address?.street || '',
        city: employee.address?.city || '',
        country: employee.address?.country || '',
      },
      isActive: employee.isActive ? 'true' : 'false',
    }));
  }
}
