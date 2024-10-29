import React, { useEffect, useState } from 'react'
import PanelComponent from './components/Panel'
import { useParams } from 'react-router-dom'
import axios from 'axios'
import WaitSection from './section/WaitSection'
import { HubConnection } from '@microsoft/signalr'

interface GamePageInterface {
	gameConnection: HubConnection | null
}

const GamePage: React.FC<GamePageInterface> = ({ gameConnection }) => {
	const matrix = Array.from({ length: 9 }, () => Array(9).fill(0))
	const [currentBoardId, setCurrentBoardId] = useState('')
	const [gameStatus, setGameStatus] = useState<null | string>(null)
	const { id } = useParams<{ id: string }>() // Extract id from params
	console.log(id)

	const createBoard = async () => {
		try {
			const res = await fetch(
				`${import.meta.env.VITE_API_URL}api/board/create-board`,
				{ method: 'GET' }
			)

			if (!res.ok) throw new Error('Failed to create board')

			const data = await res.json()
			if (data && data.boardId) {
				setCurrentBoardId(data.boardId)
			}
		} catch (error) {
			console.error('Error creating board:', error)
		}
	}

	const getGameStatus = async () => {
		try {
			const res = await axios.post(
				`${import.meta.env.VITE_API_URL}api/game/get-status`,
				{ gameId: id }, // Pass id as part of the request body
				{ headers: { 'Content-Type': 'application/json' } }
			)
			setGameStatus(res.data.status)
			console.log(res.data.status)
		} catch (error) {
			console.error('Error fetching game status:', error)
		}
	}

	useEffect(() => {
		if (id) {
			getGameStatus()
		}
	}, [id])

	return (
		<div className='flex flex-col items-center justify-center bg-bg-primary'>
			{gameStatus != null && (
				<div>{gameStatus == 'Idle' ? <WaitSection /> : <div></div>}</div>
			)}
			{/* {currentBoardId !== '' && (
				<div className='grid grid-cols-9 bg-bg-secondary'>
					{matrix.map((row, rowIndex) =>
						row.map((_, colIndex) => (
							<PanelComponent
								currentBoardId={currentBoardId}
								key={`${rowIndex}-${colIndex}`}
								ownCoords={`${rowIndex}-${colIndex}`}
							/>
						))
					)}
				</div>
			)}

			<div className='flex flex-col gap-3'></div>
			<button onClick={createBoard}>new board</button>
			<div>current board id: {currentBoardId}</div>
			<div>Game Status: {gameStatus}</div> */}
		</div>
	)
}

export default GamePage
