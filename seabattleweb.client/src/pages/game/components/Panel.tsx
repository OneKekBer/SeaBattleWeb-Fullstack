import React, { useState } from 'react'

interface PanelComponentProps {
	ownCoords: string
}

type IPanelState = 'Empty' | 'ContainsShip' | 'Shooted' | 'Miss'

const PanelComponent: React.FC<PanelComponentProps> = ({ ownCoords }) => {
	const [panelState, setPanelState] = useState<IPanelState>('Empty')

	const shootToPanel = async () => {
		const res = await fetch((process.env.VITE_API_URL as string) + '/', {})
	}

	const handleClick = () => {
		console.log(ownCoords)
		// Пример изменения состояния панели при клике (можно доработать логику)
		setPanelState('Shooted')
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
