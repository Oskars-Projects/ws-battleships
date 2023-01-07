type GameState =
	| 'waiting'
	| 'askUserName'
	| 'askGameName'
	| 'askBoatLocation'
	| 'askFireLocation';

export default GameState;
