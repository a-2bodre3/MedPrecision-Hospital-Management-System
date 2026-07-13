import { BranchModel } from './branch.model';
import { Address, AddressType } from '../../../core/model/Address.model';

export interface BranchListItem extends BranchModel {
  id: number;
}
export interface BranchDetails extends BranchListItem {
  phoneNumber: string;
  addressType: AddressType;
  address: Address;
}
