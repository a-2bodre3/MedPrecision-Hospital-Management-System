import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { DepartmentDtoModel } from '../model/department-dto.model';
import { CreateDepartmentDtoModel } from '../model/create-department-dto.model';
import { UpdateDepartmentDtoModel } from '../model/update-department-dto.model';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  //================================================
  //===================inject=======================
  //================================================
  private http = inject(HttpClient);

  //================================================
  //===================variable=====================
  //================================================
  private apiUrl: string = `${environment.baseApi}/Department`;

  //================================================
  //===================method=======================
  //================================================
  getDepartments(): Observable<DepartmentDtoModel[]> {
    return this.http.get<DepartmentDtoModel[]>(`${this.apiUrl}`);
  }
  createDepartment(department: CreateDepartmentDtoModel): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}`, department);
  }
  updateDepartment(id: number, department: UpdateDepartmentDtoModel): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, department);
  }
  deleteDepartment(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
