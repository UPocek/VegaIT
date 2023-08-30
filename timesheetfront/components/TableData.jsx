import { useEffect, useState } from "react";
import TableTotal from "./TableTotal";
import axios from "axios";
import { baseUrl } from "@/pages/_app";
import Link from "next/link";

export default function TableData({ today, dateToShow }) {
    const weekDays = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];

    const [cells, setCells] = useState([]);
    const [total, setTotal] = useState(0);
    const [timeEntrys, setTimeEntries] = useState([]);

    useEffect(() => {
        populateCells(dateToShow);
    }, [dateToShow])

    function populateCells(currentDate) {
        let startDate = '';
        let endDate = '';

        let newCells = [[]];
        let currentWeek = 0;
        let monthStart = new Date(currentDate).setDate(1);

        let dateSelected = new Date(monthStart);
        let startDay = dateSelected.getDay();
        while (startDay != 1) {
            dateSelected.setDate(dateSelected.getDate() - 1);
            newCells[currentWeek].unshift(new Date(dateSelected));
            startDay = dateSelected.getDay();
        }

        startDate = new Date(dateSelected);

        dateSelected = new Date(monthStart);
        while (currentDate.getMonth() == dateSelected.getMonth()) {
            newCells[currentWeek].push(new Date(dateSelected));
            dateSelected.setDate(dateSelected.getDate() + 1);
            if (dateSelected.getDay() == 1) {
                newCells.push([]);
                currentWeek++;
            }
        }

        startDay = dateSelected.getDay();
        while (startDay != 1) {
            newCells[currentWeek].push(new Date(dateSelected));
            dateSelected.setDate(dateSelected.getDate() + 1);
            startDay = dateSelected.getDay();
        }

        endDate = new Date(dateSelected);

        requestTimeEnteriesForDateRange(startDate, endDate);

        setCells(newCells);
    }

    function requestTimeEnteriesForDateRange(startDate, endDate) {
        axios.get(`${baseUrl}/api/timeentry/range?start=${startDate.toISOString()}&end=${endDate.toISOString()}`)
            .then(response => {
                const times = response.data.map(entry => entry.time);
                setTimeEntries(times);
                setTotal(times.reduce((partialSum, entry) => partialSum + entry, 0));
            })
            .catch(error => console.log(error));
    }

    return (
        <>
            <div className="table-wrapper">
                <table className="month-table">
                    <thead>
                        <tr>
                            {weekDays.map(dayName => <th key={dayName} className="month-table__days">{dayName}</th>)}
                        </tr>
                    </thead>
                    <tbody>
                        {cells.map((week, idx) => (
                            <tr key={idx}>
                                {week.map((day, idxDay) => (
                                    <td key={idxDay} className={`month-table__regular ${((idx <= 0 && day.getDate() > 7) || (idx >= 4 && day.getDate() < 7)) ? 'month-table__regular--disabled' : ''}  ${(today.getFullYear() == day.getFullYear() && today.getMonth() == day.getMonth() && today.getDate() == day.getDate()) ? 'month-table__regular--important' : ''}`}>
                                        <div className="month-table__date">
                                            <span >{day.getDate()}</span>
                                        </div>
                                        <div className="month-table__hours">
                                            <Link href={`/days?date=${day.toISOString()}`} className=" month-table__day">
                                                <span>Hours: </span><span>{timeEntrys[idx * 7 + idxDay]}</span>
                                            </Link>
                                        </div>
                                    </td>
                                ))}
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
            <TableTotal total={total} />
        </>
    );



}