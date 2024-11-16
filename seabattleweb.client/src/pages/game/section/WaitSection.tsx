import { HubConnection } from '@microsoft/signalr'
import { IGame } from 'interfaces/IGame'
import React from 'react'
import { useCookies } from 'react-cookie'

interface WaitSectionProps {
	gameId: string
	game: IGame | undefined
	gameConnection: HubConnection | null
}

const WaitSection: React.FC<WaitSectionProps> = ({
	gameId,
	game,
	gameConnection,
}) => {
	const [cookies] = useCookies(['user-id'])
	const emptyId = '00000000-0000-0000-0000-000000000000'

	const startGameHandle = async () => {
		console.log('start')
		await gameConnection?.invoke('StartGame', { gameId: gameId })
	}

	return (
		<div>
			<div className='flex gap-4'>
				<div className='rounded-lg bg-slate-300 w-[100px] h-[100px] flex justify-center items-center'>
					{game?.firstPlayerId == emptyId ? (
						<div>empty slot</div>
					) : (
						<div>
							{game?.firstPlayerId == cookies['user-id']
								? 'you'
								: 'opponent'}
						</div>
					)}
				</div>
				<div className='rounded-lg bg-slate-300 w-[100px] h-[100px] flex justify-center items-center'>
					{game?.secondPlayerId == emptyId ? (
						<div>empty slot</div>
					) : (
						<div>
							{game?.secondPlayerId == cookies['user-id']
								? 'you'
								: 'opponent'}
						</div>
					)}
				</div>
			</div>
			<div
				className='px-4 py-2 mx-auto rounded-md btn'
				onClick={startGameHandle}
			>
				start game
			</div>
		</div>
	)
}

export default WaitSection
