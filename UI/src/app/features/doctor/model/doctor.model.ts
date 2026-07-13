import {
  CreateEmployeeDto,
  EmployeeDetailsDto,
  EmployeeFormModel,
  UpdateEmployeeDto,
} from '../../employee/model/employee.model';

export interface DoctorDetailsDto extends EmployeeDetailsDto {
  licenseNumber: string;
  consultationFee: number;
  academicDegree: string;
  specialization: string;
  subSpecialty: string;
}

export interface CreateDoctorDto extends EmployeeFormModel {
  licenseNumber: string;
  consultationFee: number;
  academicDegree: string;
  subSpecialtyId: string;
}

export interface UpdateDoctorDto extends EmployeeFormModel {
  consultationFee: number;
}
