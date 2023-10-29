import './App.css';
import Header from './Header';
import HouseList from '../house/HouseList';
import HouseDetail from '../house/HouseDetail';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import HouseAdd from '../house/HouseAdd';
import HouseEdit from '../house/HouseEdit';

function App() {
    return (
        // directly inside BrowserRouter goes the code that you want to always show,
        // regardless of the page you're in (e.g. the header and footer)
        <BrowserRouter>
            <div className='container'>
                <Header subtitle='Providing houses all over the world :D' />
                <Routes>
                    <Route path='/' element={<HouseList />}></Route>
                    <Route path='/house/:id' element={<HouseDetail />}></Route>
                    <Route path='/house/add' element={<HouseAdd />}></Route>
                    <Route
                        path='/house/edit/:id'
                        element={<HouseEdit />}
                    ></Route>
                </Routes>
            </div>
        </BrowserRouter>
    );
}

export default App;
