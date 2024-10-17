import { Route, Routes } from 'react-router-dom'
import LobbyPage from './pages/lobby/LobbyPage'
import WebSocket from './WebSocket'
import { useEffect } from 'react'
import GamePage from 'pages/game/GamePage'
import { useCookies } from 'react-cookie'
import { v4 as uuidv4 } from 'uuid'
import { useAppDispatch } from 'store/Hooks'

function App() {
	const dispatch = useAppDispatch()

	const webSocket = new WebSocket(dispatch)
	const [cookies, setCookie] = useCookies(['user-id'])

	useEffect(() => {
		webSocket.Connect()
	}, [])

	useEffect(() => {
		if (!cookies['user-id']) {
			setCookie('user-id', uuidv4())
		}
	}, [cookies, setCookie])

	return (
		<div className='light'>
			<Routes>
				<Route
					element={
						<LobbyPage
							Connection={webSocket.conn}
							CreateNewGame={webSocket.CreateNewGame}
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
