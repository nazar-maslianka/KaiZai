// eslint-disable-next-line no-unused-vars
import * as React from 'react';
import { Card, Grid, Container } from 'semantic-ui-react'
import './style.css'



export default function Main() {
  
const items = [
  {
    header: 'Project Report - April',
    description:
      'Leverage agile frameworks to provide a robust synopsis for high level overviews.',
    meta: 'ROI: 30%',
  },
  {
    header: 'Project Report - May',
    description:
      'Bring to the table win-win survival strategies to ensure proactive domination.',
    meta: 'ROI: 34%',
  },
  {
    header: 'Project Report - June',
    description:
      'Capitalise on low hanging fruit to identify a ballpark value added activity to beta test.',
    meta: 'ROI: 27%',
  },
  ]
  
  return (
    <Container className='main-reg' >
      <Grid columns={4}  >
    <Grid.Row>
      <Grid.Column>
        <Card.Group items={items} />
      </Grid.Column>
      <Grid.Column>
        <Card.Group items={items}/>
      </Grid.Column>
      <Grid.Column>
        <Card.Group items={items} />
      </Grid.Column>
    </Grid.Row>
      </Grid>
    </Container>
    );
}