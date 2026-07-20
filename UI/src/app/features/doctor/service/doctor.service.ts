import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { DoctorDetailsResponse, DoctorQuery, DoctorResponse } from '../model/doctor-response.model';
import { PagedResult } from '../../../core/model/pagination.model';
import { CreateDoctorRequest, UpdateDoctorRequest } from '../model/doctor-request.model';
import { toFormData } from '../../../core/utils/form-data.util';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  //================================================
  //===================inject=======================
  //================================================
  private http = inject(HttpClient);

  //================================================
  //===================variable=====================
  //================================================
  private apiUrl: string = `${environment.baseApi}/Doctor`;

  //================================================
  //===================method=======================
  //================================================
  getDoctors(query: DoctorQuery): Observable<PagedResult<DoctorResponse>> {
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

    return this.http.get<PagedResult<DoctorResponse>>(`${this.apiUrl}`, {
      params: httpParams,
    });
  }

  getDoctorById(id: number): Observable<DoctorDetailsResponse> {
    return this.http.get<DoctorDetailsResponse>(`${this.apiUrl}/${id}`);
  }

  createDoctor(data: CreateDoctorRequest): Observable<boolean> {
    const formDate = toFormData(data);
    return this.http.post<boolean>(`${this.apiUrl}`, formDate);
  }

  updateDoctor(id: number, data: UpdateDoctorRequest): Observable<boolean> {
    const formData = toFormData(data);
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, formData);
  }

  changePassword(id: number, password: string): Observable<boolean> {
    return this.http.patch<boolean>(`${this.apiUrl}/ChangePassword/${id}`, {
      password: password,
    });
  }
}
