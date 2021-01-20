import React from 'react';
import {
    Switch,
    Route,
} from "react-router-dom";

// Pages
import WelcomePage from 'modules/Welcome/Welcome.page';
import SpacePage from 'modules/Space/Space.page';
import NotFoundSpace from 'modules/NotFoundSpace/NotFoundSpace.page';

function AppRoutes() {
    return (
        <div style={{ flexGrow: 10 }}>
            <Switch>
                <Route path="/space-not-found/:spaceId">
                    <NotFoundSpace />
                </Route>
                <Route path="/space/:spaceId">
                    <SpacePage />
                </Route>
                {/* Default path */}
                <Route path="/">
                    <WelcomePage />
                </Route>
            </Switch>
        </div>
    );
}

export default AppRoutes;
