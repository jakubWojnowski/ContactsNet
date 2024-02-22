// RequireAuth.tsx
import { ReactElement } from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import {useAuth} from "../../Hooks/useAuth.ts";


interface RequireAuthProps {
    children: ReactElement;
}

function RequireAuth({ children }: RequireAuthProps): ReactElement {
    const auth = useAuth();
    const location = useLocation();

    if (!auth.isAuthenticated) {
        return <Navigate to="/" state={{ from: location }} replace />;
    }

    return children;
}

export default RequireAuth;
