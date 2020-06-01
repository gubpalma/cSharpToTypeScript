import { ProfileViewModel } from './ProfileViewModel';

export class AddressViewModel {
    public applicantAddressId: string = '';
    public applicantId: string = '';
    public addressType: number = 0;
    public unitNumber: string = '';
    public streetNumber: string = '';
    public street: string = '';
    public suburb: string = '';
    public state: string = '';
    public stateCode: string = '';
    public postCode: string = '';
    public country: string = '';
    public countryCode: string = '';
    public dateFrom?: Date = undefined;
    public dateTo?: Date = undefined;
    public profile: ProfileViewModel | undefined = undefined;
}
