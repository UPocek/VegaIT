import { useEffect, useState } from "react";
import axios from "axios";
import { baseUrl } from "@/pages/_app";
import ElementsSearch from "./ElementsSearch";
import Alphabet from "./Alphabet";
import Pagination from "./Pagination";
import ProjectModal from "./ProjectModal";
import ProjectCard from "./ProjectCard";

export default function ProjectsPreview() {
    const [showModal, setShowModal] = useState(false);
    const [searchLetter, setSearchLetter] = useState('');
    const [employees, setEmployees] = useState([]);
    const [clients, setClients] = useState([]);
    const [projects, setProjects] = useState([]);
    const [firstLetters, setFirstLetters] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');

    useEffect(() => {
        axios.get(`${baseUrl}/api/client/all-minimal`)
            .then(response => setClients(response.data))
            .catch(error => console.log(error));
        axios.get(`${baseUrl}/api/user/all-minimal`)
            .then(response => setEmployees(response.data))
            .catch(error => console.log(error));
        axios.get(`${baseUrl}/api/project/all`)
            .then(response => setProjects(response.data))
            .catch(error => console.log(error));
    }, []);

    useEffect(() => {
        updateLetters(projects);
    }, [projects]);

    function updateLetters(dataForLetters) {
        let letters = [];
        for (let element of dataForLetters) {
            letters.push(element.name[0].toLowerCase());
        }
        // eslint-disable-next-line no-undef
        setFirstLetters([...new Set(letters)]);
    }

    function searchProjects(e) {
        e.preventDefault();
        setSearchLetter('');
        setSearchQuery(e.target.searchText.value);
    }

    function searchProjectsByLetter(letter) {
        setSearchLetter(letter);
        setSearchQuery(letter);
    }

    function fulfillsSearch(client) {
        if (searchQuery == '') return true;
        return client.name.toLowerCase().startsWith(searchQuery.toLowerCase());
    }

    return (
        <>
            {showModal && <ProjectModal employees={employees} clients={clients} setShowModal={setShowModal} projects={projects} setProjects={setProjects} />}
            <div className="wrapper">
                <section className="content">
                    <div className="main-content">
                        <h2 className="main-content__title">Projects</h2>
                        <div className="table-navigation">
                            <button onClick={() => setShowModal(true)} className="table-navigation__create btn-modal"><span>Create new project</span></button>
                            <ElementsSearch searchElements={searchProjects} />
                        </div>
                        <Alphabet searchByLetter={searchProjectsByLetter} searchLetter={searchLetter} firstLetters={firstLetters} />
                        {projects.filter(project => fulfillsSearch(project)).map(project => <ProjectCard key={project.id} clients={clients} employees={employees} projectId={project.id} projectName={project.name} projectDescription={project.description} projectsClient={project.clientId} projectLead={project.employeeId} projectStatus={project.status} projects={projects} setProjects={setProjects} />)}
                    </div>
                    <Pagination elements={projects} setElements={setProjects} />
                </section>
            </div>
        </>
    );
}