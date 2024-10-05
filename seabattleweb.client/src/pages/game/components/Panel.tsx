import React, { useState } from 'react'

interface PanelComponentProps {
	ownCoords: string
	currentBoardId: string | null
}

type IPanelState = 'Empty' | 'ContainsShip' | 'Shooted' | 'Miss'
interface IData {
	status: IPanelState
}

const PanelComponent: React.FC<PanelComponentProps> = ({
	ownCoords,
	currentBoardId,
}) => {
	const [panelState, setPanelState] = useState<IPanelState>('Empty')

	const shootToPanel = async () => {
		const res = await fetch(
			import.meta.env.VITE_API_URL + 'api/board/shoot-board',
			{
				method: 'POST',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify({
					boardId: currentBoardId,
					coords: {
						X: Number(ownCoords.split('-')[1]),
						Y: Number(ownCoords.split('-')[0]),
					},
				}),
			}
		)

		if (res.ok) {
			const data: IData = await res.json()
			console.log(data.status)
			setPanelState(data.status)
		}
	}

	const handleClick = () => {
		console.log(ownCoords)
		shootToPanel()
	}

	return (
		<div
			onClick={handleClick}
			className={`w-10 h-10 border border-gray-100 flex items-center justify-center ${
				panelState === 'Shooted' ? 'bg-red-500' : 'bg-gray-200'
			}`}
		></div>
	)
}

export default PanelComponent
