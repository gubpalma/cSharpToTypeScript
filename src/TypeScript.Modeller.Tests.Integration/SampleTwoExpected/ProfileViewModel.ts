import { CarViewModel } from './CarViewModel';
import { UnmarkedViewModel } from './UnmarkedViewModel';
import { AddressViewModel } from './AddressViewModel';

export class ProfileViewModel {
	public applicantId?: string = undefined;
	public firstName: string = '';
	public middleName: string = '';
	public lastName: string = '';
	public dateOfBirth?: Date = undefined;
	public gender: string = '';
	public email: string = '';
	public mobileNumber: string = '';
	public applicantStatus: number = 0;
	public car: CarViewModel | undefined = undefined;
	public unmarked: UnmarkedViewModel | undefined = undefined;
	public addresses: AddressViewModel[] = [];
}
