import { datesCompare } from "@/helper/helper";
import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useState } from "react";

export default function CreateNewTimeEntryForm({ date, clients, projects, categories, dateEnteries, setDateEnteries }) {
    const [client, setClient] = useState(clients[0].id);
    const [project, setProject] = useState(projects[0].id);
    const [category, setCategory] = useState(categories[0].id);
    const [description, setDescription] = useState('');
    const [hours, setHours] = useState(0);
    const [overtime, setOvertime] = useState(0);

    function createNewEntry(e) {
        e.preventDefault();

        if (!entryDataValid()) {
            alert("Inputs invalid. Try again.");
            return;
        }

        axios.post(`${baseUrl}/api/timeentry`, { 'description': description.trim(), 'hours': hours, 'overtime': overtime, 'date': date, 'clientId': client, 'projectId': project, 'categoryId': category })
            .then(response => {
                const allCurrentEntries = [...dateEnteries];
                allCurrentEntries.push(response.data);
                setDateEnteries(allCurrentEntries);
                alert("New TimeEntry added!");
                resetForm();
            })
            .catch(_ => alert("New time entry with given informations can't be created."));
    }

    function entryDataValid() {
        return hours > 0 && hours < 24 && overtime >= 0 && overtime < 24 && datesCompare(new Date(), date) == 1;
    }

    function resetForm() {
        setClient(clients[0].id);
        setProject(projects[0].id);
        setCategory(categories[0].id);
        setDescription('');
        setHours(0);
        setOvertime(0);
    }

    return (
        <form onSubmit={createNewEntry}>
            <table className="project-table">
                <thead>
                    <tr className="project-table__top">
                        <th className="project-table__title">Client *</th>
                        <th className="project-table__title">Project *</th>
                        <th className="project-table__title">Category *</th>
                        <th className="project-table__title project-table__title--large">Description</th>
                        <th className="project-table__title project-table__title--small">Hours *</th>
                        <th className="project-table__title project-table__title--small">Overtime</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td className="project-table__name">
                            <select value={client} onChange={(e) => setClient(e.currentTarget.value)} className="project-table__select">
                                {clients.map(clientToMap => <option key={clientToMap.id} value={clientToMap.id}>{clientToMap.name}</option>)}
                            </select>
                        </td>
                        <td className="project-table__name">
                            <select value={project} onChange={(e) => setProject(e.currentTarget.value)} className="project-table__select">
                                {projects.map(projectToMap => <option key={projectToMap.id} value={projectToMap.id}>{projectToMap.name}</option>)}
                            </select>
                        </td>
                        <td className="project-table__name">
                            <select value={category} onChange={(e) => setCategory(e.currentTarget.value)} className="project-table__select">
                                {categories.map(categoryToMap => <option key={categoryToMap.id} value={categoryToMap.id}>{categoryToMap.name}</option>)}
                            </select>
                            <span className={`validationMessage disable_calendar_element`} />
                        </td>
                        <td className="project-table__name">
                            <input type="text" value={description} onChange={(e) => setDescription(e.currentTarget.value)} className="in-text medium" />
                        </td>
                        <td className="project-table__name">
                            <input type="number" value={hours} onChange={(e) => setHours(e.currentTarget.value)} className="in-text" />
                        </td>
                        <td className="project-table__name">
                            <input type="number" value={overtime} onChange={(e) => setOvertime(e.currentTarget.value)} className="in-text" />
                        </td>
                        <td>
                            <button type="submit" className="btn btn--green"><span>Save</span></button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </form>
    );
}