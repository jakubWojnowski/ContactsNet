import {RouteObject, createBrowserRouter, Navigate} from 'react-router-dom';
import App from "../../App.tsx";
import ProfilePage from "../../Feautures/Profile/ProfilePage.tsx";


export const Routes: RouteObject[] = [
    {
        path: '/',
        element: <App/>,
        children: [
            {
                path: 'contacts',
                element:( 
                    <RequireAuth>
                    <ContactsPage />
                    </RequireAuth>
                ),
            },
            {
                path: 'profile',
                element: <ProfilePage />,
            },
            {
                path: 'login',
                element: <LoginForm />,
            },
            {
                path: 'not-found',
                element: <NotFound />,
            },
            {
                path: 'server-error',
                element: <ServerError />,
            },
            {
                path: '*',
                element: <Navigate replace to="/not-found" />,
            },
        ],
    }
];

export const router = createBrowserRouter(Routes);