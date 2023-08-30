import Footer from "@/components/Footer";
import NavBar from "@/components/NavBar";
import ReportsForm from "@/components/ReportsForm";
import { useEffect, useState } from "react";

export default function Reports() {
    return (
        <>
            <NavBar />
            <MainArea />
            <Footer />
        </>
    );
}

// eslint-disable-next-line react/no-multi-comp
function MainArea() {

    const [enteriesToShow, setEnteriesToShow] = useState([]);
    const [total, setTotal] = useState(0);
    const [overtimeFlag, setOvertimeFlag] = useState(false);

    useEffect(() => {
        const newTotal = overtimeFlag ? enteriesToShow.map(entry => entry.overtime).reduce((partialSum, entry) => partialSum + entry, 0) : enteriesToShow.map(entry => entry.hours).reduce((partialSum, entry) => partialSum + entry, 0);
        setTotal(newTotal);
    }, [enteriesToShow, overtimeFlag])

    function printReport() {
        print();
    }

    function createPDF() {
        printReport();
    }

    function exportToExcel() {
        let csvContent = "data:text/csv;charset=utf-8,";

        enteriesToShow.forEach(entry => {
            let row = '';
            for (let fieldValue of entry) {
                row += (fieldValue + ',')
            }
            csvContent += row + "\r\n";
        });

        var encodedUri = encodeURI(csvContent);
        window.open(encodedUri);
    }

    return (
        <>
            <div className="wrapper">
                <section className="content">
                    <div className="main-content">
                        <h2 className="main-content__title">Reports</h2>
                        <ReportsForm enteriesToShow={enteriesToShow} setEnteriesToShow={setEnteriesToShow} setOvertimeFlag={setOvertimeFlag} />
                        <div className="table-wrapper">
                            <table className="projects-table">
                                <thead>
                                    <tr>
                                        <th className="small"><span>Date</span></th>
                                        <th className="small"><span>Employees</span></th>
                                        <th className="small"><span>Projects</span></th>
                                        <th className="small"><span>Categories</span></th>
                                        <th className="small"><span>Description</span></th>
                                        <th className="small"><span>{overtimeFlag ? 'Overtime' : 'Hours'}</span></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {enteriesToShow.map(entry => (
                                        <tr key={entry.id}>
                                            <td>{entry.date}</td>
                                            <td>{entry.employeeName}</td>
                                            <td>{entry.projectName}</td>
                                            <td>{entry.categoryName}</td>
                                            <td>{entry.description}</td>
                                            <td>{overtimeFlag ? (entry.overtime || 0) : entry.hours}</td>
                                        </tr>))}
                                </tbody>
                            </table>
                        </div>
                        <div className="table-navigation">
                            <div className="table-navigation__next">
                                <span className="table-navigation__text">Reports Total:</span>
                                <span>{total}</span>
                            </div>
                        </div>
                        <div className="reports__buttons-bottom">
                            <button onClick={printReport} className="btn btn--transparent">Print Report</button>
                            <button onClick={createPDF} className="btn btn--transparent">Create PDF</button>
                            <button onClick={exportToExcel} className="btn btn--transparent">Export to excel</button>
                        </div>
                    </div>
                </section>
            </div>
        </>
    );
}