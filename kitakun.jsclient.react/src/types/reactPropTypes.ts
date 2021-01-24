import { ReactNode } from "react";

export interface IReactPropType {
    children?: ReactNode;
    styleName?: string;
    style?: any;
}

export interface ILocation {
    pathname: string,
    search: string,
    hash: string,
    state: any,
    key: string,
}

export interface IMatch {
    url: string,
    isExact: boolean,
    params: { [key: string]: string },
    path: string,
}

export interface IReactPropPageType extends IReactPropType {
    match: IMatch;
    staticCOntext?: any;
    history: {
        length: number,
        action: string,
        location: ILocation,
        push: (path: any, state: any) => any,
        replace: (path: any, state: any) => any,
        go: (n: any) => any,
        goBack: () => void,
        goForward: () => void,
    },
    location: ILocation,
}