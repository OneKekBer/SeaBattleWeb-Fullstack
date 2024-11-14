import React from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import WaitSection from './section/WaitSection'
import { HubConnection } from '@microsoft/signalr'
import PlaySection from './section/PlaySection'
import { getGameById } from 'store/slices/GameSlice'
import { useSelector } from 'react-redux'
import { RootState } from 'store/Store'

interface GamePageInterface {
	gameConnection: HubConnection | null
}

const GamePage: React.FC<GamePageInterface> = ({ gameConnection }) => {
	const navigate = useNavigate()
	const { gameId } = useParams<{ gameId: string }>()
	console.log(gameId)

	// useEffect(() => {
	// 	if (gameId == undefined) navigate('/')
	// }, [gameId])

	console.log('game id ' + gameId)

	const game = useSelector((state: RootState) =>
		getGameById(state.games, gameId)
	)

	return (
		<div className='flex flex-col items-center justify-center bg-bg-primary'>
			{game?.status != null && gameId ? (
				<div>
					{game.status == 'Idle' ? (
						<WaitSection game={game} gameId={gameId} />
					) : (
						<PlaySection />
					)}
				</div>
			) : (
				<div>error</div>
			)}

			{/* <PlaySection /> */}
		</div>
	)
}

export default GamePage
