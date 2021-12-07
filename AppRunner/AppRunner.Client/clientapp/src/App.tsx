import { useState } from "react";
import ViewType from './enums/ViewType'; 

import Navigation from "./components/navigation/Navigation";
import Applications from './components/applications/Applications';
import CreateApplication from "./components/createApplication/CreateApplication";

const App = () => {
    const [currentView, setCurrentView] = useState<ViewType>(ViewType.Create);

    const renderCurrentView = () => {
        switch(currentView) {
            case ViewType.Applications: return <Applications />;
            case ViewType.Running: return null;
            case ViewType.Create: return <CreateApplication />;
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