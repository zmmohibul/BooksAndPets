import { Address } from './address';

export interface UserDetails {
  userName: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  addresses: Address[];
}
