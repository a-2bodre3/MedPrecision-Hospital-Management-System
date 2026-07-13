export interface Address {
  city: string;
  country: string;
  street: string;
  addressType?: string;
}
export enum AddressType {
  Home,
  BranchLocation,
  WarehouseLocation,
}
