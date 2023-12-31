/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect, useState } from "react";
import ClientModal from "./ClientModel";
import axios from "axios";
import { baseUrl } from "@/pages/_app";
import ClientCard from "./ClientCard";
import ElementsSearch from "./ElementsSearch";
import Alphabet from "./Alphabet";
import Pagination from "./Pagination";
import { useRouter } from "next/router";

export default function ClientsPreview() {

    const router = useRouter();

    const [showModal, setShowModal] = useState(false);
    const [searchLetter, setSearchLetter] = useState('');
    const [searchQuery, setSearchQuery] = useState('');

    const [cities, setCities] = useState([]);
    const [countries, setCountries] = useState([]);
    const [clients, setClients] = useState([]);
    const [firstLetters, setFirstLetters] = useState([]);

    useEffect(() => {
        // eslint-disable-next-line no-undef
        Promise.all([
            axios.get(`${baseUrl}/api/client/cities`),
            axios.get(`${baseUrl}/api/client/countries`),
            axios.get(`${baseUrl}/api/client/all`)])
            .then(([citiesResponse, countriesResponse, clientsResponse]) => {
                setCities(citiesResponse.data);
                setCountries(countriesResponse.data);
                setClients(clientsResponse.data);
            })
            .catch(error => console.log(error));
    }, []);

    useEffect(() => {
        updateLetters(clients);
    }, [clients]);

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

    function searchClients(e) {
        e.preventDefault();
        setSearchLetter('');
        setSearchQuery(e.target.searchText.value);
        router.replace(`/clients?search=${e.target.searchText.value}`);
    }

    function searchClientsByLetter(letter) {
        setSearchLetter(letter);
        setSearchQuery(letter);
        router.replace(`/clients?search=${letter}`);
    }

    function fulfillsSearch(client) {
        if (searchQuery == '') return true;
        return client.name.toLowerCase().startsWith(searchQuery.toLowerCase());
    }

    return (
        <>
            {showModal && <ClientModal cities={cities} countries={countries} setShowModal={setShowModal} clients={clients} setClients={setClients} />}
            <div className="wrapper">
                <section className="content">
                    <div className="main-content">
                        <h2 className="main-content__title">Clients</h2>
                        <div className="table-navigation">
                            <button onClick={() => setShowModal(true)} className="table-navigation__create btn-modal"><span>Create new client</span></button>
                            <ElementsSearch searchElements={searchClients} />
                        </div>
                        <Alphabet searchByLetter={searchClientsByLetter} searchLetter={searchLetter} firstLetters={firstLetters} />
                        {clients.filter(client => fulfillsSearch(client)).map(client => <ClientCard key={client.id} cities={cities} countries={countries} clientId={client.id} clientName={client.name} clientAddress={client.address} clientCity={client.cityId} clientCountry={client.countryId} clients={clients} setClients={setClients} />)}
                    </div>
                    <Pagination elements={clients} setElements={setClients} />
                </section>
            </div>
        </>
    );
}