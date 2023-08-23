import { baseUrl } from "@/pages/_app";
import axios from "axios";
import Link from "next/link";
import { useState } from "react";

export default function LoginForm() {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [credentialsNotValid, setCredentialsNotValid] = useState(false);

    function login(e) {
        e.preventDefault();
        if (!credentialsValid()) {
            setCredentialsNotValid(true);
            return;
        }
        axios.post(`${baseUrl}/api/user/login`, { 'email': email, 'password': password })
            .then(response => console.log(response))
            .catch(_ => setCredentialsNotValid(true));
    }

    function credentialsValid() {
        return email.includes('@') && password.length >= 6;
    }

    return (
        <div className="initial-form">
            <div className="wrapper">
                <div className="main-content">
                    <h1 className="main-content__title">Login</h1>
                    <form className="info" action="index.html" onSubmit={login}>
                        <ul className="info__form">
                            <li className="info__list">
                                <label htmlFor="email" className="info__label">Email:</label>
                                <input type="text" name="email" id="email" className="in-text" value={email} onChange={(e) => setEmail(e.currentTarget.value)} />
                            </li>
                            <li className="info__list">
                                <label htmlFor="password" className="report__label">Password:</label>
                                <input name="password" id="password" type="password" className="in-text" value={password} onChange={(e) => setPassword(e.currentTarget.value)} />
                            </li>
                        </ul>
                        <div className="btn-wrap">
                            <label className="initial-form__checkbox"><input type="checkbox" name="remember-me" />Remember me</label>
                            <Link href="/forgot-password" className="btn btn--transparent"><span>Forgot password</span></Link>
                            <button type="submit" className="btn btn--green"><span>Login</span></button>
                        </div>
                    </form>
                    {credentialsNotValid && <p className="error">Email or Password not entered correctly. Try again.</p>}
                </div>
            </div>
        </div>
    );
}