import {Button, Container, Header, Image, Segment} from "semantic-ui-react";
import {Link} from "react-router-dom";
import FallingObjects from "../../app/common/components/FallingObjects.tsx";
import SmokeEffect from "../../app/common/components/SmokeEffect.tsx";
import {useStore} from "../../app/stores/Store.ts";
import LoginForm from "../users/LoginForm.tsx";
import RegisterForm from "../users/RegisterForm.tsx";


export default function HomePage() {
    const {userStore,modalStore} = useStore();


    return (
        <>

            <Segment inverted textAlign='center' vertical className='masthead' >

                <FallingObjects numberOfObjects={200}/>
                <SmokeEffect/>
                <Container text textAlign={"center"}>

                    <Header as='h1' inverted textAlign={"center"}>
                        ContactsNet

                    </Header>

                    {userStore.isLoggedIn ? (
                        <>
                            <Header as='h2' inverted content='Welcome to Contacts'/>
                            <Button as={Link} to='/contacts' size='huge' inverted>
                                go to posts!
                            </Button>
                        </>

                    ) : (

                        <>
                            <Button className="button-width" onClick={()=> modalStore.openModal(<LoginForm/>)} size='huge' inverted color={"linkedin"}>
                                Login!
                            </Button>
                            <Button className="button-width" onClick={()=> modalStore.openModal(<RegisterForm/>)} size='huge' inverted color={"linkedin"}>
                                Register!
                            </Button>
                        </>
                    )}


                </Container>

            </Segment>
        </>
    )

}