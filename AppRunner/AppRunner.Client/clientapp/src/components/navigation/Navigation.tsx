import INavigationProps from './INavigationProps';
import classes from './Navigation.module.css';
import ViewType from '../../enums/ViewType';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

const Navigation = (props: INavigationProps) => {
    return (
        <nav className={classes.Navigation}>
            <div className={classes.Logo}>AppRunner</div>

            <section className={classes.NavLinks}>
                <div 
                    className={classes.NavLink}
                    onClick={() => props.setCurrentView(ViewType.Applications)}
                >
                    Applications
                </div>
                
                <div 
                    className={classes.NavLink}
                    onClick={() => props.setCurrentView(ViewType.Running)}
                >
                    Running
                </div>

                <div 
                    className={classes.AddButton}
                    onClick={() => props.setCurrentView(ViewType.Create)}
                >
                    <FontAwesomeIcon icon={faPlus} />
                </div>
            </section>
        </nav>
    );
}

export default Navigation;