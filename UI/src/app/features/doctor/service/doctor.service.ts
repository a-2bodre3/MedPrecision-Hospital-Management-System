import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { CreateDoctorDto, DoctorDetailsDto, UpdateDoctorDto } from '../model/doctor.model';
import { EmployeeDto } from '../../employee/model/employee.model';

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
  getDoctorById(id: number): Observable<DoctorDetailsDto> {
    return this.http.get<DoctorDetailsDto>(`${this.apiUrl}/getDoctor/${id}`);
  }
  createDoctor(data: CreateDoctorDto): Observable<EmployeeDto> {
    return this.http.post<EmployeeDto>(`${this.apiUrl}/createDoctor`, data);
  }
  updateDoctor(id: number, data: FormData): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/updateDoctor/${id}`, data);
  }
  changePassword(id: number, password: string): Observable<boolean> {
    return this.http.put<boolean>(
      `${this.apiUrl}/updateDoctorPassword/${id}`,
      JSON.stringify(password),
      {
        headers: { 'Content-Type': 'application/json' },
      },
    );
  }
  getSpecialization(): Observable<{ id: number; name: string }[]> {
    return this.http.get<{ id: number; name: string }[]>(
      `${environment.baseApi}/Specialization/getSpecializations`,
    );
  }
}
