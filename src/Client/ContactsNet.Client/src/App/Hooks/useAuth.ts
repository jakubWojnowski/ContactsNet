
import {useStore} from "../stores/Store.ts";

export function useAuth() {
   const { commonStore } = useStore()
    return {
        isAuthenticated: Boolean(commonStore.token),
        login: (token: string) => commonStore.setToken(token),
        logout: () => commonStore.setToken(null),
    };
}
