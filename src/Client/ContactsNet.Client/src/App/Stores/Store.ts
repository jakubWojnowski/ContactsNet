import {createContext, useContext} from "react";
import CommonStore from "./CommonStore.ts";

interface store {
    commonStore: CommonStore
}

export const store: store = {
    commonStore: new CommonStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}