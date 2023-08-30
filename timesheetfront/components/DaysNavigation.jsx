import { getMonthNameFromIndex } from "@/helper/helper";
import { useRouter } from "next/router";
import { useEffect, useState } from "react";

export default function DaysNavigation({ firstDay, lastDay, date, setDate }) {
    const router = useRouter();
    const [weekNumer, setWeekNumber] = useState(0);

    useEffect(() => {
        calculateWeekNumber(date);
    }, [date])

    function calculateWeekNumber(date) {
        const currentDate = new Date(date);
        const startDate = new Date(currentDate.getFullYear(), 0, 1);
        const days = Math.floor((currentDate - startDate) /
            (24 * 60 * 60 * 1000));

        const weekNumber = Math.ceil(days / 7);
        setWeekNumber(weekNumber);
    }

    return (
        <div className="table-navigation">
            <button onClick={() => {
                const newDate = new Date(date);
                newDate.setDate(newDate.getDate() - 7);
                router.replace(`days?date=${newDate.toISOString()}`);
                setDate(newDate);
            }} className="table-navigation__prev"
            ><span>previous week</span>
            </button>
            <span className="table-navigation__center">{`${getMonthNameFromIndex(firstDay.getMonth())} ${firstDay.getDate()} - ${getMonthNameFromIndex(lastDay.getMonth())} ${lastDay.getDate()}, ${date.getFullYear()} (week ${weekNumer})`}</span>
            <button onClick={() => {
                const newDate = new Date(date);
                newDate.setDate(newDate.getDate() + 7);
                router.replace(`days?date=${newDate.toISOString()}`);
                setDate(newDate);
            }} className="table-navigation__next"
            ><span>next week</span>
            </button>
        </div>)
}