import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { EmployeeDetailsDto, EmployeeDto, EmployeeQueryParameters } from '../model/employee.model';
import { PagedResult } from '../../../core/model/pagination.model';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  //================================================
  //===================inject=======================
  //================================================
  private http = inject(HttpClient);

  //================================================
  //===================variable=====================
  //================================================
  private apiUrl: string = `${environment.baseApi}/Employee`;

  //================================================
  //===================method=======================
  //================================================
  getEmployees(
    params: EmployeeQueryParameters,
    includeInactive: boolean,
  ): Observable<PagedResult<EmployeeDto>> {
    let httpParams = new HttpParams()
      .set('PageNumber', params.pageNumber)
      .set('PageSize', params.pageSize)
      .set('includeInactive', includeInactive);

    if (params.searchTerm) {
      httpParams = httpParams.set('SearchTerm', params.searchTerm);
    }
    if (params.roleId) {
      httpParams = httpParams.set('RoleId', params.roleId);
    }
    if (params.departmentId) {
      httpParams = httpParams.set('DepartmentId', params.departmentId);
    }

    return this.http.get<PagedResult<EmployeeDto>>(`${this.apiUrl}/GetAllEmployees`, {
      params: httpParams,
    });
  }

  getEmployeeById(id: number): Observable<EmployeeDetailsDto> {
    return this.http.get<EmployeeDetailsDto>(`${this.apiUrl}/getEmployee/${id}`);
  }

  createEmployee(data: FormData): Observable<EmployeeDetailsDto> {
    return this.http.post<EmployeeDetailsDto>(`${this.apiUrl}/createEmployee`, data);
  }

  updateEmployee(id: number, data: FormData): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/updateEmployee/${id}`, data);
  }

  changePassword(id: number, password: string): Observable<boolean> {
    return this.http.patch<boolean>(
      `${this.apiUrl}/updateEmployeePassword/${id}`,
      JSON.stringify(password),
      {
        headers: { 'Content-Type': 'application/json' },
      },
    );
  }

  deleteEmployee(id: number): Observable<boolean> {
    return this.http.patch<boolean>(`${this.apiUrl}/deleteUser/${id}`, {});
  }
}
