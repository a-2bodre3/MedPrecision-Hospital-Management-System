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
  validFrom: string;
  validUntil?: string | null;
}

export interface DoctorScheduleDisplayDTO extends DoctorScheduleBase {
  id: number;
  doctorName: string;
  roomNumber: string;
}

export interface DoctorScheduleDetailsDTO extends DoctorScheduleDisplayDTO {
  validFrom: string;
  validUntil?: string | null;
  isActive: boolean;
  createdAt: string;
  createdByName: string;
  lastModifiedAt: string;
  lastModifiedByName: string;
}

export interface AdjustScheduleValidityDto {
  validFrom: string;
  validUntil?: string | null;
}
