/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable jsx-a11y/no-static-element-interactions */
import { useState } from "react";
import axios from "axios";
import { baseUrl } from "@/pages/_app";

export default function ProjectCard({ clients, employees, projectId, projectName, projectDescription, projectsClient, projectLead, projectStatus, projects, setProjects }) {
    const [name, setName] = useState(projectName);
    const [description, setDescription] = useState(projectDescription);
    const [client, setClient] = useState(projectsClient);
    const [lead, setLead] = useState(projectLead);
    const [status, setStatus] = useState(projectStatus);

    const [showAccordion, setShowAccordion] = useState(false);

    function updateProject(e) {
        e.preventDefault();
        if (!credentialsValid(e)) {
            alert("Invalid new data");
            return;
        }
        axios.put(`${baseUrl}/api/project/${projectId}`, { 'name': name, 'description': description, 'status': status, 'employeeId': lead, 'clientId': client })
            .then(response => {
                const allProjects = projects.filter(p => p.id != projectId);
                allProjects.push(response.data);
                setProjects(allProjects);
                alert("Project updated successfully");
            })
            .catch(error => console.log(error));
    }

    function credentialsValid() {
        return name.length > 1 && description.length > 1 && (name != projectName || description != projectDescription || client != projectsClient || lead != projectLead || status != projectStatus)
    }

    function deleteProject() {
        axios.delete(`${baseUrl}/api/project/${projectId}`)
            .then(_ => {
                const remainingProjects = projects.filter(p => p.id != projectId);
                setProjects(remainingProjects);
                alert("Project deleted successfully");
            })
            .catch(error => console.log(error));
    }

    return (
        <div className="accordion">
            <div className="accordion__intro" onClick={() => setShowAccordion(!showAccordion)}>
                <h4 className="accordion__title">{projectName}</h4>
            </div>
            <form className={`accordion__content ${showAccordion ? 'show_accordion' : 'hide_accordion'}`} onSubmit={updateProject}>
                <div className="info">
                    <div className="info__form">
                        <ul className="info__wrapper">
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
                            <li className="info__list convertToFlex" onChange={(e) => setStatus(e.target.value)}>
                                <strong className="info__label">Status:</strong>
                                <label htmlFor="active" className="info__label">Active</label>
                                <input type="radio" name="status" id="active" value="active" defaultChecked={status === "active"} />
                                <label htmlFor="inactive" className="info__label">Inactive</label>
                                <input type="radio" name="status" id="inactive" value="inactive" defaultChecked={status === "inactive"} />
                                <label htmlFor="archive" className="info__label">Archive</label>
                                <input type="radio" name="status" id="archive" value="archive" defaultChecked={status === "archive"} />
                            </li>
                        </ul>
                    </div>
                </div>
                <div className="btn-wrap">
                    <button type="submit" className="btn btn--green"><span>Save changes</span></button>
                    <button onClick={deleteProject} type="button" className="btn btn--red"><span>Delete</span></button>
                </div>
            </form>
        </div>
    );
}