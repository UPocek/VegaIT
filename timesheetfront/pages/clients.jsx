import ClientsPreview from "@/components/ClientsPreview";
import Footer from "@/components/Footer";
import NavBar from "@/components/NavBar";

export default function Clients() {
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
    return (
        <>
            <ClientsPreview />
        </>
    );
}