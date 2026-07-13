import { CreateDepartmentDtoModel } from './create-department-dto.model';

export interface UpdateDepartmentDtoModel extends CreateDepartmentDtoModel {
  isActive: boolean;
}
