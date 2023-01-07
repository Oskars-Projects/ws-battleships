import InputWindow from '../InputWindow';

type AskGameNameComponentProps = {
	onClick: (value: string) => void;
};

const AskGameNameComponent = ({ onClick }: AskGameNameComponentProps) => {
	return <InputWindow text="Enter the name of your game" onClick={onClick} />;
};

export default AskGameNameComponent;
