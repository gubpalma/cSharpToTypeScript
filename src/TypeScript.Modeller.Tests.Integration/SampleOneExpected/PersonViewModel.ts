import { CarViewModel } from './CarViewModel';

export class PersonViewModel {
    public thisIsAGuid: string = '';
    public thisIsANullableGuid?: string = undefined;
    public stringField: string = '';
    public nullableDate?: Date = undefined;
    public cars: CarViewModel[] = [];
}
