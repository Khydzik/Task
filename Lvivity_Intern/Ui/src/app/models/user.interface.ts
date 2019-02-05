import { Role } from '../models/role.interface';
export interface User {
  id: number;
  userName: string;
  role: Role;
}
