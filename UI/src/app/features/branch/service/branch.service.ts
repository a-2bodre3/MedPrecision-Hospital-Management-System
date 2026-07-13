import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { BranchDetails, BranchListItem } from '../model/branch-response.model';
import { CreateBranchRequest, UpdateBranchRequest } from '../model/branch-request.model';

@Injectable({
  providedIn: 'root',
})
export class BranchService {
  //================================================
  //===================inject=======================
  //================================================
  private http = inject(HttpClient);

  //================================================
  //===================variable=====================
  //================================================
  private apiUrl: string = `${environment.baseApi}/Branch`;

  //================================================
  //===================method=======================
  //================================================
  getAllBranches(): Observable<BranchListItem[]> {
    return this.http.get<BranchListItem[]>(`${this.apiUrl}`);
  }
  getBranchById(id: number): Observable<BranchDetails> {
    return this.http.get<BranchDetails>(`${this.apiUrl}/${id}`);
  }
  createBranch(branchForm: CreateBranchRequest): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}`, branchForm);
  }
  updateBranch(id: number, branchForm: UpdateBranchRequest): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, branchForm);
  }
  deleteBranch(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}
