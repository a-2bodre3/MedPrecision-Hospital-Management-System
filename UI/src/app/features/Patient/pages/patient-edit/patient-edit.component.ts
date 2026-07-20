import { Component, effect, inject, OnInit, signal, computed } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { form, FormField, required } from '@angular/forms/signals';
import { HeaderComponent } from '../../../../shared/components/header/header.component';
import { LookupsState } from '../../../../core/state/Lookups.store';
import { PatientStore } from '../../store/patient.store';
import { toast } from '@spartan-ng/brain/sonner';
import { UpdatePatientRequest } from '../../model/patient.request.model';
import { PatientDetailsResponse } from '../../model/patient.response.model';

@Component({
  selector: 'app-patient-edit',
  standalone: true,
  imports: [HeaderComponent, FormField],
  templateUrl: './patient-edit.component.html',
  styleUrl: './patient-edit.component.scss',
})
export class PatientEditComponent implements OnInit {
  //=========================================
  //==============inject=====================
  //=========================================
  protected lookupsStore = inject(LookupsState);
  protected patientStore = inject(PatientStore);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  protected patientId = 0;
  // private formPatched = signal(false);
  protected imagePreview = signal<string | null>(null);

  //=========================================
  //==============form model=================
  //=========================================
  protected patientModel = signal<UpdatePatientRequest>({
    firstName: '',
    lastName: '',
    phoneNumber: '',
    imageFile: null,
    emergencyPhoneNumber: '',
    emergencyEmail: '',
    allergies: [],
    chronicDiseases: [],
    gender: '',
    address: {
      city: '',
      country: '',
      street: '',
    },
    isActive: '',
  });

  protected patientForm = form(this.patientModel, (schemaPath) => {
    required(schemaPath.firstName);
    required(schemaPath.lastName);
    required(schemaPath.phoneNumber);
    required(schemaPath.gender);
    required(schemaPath.address.country);
    required(schemaPath.address.city);
    required(schemaPath.address.street);
  });

  //=========================================
  //==============computed lookups===========
  //=========================================
  protected availableAllergies = computed(() => {
    const selected = this.patientModel().allergies || [];
    return this.lookupsStore.allergies().filter((item) => !selected.includes(item.id));
  });

  protected availableChronicDiseases = computed(() => {
    const selected = this.patientModel().chronicDiseases || [];
    return this.lookupsStore.chronicDisease().filter((item) => !selected.includes(item.id));
  });

  protected selectedAllergiesItems = computed(() => {
    const selected = this.patientModel().allergies || [];
    return this.lookupsStore.allergies().filter((item) => selected.includes(item.id));
  });

  protected selectedChronicDiseasesItems = computed(() => {
    const selected = this.patientModel().chronicDiseases || [];
    return this.lookupsStore.chronicDisease().filter((item) => selected.includes(item.id));
  });

  //=========================================
  //==============life cycle=================
  //=========================================
  constructor() {
    effect(() => {
      const patient = this.patientStore.PatientDetails();
      const allergies = this.lookupsStore.allergies();
      const chronicDiseases = this.lookupsStore.chronicDisease();

      if (patient && allergies.length > 0 && chronicDiseases.length > 0) {
        this.patchFormFromSelected(patient);
        // this.formPatched.set(true);
      }
    });
  }

  ngOnInit() {
    this.patientId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.patientId) {
      this.patientStore.loadPatientById(this.patientId);
    }
  }

  //=========================================
  //==============methods====================
  //=========================================
  goBack() {
    this.patientStore.clearSelectedPatient();
    this.router.navigate(['/staff/patient/list']);
  }

  onFileChange(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0] ?? null;
    if (file) {
      this.patientModel.update((model) => ({ ...model, imageFile: file }));

      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview.set(reader.result as string);
      };
      reader.readAsDataURL(file);
    }
  }

  removeImage() {
    this.patientModel.update((model) => ({ ...model, imageFile: null }));
    this.imagePreview.set(null);
  }

  addAllergy(event: Event) {
    const select = event.target as HTMLSelectElement;
    const id = Number(select.value);
    if (!id) return;

    this.patientModel.update((model) => {
      const current = model.allergies || [];
      return { ...model, allergies: [...current, id] };
    });
    select.value = '';
  }

  removeAllergy(id: number) {
    this.patientModel.update((model) => ({
      ...model,
      allergies: (model.allergies || []).filter((item) => item !== id),
    }));
  }

  addChronicDisease(event: Event) {
    const select = event.target as HTMLSelectElement;
    const id = Number(select.value);
    if (!id) return;

    this.patientModel.update((model) => {
      const current = model.chronicDiseases || [];
      return { ...model, chronicDiseases: [...current, id] };
    });
    select.value = '';
  }

  removeChronicDisease(id: number) {
    this.patientModel.update((model) => ({
      ...model,
      chronicDiseases: (model.chronicDiseases || []).filter((item) => item !== id),
    }));
  }

  onSubmit(event: Event) {
    event.preventDefault();
    if (this.patientForm().invalid()) {
      toast.error('الرجاء التأكد من ملء كافة الحقول الإجبارية بشكل صحيح');
      return;
    }

    const data = this.patientModel();
    this.patientStore.updatePatient({ id: this.patientId, data: data });

    this.goBack();
  }

  private patchFormFromSelected(patient: PatientDetailsResponse) {
    const nameParts = patient.fullName.split(' ');
    const firstName = nameParts[0] || '';
    const lastName = nameParts.slice(1).join(' ') || '';

    const selectedAllergyIds = this.lookupsStore
      .allergies()
      .filter((item) => patient.allergies?.includes(item.name))
      .map((item) => item.id);

    const selectedChronicDiseaseIds = this.lookupsStore
      .chronicDisease()
      .filter((item) => patient.chronicDiseases?.includes(item.name))
      .map((item) => item.id);

    console.log(patient);

    this.patientModel.update((model) => ({
      ...model,
      firstName,
      lastName,
      phoneNumber: patient.phoneNumber,
      imageFile: null,
      emergencyPhoneNumber: patient.emergencyPhoneNumber || '',
      emergencyEmail: patient.emergencyEmail || '',
      allergies: selectedAllergyIds,
      chronicDiseases: selectedChronicDiseaseIds,
      gender: patient.gender === 'Mail' ? '1' : '2',
      address: {
        country: patient.address?.country || '',
        city: patient.address?.city || '',
        street: patient.address?.street || '',
      },
      isActive: patient.isActive ? 'true' : 'false',
    }));

    if (patient.imageUrl) {
      this.imagePreview.set('https://localhost:7042/images/' + patient.imageUrl);
    }
  }
}
