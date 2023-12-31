import { datesCompare, getUserEmail, getUserRole } from "@/helper/helper";
import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useState, useEffect } from "react";

export default function ReportsForm({ setEnteriesToShow, setOvertimeFlag }) {

    const [role, setRole] = useState('');

    const [clients, setClients] = useState([]);
    const [projects, setProjects] = useState([]);
    const [categories, setCategories] = useState([]);
    const [employees, setEmployees] = useState([]);

    const [client, setClient] = useState(-1);
    const [project, setProject] = useState(-1);
    const [category, setCategory] = useState(-1);
    const [employee, setEmployee] = useState(-1);
    const [quickWeek, setQuickWeek] = useState('all');
    const [quickMonth, setQuickMonth] = useState('all');
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');

    useEffect(() => {
        // eslint-disable-next-line no-undef
        Promise.all([
            axios.get(`${baseUrl}/api/client/all-minimal`),
            axios.get(`${baseUrl}/api/project/all-minimal`),
            axios.get(`${baseUrl}/api/category/all`),
            axios.get(`${baseUrl}/api/user/all-minimal`)
        ]).then(([clientResponse, projectResponse, categoryResponse, userResponse]) => {
            setClients(clientResponse.data);
            setProjects(projectResponse.data);
            setCategories(categoryResponse.data);
            setEmployees(userResponse.data);
        }).catch(error => console.log(error));
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

        axios.get(`${baseUrl}/api/timeentry/report?client=${client}&project=${project}&category=${category}&employee=${employee}&time=${defineTime()}`)
            .then(response => setEnteriesToShow(response.data))
            .catch(error => console.log(error))
    }

    function defineTime() {
        if (quickWeek != 'all') return quickWeek;
        if (quickMonth != 'all') return quickMonth;
        if (startDate == '' && endDate == '') return 'all';
        return `${startDate}|${endDate} `;
    }

    function setStart(e) {
        setStartDate(e.currentTarget.value);
        setQuickWeek('all');
        setQuickMonth('all')
    }

    function setEnd(e) {
        setEndDate(e.currentTarget.value);
        setQuickMonth('all');
        setQuickWeek('all');
    }

    function resetForm() {
        setClient(-1);
        setProject(-1);
        setCategory(-1);
        setEmployee(-1);
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
                            <option value={-1}>All</option>
                            {clients.map(clientToMap => <option key={clientToMap.id} value={clientToMap.id}>{clientToMap.name}</option>)}
                        </select>
                    </li>
                }
                {projects &&
                    <li className="reports__list">
                        <label htmlFor="sproject" className="report__label">Project</label>
                        <select value={project} onChange={(e) => setProject(e.target.value)} name="sproject" id="sproject" className="reports__select">
                            <option value={-1}>All</option>
                            {projects.map(projectToMap => <option key={projectToMap.id} value={projectToMap.id}>{projectToMap.name}</option>)}
                        </select>
                    </li>
                }
                {categories &&
                    <li className="reports__list">
                        <label htmlFor="scategory" className="report__label">Category:</label>
                        <select value={category} onChange={(e) => setCategory(e.target.value)} name="scategory" id="scategory" className="reports__select">
                            <option value={-1}>All</option>
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
                            {role == 'admin' && <option value={-1}>All</option>}
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
                    <select value={quickWeek} onChange={(e) => { setQuickMonth('all'); setStartDate(''); setEndDate(''); setQuickWeek(e.target.value) }} name="sweek" id="sweek" className="reports__select">
                        <option value="all">All</option>
                        <option value="this_week">This week</option>
                        <option value="last_week">Last week</option>
                    </select>
                </li>
                <li className="reports__list">
                    <label htmlFor="smonth" className="report__label">Quick date: (months)</label>
                    <select value={quickMonth} onChange={(e) => { setQuickWeek('all'); setStartDate(''); setEndDate(''); setQuickMonth(e.target.value) }} name="smonth" id="smonth" className="reports__select">
                        <option value="all">All</option>
                        <option value="this_month">This month</option>
                        <option value="last_month">Last month</option>
                    </select>
                </li>
                <li className="reports__list">
                    <label htmlFor="sstartdate" className="report__label">Start date</label>
                    <input value={startDate} onChange={(e) => { (endDate == '' || datesCompare(new Date(endDate), new Date(e.currentTarget.value)) >= 0) && setStart(e) }} name="sstartdate" id="sstartdate" type="date" className="in-text" />
                </li>
                <li className="reports__list">
                    <label htmlFor="senddate" className="report__label">End date</label>
                    <input value={endDate} onChange={(e) => { (startDate == '' || datesCompare(new Date(e.currentTarget.value), new Date(startDate)) >= 0) && setEnd(e) }} name="senddate" id="senddate" type="date" className="in-text" />
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