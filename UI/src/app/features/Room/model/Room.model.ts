import { RoomType } from '../../../core/enum/RoomType.enum';

export interface RoomModel {
  roomNumber: string;
  roomType: RoomType;
  isActive: boolean;
}
