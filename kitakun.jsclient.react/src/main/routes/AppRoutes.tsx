import React from 'react';
import {
    Switch,
    Route,
} from "react-router-dom";

// Pages
import WelcomePage from 'modules/Welcome/Welcome.page';
import SpacePage from 'modules/Space/Space.page';
import NotFoundSpace from 'modules/NotFoundSpace/NotFoundSpace.page';
import RecordViewPage from 'modules/RecordViewPage/RecordViewPage';

function AppRoutes() {
    return (
        <div style={{ flexGrow: 10 }}>
            <Switch>
                <Route path="/space-not-found/:spaceId">
                    <NotFoundSpace />
                </Route>
                <Route path="/space/:spaceId/view/:recordId" component={RecordViewPage}></Route>
                <Route path="/space/:spaceId" component={SpacePage}></Route>
                {/* Default path */}
                <Route path="/">
                    <WelcomePage />
                </Route>
            </Switch>
        </div>
    );
}

export default AppRoutes;
