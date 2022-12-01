import { useState } from 'react';
import GameState from '../../game/GameState';

const Play = () => {
	const [gameState, setGameState] = useState<GameState>('waiting');
	const [userName, setUserName] = useState<string>('');
	const [gameName, setGameName] = useState<string>('');
	const socket = new WebSocket('ws://localhost:5083/game');

	socket.onopen = () => {
		console.log('Connected to server');
	};

	socket.onmessage = event => {
		console.log(event.data);
		if (event.data == 's_askUserName$') {
			setGameState('askUserName');
		}
		if (event.data == 's_askGameName') {
			setGameState('askGameName');
		}

		if (event.data == 's_sendGameName') {
			// update game state
		}
	};

	socket.onclose = () => {
		console.log('Disconnected from server');
	};
	switch (gameState) {
		case 'askUserName':
			return (
				<div>
					<h1>Enter your username</h1>
					<input
						type="text"
						value={userName}
						onChange={e => setGameName(e.target.value)}
					/>
					<button onClick={() => socket.send('p_sendUserName$' + gameName)}>
						Send
					</button>
				</div>
			);
		case 'askGameName':
			return (
				<div>
					<h1>Enter the game of your game</h1>
					<input
						type="text"
						value={gameName}
						onChange={e => setUserName(e.target.value)}
					/>
					<button onClick={() => socket.send('p_sendGameName$' + userName)}>
						Send
					</button>
				</div>
			);
		default:
		case 'waiting':
			return <h1>Play</h1>;
	}
};
export default Play;
