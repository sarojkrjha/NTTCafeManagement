import { configureStore } from '@reduxjs/toolkit';
import cafeReducer from './cafeSlice';
import employeeReducer from './employeeSlice';

export const store = configureStore({
    reducer: {
        cafes: cafeReducer,
        employees: employeeReducer,
    },
});
