import { getMonthNameFromIndex } from "@/helper/helper";

export default function TableNavigation({ dateToShow, setDateToShow }) {
    return (
        <div className="table-navigation">
            <button onClick={() => {
                const newDate = new Date(dateToShow);
                newDate.setMonth(newDate.getMonth() - 1);
                setDateToShow(newDate);
            }} className="table-navigation__prev"
            ><span>previous month</span>
            </button>
            <span className="table-navigation__center">{`${getMonthNameFromIndex(dateToShow.getMonth())}, ${dateToShow.getFullYear()}`}</span>
            <button onClick={() => {
                const newDate = new Date(dateToShow);
                newDate.setMonth(newDate.getMonth() + 1);
                setDateToShow(newDate);
            }} className="table-navigation__next"
            ><span>next month</span>
            </button>
        </div>
    );
}