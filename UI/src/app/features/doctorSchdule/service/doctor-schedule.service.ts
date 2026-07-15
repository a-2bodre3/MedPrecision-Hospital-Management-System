import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import {
  AdjustScheduleValidityDto,
  DoctorScheduleDetailsDTO,
  DoctorScheduleDisplayDTO,
  DoctorScheduleFormDTO,
} from '../model/doctorSchedule.model';
import { Observable } from 'rxjs';
import { PagedResult } from '../../../core/model/pagination.model';

@Injectable({
  providedIn: 'root',
})
export class DoctorScheduleService {
  //================================================
  //===================inject=======================
  //================================================
  private http = inject(HttpClient);

  //================================================
  //===================variable=====================
  //================================================
  private apiUrl: string = `${environment.baseApi}/DoctorSchedule`;

  //================================================
  //===================method=======================
  //================================================
  createDoctorSchedule(data: DoctorScheduleFormDTO): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}`, data);
  }

  updateDoctorSchedule(id: number, data: DoctorScheduleFormDTO): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, data);
  }

  getDoctorSchedules(
    pageNumber = 1,
    pageSize = 10,
    specializationId?: number,
  ): Observable<PagedResult<DoctorScheduleDisplayDTO>> {
    let params = new HttpParams()
      .set('PageNumber', pageNumber)
      .set('PageSize', pageSize);

    if (specializationId) {
      params = params.set('SpecializationId', specializationId);
    }

    return this.http.get<PagedResult<DoctorScheduleDisplayDTO>>(`${this.apiUrl}`, { params });
  }

  getDoctorScheduleById(id: number): Observable<DoctorScheduleDetailsDTO> {
    return this.http.get<DoctorScheduleDetailsDTO>(`${this.apiUrl}/${id}`);
  }

  adjustScheduleValidity(id: number, data: AdjustScheduleValidityDto): Observable<boolean> {
    return this.http.patch<boolean>(`${this.apiUrl}/${id}`, data);
  }

  deleteDoctorSchedule(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}
