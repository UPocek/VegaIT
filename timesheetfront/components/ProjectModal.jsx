import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useState } from "react";

export default function ProjectModal({ employees, clients, setShowModal, projects, setProjects }) {
    const [credentialsNotValid, setCredentialsNotValid] = useState(false);

    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [client, setClient] = useState(clients[0].id);
    const [lead, setLead] = useState(employees[0].id);
    const [status, setStatus] = useState('active');

    function createNewProject(e) {
        e.preventDefault();
        if (!credentialsValid(e)) {
            setCredentialsNotValid(true);
            return;
        }
        axios.post(`${baseUrl}/api/project`, { 'name': name, 'description': description, 'status': status, 'employeeId': lead, 'clientId': client })
            .then(response => {
                setCredentialsNotValid(false);
                resetForm();
                const allProjects = [...projects];
                allProjects.push(response.data);
                setProjects(allProjects);
                alert("New project added successfully");
            })
            .catch(_ => setCredentialsNotValid(true));
    }

    function credentialsValid() {
        return name && description;
    }

    function resetForm() {
        setName('');
        setDescription('');
        setClient(1);
        setLead(1);
        setStatus('active');
    }

    return (
        <div className="modal">
            <div className="modal__content">
                <h2 className="heading">Create new project</h2>
                <button onClick={() => setShowModal(false)} className="modal__close">
                    <span className="modal__icon" />
                </button>
                <form className="info" onSubmit={createNewProject}>
                    <ul className="info__form">
                        <li className="info__list">
                            <label htmlFor="cname" className="info__label">Project name:</label>
                            <input type="text" name="cname" id="cname" className="in-text" value={name} onChange={(e) => setName(e.currentTarget.value)} />
                        </li>
                        <li className="info__list">
                            <label htmlFor="caddress" className="report__label">Description:</label>
                            <input type="text" name="caddress" id="caddress" className="in-text" value={description} onChange={(e) => setDescription(e.currentTarget.value)} />
                        </li>
                        <li className="info__list">
                            <label htmlFor="cclient" className="report__label">Client:</label>
                            <select name="cclient" id="cclient" className="info__select" value={client} onChange={(e) => setClient(e.currentTarget.value)}>
                                {clients.map(client => <option key={client.id} value={client.id}>{client.name}</option>)}
                            </select>
                        </li>
                        <li className="info__list">
                            <label htmlFor="ccity" className="report__label">Lead:</label>
                            <select name="ccity" id="ccity" className="info__select" value={lead} onChange={(e) => setLead(e.currentTarget.value)}>
                                {employees.map(employee => <option key={employee.id} value={employee.id}>{employee.name}</option>)}
                            </select>
                        </li>
                    </ul>
                    <div className="btn-wrap">
                        <button type="submit" className="btn btn--green"><span>Save changes</span></button>
                        <button onClick={resetForm} type="button" className="btn btn--red"><span>Reset</span></button>
                    </div>
                    {credentialsNotValid && <p className="error">All fields must be filled</p>}
                </form>
            </div>
        </div>
    );
}