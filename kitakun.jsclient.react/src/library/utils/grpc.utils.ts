type ctorForGrpcClient<T> = new (...params: any[]) => T;

const API_URL = 'http://127.0.0.1:5010';

export function createClient<T>(clientCtor: ctorForGrpcClient<T>) {
    return new clientCtor(API_URL, null, null);
}
