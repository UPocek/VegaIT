import { useEffect, useState } from "react";
import DaysNavigation from "./DaysNavigation";
import WeekDaysTable from "./WeekDaysTable";
import DaysTable from "./DaysTable";
import DaysFooter from "./DaysFooter";
import axios from "axios";
import { baseUrl } from "@/pages/_app";

export default function DaysPreview({ date, setDate, clients, projects, categories }) {

    const [wholeWeek, setWholeWeek] = useState([]);
    const [total, setTotal] = useState(0);
    const [dateEnteries, setDateEnteries] = useState([]);

    useEffect(() => {
        calculateWeek(date);
        getAllTimeEntries(date);
    }, [date]);

    useEffect(() => {
        setTotal(dateEnteries.map(entry => entry.hours + (entry.overtime || 0)).reduce((partialSum, entry) => partialSum + entry, 0));
    }, [dateEnteries])

    function calculateWeek(dateToCalculate) {
        let newWeek = [new Date(dateToCalculate)];

        let dateSelected = new Date(dateToCalculate);
        let startDay = dateSelected.getDay();
        while (startDay != 1) {
            dateSelected.setDate(dateSelected.getDate() - 1);
            newWeek.unshift(new Date(dateSelected));
            startDay = dateSelected.getDay();
        }
        dateSelected = new Date(dateToCalculate);
        while (startDay != 0) {
            dateSelected.setDate(dateSelected.getDate() + 1);
            newWeek.push(new Date(dateSelected));
            startDay = dateSelected.getDay();
        }

        setWholeWeek(newWeek);
    }

    function getAllTimeEntries(dateToGet) {
        axios.get(`${baseUrl}/api/timeentry/fordate?date=${dateToGet.toISOString()}`)
            .then(response => {
                const entries = response.data;
                setDateEnteries(entries);
            })
            .catch(error => console.log(error));
    }

    return (
        <div className="wrapper">
            <section className="content">
                <div id="mainContent" className="main-content">
                    <h2 className="main-content__title">Timesheet</h2>
                    {wholeWeek.length >= 7 && <DaysNavigation firstDay={wholeWeek[0]} lastDay={wholeWeek.at(-1)} date={date} setDate={setDate} />}
                    <WeekDaysTable wholeWeek={wholeWeek} date={date} setDate={setDate} />
                    <DaysTable dateSelected={new Date(date)} allClients={clients} allProjects={projects} allCategories={categories} allDateEnteries={dateEnteries} setAllDateEnteries={setDateEnteries} />
                    <DaysFooter total={total} />
                </div>
            </section>
        </div>
    );
}