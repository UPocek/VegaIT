/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable jsx-a11y/no-static-element-interactions */
import { useState } from "react";
import axios from "axios";
import { baseUrl } from "@/pages/_app";
import { roleIdToName } from "@/helper/helper";

export default function EmployeesCard({ roles, userId, userName, userUsername, userEmail, userStatus, userRole, users, setUsers }) {
    const [name, setName] = useState(userName);
    const [username, setUsername] = useState(userUsername);
    const [email, setEmail] = useState(userEmail);
    const [status, setStatus] = useState(userStatus ? 'active' : 'inactive');
    const [role, setRole] = useState(roleIdToName(roles, userRole));

    const [showAccordion, setShowAccordion] = useState(false);

    function updateUser(e) {
        e.preventDefault();
        if (!credentialsValid(e)) {
            alert("Invalid new data");
            return;
        }
        axios.put(`${baseUrl}/api/user/${userId}`, { 'name': name, 'username': username, 'role': role, 'email': email, 'status': status == 'active' })
            .then(response => {
                const allUsers = users.filter(p => p.id != userId);
                allUsers.push(response.data);
                setUsers(allUsers);
                alert("user updated successfully");
            })
            .catch(error => console.log(error));
    }

    function credentialsValid() {
        return name.length > 1 && username.length > 1 && email.length > 1 && (name != userName || username != userUsername || email != userEmail || status != userStatus || role != userRole)
    }

    function deleteUser() {
        axios.delete(`${baseUrl}/api/user/${userId}`)
            .then(_ => {
                const remainingUsers = users.filter(p => p.id != userId);
                setUsers(remainingUsers);
                alert("User deleted successfully");
            })
            .catch(error => console.log(error));
    }

    function capitalizeFirstLetter(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }

    return (
        <div className="accordion">
            <div className="accordion__intro" onClick={() => setShowAccordion(!showAccordion)}>
                <h4 className="accordion__title">{userName}</h4>
            </div>
            <form className={`accordion__content ${showAccordion ? 'show_accordion' : 'hide_accordion'}`} onSubmit={updateUser}>
                <div className="info">
                    <div className="info__form">
                        <ul className="info__wrapper">
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
                    </div>
                </div>
                <div className="btn-wrap">
                    <button type="submit" className="btn btn--green"><span>Save changes</span></button>
                    <button onClick={deleteUser} type="button" className="btn btn--red"><span>Delete</span></button>
                </div>
            </form>
        </div>
    );
}