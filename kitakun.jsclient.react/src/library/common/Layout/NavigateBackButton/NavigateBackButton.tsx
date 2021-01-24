

import React from 'react';
// Locals
import './NavigateBackButton.scss';

function NavigateBackButton() {
    const navigateBack = () => window.history.back();
    return (
        <button className="btn btn-navigate-back" type="button" aria-expanded="false" onClick={navigateBack}>
            Назад
        </button>
    );
}

export default NavigateBackButton;
