import { Route, Routes } from 'react-router-dom'
import LobbyPage from './pages/lobby/LobbyPage'
import { useEffect, useState } from 'react'
import GamePage from 'pages/game/GamePage'
import { useCookies } from 'react-cookie'
import { v4 as uuidv4 } from 'uuid'
import { useAppDispatch } from 'store/Hooks'
import {
	HubConnection,
	HubConnectionBuilder,
	LogLevel,
} from '@microsoft/signalr'
import { addGames } from 'store/slices/GameSlice'
import { IGame } from 'interfaces/IGame'

function App() {
	const dispatch = useAppDispatch()
	const [cookies, setCookie] = useCookies(['user-id'])
	const [connection, setConnection] = useState<HubConnection | null>(null)

	const ConnectToHub = async () => {
		const conn = new HubConnectionBuilder()
			.withUrl(import.meta.env.VITE_API_URL + 'gameHub')
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build()

		conn.on('GetAllGames', (games: IGame[]) => {
			dispatch(addGames(games))
		})

		try {
			await conn.start()
			await conn?.invoke('Connect')
			setConnection(conn)
		} catch (err) {
			console.error(
				'Error while establishing connection or sending message:',
				err
			)
		}
	}

	useEffect(() => {
		ConnectToHub()
	}, [])

	useEffect(() => {
		if (!cookies['user-id']) {
			setCookie('user-id', uuidv4())
		}
	}, [cookies, setCookie])

	return (
		<div className='light'>
			<Routes>
				<Route element={<LobbyPage Connection={connection} />} path='/' />
				<Route element={<GamePage />} path='/game/:id' />
			</Routes>
		</div>
	)
}

export default App
