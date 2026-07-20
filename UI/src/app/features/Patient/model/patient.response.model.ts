import { QueryParams } from '../../../core/model/pagination.model';
import { PatientModel } from './patient.model';
import { Address } from '../../../core/model/Address.model';

export interface PatientResponse extends PatientModel {}

export interface PatientDetailsResponse extends PatientResponse {
  createdAt: string;
  dateOfBirth: string;
  gender: string;
  emergencyPhoneNumber?: string;
  emergencyEmail?: string;
  address: Address;
  allergies?: string[];
  chronicDiseases?: string[];
}

export interface PatientQuery extends QueryParams {
  searchTerm?: string;
  isActive?: boolean;
}
