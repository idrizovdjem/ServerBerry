import IApplicationBlockProps from './IApplicationBlockProps';
import classes from './ApplicationBlock.module.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlay, faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';


const ApplicationRow = (props: IApplicationBlockProps) => {
    return (
        <article className={classes.ApplicationRow}>
            <p className={classes.ApplicationName}>{ props.name }</p>

            <div className={classes.Icons}>
                <FontAwesomeIcon icon={faPlay} className={classes.RunIcon} />
                <FontAwesomeIcon icon={faEdit} className={classes.EditIcon} />
                <FontAwesomeIcon icon={faTrash} className={classes.DeleteIcon} />
            </div>
        </article>
    );
}

export default ApplicationRow;