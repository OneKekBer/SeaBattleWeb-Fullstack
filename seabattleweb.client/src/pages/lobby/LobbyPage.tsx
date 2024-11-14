import { HubConnection } from '@microsoft/signalr'
import React from 'react'

import { useAppSelector } from 'store/Hooks'

interface LobbyPageProps {
	lobbyConnection: HubConnection | null
	ConnectToGame: (gameId: string) => Promise<void>
}

const LobbyPage: React.FC<LobbyPageProps> = ({
	lobbyConnection,
	ConnectToGame,
}) => {
	const games = useAppSelector(state => state.lobbies.lobbies)
	const [name, setName] = React.useState('')

	const handleCreateNewGameButton = async () => {
		if (!name.trim()) return
		lobbyConnection?.invoke('CreateNewGame', { name: name })
		setName('')
	}

	return (
		<div className='flex items-center justify-center bg bg-bg-primary '>
			<div className='min-h-[80vh] p-5 w-[70vw] bg-bg-secondary'>
				<div>
					<h1>Hello</h1>
				</div>
				<div className='flex flex-wrap'>
					<div className='flex w-full gap-2 mb-4'>
						<input
							type='text'
							value={name}
							onChange={e => setName(e.target.value)}
							placeholder='Enter game name'
							className='px-4 py-2 rounded-md bg-bg-primary'
						/>
						<button
							onClick={handleCreateNewGameButton}
							className='px-4 py-2 rounded-md btn'
						>
							Create new game
						</button>
					</div>
					{games.map((item, i) => {
						return (
							<div
								className='bg-bg-primary w-[200px] shadow-xl p-3 h-[300px]'
								key={i}
							>
								<div>{item.status}</div>
								<div>{item.name}</div>
								<div
									onClick={() => {
										ConnectToGame(item.id)
									}}
									className='px-4 py-2 mx-auto rounded-md btn'
								>
									Connect
								</div>
							</div>
						)
					})}
				</div>
			</div>
		</div>
	)
}

export default LobbyPage
