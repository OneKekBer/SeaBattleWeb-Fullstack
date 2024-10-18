import { HubConnection } from '@microsoft/signalr'
import React from 'react'
import { useNavigate } from 'react-router-dom'
import { useAppSelector } from 'store/Hooks'

interface LobbyPageProps {
	Connection: HubConnection | null
}

const LobbyPage: React.FC<LobbyPageProps> = ({ Connection }) => {
	const games = useAppSelector(state => state.games.games)
	const navigate = useNavigate()

	const handleConnectButton = async (id: string) => {
		navigate(`/game/${id}`)
	}

	const handleCreateNewGameButton = async () => {
		Connection?.invoke('CreateNewGame')
	}

	return (
		<div className='flex items-center justify-center bg bg-bg-primary '>
			<div className='min-h-[80vh] p-5 w-[70vw] bg-bg-secondary'>
				<div>
					<h1>Hello</h1>
				</div>
				<div className='flex flex-wrap'>
					<button onClick={handleCreateNewGameButton}>
						create new game
					</button>
					{games.map((item, i) => {
						return (
							<div
								className='bg-bg-primary w-[200px] shadow-xl p-3 h-[300px]'
								key={i}
							>
								<h1>{item.usersNames[0]}`s game</h1>
								<div>{item.state.toLocaleString()}</div>
								<div
									onClick={() => {
										handleConnectButton(item.id)
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
