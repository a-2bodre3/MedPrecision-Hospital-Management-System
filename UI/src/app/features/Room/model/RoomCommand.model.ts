import { RoomModel } from './Room.model';

export interface CreateRoomCommand extends Omit<RoomModel, 'isActive'> {
  floor: number;
  capacity: number;
  departmentId: number;
  branchId: number;
}

export interface UpdateRoomCommand extends CreateRoomCommand {
  isActive: boolean;
}
