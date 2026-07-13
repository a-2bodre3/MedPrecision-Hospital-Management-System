// import { Component, inject, signal, computed } from '@angular/core';
// import { HeaderComponent } from '../../../../shared/components/header/header.component';
// import { LookupsState } from '../../../../core/state/Lookups.store';
// import { PatientStore } from '../../store/patient.store';
// import { CreatePatientDto } from '../../model/patient.model';
// import { form, FormField, required } from '@angular/forms/signals';
// import { Router } from '@angular/router';
// import {toast} from '@spartan-ng/brain/sonner';
//
// @Component({
//   selector: 'app-patient-create',
//   standalone: true,
//   imports: [HeaderComponent, FormField],
//   templateUrl: './patient-create.component.html',
//   styleUrl: './patient-create.component.scss',
// })
// export class PatientCreateComponent {
//   //=======================================
//   //==============inject===================
//   //=======================================
//   protected lookupsStore = inject(LookupsState);
//   protected patientStore = inject(PatientStore);
//   private router = inject(Router);
//
//   //=======================================
//   //==============Signals & States=========
//   //=======================================
//   protected imagePreview = signal<string | null>(null);
//   // protected toast = signal<{ message: string; type: 'success' | 'error' | null }>({
//   //   message: '',
//   //   type: null
//   // });
//
//   //=======================================
//   //==============life cycle===============
//   //=======================================
//   constructor() {
//     this.lookupsStore.loadLookups();
//   }
//
//   //=======================================
//   //==============form=====================
//   //=======================================
//   protected patientModel = signal<CreatePatientDto>({
//     email: '',
//     password: '',
//     firstName: '',
//     lastName: '',
//     phoneNumber: '',
//     imageFile: null,
//     dateOfBirth: '',
//     emergencyPhoneNumber: '',
//     emergencyEmail: '',
//     allergies: [],
//     chronicDiseases: [],
//     gender: '',
//     country: '',
//     city: '',
//     street: '',
//   });
//
//   protected patientForm = form(this.patientModel, (schemaPath) => {
//     required(schemaPath.email);
//     required(schemaPath.phoneNumber);
//     required(schemaPath.firstName);
//     required(schemaPath.lastName);
//     required(schemaPath.phoneNumber);
//     required(schemaPath.imageFile);
//     required(schemaPath.dateOfBirth);
//     required(schemaPath.gender);
//     required(schemaPath.country);
//     required(schemaPath.city);
//     required(schemaPath.street);
//   });
//
//   //=======================================
//   //==============Computed Properties======
//   //=======================================
//   // تصفية القوائم لإظهار العناصر غير المختارة فقط في الـ Select
//   protected availableAllergies = computed(() => {
//     const selected = this.patientModel().allergies || [];
//     return this.lookupsStore.allergies().filter(item => !selected.includes(item.id));
//   });
//
//   protected availableChronicDiseases = computed(() => {
//     const selected = this.patientModel().chronicDiseases || [];
//     return this.lookupsStore.chronicDisease().filter(item => !selected.includes(item.id));
//   });
//
//   // جلب كائنات العناصر المختارة لعرضها كـ Tags
//   protected selectedAllergiesItems = computed(() => {
//     const selected = this.patientModel().allergies || [];
//     return this.lookupsStore.allergies().filter(item => selected.includes(item.id));
//   });
//
//   protected selectedChronicDiseasesItems = computed(() => {
//     const selected = this.patientModel().chronicDiseases || [];
//     return this.lookupsStore.chronicDisease().filter(item => selected.includes(item.id));
//   });
//
//   //=======================================
//   //==============methods==================
//   //=======================================
//
//   onFileChange(event: Event) {
//     const file = (event.target as HTMLInputElement).files?.[0] ?? null;
//     if (file) {
//       this.patientModel.update((model) => ({ ...model, imageFile: file }));
//
//       const reader = new FileReader();
//       reader.onload = () => {
//         this.imagePreview.set(reader.result as string);
//       };
//       reader.readAsDataURL(file);
//     }
//   }
//
//   removeImage() {
//     this.patientModel.update((model) => ({ ...model, imageFile: null }));
//     this.imagePreview.set(null);
//   }
//
//   addAllergy(event: Event) {
//     const select = event.target as HTMLSelectElement;
//     const id = Number(select.value);
//     if (!id) return;
//
//     this.patientModel.update((model) => {
//       const current = model.allergies || [];
//       return { ...model, allergies: [...current, id] };
//     });
//     select.value = '';
//   }
//
//   removeAllergy(id: number) {
//     this.patientModel.update((model) => ({
//       ...model,
//       allergies: (model.allergies || []).filter(item => item !== id)
//     }));
//   }
//
//   addChronicDisease(event: Event) {
//     const select = event.target as HTMLSelectElement;
//     const id = Number(select.value);
//     if (!id) return;
//
//     this.patientModel.update((model) => {
//       const current = model.chronicDiseases || [];
//       return { ...model, chronicDiseases: [...current, id] };
//     });
//     select.value = '';
//   }
//
//   removeChronicDisease(id: number) {
//     this.patientModel.update((model) => ({
//       ...model,
//       chronicDiseases: (model.chronicDiseases || []).filter(item => item !== id)
//     }));
//   }
//
//   onSubmit(event: Event) {
//     event.preventDefault();
//     if (this.patientForm().invalid() || !this.patientModel().imageFile) {
//
//       toast.error('لرجاء التأكد من ملء كافة الحقول الإجبارية وإرفاق الصورة الشخصية');
//       return;
//     }
//     const formData = this.toFormData();
//     this.patientStore.createPatient(formData);
//
//     toast.success('تم إنشاء حساب الموظف الجديد بنجاح مذهل!');
//     this.router.navigate(['/staff/patient/list']);
//   }
//
//   private toFormData() {
//     const value = this.patientModel();
//     const data = new FormData();
//     data.append('Email', value.email);
//     data.append('Password', value.password);
//     data.append('FirstName', value.firstName);
//     data.append('LastName', value.lastName);
//     data.append('PhoneNumber', value.phoneNumber);
//     data.append('ImageFile', value.imageFile as File);
//     data.append('DateOfBirth', value.dateOfBirth);
//     data.append('EmergencyPhoneNumber', value.emergencyPhoneNumber || '');
//     data.append('EmergencyEmail', value.emergencyEmail || '');
//
//     if (value.allergies && value.allergies.length > 0) {
//       value.allergies.forEach(id => data.append('Allergies', id.toString()));
//     }
//
//     if (value.chronicDiseases && value.chronicDiseases.length > 0) {
//       value.chronicDiseases.forEach(id => data.append('ChronicDiseases', id.toString()));
//     }
//     data.append('Gender', value.gender);
//     data.append('Street', value.street);
//     data.append('City', value.city);
//     data.append('Country', value.country);
//     return data;
//   }
// }
