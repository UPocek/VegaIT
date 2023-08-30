import { datesCompare } from "@/helper/helper";
import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useState } from "react";

export default function ExistingTimeEntry({ date, entry, clients, projects, categories, dateEnteries, setDateEnteries }) {
    const [client, setClient] = useState(entry.clientId);
    const [project, setProject] = useState(entry.projectId);
    const [category, setCategory] = useState(entry.categoryId);
    const [description, setDescription] = useState(entry.description);
    const [hours, setHours] = useState(entry.hours);
    const [overtime, setOvertime] = useState(entry.overtime);

    function updateThisEntry(e) {
        e.preventDefault();

        if (!updateValid()) {
            alert("New data not valid");
            return;
        }

        axios.put(`${baseUrl}/api/timeentry/${entry.id}`, { 'description': description.trim(), 'hours': hours, 'overtime': overtime, 'date': date, 'clientId': client, 'projectId': project, 'categoryId': category })
            .then(response => {
                let allCurrentEntries = dateEnteries.filter(oneEntry => oneEntry.id != entry.id);
                allCurrentEntries.push(response.data);
                setDateEnteries(allCurrentEntries);
                alert("TimeEntry updated!");
            })
            .catch(_ => alert("TimeEntry with updated informations is not valid."));
    }

    function updateValid() {
        return hours > 0 && hours < 24 && overtime >= 0 && overtime < 24 && (client != entry.clientId || project != entry.projectId || category != entry.categoryId || description != entry.description || hours != entry.hours || overtime != entry.overtime);
    }

    return (
        <form onSubmit={updateThisEntry}>
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
                            <button type="submit" className="btn btn--green"><span>Update</span></button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </form>
    );
}