
import  {FC} from "react";
import {Container, Dropdown, Menu} from "semantic-ui-react";
import {NavLink} from "react-router-dom";
import {useStore} from "../../App/Stores/Store.ts";

    


const NavBar: FC = () =>  {
    const {userStore:{user,logout, isLoggedIn}} = useStore();
    return (
        <Menu inverted fixed='top' size="large">
            <Container  fluid={true}> {/* This will take up available space */}
                <Menu.Item as={NavLink} to='/' header>
                </Menu.Item>
                <Menu.Item as={NavLink} to={`/profile/${user?.id}`} name="Profile" />
                <Menu.Item as={NavLink} to='/posts' name='Posts' />
            </Container>
            <Container fluid={true}>
                {isLoggedIn && (
                <Menu.Item position='right'>
                    <Dropdown pointing='top left' text={user?.name +" "+ " "+ user?.surname}>
                        <Dropdown.Menu>
                            <Dropdown.Item as={NavLink} to={`/profile/${user?.id}`} text='My profile' icon='user' />
                            <Dropdown.Item onClick={logout} text='Logout' icon='power' />
                        </Dropdown.Menu>
                    </Dropdown>
                        
                </Menu.Item>
                )}
            </Container>
        </Menu>
    );
}

export default NavBar;