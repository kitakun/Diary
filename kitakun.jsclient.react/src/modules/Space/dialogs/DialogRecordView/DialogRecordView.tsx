import React, { Component } from "react";
import { Route, Link } from "react-router-dom";
import Modal from "library/common/Dialogs/Modal/Modal";

interface IDialogRecViewProps {
    match?: { url: string },
    history: { push(dt: string): void }
}

export default class DialogRecordView extends Component<IDialogRecViewProps, {}> {
    private readonly pushUrlToHistory = () => this.props.history!.push(this.props.match!.url);
    private readonly divstyle = {
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        height: "100%",
    };

    render() {
        return (
            <div>
                <Link to={`${this.props.match!.url}/edit`}>Edit Profile</Link>

                <Route
                    path={`${this.props.match!.url}/edit`}
                    render={() => {
                        return (
                            <Modal onClick={this.pushUrlToHistory.bind(this)}>
                                <div style={this.divstyle}>
                                    Edit Profile Modal!
                                </div>
                            </Modal>
                        );
                    }}
                />
            </div>
        );
    }
}