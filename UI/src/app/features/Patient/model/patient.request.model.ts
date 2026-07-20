import { PatientModel } from './patient.model';
import { Address } from '../../../core/model/Address.model';

export interface CreatePatientRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  imageFile: File | null;
  dateOfBirth: string;
  gender: string;
  emergencyPhoneNumber?: string;
  emergencyEmail?: string;
  allergies: number[];
  chronicDiseases: number[];
  address: Address;
}
export interface UpdatePatientRequest extends Omit<
  CreatePatientRequest,
  'id' | 'email' | 'password' | 'dateOfBirth'
> {
  isActive: string;
}
