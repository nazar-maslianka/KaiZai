import { useState } from 'react'
import {
    Menu, Image
} from "semantic-ui-react";

export default function MainMenu() {
    // eslint-disable-next-line no-unused-vars
    const [activeMenuItem, setActiveMenuItem] = useState("expenses");
    return (
        <Menu
            vertical fixed='left'
            inverted style={{ background: '#7d38b3', marginLeft: '1.2em' }}>
            <Menu.Item header={true}>
                <Image src='https://react.semantic-ui.com/images/wireframe/square-image.png' avatar />
                &nbsp;&nbsp;Makoto Edamura
            </Menu.Item>
            <Menu.Item
                name='dashboard'
                active={activeMenuItem === 'dashboard'}>
                Dashboard
            </Menu.Item>
            <Menu.Item>
                Categories
            </Menu.Item>
            <Menu.Item
                name='incomes'
                active={activeMenuItem === 'incomes'}>
                Incomes
            </Menu.Item>
            <Menu.Item
                name='expenses'
                active={activeMenuItem === 'expenses'}>
                Expenses
            </Menu.Item>
        </Menu>)
}