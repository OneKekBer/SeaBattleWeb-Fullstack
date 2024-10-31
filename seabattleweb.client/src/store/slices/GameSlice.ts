import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { IGame } from 'interfaces/IGame'

// Define a type for the slice state
interface CounterState {
	games: IGame[]
}

// Define the initial state using that type
const initialState: CounterState = {
	games: [],
}

export const gamesSlice = createSlice({
	name: 'games',
	initialState,
	reducers: {
		addGame: (state, action: PayloadAction<IGame>) => {
			const existingGame = state.games.find(
				game => game.id === action.payload.id
			)
			if (!existingGame) {
				state.games.push(action.payload)
			}
		},
	},
})

// Selector to find a game by its ID
export const getGameById = (state: CounterState, id: string) =>
	state.games.find(game => game.id === id)

export const { addGame } = gamesSlice.actions

export default gamesSlice.reducer
