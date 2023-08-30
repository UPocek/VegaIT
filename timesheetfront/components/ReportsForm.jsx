import { datesCompare, getUserEmail, getUserRole } from "@/helper/helper";
import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useState, useEffect } from "react";

export default function ReportsForm({ enteriesToShow, setEnteriesToShow, setOvertimeFlag }) {

    const [role, setRole] = useState('');

    const [clients, setClients] = useState([]);
    const [projects, setProjects] = useState([]);
    const [categories, setCategories] = useState([]);
    const [employees, setEmployees] = useState([]);

    const [client, setClient] = useState('all');
    const [project, setProject] = useState('all');
    const [category, setCategory] = useState('all');
    const [employee, setEmployee] = useState('');
    const [quickWeek, setQuickWeek] = useState('all');
    const [quickMonth, setQuickMonth] = useState('all');
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');

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
        axios.get(`${baseUrl}/api/user/all-minimal`)
            .then(response => setEmployees(response.data))
            .catch(error => console.log(error));
    }, []);

    useEffect(() => {
        setRole(getUserRole());
    }, []);

    function performSearch(e) {
        e.preventDefault();
        const buttonName = e.nativeEvent.submitter.name;

        if (buttonName === 'search') {
            setOvertimeFlag(false);
        } else if (buttonName === 'searchOvertime') {
            setOvertimeFlag(true);
        }

        axios.get(`${baseUrl}/api/timeentry/report?client=${client}&prject=${project}&category=${category}&employee=${employee}&time=${defineTime()}`)
    }

    function defineTime() {
        if (quickWeek != 'all') return quickWeek;
        if (quickMonth != 'all') return quickMonth;
        if (startDate == '' && endDate == '') return '';
    }

    function resetForm() {
        setClient('all');
        setProject('all');
        setCategory('all');
        setEmployee('');
        setQuickWeek('all');
        setQuickMonth('all');
        setStartDate('');
        setEndDate('');
    }

    return (
        <form className="reports" onSubmit={performSearch}>
            <ul className="reports__form">
                {clients &&
                    <li className="reports__list">
                        <label htmlFor="sclient" className="report__label">Client</label>
                        <select value={client} onChange={(e) => setClient(e.target.value)} name="sclient" id="sclient" className="reports__select">
                            <option value="all">All</option>
                            {clients.map(clientToMap => <option key={clientToMap.id} value={clientToMap.id}>{clientToMap.name}</option>)}
                        </select>
                    </li>
                }
                {projects &&
                    <li className="reports__list">
                        <label htmlFor="sproject" className="report__label">Project</label>
                        <select value={project} onChange={(e) => setProject(e.target.value)} name="sproject" id="sproject" className="reports__select">
                            <option value="all">All</option>
                            {projects.map(projectToMap => <option key={projectToMap.id} value={projectToMap.id}>{projectToMap.name}</option>)}
                        </select>
                    </li>
                }
                {categories &&
                    <li className="reports__list">
                        <label htmlFor="scategory" className="report__label">Category:</label>
                        <select value={category} onChange={(e) => setCategory(e.target.value)} name="scategory" id="scategory" className="reports__select">
                            <option value="all">All</option>
                            {categories.map(categoryToMap => <option key={categoryToMap.id} value={categoryToMap.id}>{categoryToMap.name}</option>)}
                        </select>
                    </li>
                }
            </ul>
            <ul className="reports__form">
                {(employees && role) &&
                    <li className="reports__list">
                        <label htmlFor="semployee" className="report__label">Employees</label>
                        <select value={employee} onChange={(e) => setEmployee(e.target.value)} name="semployee" id="semployee" className="reports__select">
                            {role == 'admin' && <option value="all">All</option>}
                            {role == 'admin' ?
                                employees.map(employeeToMap => <option key={employeeToMap.id} value={employeeToMap.id}>{employeeToMap.name}</option>) :
                                employees.filter(employeeToFilter => employeeToFilter.email == getUserEmail()).map(employeeToMap => <option key={employeeToMap.id} value={employeeToMap.id}>{employeeToMap.name}</option>)
                            }
                        </select>
                    </li>
                }
            </ul>
            <ul className="reports__form">
                <li className="reports__list">
                    <label htmlFor="sweek" className="report__label">Quick date (weeks):</label>
                    <select value={quickWeek} onChange={(e) => setQuickWeek(e.target.value)} name="sweek" id="sweek" className="reports__select">
                        <option value="all">All</option>
                        <option value="this_week">This week</option>
                        <option value="last_week">Last week</option>
                    </select>
                </li>
                <li className="reports__list">
                    <label htmlFor="smonth" className="report__label">Quick date: (months)</label>
                    <select value={quickMonth} onChange={(e) => setQuickMonth(e.target.value)} name="smonth" id="smonth" className="reports__select">
                        <option value="">All</option>
                        <option value="this_month">This month</option>
                        <option value="last_month">Last month</option>
                    </select>
                </li>
                <li className="reports__list">
                    <label htmlFor="sstartdate" className="report__label">Start date</label>
                    <input value={startDate} onChange={(e) => { (endDate == '' || datesCompare(new Date(endDate), new Date(e.currentTarget.value)) >= 0) && setStartDate(e.currentTarget.value) }} name="sstartdate" id="sstartdate" type="date" className="in-text" />
                </li>
                <li className="reports__list">
                    <label htmlFor="senddate" className="report__label">End date</label>
                    <input value={endDate} onChange={(e) => { (startDate == '' || datesCompare(new Date(e.currentTarget.value), new Date(startDate)) >= 0) && setEndDate(e.currentTarget.value) }} name="senddate" id="senddate" type="date" className="in-text" />
                </li>
            </ul>
            <div className="reports__buttons">
                <button type="submit" name="search" className="btn btn--green">Search</button>
                <button type="submit" name="searchOvertime" className="btn btn--green">Search Overtime</button>
                <button onClick={resetForm} type="button" className="btn btn--green">Reset</button>
            </div>
        </form>
    );
}