import React, { Component } from "react";
import { createPortal } from "react-dom";
import './Modal.scss';

interface IModalProps {
    onClick?: (e: any) => void;
}

export default class Modal extends Component<IModalProps, {}> {
    private readonly modalContainer: HTMLElement | null;
    constructor(props: IModalProps) {
        super(props);
        this.modalContainer = document.getElementById("modal_root")
    }

    render() {
        if (this.modalContainer) {
            return createPortal(
                <div className="d-modal" onClick={this.props.onClick}>
                    {this.props.children}
                </div>,
                this.modalContainer
            );
        } else {
            return <></>
        }
    }
}