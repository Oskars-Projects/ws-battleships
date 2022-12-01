import { useState } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './App.css';
import Home from './pages/Home';
import Play from './pages/Play';
import Header from './pages/shared/components/Header';
import Spectate from './pages/Spectate';

function App() {
	const [count, setCount] = useState(0);

	return (
		<>
			<Header />
			<div className="App">
				<BrowserRouter>
					<Routes>
						<Route path="/" element={<Home />} />
						<Route path="/play" element={<Play />} />
						<Route path="/spectate" element={<Spectate />} />
					</Routes>
				</BrowserRouter>
			</div>
		</>
	);
}

export default App;
