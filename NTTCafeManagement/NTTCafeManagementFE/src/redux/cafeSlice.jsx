import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';

// Initial state
const initialState = {
    cafes: [],
    loading: false,
    error: null,
};

// Async thunk to fetch cafes
export const fetchCafes = createAsyncThunk('cafes/fetchCafes', async () => {
    const response = await axios.get('https://localhost:44371/api/cafes');
    return response.data;
});
// Asynchronous Thunks for adding and updating cafes
export const addCafe = createAsyncThunk(
    'cafes/addCafe',
    async (cafeData, { rejectWithValue }) => {
        try {
            // Mock API call - replace this with actual API logic
            const response = await fetch('https://localhost:44371/api/cafes', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(cafeData),
            });

            if (!response.ok) {
                throw new Error('Failed to add cafe');
            }

            const data = await response.json();
            return data; // Return the added cafe data
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

export const updateCafe = createAsyncThunk(
    'cafes/updateCafe',
    async ({ id, cafeData }, { rejectWithValue }) => {
        try {
            // Mock API call - replace with actual API logic
            const response = await fetch(`https://localhost:44371/api/cafes/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(cafeData),
            });

            if (!response.ok) {
                throw new Error('Failed to update cafe');
            }

            const data = await response.json();
            return data; // Return the updated cafe data
        } catch (error) {
            return rejectWithValue(error.message);
        }
    }
);

// Async thunk to delete a cafe
export const deleteCafe = createAsyncThunk('cafes/deleteCafe', async (id) => {
    await axios.delete(`https://localhost:44371/api/cafe?id=${id}`);
    return id;  // Return cafe ID for deletion
});

// Create slice
const cafeSlice = createSlice({
    name: 'cafes',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchCafes.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchCafes.fulfilled, (state, action) => {
                state.loading = false;
                state.cafes = action.payload;
            })
            .addCase(fetchCafes.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message;
            })

            .addCase(addCafe.pending, (state) => {
                state.loading = true;
            })
            .addCase(addCafe.fulfilled, (state, action) => {
                state.loading = false;
                state.cafes.push(action.payload); // Add the new cafe to the list
            })
            .addCase(addCafe.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload;
            })

            .addCase(updateCafe.pending, (state) => {
                state.loading = true;
            })
            .addCase(updateCafe.fulfilled, (state, action) => {
                state.loading = false;
                const index = state.cafes.findIndex((cafe) => cafe.id === action.payload.id);
                if (index !== -1) {
                    state.cafes[index] = action.payload; // Update the cafe in the list
                }
            })
            .addCase(updateCafe.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload;
            })
            
            .addCase(deleteCafe.fulfilled, (state, action) => {
                state.cafes = state.cafes.filter(cafe => cafe.id !== action.payload);
            });
    },
});

export default cafeSlice.reducer;
