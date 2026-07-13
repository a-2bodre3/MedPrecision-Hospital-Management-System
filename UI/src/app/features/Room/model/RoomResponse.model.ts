import { RoomModel } from './Room.model';

export interface RoomResponse extends RoomModel {
  id: number;
}

export interface RoomDetailsResponse extends RoomResponse {
  floor: number;
  capacity: number;
  departmentName: string;
  branchName: string;
}
