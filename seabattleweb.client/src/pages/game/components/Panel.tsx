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

	const getPanelStyles = (state: IPanelState): string => {
		const baseStyles =
			'w-12 h-12 rounded-sm border flex items-center justify-center transition-all duration-200'
	
		switch (state) {
			case 'Shooted':
				return `${baseStyles} bg-red-500 border-red-600 shadow-inner animate-[ping_0.5s_ease-in-out_1]`
			case 'Miss':
				return `${baseStyles} bg-blue-100 border-blue-200 after:content-['•'] after:text-blue-500 after:text-2xl`
			case 'ContainsShip':
				return `${baseStyles} bg-green-500 border-green-600 shadow-md`
			default:
				return `${baseStyles} bg-slate-100 hover:bg-slate-200 border-slate-300
					${currentBoardId ? 'cursor-crosshair' : 'cursor-not-allowed opacity-50'}	
				`
		}
	}

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
		// shootToPanel()
	}

	return (
		<div onClick={handleClick} className={getPanelStyles(panelState)}></div>
	)
}

export default PanelComponent
