import IApplication from "../interfaces/IApplication";
import ApplicationsConstants from '../constants/ApplicationsConstants';

const getAllAsync = async (): Promise<IApplication[]> => {
    return fetch(ApplicationsConstants.getAllEndpoint)
        .then((response: Response) => response.json())
        .then(data => data)
        .catch(() => []);
};

const isNameAvailableAsync = async (name: string): Promise<boolean> => {
    return fetch(ApplicationsConstants.checkNameEndpoint + name)
        .then((response: Response) => response.json())
        .then(data => data)
        .catch(() => false);
}

// eslint-disable-next-line import/no-anonymous-default-export
export default {
    getAllAsync,
    isNameAvailableAsync
};