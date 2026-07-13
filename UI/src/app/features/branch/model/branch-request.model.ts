import { BranchModel } from './branch.model';
import { Address } from '../../../core/model/Address.model';

export interface CreateBranchRequest extends Omit<BranchModel, 'isActive'> {
  phoneNumber: string;
  address: Address;
}

export interface UpdateBranchRequest extends Omit<BranchModel, 'code'> {
  phoneNumber: string;
  address: Address;
}
