import { datesEqual, getMonthShortNameFromIndex, getWeekDayNameFromIndex } from "@/helper/helper";
import { useRouter } from "next/router";

export default function WeekDaysTable({ wholeWeek, date, setDate }) {
    const router = useRouter();
    return (
        <div className="day-table">
            <ul className="day-table__wrap">
                {wholeWeek.map(weekDay => (
                    <li key={weekDay.getDay()} className={`day-table__list ${datesEqual(weekDay, date) ? 'day-table__list--active' : ''}`}>
                        <button onClick={() => { router.replace(`/days?date=${weekDay.toISOString()}`); setDate(weekDay) }} className="day-table__link">
                            <b className="day-table__month">{`${getMonthShortNameFromIndex(weekDay.getMonth())} ${weekDay.getDate()}`}</b>
                            <span className="day-table__span hide-on-mob">{getWeekDayNameFromIndex(weekDay.getDay())}</span>
                            <span className="day-table__span show-on-mob">{getMonthShortNameFromIndex(weekDay.getDay())}</span>
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
}