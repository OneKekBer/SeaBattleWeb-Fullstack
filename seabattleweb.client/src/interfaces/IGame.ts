export interface IGame {
	id: string
	status: GameStatus
	firstPlayerId: string
	secondPlayerId: string
	name: string
}

// export enum GameState {
// 	idle = 0,
// 	active = 1,
// 	finished = 2,
// }

export enum GameStatus {
	Idle = 0,
	Active = 1,
	Finished = 2,
}
