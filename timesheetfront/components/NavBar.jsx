import Image from "next/image";
import Link from "next/link";
import { getUserRole, getUserUsername, logOut } from "@/helper/helper";
import { useEffect, useState } from "react";
import { useRouter } from "next/router";

export default function NavBar() {

    const [username, setUsername] = useState('');
    const [role, setRole] = useState('');
    const [urlPath, setUrlPath] = useState('');
    const router = useRouter();

    useEffect(() => {
        setUsername(getUserUsername());
        setUrlPath(router.pathname);
        setRole(getUserRole());
    }, [router.pathname])

    return (
        <header className="header">
            <div className="inner-wrap">
                <Link href="/" className="logo">
                    <Image src="/images/logo-white.png" width={115} height={54} alt="Logo" />
                </Link>
                <nav className="navigation">
                    <button id="navigation__link" type="button" className="navigation__link"><span id="navigation__text" className="nav-toggle" /></button>
                    <ul className="navigation__menu">
                        <li className="navigation__list">
                            <Link href="/" className={`btn navigation__button ${urlPath == '/' ? 'navigation__button--active' : ''}`}>Timesheet</Link>
                        </li>
                        {role == 'admin' &&
                            <li className="navigation__list">
                                <Link href="/clients" className={`btn navigation__button ${urlPath.startsWith('/clients') ? 'navigation__button--active' : ''}`}>Clients</Link>
                            </li>}
                        {role == 'admin' &&
                            <li className="navigation__list">
                                <Link href="/projects" className={`btn navigation__button ${urlPath.startsWith('/projects') ? 'navigation__button--active' : ''}`}>Projects</Link>
                            </li>}
                        {role == 'admin' &&
                            <li className="navigation__list">
                                <Link href="/categories" className={`btn navigation__button ${urlPath.startsWith('/categories') ? 'navigation__button--active' : ''}`}>Categories</Link>
                            </li>}
                        {role == 'admin' &&
                            <li className="navigation__list">
                                <Link href="/employees" className={`btn navigation__button ${urlPath.startsWith('/employees') ? 'navigation__button--active' : ''}`}>Employees</Link>
                            </li>}
                        <li className="navigation__list">
                            <Link href="/reports" className={`btn navigation__button ${urlPath.startsWith('/reports') ? 'navigation__button--active' : ''}`}>Reports</Link>
                        </li>
                    </ul>
                </nav>
                <div className="user">
                    <div className="user__nav">
                        <h2 className="user__name">{username}</h2>
                        <ul className="user__dropdown">
                            <li className="user__list"><Link className="user__link" href="/">Change password</Link></li>
                            <li className="user__list"><Link className="user__link" href="/">Settings</Link></li>
                            <li className="user__list"><Link className="user__link" href="/">Export all data</Link></li>
                        </ul>
                    </div>
                    <ul>
                        <li className="logout">
                            <button className="logout__link" onClick={logOut}>Logout</button>
                        </li>
                    </ul>
                </div>
            </div>
        </header>
    );
}