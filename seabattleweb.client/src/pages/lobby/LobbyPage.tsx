import React from 'react'
import { useNavigate } from 'react-router-dom'
import { useAppSelector } from 'store/Hooks'

interface LobbyPageProps {
	ConnectToExistingGame(): Promise<void>
}

const LobbyPage: React.FC<LobbyPageProps> = ({ ConnectToExistingGame }) => {
	const games = useAppSelector(state => state.games.games)
	const navigate = useNavigate()

	const handleConnectButton = async (id: string) => {
		await ConnectToExistingGame()
		navigate(`/game/${id}`)
	}

	return (
		<div className='bg bg-bg-primary flex justify-center items-center '>
			<div className='min-h-[80vh] p-5 w-[70vw] bg-bg-secondary'>
				<div>
					<h1>Hello</h1>
				</div>
				<div className='flex flex-wrap'>
					{games.map((item, i) => {
						return (
							<div
								className='bg-bg-primary w-[200px] shadow-xl p-3 h-[300px] '
								key={i}
							>
								<h1>{item.usersNames[0]}`s game</h1>
								<div
									onClick={() => {
										handleConnectButton(item.id)
									}}
									className='btn px-4 py-2 rounded-md mx-auto'
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
