// import { inject, Injectable } from '@angular/core';
// import { HttpClient, HttpParams } from '@angular/common/http';
// import { environment } from '../../../../environments/environment.development';
// import { Observable } from 'rxjs';
// import { DoctorDetailsDto } from '../model/doctor.model';
// import { EmployeeDto } from '../../employee/model/employee.model';
// import { PagedResult } from '../../../core/model/pagination.model';
//
// @Injectable({
//   providedIn: 'root',
// })
// export class DoctorService {
//   //================================================
//   //===================inject=======================
//   //================================================
//   private http = inject(HttpClient);
//
//   //================================================
//   //===================variable=====================
//   //================================================
//   private apiUrl: string = `${environment.baseApi}/Doctor`;
//   private lookupsUrl: string = `${environment.baseApi}/Lookups`;
//
//   //================================================
//   //===================method=======================
//   //================================================
//   getDoctorById(id: number): Observable<DoctorDetailsDto> {
//     return this.http.get<DoctorDetailsDto>(`${this.apiUrl}/${id}`);
//   }
//
//   createDoctor(data: FormData): Observable<boolean> {
//     return this.http.post<boolean>(`${this.apiUrl}`, data);
//   }
//
//   updateDoctor(id: number, data: FormData): Observable<boolean> {
//     return this.http.put<boolean>(`${this.apiUrl}/${id}`, data);
//   }
//
//   changePassword(id: number, password: string): Observable<boolean> {
//     return this.http.patch<boolean>(
//       `${this.apiUrl}/changePassword/${id}`,
//       JSON.stringify(password),
//       {
//         headers: { 'Content-Type': 'application/json' },
//       },
//     );
//   }
//
//   getSpecialization(): Observable<{ id: number; name: string }[]> {
//     return this.http.get<{ id: number; name: string }[]>(
//       `${this.lookupsUrl}/specializations`,
//     );
//   }
//
//   getSubSpecialties(specializationId?: number): Observable<{ id: number; name: string; specializationName: string }[]> {
//     let params = new HttpParams();
//     if (specializationId) {
//       params = params.set('specializationId', specializationId);
//     }
//     return this.http.get<{ id: number; name: string; specializationName: string }[]>(
//       `${this.lookupsUrl}/sub-specialties`,
//       { params },
//     );
//   }
// }
