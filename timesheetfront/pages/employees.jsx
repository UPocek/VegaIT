/* eslint-disable react/no-multi-comp */
import NavBar from "@/components/NavBar";
import RegistrationForm from "@/components/RegistrationForm";

export default function Employees() {
    return (
        <>
            <NavBar />
            <MainArea />
        </>
    );
}

function MainArea() {
    return (
        <RegistrationForm />
    );
}