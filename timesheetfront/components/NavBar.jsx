import Image from "next/image";
import Link from "next/link";
import { getUserUsername, logOut } from "@/helper/helper";
import { useEffect, useState } from "react";

export default function NavBar() {

    const [username, setUsername] = useState('');

    useEffect(() => {
        // setUsername(getUserUsername());
    }, [])

    return (
        <header className="header">
            <div className="inner-wrap">
                <a href="./index.html" className="logo">
                    <Image src="/images/logo-white.png" width={115} height={54} alt="Logo" />
                </a>
                <nav className="navigation">
                    <button id="navigation__link" type="button" className="navigation__link"><span id="navigation__text" className="nav-toggle" /></button>
                    <ul className="navigation__menu">
                        <li className="navigation__list">
                            <Link href="/" className="btn navigation__button navigation__button--active">Timesheet</Link>
                        </li>
                        <li className="navigation__list">
                            <Link href="/clients" className="btn navigation__button">Clients</Link>
                        </li>
                        <li className="navigation__list">
                            <Link href="/projects" className="btn navigation__button">Projects</Link>
                        </li>
                        <li className="navigation__list">
                            <Link href="/categories" className="btn navigation__button">Categories</Link>
                        </li>
                        <li className="navigation__list">
                            <Link href="/employees" className="btn navigation__button">Employees</Link>
                        </li>
                        <li className="navigation__list">
                            <Link href="/reports" className="btn navigation__button">Reports</Link>
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