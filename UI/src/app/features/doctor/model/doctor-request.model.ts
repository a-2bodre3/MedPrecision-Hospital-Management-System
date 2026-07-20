import { DoctorModel } from './doctor.model';
import { Address } from '../../../core/model/Address.model';
import { AcademicDegree } from '../../../core/enum/AcademicDegree.enum';
import { UpdateEmployeeRequest } from '../../employee/model/employee-request.model';

export interface CreateDoctorRequest extends Omit<DoctorModel, 'id' | 'isActive' | 'imageUrl'> {
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  imageFile?: File | null;
  dateOfBirth: string;
  gender: string;
  salary: string;
  roleId: string;
  branchId: string;
  departmentId: string;
  address: Address;
  licenseNumber: string;
  consultationFee: number;
  degree: AcademicDegree;
  subSpecialtyId: string;
}

export interface UpdateDoctorRequest extends Omit<UpdateEmployeeRequest, 'email' | 'password'> {
  consultationFee: number;
}
