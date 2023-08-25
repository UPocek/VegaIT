import { useEffect, useState } from "react";
import ClientModal from "./ClientModel";
import axios from "axios";
import { baseUrl } from "@/pages/_app";
import ClientCard from "./ClientCard";
import ElementsSearch from "./ElementsSearch";
import Alphabet from "./Alphabet";
import Pagination from "./Pagination";

export default function ClientsPreview() {
    const [showModal, setShowModal] = useState(false);
    const [searchLetter, setSearchLetter] = useState('');
    const [cities, setCities] = useState([]);
    const [countries, setCountries] = useState([]);
    const [clients, setClients] = useState([]);
    const [firstLetters, setFirstLetters] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');

    useEffect(() => {
        axios.get(`${baseUrl}/api/client/cities`)
            .then(response => setCities(response.data))
            .catch(error => console.log(error));
        axios.get(`${baseUrl}/api/client/countries`)
            .then(response => setCountries(response.data))
            .catch(error => console.log(error));
        axios.get(`${baseUrl}/api/client/all`)
            .then(response => setClients(response.data))
            .catch(error => console.log(error));
    }, []);

    useEffect(() => {
        updateLetters(clients);
    }, [clients]);

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
    }

    function searchClientsByLetter(letter) {
        setSearchLetter(letter);
        setSearchQuery(letter);
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