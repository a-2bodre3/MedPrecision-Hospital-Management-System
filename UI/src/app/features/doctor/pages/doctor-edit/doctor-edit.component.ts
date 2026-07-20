import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { DoctorStore } from '../../store/doctor.store';
import { BranchStore } from '../../../branch/state/branch.store';
import { DepartmentStore } from '../../../Department/state/department.store';
import { RoleStore } from '../../../Role&Permission/state/role.store';
import { ActivatedRoute, Router } from '@angular/router';
import { form, FormField, required } from '@angular/forms/signals';
import { toast } from '@spartan-ng/brain/sonner';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { HlmButton } from '@spartan-ng/helm/button';
import { LucideArrowRight, LucideSave } from '@lucide/angular';
import { UpdateDoctorRequest } from '../../model/doctor-request.model';
import { LookupsState } from '../../../../core/state/Lookups.store';

@Component({
  selector: 'app-doctor-edit',
  standalone: true,
  imports: [HeaderComponent, HlmButton, LucideArrowRight, LucideSave, FormField],
  templateUrl: './doctor-edit.component.html',
  styleUrl: './doctor-edit.component.scss',
})
export class DoctorEditComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected doctorStore = inject(DoctorStore);
  protected branchStore = inject(BranchStore);
  protected departmentStore = inject(DepartmentStore);
  protected roleStore = inject(RoleStore);
  protected lookupsStore = inject(LookupsState);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  protected doctorId = 0;
  private formPatched = signal(false);

  //=========================================
  //==============form=======================
  //=========================================
  protected doctorModel = signal<UpdateDoctorRequest & { imageFile: File | null }>({
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
    gender: '1',
    address: {
      street: '',
      city: '',
      country: '',
    },
    isActive: 'true',
    consultationFee: 0,
  });

  protected doctorForm = form(this.doctorModel, (schemaPath) => {
    required(schemaPath.firstName);
    required(schemaPath.lastName);
    required(schemaPath.phoneNumber);
    required(schemaPath.roleId);
    required(schemaPath.branchId);
    required(schemaPath.jobTitle);
    required(schemaPath.departmentId);
    required(schemaPath.salary);
    required(schemaPath.dateOfBirth);
    required(schemaPath.gender);
    required(schemaPath.address.street);
    required(schemaPath.address.city);
    required(schemaPath.address.country);
    required(schemaPath.consultationFee);
  });

  //=========================================
  //==============life cycle=================
  //=========================================
  constructor() {
    effect(() => {
      const doctor = this.doctorStore.doctorDetails();
      if (doctor && !this.formPatched()) {
        this.patchFormFromSelected();
        this.formPatched.set(true);
      }
    });
  }

  async ngOnInit() {
    this.doctorId = Number(this.route.snapshot.paramMap.get('id'));
    await this.branchStore.loadBranch({});
    await this.departmentStore.fetchDepartments();
    this.doctorStore.loadDoctorById(this.doctorId);
  }

  //=========================================
  //==============method=====================
  //=========================================
  goBack() {
    this.doctorStore.clearSelectedDoctor();
    this.formPatched.set(false);
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
      toast.error('الرجاء تعبئة كافة الحقول المطلوبة بشكل صحيح');
      return;
    }
    
    // Convert string isActive to proper format or leave it if backend expects string
    const data: UpdateDoctorRequest = this.doctorModel();
    this.doctorStore.updateDoctor({ id: this.doctorId, data });
    toast.success('تم تعديل بيانات الطبيب بنجاح');
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

  private patchFormFromSelected() {
    const doctor = this.doctorStore.doctorDetails();
    if (!doctor) {
      return;
    }
    const [firstName, ...rest] = doctor.fullName.split(' ');
    
    // We assume backend sends IDs for branch, role, department or we match by name if needed.
    // Assuming backend returns doctorDetails similar to EmployeeDetailsResponse
    // For simplicity, we assign empty string if undefined and let the user select if not matched exactly.
    // If the backend returns branchName instead of ID, you might need to find the ID.
    const role = this.lookupsStore.roles().find((item) => item.name === doctor.roleName);
    const branch = this.lookupsStore.branches().find((item) => item.name === doctor.branchName);
    const department = this.departmentStore.departments().find((item) => item.name === doctor.departmentName);

    this.doctorModel.update((model) => ({
      ...model,
      firstName: firstName || '',
      lastName: rest.join(' '),
      phoneNumber: doctor.phoneNumber,
      roleId: role ? String(role.id) : '',
      branchId: branch ? String(branch.id) : '',
      departmentId: department ? String(department.id) : '',
      jobTitle: doctor.jobTitle,
      salary: String(doctor.salary),
      dateOfBirth: doctor.birthDate ? doctor.birthDate.substring(0, 10) : '',
      address: {
        street: doctor.address?.street || '',
        city: doctor.address?.city || '',
        country: doctor.address?.country || '',
      },
      isActive: doctor.isActive ? 'true' : 'false',
      consultationFee: doctor.consultationFee || 0,
      gender: '1',
    }));
  }
}
