import FormType from "../../../enums/FormType";
import IArgument from "../../../interfaces/IArgument";

export default interface IArgumentsFormProps {
    backgroundColor: string;
    textColor: string;
    applicationArguments: IArgument[];
    errorMessage: string;
    setApplicationArguments: (newArguments: IArgument[]) => void;
    changeForm: (formType: FormType) => void;
    createHandler: () => void;
};