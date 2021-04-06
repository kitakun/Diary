import React from 'react';
// tag selector
import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";

interface ITagSelectorProps {
    readonly?: boolean;
    presettedTags?: string[];
    onChanged?: React.Dispatch<React.SetStateAction<string[]>>;
}

export default function TagSelector(props: ITagSelectorProps) {
    const [tags, setTags] = React.useState(props.presettedTags ?? [])
    const setTagscallback = (tags: string[]) => {
        setTags(tags);
        if (props?.onChanged) {
            props?.onChanged(tags);
        }
    };
    return (
        <ReactTagInput
            tags={tags}
            readOnly={props.readonly}
            placeholder={'Введите тег и нажмите enter'}
            onChange={setTagscallback}
        />
    )
}