/* eslint-disable react/no-multi-comp */
import Footer from "@/components/Footer";
import NavBar from "@/components/NavBar";
import RegistrationForm from "@/components/RegistrationForm";

export default function Registration() {
    return (
        <>
            <NavBar />
            <MainArea />
            <Footer />
        </>
    );
}

function MainArea() {
    return (
        <RegistrationForm />
    );
}