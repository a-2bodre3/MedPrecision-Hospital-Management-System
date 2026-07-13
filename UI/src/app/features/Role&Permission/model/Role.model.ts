export interface RoleModel {
  id: number;
  name: string;
}

export interface PermissionModel {
  id: number;
  token: string;
  description: string;
  module: string;
}
export interface RolePermissionsResponse extends RoleModel {
  permissions: PermissionModel[];
}
