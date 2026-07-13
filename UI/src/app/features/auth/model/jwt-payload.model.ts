export interface JwtPayload {
  UserId: string;
  FullName: string;
  email: string;
  role: string;
  Branch: string;
  ImageUrl: string;
  Permissions: string[];
}
