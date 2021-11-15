import classes from './Navigation.module.css';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

const Navigation = () => {
    return (
        <nav className={classes.Navigation}>
            <div className={classes.Logo}>AppRunner</div>

            <section className={classes.NavLinks}>
                <div className={classes.NavLink}>Applications</div>
                <div className={classes.NavLink}>Running</div>

                <div className={classes.AddButton}>
                    <FontAwesomeIcon icon={faPlus} />
                </div>
            </section>
        </nav>
    );
}

export default Navigation;