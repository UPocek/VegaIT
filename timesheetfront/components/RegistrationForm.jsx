import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useState } from "react";

export default function RegistrationForm() {
    const minPasswordLength = 6;
    const [name, setName] = useState('');
    const [username, setUsername] = useState('');
    const [role, setRole] = useState('worker');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [credentialsNotValid, setCredentialsNotValid] = useState(false);
    const [badRequest, setBadRequest] = useState(false);

    function register(e) {
        e.preventDefault();
        if (!credentialsValid()) {
            setCredentialsNotValid(true);
            return;
        }

        axios.post(`${baseUrl}/api/user/registration`, { 'name': name, 'username': username, 'role': role, 'email': email, 'password': password }, {
            headers: {
                'skip': 'true'
            }
        })
            .then(response => { setBadRequest(false); setCredentialsNotValid(false); alert(`New employee added sucessfully ${response.data} !`) })
            .catch(_ => { setCredentialsNotValid(false); setBadRequest(true) });
    }

    function credentialsValid() {
        return email.includes('@') && password.length >= minPasswordLength && name && username;
    }

    return (
        <div className="initial-form">
            <div className="wrapper">
                <div className="main-content">
                    <h1 className="main-content__title">Register new employee</h1>
                    <form className="info" action="index.html" onSubmit={register}>
                        <ul className="info__form">
                            <li className="info__list">
                                <label htmlFor="name" className="info__label">Name:</label>
                                <input type="text" name="name" id="name" className="in-text" value={name} onChange={(e) => setName(e.currentTarget.value)} />
                            </li>
                            <li className="info__list">
                                <label htmlFor="username" className="info__label">Username:</label>
                                <input type="text" name="username" id="username" className="in-text" value={username} onChange={(e) => setUsername(e.currentTarget.value)} />
                            </li>
                            <li className="info__list">
                                <label htmlFor="email" className="info__label">Email:</label>
                                <input type="email" name="email" id="email" className="in-text" value={email} onChange={(e) => setEmail(e.currentTarget.value)} />
                            </li>
                            <li className="info__list">
                                <label htmlFor="password" className="report__label">{`Password (min ${minPasswordLength} characters)`}</label>
                                <input name="password" id="password" type="password" className="in-text" value={password} onChange={(e) => setPassword(e.currentTarget.value)} />
                            </li>
                            <p className="info__label">Role:</p>
                            <li className="info__list convertToFlex" onChange={(e) => setRole(e.target.value)}>
                                <label htmlFor="admin" className="info__label">Admin</label>
                                <input type="radio" name="role" id="admin" value="admin" defaultChecked={role === "admin"} />
                                <label htmlFor="worker" className="info__label">Worker</label>
                                <input type="radio" name="role" id="worker" value="worker" defaultChecked={role === "worker"} />
                            </li>
                        </ul>
                        <div className="btn-wrap">
                            <button type="submit" className="btn btn--green"><span>Register new</span></button>
                        </div>
                    </form>
                    {credentialsNotValid && <p className="error">Some inputs are not valid. Check rules and try again.</p>}
                    {badRequest && <p className="error">User with that email already exists.</p>}
                </div>
            </div>
        </div>
    )
}