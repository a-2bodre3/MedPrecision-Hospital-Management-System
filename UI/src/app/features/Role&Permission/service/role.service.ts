import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { PermissionModel, RoleModel, RolePermissionsResponse } from '../model/Role.model';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  //================================================
  //===================inject=======================
  //================================================
  private http = inject(HttpClient);

  //================================================
  //===================variable=====================
  //================================================
  private apiUrl: string = `${environment.baseApi}/Role`;

  //================================================
  //===================method=======================
  //================================================
  getRoles(): Observable<RoleModel[]> {
    return this.http.get<RoleModel[]>(`${this.apiUrl}`);
  }
  createRole(name: string): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}`, { name });
  }

  getPermissions(): Observable<PermissionModel[]> {
    return this.http.get<PermissionModel[]>(`${this.apiUrl}/GetPermissions`);
  }

  getRolePermission(id: number): Observable<RolePermissionsResponse> {
    return this.http.get<RolePermissionsResponse>(`${this.apiUrl}/${id}/permissions`);
  }

  updateRole(id: number, name: string): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, { name });
  }
  deleteRole(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }

  updateRolePermissions(id: number, permissions: number[]): Observable<boolean> {
    const body = {
      roleId: id,
      permissionIds: permissions,
    };
    return this.http.patch<boolean>(`${this.apiUrl}/${id}`, body);
  }
}
