import {Fragment, useEffect} from "react";
import './styles.css';
import {Container} from "semantic-ui-react";
import {observer} from "mobx-react-lite";
import {Outlet, useLocation} from "react-router-dom";
import {ToastContainer} from "react-toastify";
import {useStore} from "./App/Stores/Store.ts";
import HomePage from "./Feautures/Home/HomePage.tsx";
import ModalContainer from "./App/Common/Modals/ModalContainer.tsx";
import LoadingComponent from "./App/Common/Components/LoadingComponent.tsx";
import NavBar from "./Feautures/NavBar/NavBar.tsx";


function App() {
  const location = useLocation();
  const {commonStore, userStore} = useStore();

  useEffect(() => {
        if (commonStore.token) {
          userStore.getUser().finally(() => commonStore.setAppLoaded());
        } else {
          commonStore.setAppLoaded();
        }
      }
      , [commonStore, userStore]);

  if (!commonStore.appLoaded) return <LoadingComponent content='Loading app...'/>


  return (
      <>
        <ModalContainer/>
        <ToastContainer position='bottom-right' hideProgressBar theme='colored'/>
        {location.pathname === '/' ? <HomePage/> : (
            <>
              <Fragment>
                <NavBar/>
                <Container style={{marginTop: '7em'}}>
                  <Outlet/>
                </Container>
              </Fragment>
            </>
        )}


      </>
  );

}

export default observer(App);
