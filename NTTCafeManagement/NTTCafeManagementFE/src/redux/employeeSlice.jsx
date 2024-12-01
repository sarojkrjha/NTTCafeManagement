import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';

// Initial state
const initialState = {
    employees: [],
    loading: false,
    error: null,
};

// Async thunk to fetch employees
export const fetchEmployees = createAsyncThunk('employees/fetchEmployees', async () => {
    const response = await axios.get('https://localhost:44371/api/employees');
    return response.data;
});
// Asynchronous Thunks for adding and updating employees
export const addEmployee = createAsyncThunk(
    'employees/addEmployee',
    async (employeeData, { rejectWithValue }) => {
        try {
            // Mock API call - replace this with actual API logic
            const response = await fetch('https://localhost:44371/api/employees', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(employeeData),
            });

            if (!response.ok) {
                throw new Error('Failed to add employee');
            }

            const data = await response.json();
            return data; // Return the added employee data
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const updateEmployee = createAsyncThunk(
    'employees/updateEmployee',
    async ({ id, employeeData }, { rejectWithValue }) => {
        try {
            // Mock API call - replace with actual API logic
            const response = await fetch(`https://localhost:44371/api/employees/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(employeeData),
            });

            if (!response.ok) {
                throw new Error('Failed to update employee');
            }

            const data = await response.json();
            return data; // Return the updated employee data
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

// Async thunk to delete an employee
export const deleteEmployee = createAsyncThunk('employees/deleteEmployee', async (id) => {
    await axios.delete(`https://localhost:44371/api/employee?id=${id}`);
    return id;
});

// Create slice
const employeeSlice = createSlice({
    name: 'employees',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchEmployees.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchEmployees.fulfilled, (state, action) => {
                state.loading = false;
                state.employees = action.payload;
            })
            .addCase(fetchEmployees.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            })

            .addCase(addEmployee.pending, (state) => {
                state.loading = true;
            })
            .addCase(addEmployee.fulfilled, (state, action) => {
                state.loading = false;
                state.employees.push(action.payload); // Add new employee to the list
            })
            .addCase(addEmployee.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload;
            })

            .addCase(updateEmployee.pending, (state) => {
                state.loading = true;
            })
            .addCase(updateEmployee.fulfilled, (state, action) => {
                state.loading = false;
                const index = state.employees.findIndex(
                    (employee) => employee.id === action.payload.id
                );
                if (index !== -1) {
                    state.employees[index] = action.payload; // Update the employee in the list
                }
            })
            .addCase(updateEmployee.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload;
            })

            .addCase(deleteEmployee.fulfilled, (state, action) => {
                state.employees = state.employees.filter(emp => emp.id !== action.payload);
            });
    },
});

export default employeeSlice.reducer;
