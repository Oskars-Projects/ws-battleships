import InputWindow from '../InputWindow';

type AskUserNameComponentProps = {
	onClick: (value: string) => void;
};

const AskUserNameComponent = ({ onClick }: AskUserNameComponentProps) => {
	return <InputWindow text="Enter your username" onClick={onClick} />;
};

export default AskUserNameComponent;
