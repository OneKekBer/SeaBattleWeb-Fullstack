import React from 'react'
import PanelComponent from './components/Panel'

// interface GamePageProps {}

const GamePage: React.FC = () => {
	const matrix = Array.from({ length: 9 }, () => Array(9).fill(0))

	return (
		<div className='flex items-center justify-center bg-bg-primary'>
			<div className='grid grid-cols-9 bg-bg-secondary'>
				{matrix.map((row, rowIndex) =>
					row.map((col, colIndex) => (
						<PanelComponent
							key={`${rowIndex}-${colIndex}`}
							ownCoords={`${rowIndex}-${colIndex}`}
						/>
					))
				)}
			</div>
		</div>
	)
}

export default GamePage
