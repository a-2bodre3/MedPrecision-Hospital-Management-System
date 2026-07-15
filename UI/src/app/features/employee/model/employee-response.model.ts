import { EmployeeModel } from './employee.model';
import { Address } from '../../../core/model/Address.model';
import { QueryParams } from '../../../core/model/pagination.model';

export interface EmployeeResponse extends EmployeeModel {
  departmentName: string;
  fullName: string;
}

export interface EmployeeDetailsResponse extends EmployeeResponse {
  phoneNumber: string;
  createdAt: string;
  roleName: string;
  branchName: string;
  salary: number;
  birthDate: string;
  hireDate: string;
  address: Address;
}

export interface EmployeeQuery extends QueryParams {
  searchTerm?: string;
  isActive?: boolean;
  departmentId?: number;
}
