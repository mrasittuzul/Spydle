import { BASE_API_URL } from "../Constants";

type ApiGenericResponse = { status: number, data: ApiLoginResponse | ApiErrorResponse}
export type ApiLoginResponse = { token: string, tokenExpireDate: string };
export type ApiErrorResponse = { errors: string[]};

export async function Get(url: string, body: object) : Promise<ApiGenericResponse>{
    return await Request("GET", url, body);
}

export async function Post(url: string, body: object) : Promise<ApiGenericResponse>{
    return await Request("POST", url, body);
}

async function Request(method: string, url: string, body: object) : Promise<ApiGenericResponse> {
    const requestOptions = {
        method: method,
        headers: { 
            "Accept" : "application/json",
            "Content-Type" : "application/json", 
            "Authorization" : "Bearer " + localStorage.getItem('jwt'),
        },
        body: JSON.stringify(body)
    };
    const response: Response = await fetch(BASE_API_URL + url, requestOptions);
    const data = await response.json();
    return { status: response.status, data: data };
}