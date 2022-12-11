import './gameField.css';

export type GameFieldProps = {
	data: number[][];
};

const GameField = (props: GameFieldProps) => {
	return (
		<div className="grid">
			<div className="column">
				<div className="cell">&nbsp;</div>
				{Array.apply(null, Array(props.data[0].length))
					.map(function () {})
					.map((_, index) => (
						<div className="cell" key={index}>
							{index + 1}
						</div>
					))}
			</div>
			{props.data.map((column, i) => (
				<div className="column" key={i}>
					<div className="cell">{String.fromCharCode(i + 65)}</div>
					{column.map((val, j) => (
						<div
							className="cell"
							style={{ backgroundColor: mapNumberToColor(val) }}
							key={i * props.data[0].length + j}
						>
							&nbsp;
						</div>
					))}
				</div>
			))}
		</div>
	);
};

const mapNumberToColor = (number: number) => {
	switch (number) {
		case 0:
			return 'blue';
		case 1:
			return 'gray';
		case 2:
			return 'red';
		case 3:
			return 'yellow';
		default:
			throw 'Invalid FieldType: ' + number;
	}
};

export default GameField;
