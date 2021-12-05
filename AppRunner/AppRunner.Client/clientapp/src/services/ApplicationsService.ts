import IApplication from "../interfaces/IApplication";
import ApplicationsConstants from '../constants/ApplicationsConstants';

const getAllAsync = async (): Promise<IApplication[]> => {
    return fetch(ApplicationsConstants.getAllEndpoint)
        .then((response: Response) => response.json())
        .then(data => data)
        .catch(() => []);
};

// eslint-disable-next-line import/no-anonymous-default-export
export default {
    getAllAsync
};