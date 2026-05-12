const BASE_API_URL = import.meta.env.VITE_API_URL;

type ApiGenericResponse<T> = { status: number, data: T | ApiErrorResponse }
export type ApiErrorResponse = { errors: string[]};

export async function Get<T>(url: string) : Promise<ApiGenericResponse<T>>{
    return await Request<T>("GET", url, null);
}

export async function Post<T>(url: string, body: object) : Promise<ApiGenericResponse<T>>{
    return await Request<T>("POST", url, body);
}

async function Request<T>(method: string, url: string, body: object | null) : Promise<ApiGenericResponse<T>> {
    const requestOptions = {
        method: method,
        headers: { 
            "Accept" : "application/json",
            "Content-Type" : "application/json", 
            "Authorization" : "Bearer " + localStorage.getItem('jwt'),
        },
        body: body ? JSON.stringify(body) : null
    };
    const response: Response = await fetch(BASE_API_URL + url, requestOptions);
    const data = await response.json();
    return { status: response.status, data: data };
}