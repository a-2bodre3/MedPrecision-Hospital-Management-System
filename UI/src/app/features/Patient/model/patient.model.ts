export interface PatientDto {
  id: number;
  email: string;
  fullName: string;
  imageUrl: string;
  patientCode: string;
  isActive: boolean;
  phoneNumber: string;
}

export interface PatientDetailsDto extends PatientDto {
  createdAt: string;
  dateOfBirth: string;
  gender: number;
  emergencyPhoneNumber?: string;
  emergencyEmail?: string;
  address: {
    street: string;
    city: string;
    country: string;
  };
  allergies?: string[];
  chronicDiseases?: string[];
}

export interface CreatePatientDto {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  imageFile: File | null;
  dateOfBirth: string;
  emergencyPhoneNumber?: string;
  emergencyEmail?: string;
  allergies?: number[];
  chronicDiseases?: number[];
  gender: number;
  street: string;
  city: string;
  country: string;
}

export interface UpdatePatientDto {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  isActive: boolean;
  imageFile?: File | null;
  gender: number;
  street: string;
  city: string;
  country: string;
  emergencyPhoneNumber?: string;
  emergencyEmail?: string;
  allergies?: number[];
  chronicDiseases?: number[];
}

export interface PatientQueryParameters {
  searchTerm?: string | null;
  isActive?: boolean | null;
  pageNumber: number;
  pageSize: number;
}

export type PatientFormModel = {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  imageFile: File | null;
  dateOfBirth: string;
  gender: string;
  street: string;
  city: string;
  country: string;
  isActive: string;
  emergencyPhoneNumber: string;
  emergencyEmail: string;
};
