import classes from './ApplicationForm.module.css';
import React from 'react';
import { useDropzone } from 'react-dropzone';
import utilitiesService from '../../../services/UtilitiesService';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleRight } from '@fortawesome/free-solid-svg-icons';
import ApplicationType from '../../../enums/ApplicationType';
import ApplicationsService from '../../../services/ApplicationsService';
import IApplicationFormProps from './IApplicationFormProps';
import FormType from '../../../enums/FormType';

const ApplicationForm = (props: IApplicationFormProps) => {
    const onDrop = (acceptedFiles: File[]) => {
        alert('Not implemented yet');
    }

    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop });

    const onColorChangeHandler = (event: React.FormEvent<HTMLInputElement>): void => {
        const blockColor: string = event.currentTarget.value;
        const invertedColor: string = utilitiesService.invertColor(blockColor, true);

        props.setBackgroundColor(blockColor);
        props.setTextColor(invertedColor);
    }

    const onApplicationNameChangeHandler = async (event: React.FormEvent<HTMLInputElement>): Promise<void> => {
        const newName: string = event.currentTarget.value;
        props.setApplicationName(newName);

        if (newName.trim() === '') {
            props.setShowNameError(false);
            props.setApplicationName('');
            return;
        }

        const isNameAvailable: boolean = await ApplicationsService.isNameAvailableAsync(newName);
        if (isNameAvailable === false) {
            props.setShowNameError(true);
            return;
        }

        props.setShowNameError(false);
    }

    const onApplicationTypeChangeHandler = (event: React.FormEvent<HTMLSelectElement>): void => {
        const newTypeValue: string = event.currentTarget.value;
        let newType: ApplicationType | undefined = undefined;

        switch (newTypeValue) {
            case 'Api': newType = ApplicationType.Api; break;
            case 'Mvc': newType = ApplicationType.Mvc; break;
            case 'Spa': newType = ApplicationType.Spa; break;
            case 'HttpTrigger': newType = ApplicationType.HttpTrigger; break;
            default: alert('Invalid application type'); break;
        }

        if (newType === undefined) {
            return;
        }

        props.setApplicationType(newType);
    }

    return (
        <div>
            <p className={classes.CreateLabel}>Create an application</p>

            <div>
                <label htmlFor="applicationName" className={classes.Label}>Application name</label>
                <input
                    placeholder="Application name"
                    id="applicationName"
                    className={classes.TextInput}
                    onChange={onApplicationNameChangeHandler}
                    value={props.applicationName}
                />
                {
                    props.showNameError
                        ? <p className={classes.Error}>Appication name is already taken</p>
                        : null
                }
            </div>

            <div>
                <label htmlFor="applicationType" className={classes.Label}>Application type</label>
                <select id="applicationType" className={classes.SelectInput} onChange={onApplicationTypeChangeHandler}>
                    <option value="Api">Web API</option>
                    <option value="Mvc">MVC</option>
                    <option value="Spa">SPA</option>
                    <option value="HttpTrigger">Http Trigger</option>
                </select>
            </div>

            <div className={classes.DragSection}>
                <div {...getRootProps()}>
                    <input {...getInputProps()} />
                    {
                        isDragActive ?
                            <p>Drop the files here ...</p> :
                            <p>Drag 'n' drop some files here, or click to select files</p>
                    }
                </div>
            </div>

            <div>
                <label id="applicationColor" className={classes.Label}>Application block color</label>
                <input
                    type="color"
                    className={classes.ColorInput}
                    id="applicationColor"
                    onChange={onColorChangeHandler}
                    value={props.backgroundColor}
                />
            </div>

            <div>
                <button
                    className={classes.ArgumentsButton}
                    style={{ backgroundColor: props.backgroundColor, color: props.textColor }}
                    onClick={() => props.changeForm(FormType.Arguments)}
                >
                    Arguments
                    <FontAwesomeIcon icon={faAngleRight} className={classes.RightArrow} />
                </button>
            </div>
        </div>
    );
};

export default ApplicationForm;