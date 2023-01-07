import { useState } from 'react';

type InputWindowProps = {
	text: string;
	defaultValue?: string;
	onClick: (value: string) => void;
};

const InputWindow = ({ text, defaultValue, onClick }: InputWindowProps) => {
	const [value, setValue] = useState(defaultValue || '');

	return (
		<div>
			<h1>{text}</h1>
			<input
				type="text"
				value={value}
				onChange={e => setValue(e.target.value)}
			/>
			<button onClick={() => onClick(value)}></button>
		</div>
	);
};

export default InputWindow;
