import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useState } from "react";

export default function EmployeesModal({ roles, setShowModal, users, setUsers }) {
    const [credentialsNotValid, setCredentialsNotValid] = useState(false);
    const [badRequest, setBadRequest] = useState(false);

    const [name, setName] = useState('');
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [status, setStatus] = useState('active');
    const [role, setRole] = useState('worker');
    const [password, setPassword] = useState('');

    function registerNewEmployee(e) {
        e.preventDefault();
        if (!credentialsValid(e)) {
            setCredentialsNotValid(true);
            return;
        }
        axios.post(`${baseUrl}/api/user/registration`, { 'name': name, 'username': username, 'role': role, 'email': email, 'password': password, 'status': status == 'active' }, {
            headers: {
                'skip': 'true'
            }
        })
            .then(response => {
                setCredentialsNotValid(false);
                setBadRequest(false);
                resetForm();
                const allUsers = [...users];
                allUsers.push(response.data);
                setUsers(allUsers);
                alert("New employee added successfully");
            })
            .catch(_ => { setCredentialsNotValid(false); setBadRequest(true); });
    }

    function credentialsValid() {
        return name && username && email && password;
    }

    function resetForm() {
        setName('');
        setUsername('');
        setEmail('');
        setStatus('active');
        setRole('worker');
        setPassword('');
    }

    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    return (
        <div className="modal">
            <div className="modal__content">
                <h2 className="heading">Register new employee</h2>
                <button onClick={() => setShowModal(false)} className="modal__close">
                    <span className="modal__icon" />
                </button>
                <form className="info" onSubmit={registerNewEmployee}>
                    <ul className="info__form">
                        <li className="info__list">
                            <label htmlFor="name" className="info__label">Name:</label>
                            <input type="text" name="name" id="name" className="in-text" value={name} onChange={(e) => setName(e.currentTarget.value)} />
                        </li>
                        <li className="info__list">
                            <label htmlFor="username" className="info__label">Username:</label>
                            <input type="text" name="username" id="username" className="in-text" value={username} onChange={(e) => { setUsername(e.currentTarget.value); setPassword(e.currentTarget.value) }} />
                        </li>
                        <li className="info__list">
                            <label htmlFor="email" className="info__label">Email:</label>
                            <input type="email" name="email" id="email" className="in-text" value={email} onChange={(e) => setEmail(e.currentTarget.value)} />
                        </li>
                        <p className="info__label">Status:</p>
                        <li className="info__list convertToFlex" onChange={(e) => setStatus(e.target.value)}>
                            <label htmlFor="active" className="info__label">Active</label>
                            <input type="radio" name="status" id="active" value="active" defaultChecked={status === "active"} />
                            <label htmlFor="inactive" className="info__label">Inactive</label>
                            <input type="radio" name="status" id="inactive" value="inactive" defaultChecked={status === "inactive"} />
                        </li>
                        <p className="info__label">Role:</p>
                        <li className="info__list convertToFlex" onChange={(e) => setRole(e.target.value)}>
                            {roles.map(roleItem => (
                                <span key={roleItem.id}>
                                    <label htmlFor={roleItem.name} className="info__label">{capitalizeFirstLetter(roleItem.name)}</label>
                                    <input type="radio" name="role" id={roleItem.name} value={roleItem.name} defaultChecked={role === roleItem.name} />
                                </span>))}
                        </li>
                    </ul>
                    <div className="btn-wrap">
                        <button type="submit" className="btn btn--green"><span>Invite an employee</span></button>
                    </div>
                    {credentialsNotValid && <p className="error">All fields must be filled.</p>}
                    {badRequest && <p className="error">Employee with that email already exists. Try again.</p>}
                </form>
            </div>
        </div>
    );
}