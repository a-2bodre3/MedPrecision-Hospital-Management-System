import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';
import { Observable } from 'rxjs';
import { LookupsResponse } from '../model/Lookups.model';

@Injectable({
  providedIn: 'root',
})
export class LookupsService {
  //================================================
  //===================inject=======================
  //================================================
  private http = inject(HttpClient);

  //================================================
  //===================variable=====================
  //================================================
  private apiUrl: string = `${environment.baseApi}/Lookups`;

  //================================================
  //===================method=======================
  //================================================
  getLookups(): Observable<LookupsResponse> {
    return this.http.get<LookupsResponse>(this.apiUrl);
  }
}
