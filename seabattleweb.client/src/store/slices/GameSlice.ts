import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import IGame from 'interfaces/IGame'

// Define a type for the slice state
interface CounterState {
	games: IGame[]
}

// Define the initial state using that type
const initialState: CounterState = {
	games: [{ id: '31231', state: 'idle', usersNames: ['murray'] }],
}

export const gamesSlice = createSlice({
	name: 'games',
	// `createSlice` will infer the state type from the `initialState` argument
	initialState,
	reducers: {
		addGames: (state, action: PayloadAction<IGame[]>) => {
			state.games = []
			state.games = action.payload
		},
	},
})

export const { addGames } = gamesSlice.actions

export default gamesSlice.reducer
