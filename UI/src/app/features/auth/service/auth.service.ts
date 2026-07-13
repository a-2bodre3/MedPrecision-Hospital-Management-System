import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { LoginDto } from '../model/login-dto.model';
import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { JwtPayload } from '../model/jwt-payload.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  //================================================
  //===================inject=======================
  //================================================
  private http = inject(HttpClient);
  private cookiesService = inject(CookieService);
  //================================================
  //===================variable=====================
  //================================================
  private apiUrl: string = `${environment.baseApi}/auth`;

  //================================================
  //===================method=======================
  //================================================
  login(credentials: LoginDto): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, credentials, {
      withCredentials: true,
    });
  }

  logout(): Observable<string> {
    const httpOptions = {
      withCredentials: true,
    };
    return this.http.post<string>(`${this.apiUrl}/logout`, {}, httpOptions);
  }

  //================================================
  //===================token handle=================
  //================================================
  getTokenFromCookie() {
    this.cookiesService.get('user-token');
  }

  decodeToken(token: string) {
    try {
      const decoded = jwtDecode<JwtPayload>(token);
      return {
        UserId: decoded.UserId,
        FullName: decoded.FullName,
        email: decoded.email,
        Branch: decoded.Branch,
        ImageUrl: decoded.ImageUrl,
        role: decoded.role,
        Permissions: decoded.Permissions,
      };
    } catch (e) {
      return null;
    }
  }
}
