import ApplicationType from '../enums/ApplicationType';
import IArgument from './IArgument';

export default interface ICreateApplication {
    name: string;
    type: ApplicationType;
    backgroundColor: string;
    textColor: string;
    appArguments: IArgument[];
};