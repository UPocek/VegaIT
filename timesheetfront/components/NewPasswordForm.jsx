import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useRouter } from "next/router";
import { useState, useEffect } from "react";

export default function NewPasswordForm() {

    const minPasswordLength = 6;

    const router = useRouter();
    const [code, setCode] = useState('');
    const [password, setPassword] = useState('');
    const [rePassword, setRePassword] = useState('');
    const [credentialsNotValid, setCredentialsNotValid] = useState(false);

    useEffect(() => {
        setCode(router.query.code);
    }, [router.query.code]);

    function resetPassword(e) {
        e.preventDefault();
        if (!credentialsValid()) {
            setCredentialsNotValid(true);
            return;
        }
        axios.put(`${baseUrl}/api/user/newpassword`, { 'code': code, 'password': password }, {
            headers: {
                'skip': 'true'
            }
        })
            .then(response => {
                if (response.status == 200) {
                    router.replace('/login');
                }
            })
            .catch(_ => setCredentialsNotValid(true));
    }

    function credentialsValid() {
        return password.length >= minPasswordLength && password === rePassword;
    }

    return (
        <div className="initial-form">
            <div className="wrapper">
                <div className="main-content">
                    <h1 className="main-content__title">Set up new password</h1>
                    <form className="info" action="index.html" onSubmit={resetPassword}>
                        <ul className="info__form">
                            <li className="info__list">
                                <label htmlFor="password" className="info__label">{`Password: (Min ${minPasswordLength} characters)`}</label>
                                <input type="password" name="password" id="password" className="in-text" autoComplete="off" value={password} onChange={(e) => setPassword(e.currentTarget.value)} />
                            </li>
                            <li className="info__list">
                                <label htmlFor="repassword" className="report__label">Repeate password:</label>
                                <input name="repassword" id="repassword" type="password" className="in-text" autoComplete="off" value={rePassword} onChange={(e) => setRePassword(e.currentTarget.value)} />
                            </li>
                        </ul>
                        <div className="btn-wrap">
                            <button type="submit" className="btn btn--green"><span>Reset</span></button>
                        </div>
                    </form>
                    {credentialsNotValid && <p className="error">{`Password must be longerv then ${minPasswordLength} not entered correctly. Try again.`}</p>}
                </div>
            </div>
        </div>
    );
}