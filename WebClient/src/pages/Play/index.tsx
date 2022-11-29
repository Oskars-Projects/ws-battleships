const Play = () => {
	const socket = new WebSocket('ws://localhost:5083/game');

	socket.onopen = () => {
		console.log('Connected to server');
	};

	socket.onmessage = event => {
		console.log('received: ' + event.data);
	};

	socket.onclose = () => {
		console.log('Disconnected from server');
	};

	return <h1>Play</h1>;
};
export default Play;
