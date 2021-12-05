import ViewType from '../../enums/ViewType';

export default interface INavigationProps {
    setCurrentView: (newView: ViewType) => void;
};