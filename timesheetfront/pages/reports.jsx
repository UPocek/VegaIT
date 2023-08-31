/* eslint-disable react-hooks/exhaustive-deps */
import Footer from "@/components/Footer";
import NavBar from "@/components/NavBar";
import ReportsForm from "@/components/ReportsForm";
import { convertFormISOToOur } from "@/helper/helper";
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
    const [excelUrl, setExcelUrl] = useState('');

    useEffect(() => {
        const newTotal = overtimeFlag ? enteriesToShow.map(entry => entry.overtime).reduce((partialSum, entry) => partialSum + entry, 0) : enteriesToShow.map(entry => entry.hours).reduce((partialSum, entry) => partialSum + entry, 0);
        setTotal(newTotal);
        prepareExcel();
    }, [enteriesToShow, overtimeFlag])

    function printReport() {
        print();
    }

    function printDiv(elem) {
        var divContents = document.getElementById(elem).innerHTML;
        var a = window.open('', '', 'height=500, width=500');
        a.document.write('<html>');
        a.document.write('<body > <h1>Report</h1> <br/><br/>');
        a.document.write(divContents);
        a.document.write('</body></html>');
        a.document.close();
        a.print();
    }

    function createPDF() {
        printDiv('reportContent');
    }

    function prepareExcel() {
        let csvContent = "";

        const tableHead = `Date,Employees,Projects,Categories,Description,${overtimeFlag ? 'Overtime' : 'Hours'}`;
        csvContent += tableHead + "\r\n";

        enteriesToShow.forEach(entry => {
            let row = '';
            for (let key in entry) {
                row += `${entry[key]},`
            }
            csvContent += row + "\r\n";
        });

        const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8,' })
        const objUrl = URL.createObjectURL(blob);
        setExcelUrl(objUrl);
    }

    return (
        <>
            <div className="wrapper">
                <section className="content">
                    <div className="main-content">
                        <h2 className="main-content__title">Reports</h2>
                        <ReportsForm setEnteriesToShow={setEnteriesToShow} setOvertimeFlag={setOvertimeFlag} />
                        <div id="reportContent" className="table-wrapper">
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
                                            <td>{convertFormISOToOur(entry.date)}</td>
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
                            <a href={excelUrl} className="btn btn--transparent" download="Report.csv">Export to excel</a>
                        </div>
                    </div>
                </section>
            </div>
        </>
    );
}