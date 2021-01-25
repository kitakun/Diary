export function splitToChunks<T>(array: T[], parts: number): T[][] {
    const result = [];
    for (let i = parts; i > 0; i--) {
        result.push(array.splice(0, Math.ceil(array.length / i)));
    }
    return result;
}

export function takeEveryToChunks<T>(array: T[], parts: number): T[][] {
    const result = [];
    let taken = 0;
    let curArray = [];

    for (let i = 0; i < array.length; i++) {
        curArray.push(array[i]);
        taken++;
        if (taken >= parts) {
            taken = 0;
            result.push(curArray);
            curArray = [];
        }
    }
    result.push(curArray);

    return result;
}
