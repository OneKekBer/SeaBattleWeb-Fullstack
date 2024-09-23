import { Route, Routes } from 'react-router-dom'
import LobbyPage from './pages/lobby/LobbyPage'
import WebSocket from './WebSocket'
import { useEffect } from 'react'
import { useAppDispatch } from 'store/Hooks'
import { addGames } from 'store/slices/GameSlice'
import GamePage from 'pages/game/GamePage'
function App() {
	const webSocket = new WebSocket()
	const dispatch = useAppDispatch()

	useEffect(() => {
		webSocket.GetAllGames(games => {
			console.log(games)
			dispatch(addGames(games))
		})
	}, [])
	return (
		<div className='light'>
			<Routes>
				<Route
					element={
						<LobbyPage
							ConnectToExistingGame={webSocket.ConnectToExistingGame}
						/>
					}
					path='/'
				/>
				<Route element={<GamePage />} path='/game/:id' />
			</Routes>
		</div>
	)
}

export default App
