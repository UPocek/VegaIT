/* eslint-disable react/no-multi-comp */
import EmployeesPreview from "@/components/EmployeesPreview";
import Footer from "@/components/Footer";
import NavBar from "@/components/NavBar";


export default function Employees() {
    return (
        <>
            <NavBar />
            <EmployeesPreview />
            <Footer />
        </>
    );
}