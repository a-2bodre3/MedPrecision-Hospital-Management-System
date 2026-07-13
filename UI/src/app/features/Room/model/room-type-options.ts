export const ROOM_TYPE_OPTIONS = [
  { value: 1, label: 'Clinic' },
  { value: 2, label: 'Ward' },
  { value: 3, label: 'Operation Room' },
  { value: 4, label: 'Office' },
  { value: 5, label: 'Laboratory' },
] as const;

export function getRoomTypeLabel(value: number): string {
  return ROOM_TYPE_OPTIONS.find((option) => option.value === value)?.label ?? 'Unknown';
}
