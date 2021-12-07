import { useState, useEffect } from "react";
import classes from './Applications.module.css';
import IApplication from '../../interfaces/IApplication';
import ApplicationsService from '../../services/ApplicationsService';
import ApplicationBlock from '../applicationBlock/ApplicationBlock';

const Applications = () => {
    const [applications, setApplications] = useState<IApplication[]>([]);

    useEffect(() => {
        const fetchApplications = async () => {
            const fetchedApplications: IApplication[] = await ApplicationsService.getAllAsync();
            setApplications(fetchedApplications);
        };

        fetchApplications();
    }, []);

    const mapToApplicationBlock = (application: IApplication) => {
        return (
            <ApplicationBlock
                id={application.id}
                name={application.name}
                type={application.type}
                key={application.id}
            />
        );
    }

    return (
        <main>
            <section className={classes.ApplicationsContainer}>
                { applications.map((application: IApplication) => mapToApplicationBlock(application)) }
            </section>
        </main>
    );
};

export default Applications;