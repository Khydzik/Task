import {Role} from '../models/role.interface';

export interface User {
  id: string;
  userName: string;
  role: Role;
}
