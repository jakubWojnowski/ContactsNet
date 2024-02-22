import {makeAutoObservable, runInAction} from "mobx";
import agent from "../Api/Agent.ts";
import {store} from "./Store.ts";
import {router} from "../Router/Routes.tsx";
import {LoginForm} from "../Models/LoginForm.ts";
import {RegisterForm} from "../Models/RegisterForm.ts";
import {User} from "../Models/User.ts";

export default class UserStore {
    user: User | null = null;
    loading: boolean = false;
    loadingInitial: boolean = false;
    constructor() {
        makeAutoObservable(this);
    }
    get isLoggedIn() {
        return !!this.user;
    }
    login = async (values: LoginForm) => {
        const user = await agent.Authentication.login(values);
        store.commonStore.setToken(user.token);
        runInAction(() => {
            router.navigate('/profile')
            this.user = user;
            store.modalStore.closeModal();
        });

    };
    register = async (values: RegisterForm): Promise<void> => {
        try {
            await agent.Authentication.register(values);
            const user = await agent.Authentication.login(values);
            runInAction(() => {
                this.user = user;
                store.commonStore.setToken(user.token);
                router.navigate('/profile');
            });
            store.modalStore.closeModal();
        } catch (error) {
            throw error;
        }
    };
    logout = () => {
        store.commonStore.setToken(null);
        this.user = null;
        router.navigate('/').then(r => console.log(r));
    }
    getUser = async () => {
        this.loadingInitial = true;
        try {
            const user = await agent.Account.current();
            runInAction(() => {
                this.user = user;
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => {
                this.loadingInitial = false;
            })
        }
    }








}
