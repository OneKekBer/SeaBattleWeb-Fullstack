import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import IGame from './interfaces/IGame'
import { addGames } from 'store/slices/GameSlice'
import { AppDispatch } from 'store/Store'

class WebSocket {
	conn
	private dispatch

	constructor(dispatch: AppDispatch) {
		this.conn = new HubConnectionBuilder()
			.withUrl(import.meta.env.VITE_API_URL + 'chatHub')
			.configureLogging(LogLevel.Information)
			.withAutomaticReconnect()
			.build()

		this.dispatch = dispatch
	}

	async ensureConnected() {
		if (this.conn.state !== 'Connected') {
			try {
				await this.conn.start()
				console.log('Reconnected successfully')
			} catch (err) {
				console.error('Error while reconnecting: ', err)
			}
		}
	}

	async Connect() {
		try {
			await this.conn.start()
			console.log('Connection started')
		} catch (err) {
			console.error('Error while starting connection: ', err)
		}
	}

	async CreateNewGame() {
		this.ensureConnected()
		console.log('rabotaju')
		try {
			this.conn.invoke('CreateNewGame')
		} catch {
			console.log()
		}
	}

	async ConnectToExistingGame() {
		this.ensureConnected()

		try {
			this.conn.invoke('ConnectToExistingGame', {})
		} catch {
			console.log()
		}
	}

	//////

	GetAllGames() {
		this.conn.on('GetAllGames', (games: IGame[]) => {
			this.dispatch(addGames(games))
		})
	}
}

export default WebSocket
