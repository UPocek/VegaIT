/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable jsx-a11y/no-static-element-interactions */
import { useState } from "react";
import axios from "axios";
import { baseUrl } from "@/pages/_app";

export default function ClientCard({ cities, countries, clientId, clientName, clientAddress, clientCity, clientCountry, clients, setClients }) {
    const [name, setName] = useState(clientName);
    const [address, setAddress] = useState(clientAddress);
    const [city, setCity] = useState(clientCity);
    const [country, setCountry] = useState(clientCountry);

    const [showAccordion, setShowAccordion] = useState(false);

    function updateClient(e) {
        e.preventDefault();
        if (!credentialsValid(e)) {
            alert("Invalid new data");
            return;
        }
        axios.put(`${baseUrl}/api/client/${clientId}`, { 'name': name, 'address': address, 'countryId': country, 'cityId': city })
            .then(response => {
                const allClients = clients.filter(c => c.id != clientId);
                allClients.push(response.data);
                setClients(allClients);
                alert("Client updated successfully");
            })
            .catch(error => console.log(error));
    }

    function credentialsValid() {
        return name.length > 1 && address.length > 1 && (name != clientName || address != clientAddress || city != clientCity || country != clientCountry)
    }

    function deleteClient() {
        axios.delete(`${baseUrl}/api/client/${clientId}`)
            .then(_ => {
                alert("Client deleted successfully");
                const remainingClients = clients.filter(c => c.id != clientId);
                setClients(remainingClients);
            })
            .catch(error => console.log(error));
    }

    return (
        <div className="accordion">
            <div className="accordion__intro" onClick={() => setShowAccordion(!showAccordion)}>
                <h4 className="accordion__title">{clientName}</h4>
            </div>
            <form className={`accordion__content ${showAccordion ? 'show_accordion' : 'hide_accordion'}`} onSubmit={updateClient}>
                <div className="info">
                    <div className="info__form">
                        <ul className="info__wrapper">
                            <li className="info__list">
                                <label htmlFor="cname" className="info__label">Client name:</label>
                                <input type="text" name="cname" id="cname" className="in-text" value={name} onChange={(e) => setName(e.currentTarget.value)} />
                            </li>
                            <li className="info__list">
                                <label htmlFor="caddress" className="report__label">Address:</label>
                                <input type="text" name="caddress" id="caddress" className="in-text" value={address} onChange={(e) => setAddress(e.currentTarget.value)} />
                            </li>
                            <li className="info__list">
                                <label htmlFor="ccountry" className="report__label">Country:</label>
                                <select name="ccountry" id="ccountry" className="info__select" value={country} onChange={(e) => setCountry(e.currentTarget.value)}>
                                    {countries.map(country => <option key={country.id} value={country.id}>{country.name}</option>)}
                                </select>
                            </li>
                            <li className="info__list">
                                <label htmlFor="ccity" className="report__label">City:</label>
                                <select name="ccity" id="ccity" className="info__select" value={city} onChange={(e) => setCity(e.currentTarget.value)}>
                                    {cities.map(city => <option key={city.id} value={city.id}>{`${city.name} / ${city.zip}`}</option>)}
                                </select>
                            </li>
                        </ul>
                    </div>
                </div>
                <div className="btn-wrap">
                    <button type="submit" className="btn btn--green"><span>Save changes</span></button>
                    <button onClick={deleteClient} type="button" className="btn btn--red"><span>Delete</span></button>
                </div>
            </form>
        </div>
    );
}