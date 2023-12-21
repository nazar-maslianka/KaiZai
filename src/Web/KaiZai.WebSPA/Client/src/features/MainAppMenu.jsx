// eslint-disable-next-line no-unused-vars
import * as React from 'react';
import './style.css'
import "semantic-ui-css/semantic.min.css";
import { Sidebar, Menu, Icon } from 'semantic-ui-react';
export default function MainAppMenu() {

  return (
    // visible={visible}
    <Sidebar as={Menu} icon="labeled" vertical  width='thin' className='sidebar' >
          <Menu.Item>
            <Icon name="home" />
            Dashboard
          </Menu.Item>
          <Menu.Item>
            <Icon name="chart bar" />
            Analytics
          </Menu.Item>
          {/* Add more menu items as needed */}
    </Sidebar>
  );
}