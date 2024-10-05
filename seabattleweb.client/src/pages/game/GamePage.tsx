import React, { useState } from 'react'
import PanelComponent from './components/Panel'

// interface GamePageProps {}

const GamePage: React.FC = () => {
	const matrix = Array.from({ length: 9 }, () => Array(9).fill(0))
	const [currentBoardId, setCurrentBoardId] = useState('')
	const createBoard = async () => {
		console.log(import.meta.env.VITE_API_URL + 'api/board/create-board')
		try {
			const res = await fetch(
				import.meta.env.VITE_API_URL + 'api/board/create-board',
				{
					method: 'GET',
				}
			)

			if (!res.ok) {
				throw new Error('Failed to create board')
			}

			const data = await res.json()
			console.log(data)
			if (data && data.boardId) {
				console.log(data)
				setCurrentBoardId(data.boardId)
			}
		} catch (error) {
			console.error('Error creating board:', error)
		}
	}

	return (
		<div className='flex flex-col items-center justify-center bg-bg-primary'>
			{currentBoardId != '' && (
				<div className='grid grid-cols-9 bg-bg-secondary'>
					{matrix.map((row, rowIndex) =>
						row.map((col, colIndex) => (
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
			<div>current board id:{currentBoardId}</div>
		</div>
	)
}

export default GamePage
