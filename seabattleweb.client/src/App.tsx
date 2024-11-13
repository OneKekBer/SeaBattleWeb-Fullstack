import { Route, Routes, useNavigate } from 'react-router-dom'
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

import { IGame } from 'interfaces/IGame'
import { addLobby } from 'store/slices/LobbySlice'
import { addGame } from 'store/slices/GameSlice'

interface IGameConnect {
	gameStatus: string
	gameId: string
	firstPlayerId: string
	secondPlayerId: string
}

function App() {
	const dispatch = useAppDispatch()
	const [cookies, setCookie] = useCookies(['user-id'])
	const [lobbyConnection, setLobbyConnection] = useState<HubConnection | null>(
		null
	)
	const navigate = useNavigate()
	const [gameConnection, setGameConnection] = useState<HubConnection | null>(
		null
	)

	const ConnectToGame = async (gameId: string) => {
		const conn = new HubConnectionBuilder()
			.withUrl(import.meta.env.VITE_API_URL + 'gameHub')
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build()

		conn.on('Connect', (gameInfo: IGameConnect) => {
			console.log('gameinfo:' + gameInfo.gameId)
			console.log('gameinfo:' + gameInfo.gameStatus)
			navigate(`/game/${gameInfo.gameId}`)
			dispatch(
				addGame({
					id: gameInfo.gameId,
					status: gameInfo.gameStatus,
					firstPlayerId: gameInfo.firstPlayerId,
					secondPlayerId: gameInfo.secondPlayerId,
				})
			)
		})

		conn.on('StartGame', () => {})

		try {
			await conn.start()
			await conn?.invoke('Connect', {
				userId: cookies['user-id'],
				gameId: gameId,
			})
			setGameConnection(conn)
		} catch (err) {
			console.error(
				'Error while establishing connection or sending message:',
				err
			)
		}
	}

	const ConnectToLobby = async () => {
		const conn = new HubConnectionBuilder()
			.withUrl(import.meta.env.VITE_API_URL + 'lobbyHub')
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build()

		conn.on('GetAllGames', (games: IGame[]) => {
			dispatch(addLobby(games))
		})

		try {
			await conn.start()
			// await conn?.invoke('Connect')
			setLobbyConnection(conn)
		} catch (err) {
			console.error(
				'Error while establishing connection or sending message:',
				err
			)
		}
	}

	useEffect(() => {
		ConnectToLobby()
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
							ConnectToGame={ConnectToGame}
							lobbyConnection={lobbyConnection}
						/>
					}
					path='/'
				/>
				<Route
					element={<GamePage gameConnection={gameConnection} />}
					path='/game/:gameId'
				/>
			</Routes>
		</div>
	)
}

export default App
