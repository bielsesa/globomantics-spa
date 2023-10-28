import './App.css';
import Header from './Header';
import HouseList from '../house/HouseList';

function App() {
    return (
        <div className='container'>
            <Header subtitle='Providing houses all over the world :D' />
            <HouseList />
        </div>
    );
}

export default App;
