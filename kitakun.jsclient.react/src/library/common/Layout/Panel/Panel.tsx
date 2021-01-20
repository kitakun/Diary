import React from 'react';
import classnames from 'classnames';
import './Panel.scss';
// Locals
import Container from '../Container/Container';
import { IReactPropType } from 'types';

interface IPanelProps extends IReactPropType {
    color?: string;
    variant?: 'white' | 'none';
    classNames?: string | string[];
}

function Panel(props: IPanelProps) {
    const dynamicStyles = {} as { [key: string]: string };
    if (props.color) {
        dynamicStyles['background-color'] = props.color;
    }

    const variantStyleName = props.variant
        ? `panel-variant-${props.variant}`
        : '';

    return (
        <Container style={dynamicStyles} styleName={classnames(props.styleName, variantStyleName, props.classNames)}>
            {props.children}
        </Container>
    );
}

export default Panel;
