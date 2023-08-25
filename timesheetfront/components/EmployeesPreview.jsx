import { useEffect, useState } from "react";
import axios from "axios";
import { baseUrl } from "@/pages/_app";
import ElementsSearch from "./ElementsSearch";
import Alphabet from "./Alphabet";
import Pagination from "./Pagination";
import EmployeesCard from "./EmployeesCard";
import EmployeesModal from "./EmployeesModal";

export default function EmployeesPreview() {
    const [showModal, setShowModal] = useState(false);
    const [searchLetter, setSearchLetter] = useState('');
    const [users, setUsers] = useState([]);
    const [roles, setRoles] = useState([]);
    const [firstLetters, setFirstLetters] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');

    useEffect(() => {
        axios.get(`${baseUrl}/api/role/all`)
            .then(response => setRoles(response.data))
            .catch(error => console.log(error));
        axios.get(`${baseUrl}/api/user/all`)
            .then(response => setUsers(response.data))
            .catch(error => console.log(error));
    }, []);

    useEffect(() => {
        updateLetters(users);
    }, [users]);

    function updateLetters(dataForLetters) {
        let letters = [];
        for (let element of dataForLetters) {
            letters.push(element.name[0].toLowerCase());
        }
        // eslint-disable-next-line no-undef
        setFirstLetters([...new Set(letters)]);
    }

    function searchUsers(e) {
        e.preventDefault();
        setSearchLetter('');
        setSearchQuery(e.target.searchText.value);
    }

    function searchUsersByLetter(letter) {
        setSearchLetter(letter);
        setSearchQuery(letter);
    }

    function fulfillsSearch(client) {
        if (searchQuery == '') return true;
        return client.name.toLowerCase().startsWith(searchQuery.toLowerCase());
    }

    return (
        <>
            {showModal && <EmployeesModal roles={roles} setShowModal={setShowModal} users={users} setUsers={setUsers} />}
            <div className="wrapper">
                <section className="content">
                    <div className="main-content">
                        <h2 className="main-content__title">Employees</h2>
                        <div className="table-navigation">
                            <button onClick={() => setShowModal(true)} className="table-navigation__create btn-modal"><span>Create new employee</span></button>
                            <ElementsSearch searchElements={searchUsers} />
                        </div>
                        <Alphabet searchByLetter={searchUsersByLetter} searchLetter={searchLetter} firstLetters={firstLetters} />
                        {users.filter(userToMap => fulfillsSearch(userToMap)).map(userToMap => <EmployeesCard key={userToMap.id} roles={roles} userId={userToMap.id} userName={userToMap.name} userUsername={userToMap.username} userEmail={userToMap.email} userStatus={userToMap.isActive} userRole={userToMap.roleId} users={users} setUsers={setUsers} />)}
                    </div>
                    <Pagination elements={users} setElements={setUsers} />
                </section>
            </div>
        </>
    );
}