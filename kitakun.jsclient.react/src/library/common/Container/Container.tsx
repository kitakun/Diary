import classnames from 'classnames';
import './Container.scss';
// locals
import { IReactPropType } from '../../../types';

function Container(props: IReactPropType) {
    return (
        <div className={classnames('root-container', props.styleName)}>
            {props.children}
        </div>
    );
}

export default Container;
