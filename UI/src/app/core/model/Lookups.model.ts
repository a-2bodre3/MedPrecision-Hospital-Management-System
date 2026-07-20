export interface LookupItem {
  id: number;
  name: string;
}
export interface LookupsResponse {
  branches: LookupItem[];
  departments: LookupItem[];
  rooms: LookupItem[];
  roles: LookupItem[];
  specialization: LookupItem[];
  subSpecialty: LookupItem[];
  allergies: LookupItem[];
  chronicDisease: LookupItem[];
}
