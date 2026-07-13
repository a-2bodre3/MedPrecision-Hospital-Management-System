// import { Component, effect, inject, OnInit, signal, computed } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { form, FormField, required } from '@angular/forms/signals';
// import { HeaderComponent } from '../../../../shared/components/header/header.component';
// import { LookupsState } from '../../../../core/state/Lookups.store';
// import { PatientStore } from '../../store/patient.store';
// import { PatientDetailsDto } from '../../model/patient.model';
// import { toast } from '@spartan-ng/brain/sonner';
//
// interface UpdatePatientFormModel {
//   firstName: string;
//   lastName: string;
//   phoneNumber: string;
//   imageFile: File | null;
//   emergencyPhoneNumber?: string;
//   emergencyEmail?: string;
//   allergies?: number[];
//   chronicDiseases?: number[];
//   gender: string;
//   country: string;
//   city: string;
//   street: string;
//   isActive: string;
// }
//
// @Component({
//   selector: 'app-patient-edit',
//   standalone: true,
//   imports: [HeaderComponent, FormField],
//   templateUrl: './patient-edit.component.html',
//   styleUrl: './patient-edit.component.scss',
// })
// export class PatientEditComponent implements OnInit {
//   //=========================================
//   //==============inject=====================
//   //=========================================
//   protected lookupsStore = inject(LookupsState);
//   protected patientStore = inject(PatientStore);
//   private route = inject(ActivatedRoute);
//   private router = inject(Router);
//
//   protected patientId = 0;
//   private formPatched = signal(false);
//   protected imagePreview = signal<string | null>(null);
//
//   //=========================================
//   //==============form model=================
//   //=========================================
//   protected patientModel = signal<UpdatePatientFormModel>({
//     firstName: '',
//     lastName: '',
//     phoneNumber: '',
//     imageFile: null,
//     emergencyPhoneNumber: '',
//     emergencyEmail: '',
//     allergies: [],
//     chronicDiseases: [],
//     gender: '',
//     country: '',
//     city: '',
//     street: '',
//     isActive: 'true',
//   });
//
//   protected patientForm = form(this.patientModel, (schemaPath) => {
//     required(schemaPath.firstName);
//     required(schemaPath.lastName);
//     required(schemaPath.phoneNumber);
//     required(schemaPath.gender);
//     required(schemaPath.country);
//     required(schemaPath.city);
//     required(schemaPath.street);
//   });
//
//   //=========================================
//   //==============computed lookups===========
//   //=========================================
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
//   //=========================================
//   //==============life cycle=================
//   //=========================================
//   constructor() {
//     effect(() => {
//       const patient = this.patientStore.selectedPatient();
//       const allergies = this.lookupsStore.allergies();
//       const chronicDiseases = this.lookupsStore.chronicDisease();
//
//       if (patient && allergies.length > 0 && chronicDiseases.length > 0 && !this.formPatched()) {
//         this.patchFormFromSelected(patient);
//         this.formPatched.set(true);
//       }
//     });
//   }
//
//   ngOnInit() {
//     this.lookupsStore.loadLookups();
//     this.patientId = Number(this.route.snapshot.paramMap.get('id'));
//     if (this.patientId) {
//       this.patientStore.loadPatientById(this.patientId);
//     }
//   }
//
//   //=========================================
//   //==============methods====================
//   //=========================================
//   goBack() {
//     this.patientStore.clearSelectedPatient();
//     this.formPatched.set(false);
//     this.router.navigate(['/staff/patient/list']);
//   }
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
//       allergies: (model.allergies || []).filter(item => item !== id),
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
//       chronicDiseases: (model.chronicDiseases || []).filter(item => item !== id),
//     }));
//   }
//
//   onSubmit(event: Event) {
//     event.preventDefault();
//     if (this.patientForm().invalid()) {
//       toast.error('الرجاء التأكد من ملء كافة الحقول الإجبارية بشكل صحيح');
//       return;
//     }
//
//     const formData = this.toFormData();
//     this.patientStore.updatePatient({ id: this.patientId, data: formData });
//     toast.success('تم تعديل بيانات المريض بنجاح');
//     this.goBack();
//   }
//
//   private patchFormFromSelected(patient: PatientDetailsDto) {
//     const nameParts = patient.fullName.split(' ');
//     const firstName = nameParts[0] || '';
//     const lastName = nameParts.slice(1).join(' ') || '';
//
//     const selectedAllergyIds = this.lookupsStore.allergies()
//       .filter((item) => patient.allergies?.includes(item.name))
//       .map((item) => item.id);
//
//     const selectedChronicDiseaseIds = this.lookupsStore.chronicDisease()
//       .filter((item) => patient.chronicDiseases?.includes(item.name))
//       .map((item) => item.id);
//
//     this.patientModel.update((model) => ({
//       ...model,
//       firstName,
//       lastName,
//       phoneNumber: patient.phoneNumber,
//       imageFile: null,
//       emergencyPhoneNumber: patient.emergencyPhoneNumber || '',
//       emergencyEmail: patient.emergencyEmail || '',
//       allergies: selectedAllergyIds,
//       chronicDiseases: selectedChronicDiseaseIds,
//       gender: patient.gender ? String(patient.gender) : '',
//       country: patient.country || '',
//       city: patient.city || '',
//       street: patient.street || '',
//       isActive: patient.isActive ? 'true' : 'false',
//     }));
//
//     if (patient.imageUrl) {
//       this.imagePreview.set('https://localhost:7042' + patient.imageUrl);
//     }
//   }
//
//   private toFormData() {
//     const value = this.patientModel();
//     const data = new FormData();
//     data.append('FirstName', value.firstName);
//     data.append('LastName', value.lastName);
//     data.append('PhoneNumber', value.phoneNumber);
//     if (value.imageFile) {
//       data.append('ImageFile', value.imageFile);
//     }
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
//     data.append('IsActive', value.isActive);
//     return data;
//   }
// }
