import { AiOutlineHome } from 'react-icons/ai';
import './header.css';

const Header = () => {
	return (
		<header id="page-header">
			<a className="header-icon" href="/">
				<AiOutlineHome className="header-icon" />
			</a>
		</header>
	);
};

export default Header;
