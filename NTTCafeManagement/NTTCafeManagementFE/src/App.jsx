import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { Provider } from 'react-redux';
import { store } from './redux/store';
import CafePage from './components/CafePage';
import EmployeePage from './components/EmployeePage';
import AddEditCafe from './components/AddEditCafe';
import AddEditEmployee from './components/AddEditEmployee';
import 'ag-grid-community/styles/ag-grid.css'; // Core Grid CSS
import 'ag-grid-community/styles/ag-theme-alpine.css'; // Alpine Theme CSS
import 'ag-grid-community/styles/ag-theme-balham.css'; // Balham Theme CSS (Optional)
import 'antd/dist/reset.css';

const App = () => {
    return (
        <Provider store={store}>
            <Router>
                <Routes>
                    <Route path="/" element={<CafePage />} />
                    <Route path="/cafes" element={<CafePage />} />
                    <Route path="/employees" element={<EmployeePage />} />
                    <Route path="/cafes/add" element={<AddEditCafe />} />
                    <Route path="/cafes/edit/:id" element={<AddEditCafe />} />
                    <Route path="/employees/add" element={<AddEditEmployee />} />
                    <Route path="/employees/edit/:id" element={<AddEditEmployee />} />
                </Routes>
            </Router>
        </Provider>
    );
};

export default App;
