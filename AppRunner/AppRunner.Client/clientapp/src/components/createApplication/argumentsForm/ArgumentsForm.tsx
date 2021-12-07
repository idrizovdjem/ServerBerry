import classes from './ArgumentsForm.module.css';
import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTimes, faPlus, faArrowLeft, faCheck } from '@fortawesome/free-solid-svg-icons';
import IArgument from '../../../interfaces/IArgument';
import IArgumentsFromProps from './IArgumentsFormProps';
import FormType from '../../../enums/FormType';

const ArgumentsForm = (props: IArgumentsFromProps) => {
    const addArgument = (): void => {
        const newArgument: IArgument = { key: '', value: '' };
        props.setApplicationArguments([...props.applicationArguments, newArgument]);
    }

    const removeArgument = (index: number): void => {
        const argumentsCopy: IArgument[] = [...props.applicationArguments];
        argumentsCopy.splice(index, 1);
        props.setApplicationArguments(argumentsCopy);
    }

    const onKeyChangeHandler = (event: React.FormEvent<HTMLInputElement>, index: number): void => {
        const newKey: string = event.currentTarget.value;
        const argumentsCopy = [...props.applicationArguments];
        argumentsCopy[index].key = newKey;
        props.setApplicationArguments(argumentsCopy);
    }

    const onValueChangeHandler = (event: React.FormEvent<HTMLInputElement>, index: number): void => {
        const newValue: string = event.currentTarget.value;
        const argumentsCopy = [...props.applicationArguments];
        argumentsCopy[index].value = newValue;
        props.setApplicationArguments(argumentsCopy);
    }

    return (
        <div>
            <p className={classes.ArgumentsLabel}>Add application arguments</p>

            {
                props.applicationArguments.map((argument: IArgument, index: number) => {
                    return (
                        <div>
                            <input 
                                placeholder="Key" 
                                className={classes.KeyInput} 
                                value={argument.key} 
                                onChange={(event) => onKeyChangeHandler(event, index)}    
                            />

                            <input 
                                placeholder="Value" 
                                className={classes.ValueInput} 
                                value={argument.value} 
                                onChange={(event) => onValueChangeHandler(event, index)}    
                            />
                            
                            <FontAwesomeIcon icon={faTimes} className={classes.CrossIcon} onClick={() => removeArgument(index)}/>
                        </div>
                    );
                })
            }

            <FontAwesomeIcon icon={faPlus} className={classes.PlusIcon} onClick={addArgument} />

            <p className={classes.ErrorMessage}>{ props.errorMessage }</p>

            <div>
                <button 
                    className={classes.ApplicationsButton}
                    style={{ backgroundColor: props.backgroundColor, color: props.textColor }}
                    onClick={() => props.changeForm(FormType.Application)}
                >
                    <FontAwesomeIcon icon={faArrowLeft} className={classes.LeftArrow} />
                    Application
                </button>

                <button 
                    className={classes.CreateButton}
                    style={{ backgroundColor: props.backgroundColor, color: props.textColor }}
                    onClick={props.createHandler}
                >
                    Create
                    <FontAwesomeIcon icon={faCheck} className={classes.CheckIcon} />
                </button>
            </div>
        </div>
    );
};

export default ArgumentsForm;