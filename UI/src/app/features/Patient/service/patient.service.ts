import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { PatientDetailsDto, PatientDto, PatientQueryParameters } from '../model/patient.model';
import { Observable } from 'rxjs';
import { PagedResult } from '../../../core/model/pagination.model';

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
  private apiUrl: string = `${environment.baseApi}/Patients`;

  //================================================
  //===================method=======================
  //================================================

  getPatients(params: PatientQueryParameters): Observable<PagedResult<PatientDto>> {
    let httpParams = new HttpParams()
      .set('PageNumber', params.pageNumber || 1)
      .set('PageSize', params.pageSize || 10);

    if (params.searchTerm) {
      httpParams = httpParams.set('SearchTerm', params.searchTerm);
    }
    return this.http.get<PagedResult<PatientDto>>(`${this.apiUrl}`, {
      params: httpParams,
    });
  }

  getPatientById(id: number): Observable<PatientDetailsDto> {
    return this.http.get<PatientDetailsDto>(`${this.apiUrl}/${id}`);
  }

  createPatient(data: FormData): Observable<PatientDto> {
    return this.http.post<PatientDto>(`${this.apiUrl}`, data);
  }

  updatePatient(id: number, data: FormData): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, data);
  }
  changePassword(id: number, password: string): Observable<boolean> {
    return this.http.patch<boolean>(`${this.apiUrl}/${id}/change-password`, password);
  }
  deletePatient(id: number): Observable<boolean> {
    return this.http.patch<boolean>(`${this.apiUrl}/delete/${id}`, {});
  }
}
