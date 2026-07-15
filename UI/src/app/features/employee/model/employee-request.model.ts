import { EmployeeModel } from './employee.model';
import { Address } from '../../../core/model/Address.model';

export interface CreateEmployeeRequest extends Omit<EmployeeModel, 'id' | 'isActive' | 'imageUrl'> {
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  imageFile: File | null;
  dateOfBirth: string;
  gender: string;
  salary: string;
  roleId: string;
  branchId: string;
  departmentId: string;
  address: Address;
}
export interface UpdateEmployeeRequest extends Omit<CreateEmployeeRequest, 'email' | 'password'> {
  isActive: string;
}
