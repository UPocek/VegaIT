/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect, useState } from "react";
import axios from "axios";
import { baseUrl } from "@/pages/_app";
import ElementsSearch from "./ElementsSearch";
import Alphabet from "./Alphabet";
import Pagination from "./Pagination";
import ProjectModal from "./ProjectModal";
import ProjectCard from "./ProjectCard";
import { useRouter } from "next/router";

export default function ProjectsPreview() {

    const router = useRouter();

    const [showModal, setShowModal] = useState(false);
    const [searchLetter, setSearchLetter] = useState('');
    const [searchQuery, setSearchQuery] = useState('');
    const [employees, setEmployees] = useState([]);
    const [clients, setClients] = useState([]);
    const [projects, setProjects] = useState([]);
    const [firstLetters, setFirstLetters] = useState([]);

    useEffect(() => {
        // eslint-disable-next-line no-undef
        Promise.all([
            axios.get(`${baseUrl}/api/client/all-minimal`),
            axios.get(`${baseUrl}/api/user/all-minimal`),
            axios.get(`${baseUrl}/api/project/all`)
        ]).then(([clientResponse, userResponse, projectResponse]) => {
            setClients(clientResponse.data);
            setEmployees(userResponse.data);
            setProjects(projectResponse.data);
        }).catch(error => console.log(error));
    }, []);

    useEffect(() => {
        updateLetters(projects);
    }, [projects]);

    useEffect(() => {
        const search = router.query.search;
        if (search == null || search.length <= 0) return;
        if (search.length == 1 && searchLetter == '') {
            setSearchLetter(search);
        }
        if (searchQuery == '') {
            setSearchQuery(search);
        }
    }, [router.query.search]);

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
        router.replace(`/projects?search=${e.target.searchText.value}`);
    }

    function searchProjectsByLetter(letter) {
        setSearchLetter(letter);
        setSearchQuery(letter);
        router.replace(`/projects?search=${letter}`);
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