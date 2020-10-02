import React, {useState, useEffect} from 'react';
import axios from 'axios';
import { Container } from 'semantic-ui-react';
import { IActivity } from '../models/activity';
import { NavBar } from '../../features/navbar/NavBar';
import { ActivityDashboard } from '../../features/activities/dashboard/ActivityDashboard';


const App = () => {
  const [activities, setActivities] = useState<IActivity[]>([])


  useEffect( () => {
    axios.get<IActivity[]>('http://localhost:5555/api/activities')
    .then((response) => {
     setActivities(response.data)
   });
  }, []);

  return (
    <div>
      <NavBar />
      <Container style={{marginTop: '7em'}}>
        <ActivityDashboard activities={activities}/>
      </Container>
    </div>
    );
  }


export default App;
