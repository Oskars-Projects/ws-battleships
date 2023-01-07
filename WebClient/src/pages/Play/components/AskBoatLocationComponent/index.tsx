import { useContext, useState } from 'react';
import { GameFieldContext } from '../..';
import GameField from '../../../shared/GameField';

type AskBoatLocationComponentProps = {
	onClick: (x: string, y: number) => void;
	boatLength: number;
};

const AskBoatLocationComponent = ({
	boatLength,
	onClick,
}: AskBoatLocationComponentProps) => {
	const gameFields = useContext(GameFieldContext);
	const [xValue, setXValue] = useState<string>('');
	const [yValue, setYValue] = useState<number>(0);

	return (
		<div>
			<h1>Place your boats</h1>
			<GameField data={gameFields[0]} />
			<label htmlFor="boatLength">
				Place a boat with the length of {boatLength}
			</label>
			<div>
				<label htmlFor="xInput">X</label>
				<input
					type="string"
					id="xInput"
					value={xValue}
					onChange={e => setXValue(e.target.value)}
				/>
			</div>
			<div>
				<label htmlFor="yInput">Y</label>
				<input
					type="number"
					id="yInput"
					value={yValue}
					onChange={e => setYValue(e.target.value as unknown as number)}
				/>
			</div>
			<button onClick={() => onClick(xValue, yValue)}>Send</button>
		</div>
	);
};

export default AskBoatLocationComponent;
