import React from 'react';
import ReactTagInput from "@pathofdev/react-tag-input";
import "@pathofdev/react-tag-input/build/index.css";

interface ITagSelectorProps {
    presettedTags?: string[];
}

export default function TagSelector(props: ITagSelectorProps) {
    const [tags, setTags] = React.useState(props.presettedTags ?? [])
    return (
        <ReactTagInput
            tags={tags}
            placeholder={'Введите тег и нажмите enter'}
            onChange={(newTags) => setTags(newTags)}
        />
    )
}