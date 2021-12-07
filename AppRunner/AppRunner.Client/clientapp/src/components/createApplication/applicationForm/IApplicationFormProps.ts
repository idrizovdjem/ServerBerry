import ApplicationType from "../../../enums/ApplicationType";
import FormType from "../../../enums/FormType";

export default interface IApplicationFormProps {
    setBackgroundColor: (blockColor: string) => void;
    setTextColor: (textColor: string) => void;
    setShowNameError: (show: boolean) => void;
    setApplicationName: (name: string) => void;
    setApplicationType: (type: ApplicationType) => void;
    changeForm: (formType: FormType) => void;
    showNameError: boolean;
    backgroundColor: string;
    textColor: string;
    applicationName: string;
};