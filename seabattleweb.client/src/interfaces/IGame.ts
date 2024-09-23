export default interface IGame {
	id: string
	state: 'idle' | 'active' | 'finished'
	usersNames: string[]
}
