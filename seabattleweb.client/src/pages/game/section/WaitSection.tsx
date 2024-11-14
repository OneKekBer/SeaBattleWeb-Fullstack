import { IGame } from 'interfaces/IGame'
import React from 'react'
import { useCookies } from 'react-cookie'

interface WaitSectionProps {
	gameId: string
	game: IGame | undefined
}

const WaitSection: React.FC<WaitSectionProps> = ({ gameId, game }) => {
	const startGameHandle = () => {}
	const [cookies, setCookie] = useCookies(['user-id'])

	return (
		<div>
			<div>
				{game?.firstPlayerId == cookies['user-id'] ? 'you' : 'opponent'}
			</div>
			<div>
				{game?.secondPlayerId == cookies['user-id'] ? 'you' : 'opponent'}
			</div>

			<div onClick={startGameHandle}>start game</div>
		</div>
	)
}

export default WaitSection
