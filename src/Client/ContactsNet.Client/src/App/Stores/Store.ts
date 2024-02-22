import {createContext, useContext} from "react";
import CommonStore from "./CommonStore.ts";
import ModalStore from "./ModalStore.ts";
import UserStore from "./UserStore.ts";

interface store {
    commonStore: CommonStore,
    modalStore: ModalStore,
    userStore: UserStore,
    
}

export const store: store = {
    commonStore: new CommonStore(),
    modalStore: new ModalStore(),
    userStore: new UserStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}