import { configureStore } from '@reduxjs/toolkit'
import gamesSlice from './slices/GameSlice'
// ...

export const store = configureStore({
	reducer: {
		games: gamesSlice,
	},
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch
