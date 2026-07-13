export interface EmployeeDto {
  id: number;
  email: string;
  fullName: string;
  imageUrl: string;
  jobTitle: string;
  isActive: boolean;
  departmentName: string;
}

export interface EmployeeDetailsDto extends EmployeeDto {
  phoneNumber: string;
  createdAt: string;
  roleName: string;
  branchName: string;
  salary: number;
  birthDate: string | null;
  hireDate: string | null;
  street: string;
  city: string;
  country: string;
}

export interface CreateEmployeeDto {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  imageFile: File | Blob;
  roleId: number;
  branchId: number;
  jobTitle: string;
  departmentId: number;
  salary: number;
  dateOfBirth: string;
  gender: string;
  street: string;
  city: string;
  country: string;
}

export interface UpdateEmployeeDto {
  firstName: string | null;
  lastName: string | null;
  phoneNumber: string | null;
  imageFile?: File | Blob | null;
  roleId: number;
  branchId: number;
  jobTitle: string;
  departmentId: number;
  isActive: boolean;
  salary: number;
  dateOfBirth: string;
  gender: string;
  street: string;
  city: string;
  country: string;
}

export interface EmployeeQueryParameters {
  searchTerm?: string | null;
  roleId?: number | null;
  departmentId?: number | null;
  pageNumber: number;
  pageSize: number;
}

export type EmployeeFormModel = {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  imageFile: File | null;
  roleId: string;
  branchId: string;
  jobTitle: string;
  departmentId: string;
  salary: string;
  dateOfBirth: string;
  gender: string;
  street: string;
  city: string;
  country: string;
  isActive: string;
};
