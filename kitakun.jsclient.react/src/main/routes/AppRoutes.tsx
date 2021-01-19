import React from 'react';
import {
    Switch,
    Route,
} from "react-router-dom";

// Pages
import WelcomePage from 'modules/Welcome/Welcome.page';

function AppRoutes() {
    return (
        <Switch>
            <Route path="/">
                <WelcomePage />
            </Route>
        </Switch>
    );
}

export default AppRoutes;
