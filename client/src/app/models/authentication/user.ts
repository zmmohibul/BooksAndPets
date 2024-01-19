import { UserRole } from './userRole';

export interface User {
  userName: string;
  role: UserRole;
  token: string;
}
