import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import IGame from './interfaces/IGame'

type GetAllGamesCallback = (game: IGame[]) => void

class WebSocket {
	conn

	constructor() {
		this.conn = new HubConnectionBuilder()
			.withUrl(import.meta.env.VITE_API_URL + 'chatHub')
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build()
	}

	async Connect() {
		try {
			await this.conn.start()
			console.log('Connection started')
		} catch (err) {
			console.error('Error while starting connection: ', err)
		}
	}

	async CreateNewGame() {}

	async ConnectToExistingGame() {
		try {
			this.conn.invoke('ConnectToExistingGame', {})
		} catch {
			console.log()
		}
	}

	//////

	GetAllGames(callback: GetAllGamesCallback) {
		this.conn.on('GetAllGames', (games: IGame[]) => {
			return callback(games)
		})
	}
}

export default WebSocket
