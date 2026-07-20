import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { PagedResult } from '../../../core/model/pagination.model';
import {
  PatientDetailsResponse,
  PatientQuery,
  PatientResponse,
} from '../model/patient.response.model';
import { CreatePatientRequest, UpdatePatientRequest } from '../model/patient.request.model';
import { toFormData } from '../../../core/utils/form-data.util';

@Injectable({
  providedIn: 'root',
})
export class PatientService {
  //================================================
  //===================inject=======================
  //================================================
  private http = inject(HttpClient);

  //================================================
  //===================variable=====================
  //================================================
  private apiUrl: string = `${environment.baseApi}/Patient`;

  //================================================
  //===================method=======================
  //================================================

  getPatients(query: PatientQuery): Observable<PagedResult<PatientResponse>> {
    let httpParams = new HttpParams()
      .set('PageNumber', query.pageNumber || 1)
      .set('PageSize', query.pageSize || 10);

    if (query.searchTerm) {
      httpParams = httpParams.set('SearchTerm', query.searchTerm);
    }
    if (query.isActive !== undefined && query.isActive !== null) {
      httpParams = httpParams.set('IsActive', query.isActive);
    }
    return this.http.get<PagedResult<PatientResponse>>(`${this.apiUrl}`, {
      params: httpParams,
    });
  }

  getPatientById(id: number): Observable<PatientDetailsResponse> {
    return this.http.get<PatientDetailsResponse>(`${this.apiUrl}/${id}`);
  }

  createPatient(data: CreatePatientRequest): Observable<boolean> {
    const formData = toFormData(data);
    return this.http.post<boolean>(`${this.apiUrl}`, formData);
  }

  updatePatient(id: number, data: UpdatePatientRequest): Observable<boolean> {
    const formData = toFormData(data);
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, formData);
  }

  changePassword(id: number, password: string): Observable<boolean> {
    return this.http.patch<boolean>(
      `${this.apiUrl}/ChangePassword/${id}`,
      JSON.stringify(password),
      {
        headers: { 'Content-Type': 'application/json' },
      },
    );
  }

  deletePatient(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}
