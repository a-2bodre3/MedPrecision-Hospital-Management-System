import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { PagedResult } from '../../../core/model/pagination.model';
import {
  EmployeeDetailsResponse,
  EmployeeQuery,
  EmployeeResponse,
} from '../model/employee-response.model';
import { CreateEmployeeRequest, UpdateEmployeeRequest } from '../model/employee-request.model';
import { toFormData } from '../../../core/utils/form-data.util';

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
  getEmployees(query: EmployeeQuery): Observable<PagedResult<EmployeeResponse>> {
    let httpParams = new HttpParams()
      .set('PageNumber', query.pageNumber)
      .set('PageSize', query.pageSize);

    if (query.searchTerm) {
      httpParams = httpParams.set('searchTerm', query.searchTerm);
    }
    if (query.isActive !== undefined && query.isActive !== null) {
      httpParams = httpParams.set('isActive', query.isActive.toString());
    }
    if (query.departmentId) {
      httpParams = httpParams.set('departmentId', query.departmentId.toString());
    }

    return this.http.get<PagedResult<EmployeeResponse>>(`${this.apiUrl}`, {
      params: httpParams,
    });
  }

  getEmployeeById(id: number): Observable<EmployeeDetailsResponse> {
    return this.http.get<EmployeeDetailsResponse>(`${this.apiUrl}/${id}`);
  }

  createEmployee(data: CreateEmployeeRequest): Observable<boolean> {
    const formData = toFormData(data);
    return this.http.post<boolean>(`${this.apiUrl}`, formData);
  }

  updateEmployee(id: number, data: UpdateEmployeeRequest): Observable<boolean> {
    const formData = toFormData(data);
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, formData);
  }

  changePassword(id: number, password: string): Observable<boolean> {
    return this.http.patch<boolean>(`${this.apiUrl}/ChangePassword/${id}`, {
      password: password,
    });
  }

  deleteEmployee(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}
