import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { RoomDetailsResponse, RoomResponse } from '../model/RoomResponse.model';
import { CreateRoomCommand, UpdateRoomCommand } from '../model/RoomCommand.model';

@Injectable({
  providedIn: 'root',
})
export class RoomService {
  //================================================
  //===================inject=======================
  //================================================
  private http = inject(HttpClient);

  //================================================
  //===================variable=====================
  //================================================
  private apiUrl: string = `${environment.baseApi}/Room`;

  //================================================
  //===================method=======================
  //================================================
  getRooms(): Observable<RoomResponse[]> {
    return this.http.get<RoomResponse[]>(`${this.apiUrl}`);
  }
  getRoomById(id: number): Observable<RoomDetailsResponse> {
    return this.http.get<RoomDetailsResponse>(`${this.apiUrl}/${id}`);
  }
  createRoom(room: CreateRoomCommand): Observable<boolean> {
    return this.http.post<boolean>(`${this.apiUrl}/`, room);
  }
  updateRoom(id: number, room: UpdateRoomCommand): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${id}`, room);
  }
  deleteRoom(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }
}
