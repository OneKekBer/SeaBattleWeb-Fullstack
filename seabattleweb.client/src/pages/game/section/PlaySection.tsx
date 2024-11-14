import React, { useState } from 'react'
import PanelComponent from '../components/Panel'

interface PlaySectionProps {}

const PlaySection: React.FC<PlaySectionProps> = () => {
	const matrix = Array.from({ length: 9 }, () => Array(9).fill(0))
	console.log(matrix)
	const [currentBoardId, setCurrentBoardId] = useState('')

	return (
		<div className='flex items-center justify-center'>
			<div className='w-[90vw] grid grid-cols-2 justify-center items-center justify-items-center bg-slate-100 h-[90vh]'>
				<div className=''>
					<div className='grid grid-cols-9'>
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
				</div>
				<div className=' bg-slate-200'>board</div>
			</div>
		</div>
	)
}

export default PlaySection
