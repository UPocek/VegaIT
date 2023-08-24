import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useState } from "react";

export default function ClientModal({ cities, countries, setShowModal, clients, setClients }) {
    const [credentialsNotValid, setCredentialsNotValid] = useState(false);

    const [name, setName] = useState('');
    const [address, setAddress] = useState('');
    const [city, setCity] = useState(1);
    const [country, setCountry] = useState(1);

    function createNewClient(e) {
        e.preventDefault();
        if (!credentialsValid(e)) {
            setCredentialsNotValid(true);
            return;
        }
        axios.post(`${baseUrl}/api/client`, { 'name': name, 'address': address, 'countryId': country, 'cityId': city }, {

        })
            .then(response => {
                setCredentialsNotValid(false);
                resetForm();
                const allClients = [...clients];
                allClients.push(response.data);
                setClients(allClients);
                alert("New client added successfully");
            })
            .catch(_ => setCredentialsNotValid(true));
    }

    function credentialsValid() {
        return name && address;
    }

    function resetForm() {
        setName('');
        setAddress('');
        setCity(1);
        setCountry(1);
    }

    return (
        <div className="modal">
            <div className="modal__content">
                <h2 className="heading">Create new client</h2>
                <button onClick={() => setShowModal(false)} className="modal__close">
                    <span className="modal__icon" />
                </button>
                <form className="info" onSubmit={createNewClient}>
                    <ul className="info__form">
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
                    <div className="btn-wrap">
                        <button type="submit" className="btn btn--green"><span>Save changes</span></button>
                        <button onClick={resetForm} type="button" className="btn btn--red"><span>Reset</span></button>
                    </div>
                    {credentialsNotValid && <p className="error">All fields must be filled</p>}
                </form>
            </div>
        </div>
    );
}