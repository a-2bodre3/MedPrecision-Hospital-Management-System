import { DayOfWeek } from '../../../core/enum/day-of-week.enum';

interface DoctorScheduleBase {
  dayOfWeeks: DayOfWeek[];
  startTime: string;
  endTime: string;
  maxPatients: number | string;
}

export interface DoctorScheduleFormDTO extends DoctorScheduleBase {
  doctorId: number | string;
  roomId: number | string;
}
export interface DoctorScheduleDisplayDTO extends DoctorScheduleBase {
  id: number;
  doctorName: string;
  roomNumber: string;
}
