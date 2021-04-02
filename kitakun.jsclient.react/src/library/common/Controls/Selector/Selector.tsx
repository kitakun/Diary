import React from 'react';
import Select from 'react-select';

interface ISelectOptionProps {
    values: ISelectOption[];
    onChanged?: React.Dispatch<React.SetStateAction<ISelectOption>> | undefined;
}

export interface ISelectOption {
    value: string;
    label: string;
}

export default function Selector(props: ISelectOptionProps) {
    const [selectedOption, setSelectedOption] = React.useState(props.values[0])

    return (
        <Select
            value={selectedOption}
            onChange={(newVal) => {
                const selectedData = {
                    label: newVal!.label,
                    value: newVal!.value,
                };
                setSelectedOption(selectedData);
                if (props.onChanged) {
                    props.onChanged(selectedData);
                }
            }}
            options={props.values}
        />
    );
}