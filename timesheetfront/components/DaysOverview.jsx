import DaysPreview from "@/components/DaysPreview";
import { getQueryVariable } from "@/helper/helper";
import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useEffect, useState } from "react";

export default function DaysOverview() {
    const [date, setDate] = useState('');
    const [clients, setClients] = useState([]);
    const [projects, setProjects] = useState([]);
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        // eslint-disable-next-line no-undef
        Promise.all([
            axios.get(`${baseUrl}/api/client/all-minimal`),
            axios.get(`${baseUrl}/api/project/all-minimal`),
            axios.get(`${baseUrl}/api/category/all`),
            getQueryVariable('date')
        ])
            .then(([clientsResponse, projectsResponse, categoriesResponse, newDate]) => {
                setClients(clientsResponse.data);
                setProjects(projectsResponse.data);
                setCategories(categoriesResponse.data);
                setDate(newDate);
            })
            .catch(error => console.log(error));
    }, []);

    return (
        <>
            {(date && clients && projects && categories) && <DaysPreview date={new Date(date)} setDate={setDate} clients={clients} projects={projects} categories={categories} />}
        </>
    );
}