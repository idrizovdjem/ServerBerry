import classes from './CreateApplication.module.css';
import { ReactNode, useState } from 'react';
import ApplicationsConstants from '../../constants/ApplicationsConstants';
import ApplicationType from '../../enums/ApplicationType';
import FormType from '../../enums/FormType';
import ApplicationForm from './applicationForm/ApplicationForm';
import ArgumentsForm from './argumentsForm/ArgumentsForm';
import IArgument from '../../interfaces/IArgument';
import ICreateApplication from '../../interfaces/ICreateApplication';
import ApplicationsService from '../../services/ApplicationsService';

const CreateApplication = () => {
    const [activeForm, setActiveForm] = useState<FormType>(FormType.Application);
    const [applicationName, setApplicationName] = useState<string>('');
    const [showNameError, setShowNameError] = useState<boolean>(false);
    const [applicationType, setApplicationType] = useState<ApplicationType>(ApplicationType.Api);
    const [backgroundColor, setBackgroundColor] = useState<string>(ApplicationsConstants.backgroundColor);
    const [textColor, setTextColor] = useState<string>(ApplicationsConstants.textColor);
    const [applicationArguments, setApplicationArguments] = useState<IArgument[]>([]);
    const [errorMessage, setErrorMessage] = useState<string>('');

    const changeForm = (newForm: FormType): void => {
        setActiveForm(newForm);
    }

    const createApplication = async (): Promise<void> => {
        const filteredArguments: IArgument[] = applicationArguments.filter((argument: IArgument) => {
            return argument.key !== '' && argument.value !== '';
        });

        const application: ICreateApplication = {
            name: applicationName.trim(),
            type: applicationType,
            backgroundColor: backgroundColor,
            textColor: textColor,
            appArguments: filteredArguments
        };

        if(application.name === '') {
            setErrorMessage('Application name is required');
            return;
        }

        const isNameAvailable: boolean = await ApplicationsService.isNameAvailableAsync(application.name);
        if(isNameAvailable === false) {
            setErrorMessage('Application name must be unique');
            return;
        }

        setErrorMessage('');
    }

    const renderActiveForm = (): ReactNode => {
        if(activeForm === FormType.Application) {
            return (
                <ApplicationForm
                    backgroundColor={backgroundColor}
                    textColor={textColor}
                    showNameError={showNameError}
                    applicationName={applicationName}
                    setApplicationName={setApplicationName}
                    setApplicationType={setApplicationType}
                    setBackgroundColor={setBackgroundColor}
                    setShowNameError={setShowNameError}
                    setTextColor={setTextColor}
                    changeForm={changeForm}
                />
            );
        } else {
            return (
                <ArgumentsForm 
                    backgroundColor={backgroundColor}
                    textColor={textColor}
                    changeForm={changeForm}
                    setApplicationArguments={setApplicationArguments}
                    applicationArguments={applicationArguments}
                    createHandler={createApplication}
                    errorMessage={errorMessage}
                />
            );
        }
    }

    return (
        <section className={classes.CreateApplication}>
            { renderActiveForm() }
        </section>
    );
}

export default CreateApplication;