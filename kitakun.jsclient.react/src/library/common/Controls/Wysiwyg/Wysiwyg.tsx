import React from "react";
// quill
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';

interface IWysiwtyProps {
    readonly?: boolean;
    initialValue: string;
    setValue: React.Dispatch<React.SetStateAction<string>>;
}

export default function Wysiwyg(props: IWysiwtyProps) {
    return (
        <ReactQuill
            theme="snow"
            readOnly={props.readonly}
            value={props.initialValue}
            onChange={props.setValue} />
    );
}