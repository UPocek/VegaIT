import { useEffect, useState } from "react";
import ClientModal from "./ClientModel";
import axios from "axios";
import { baseUrl } from "@/pages/_app";
import ClientCard from "./ClientCard";

export default function ClientsPreview() {
    const alphabetChacacters = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];
    const [numberOfTabs, setNumberOfTabs] = useState(1);
    const [showModal, setShowModal] = useState(false);
    const [currentTabIndex, setCurrentTabIndex] = useState(1);
    const [searchLetter, setSearchLetter] = useState('');
    const [cities, setCities] = useState([]);
    const [countries, setCountries] = useState([]);
    const [clients, setClients] = useState([]);
    const [firstLetters, setFirstLetters] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');

    useEffect(() => {
        let letters = [];
        axios.get(`${baseUrl}/api/client/cities`)
            .then(response => setCities(response.data))
            .catch(error => console.log(error));
        axios.get(`${baseUrl}/api/client/countries`)
            .then(response => setCountries(response.data))
            .catch(error => console.log(error));
        axios.get(`${baseUrl}/api/client/all`)
            .then(response => {
                setClients(response.data);
                for (let client of response.data) {
                    letters.push(client.name[0].toLowerCase())
                }
                // eslint-disable-next-line no-undef
                setFirstLetters([...new Set(letters)]);
            })
            .catch(error => console.log(error));
    }, [])

    function searchClients(e) {
        e.preventDefault();
        setSearchLetter('');
        setSearchQuery(e.target.searchText.value);
    }

    function searchByLetter(letter) {
        setSearchLetter(letter);
        setSearchQuery(letter);
    }

    function fulfillsSearch(client) {
        if (searchQuery == '') return true;
        return client.name.toLowerCase().startsWith(searchQuery.toLowerCase());

    }

    function previous() {
        setCurrentTabIndex(Math.max(currentTabIndex - 1, 1));
    }

    function next() {
        setCurrentTabIndex(Math.min(currentTabIndex + 1, numberOfTabs));
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
                            <form className="table-navigation__input-container" onSubmit={searchClients}>
                                <input type="text" name="searchText" id="searchText" className="table-navigation__search" placeholder="" />
                                <button type="submit" className="icon__search" />
                            </form>
                        </div>
                        <div className="alphabet">
                            <ul className="alphabet__navigation">
                                {alphabetChacacters.map(letter => <li key={letter} className="alphabet__list"> <button onClick={() => searchByLetter(letter)} className={`alphabet__button ${searchLetter == letter ? 'alphabet__button--active' : ''}  ${!firstLetters.includes(letter) ? 'alphabet__button--disabled' : ''}`}>{letter}</button> </li>)}
                            </ul>
                        </div>
                        {clients.filter(client => fulfillsSearch(client)).map(client => <ClientCard key={client.id} cities={cities} countries={countries} clientId={client.id} clientName={client.name} clientAddress={client.address} clientCity={client.cityId} clientCountry={client.countryId} clients={clients} setClients={setClients} />)}
                    </div>
                    <div className="pagination">
                        <ul className="pagination__navigation">
                            <li className="pagination__list">
                                <button className="pagination__button" onClick={previous}>Previous</button>
                            </li>
                            <li className="pagination__list">
                                <button className="pagination__button pagination__button--active" onClick={() => setCurrentTabIndex(1)}>1</button>
                            </li>

                            <li className="pagination__list">
                                <button className="pagination__button" onClick={next}>Next</button>
                            </li>
                        </ul>
                    </div>
                </section>
            </div>
        </>
    );
}