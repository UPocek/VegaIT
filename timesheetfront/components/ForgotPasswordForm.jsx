import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useState } from "react";

export default function ForgotPasswordForm() {

    const [email, setEmail] = useState('');

    function requestNewPassword(e) {
        e.preventDefault();

        axios.put(`${baseUrl}/api/user/forgotPassword`, { 'email': email }, { 'skip': true })
            .then(response => console.log(response))
            .catch(error => console.log(error));
    }

    return (
        <div className="initial-form">
            <div className="wrapper">
                <div className="main-content">
                    <h1 className="main-content__title">Forgot password</h1>
                    <form className="info" onSubmit={requestNewPassword}>
                        <ul className="info__form">
                            <li className="info__list">
                                <label htmlFor="email" className="info__label">Email:</label>
                                <input value={email} onChange={(e) => setEmail(e.currentTarget.value)} name="email" id="email" type="email" className="in-text" />
                            </li>
                        </ul>
                        <div className="btn-wrap">
                            <button type="submit" className="btn btn--orange"><span>Reset password</span></button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}