import React from "react";
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';

export default function Wysiwyg(props: { initialValue: string, setValue: React.Dispatch<React.SetStateAction<string>> }) {
    return (
        <ReactQuill theme="snow" value={props.initialValue} onChange={props.setValue} />
    );
}