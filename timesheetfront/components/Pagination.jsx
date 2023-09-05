import { useState } from "react";

export default function Pagination({ elements, setElements }) {
    const [currentTabIndex, setCurrentTabIndex] = useState(1);
    const [numberOfTabs, setNumberOfTabs] = useState(1);

    function previous() {
        setCurrentTabIndex(Math.max(currentTabIndex - 1, 1));
    }

    function next() {
        setCurrentTabIndex(Math.min(currentTabIndex + 1, numberOfTabs));
    }

    return (
        <div className="pagination">
            <ul className="pagination__navigation">
                <li className="pagination__list">
                    <button className="pagination__button" onClick={previous}>Previous</button>
                </li>
                <li className="pagination__list">
                    <button className="pagination__button pagination__button--active" onClick={() => setCurrentTabIndex(1)}>1</button>
                </li>

                <li className="pagination__list">
                    <button className="pagination__button" onClick={next}>Next</button>
                </li>
            </ul>
        </div>
    );
}