import DaysPreview from "@/components/DaysPreview";
import Footer from "@/components/Footer";
import NavBar from "@/components/NavBar";
import { getQueryVariable } from "@/helper/helper";
import axios from "axios";
import { useEffect, useState } from "react";
import { baseUrl } from "./_app";

export default function Days() {
    return (
        <>
            <NavBar />
            <MainArea />
            <Footer />
        </>
    );
}

// eslint-disable-next-line react/no-multi-comp
function MainArea() {
    const [date, setDate] = useState('');
    const [clients, setClients] = useState([]);
    const [projects, setProjects] = useState([]);
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        axios.get(`${baseUrl}/api/client/all-minimal`)
            .then(response => setClients(response.data))
            .catch(error => console.log(error));
        axios.get(`${baseUrl}/api/project/all-minimal`)
            .then(response => setProjects(response.data))
            .catch(error => console.log(error));
        axios.get(`${baseUrl}/api/category/all`)
            .then(response => setCategories(response.data))
            .catch(error => console.log(error));
    }, []);

    useEffect(() => {
        setDate(getQueryVariable('date'));
    }, []);

    return (
        <>
            {(date && clients && projects && categories) && <DaysPreview date={new Date(date)} setDate={setDate} clients={clients} projects={projects} categories={categories} />}
        </>
    );
}