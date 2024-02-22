import axios, {AxiosError, AxiosResponse} from 'axios';
import {router} from "../Router/Routes.tsx";
import {toast} from "react-toastify";
import {store} from "../Stores/Store.ts";
import {RegisterForm} from "../Models/RegisterForm.ts";
import {User} from "../Models/User.ts";
import {LoginForm} from "../Models/LoginForm.ts";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)

    })
};
axios.defaults.baseURL = 'http://localhost:5000';
axios.interceptors.response.use(async response => {
    await sleep(500);
    return response;


}, (error: AxiosError) => {

    const {data, status,config} = error.response as AxiosResponse;
    switch (status) {
        case 400:
            if(config.method === 'get' && Object.prototype.hasOwnProperty.call(data.errors, 'id')) {
                router.navigate('/not-found').then(r => console.log(r));
            }
            if (data && data.response && data.response.errors) {
                const errorMessages = data.response.errors.map((e: { message: string }) => e.message).join(', ');
                toast.error(`Bad request: ${errorMessages}`);
            } else {
                toast.error('Bad request with no error details.');
            }
            break;
        case 401:
            toast.error('unauthorized');
            break;
        case 404:
            router.navigate('/not-found').then(r => console.log(r));
            break;
        case 500:
            store.commonStore.setServerError(data);
            router.navigate('/server-error').then(r => console.log(r));
            break;
    }
    return Promise.reject(error);

});

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

axios.interceptors.request.use((config) => {
    const token = store.commonStore.token;
    if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});
const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string, body: {}) => axios.delete<T>(url, body).then(responseBody),
    patch: <T>(url: string, body: {}) => axios.patch<T>(url, body).then(responseBody)
};

const Authentication = {
    login: (user: LoginForm) => requests.post<User>('/api/Authentication/login', user),
    register: (user: RegisterForm) => requests.post<User>('/api/Authentication/register', user)
};
const Contacts = {

};

export default {
    Authentication,
    Contacts
};