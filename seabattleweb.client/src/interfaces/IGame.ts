export interface IGame {
	id: string
	state: GameState
	usersNames: string[]
}

export enum GameState {
	idle = 0,
	active = 1,
	finished = 2,
}
