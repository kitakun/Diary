import React from 'react';
import Select from 'react-select';

interface ISelectOptionProps {
    values: ISelectOption[];
}

export interface ISelectOption {
    value: string;
    label: string;
}

export default class Selector extends React.Component<ISelectOptionProps, {}> {
    state = {
        selectedOption: null,
    };
    handleChange = (selectedOption: any) => {
        this.setState(
            { selectedOption },
            () => console.log(`Option selected:`, this.state.selectedOption)
        );
    };
    render() {
        const { selectedOption } = this.state;

        return (
            <Select
                value={selectedOption}
                onChange={this.handleChange}
                options={this.props.values}
            />
        );
    }
}