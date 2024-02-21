import {makeAutoObservable, reaction} from "mobx";
import {ServerError} from "../Errors/ServerError.ts";
export default class CommonStore {
    token: string | null = localStorage.getItem('token');
    appLoaded: boolean = false;
    error: ServerError | null = null;

    constructor() {
        makeAutoObservable(this);
        reaction(()=>
            this.token, token => {
            if (token) {
                localStorage.setItem('token', token);
            } else {
                localStorage.removeItem('token');
            }
        })
    }

    setServerError = (error: ServerError) => {
        this.error = error;
    }
    setToken = (token: string | null) => {
        this.token = token;
    }
    setAppLoaded = () => {
        this.appLoaded = true;
    }
}