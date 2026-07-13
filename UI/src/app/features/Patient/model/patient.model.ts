export interface PatientDto {
  id: number;
  email: string;
  fullName: string;
  imageUrl: string;
  patientCode: string;
  isActive: boolean;
  phoneNumber: string;
}

export interface PatientDetailsDto extends PatientDto, Address {
  createdAt: string;
  dateOfBirth: string;
  gender: string;
  emergencyPhoneNumber?: string;
  emergencyEmail?: string;
  allergies?: string[];
  chronicDiseases?: string[];
}

export interface CreatePatientDto extends Address {
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
  gender: string;
}

export interface UpdatePatientDto extends Omit<
  CreatePatientDto,
  'email' | 'password' | 'dateOfBirth'
> {
  isActive: boolean;
}

export interface PatientQueryParameters {
  searchTerm?: string | null;
  pageNumber: number;
  pageSize: number;
}

interface Address {
  country: string;
  city: string;
  street: string;
}
