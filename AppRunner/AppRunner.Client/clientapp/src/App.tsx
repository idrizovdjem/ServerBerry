import { useState } from "react";
import ViewType from './enums/ViewType'; 

import Navigation from "./components/navigation/Navigation";
import Applications from './components/applications/Applications';

const App = () => {
    const [currentView, setCurrentView] = useState<ViewType>(ViewType.Applications);

    const renderCurrentView = () => {
        switch(currentView) {
            case ViewType.Applications: return <Applications />;
            case ViewType.Running: return null;
            case ViewType.Create: return null;
        }
    }

    return (
        <>
            <Navigation setCurrentView={setCurrentView} />
            { renderCurrentView() }
        </>
    );
}

export default App;