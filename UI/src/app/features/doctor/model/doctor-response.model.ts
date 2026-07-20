import { DoctorModel } from './doctor.model';
import { Address } from '../../../core/model/Address.model';
import { QueryParams } from '../../../core/model/pagination.model';

export interface DoctorResponse extends DoctorModel {
  departmentName: string;
  fullName: string;
}

export interface DoctorDetailsResponse extends DoctorResponse {
  phoneNumber: string;
  createdAt: string;
  roleName: string;
  branchName: string;
  salary: number;
  birthDate: string;
  hireDate: string;
  address: Address;
  licenseNumber: string;
  consultationFee: number;
  degree: string;
  specialization: string;
  subSpecialty: string;
}
export interface DoctorQuery extends QueryParams {
  searchTerm?: string;
  isActive?: boolean;
  departmentId?: number;
}
