import { PersonViewModel } from './PersonViewModel';
import { RegistrationViewModel } from './RegistrationViewModel';

export class CarViewModel {
    public stringWithLongCamelCase: string = '';
    public singleDataType: number = 0;
    public year?: number = undefined;
    public owner: PersonViewModel | undefined = undefined;
    public registration: RegistrationViewModel | undefined = undefined;
}
