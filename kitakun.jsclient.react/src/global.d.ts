declare global {
    interface Window {
        config: {
            url: string;
        };
    }
    interface proto extends any {

    }
    interface COMPILLED extends any {

    }
}

// Adding this exports the declaration file which Typescript/CRA can now pickup:
export { }