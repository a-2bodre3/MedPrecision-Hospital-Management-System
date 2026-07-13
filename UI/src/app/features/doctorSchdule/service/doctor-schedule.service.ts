import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { DoctorScheduleDisplayDTO, DoctorScheduleFormDTO } from '../model/doctorSchedule.model';
import { Observable } from 'rxjs';

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

  getDoctorSchedule(doctorId?: number): Observable<DoctorScheduleDisplayDTO[]> {
    let params = new HttpParams();

    if (doctorId) {
      params = params.set('doctorId', doctorId.toString());
    }

    return this.http.get<DoctorScheduleDisplayDTO[]>(`${this.apiUrl}`, { params });
  }

  deleteDoctorSchedule(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}
