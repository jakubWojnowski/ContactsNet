import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import {store,StoreContext} from "./App/Stores/Store.ts";
import {router} from "./App/Router/Routes.tsx";
import {RouterProvider} from "react-router-dom";

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <StoreContext.Provider value={store}>
            <RouterProvider router={router}/>
        </StoreContext.Provider>

    </React.StrictMode>,
)
