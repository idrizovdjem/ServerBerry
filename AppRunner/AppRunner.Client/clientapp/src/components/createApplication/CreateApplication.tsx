import classes from './CreateApplication.module.css';
import React, { useState } from 'react';
import { useDropzone } from 'react-dropzone';
import utilitiesService from '../../services/UtilitiesService';
import ApplicationsConstants from '../../constants/ApplicationsConstants';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleRight } from '@fortawesome/free-solid-svg-icons';
import ApplicationType from '../../enums/ApplicationType';
import ApplicationsService from '../../services/ApplicationsService';

const CreateApplication = () => {
    const [applicationName, setApplicationName] = useState<string>('');
    const [showNameError, setShowNameError] = useState<boolean>(false);
    const [applicationType, setApplicationType] = useState<ApplicationType>(ApplicationType.Api);
    const [backgroundColor, setBackgroundColor] = useState<string>(ApplicationsConstants.backgroundColor);
    const [textColor, setTextColor] = useState<string>(ApplicationsConstants.textColor);

    const onDrop = (acceptedFiles: File[]) => {
        alert('Not implemented yet');
    }

    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop });

    const onColorChangeHandler = (event: React.FormEvent<HTMLInputElement>): void => {
        const blockColor: string = event.currentTarget.value;
        const invertedColor: string = utilitiesService.invertColor(blockColor, true);

        setBackgroundColor(blockColor);
        setTextColor(invertedColor);
    }

    const onApplicationNameInputHandler = async (event: React.FormEvent<HTMLInputElement>): Promise<void> => {
        const newName: string = event.currentTarget.value;
        if(newName.trim() === '') {
            setShowNameError(false);
            setApplicationName('');
            return;
        }

        const isNameAvailable: boolean = await ApplicationsService.isNameAvailableAsync(newName);
        if (isNameAvailable === false) {
            setShowNameError(true);
            return;
        }

        setShowNameError(false);
        setApplicationName(newName);
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

        setApplicationType(newType);
    }

    return (
        <section className={classes.CreateApplication}>
            <p className={classes.CreateLabel}>Create an application</p>

            <div>
                <label htmlFor="applicationName" className={classes.Label}>Application name</label>
                <input
                    placeholder="Application name"
                    id="applicationName"
                    className={classes.TextInput}
                    onInput={onApplicationNameInputHandler}
                />
                {
                    showNameError
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
                    value={backgroundColor}
                />
            </div>

            <div>
                <button
                    className={classes.ArgumentsButton}
                    style={{ backgroundColor: backgroundColor, color: textColor }}
                >
                    Arguments
                    <FontAwesomeIcon icon={faAngleRight} className={classes.RightArrow} />
                </button>
            </div>
        </section>
    );
}

export default CreateApplication;