import CategoriesPreview from "@/components/CategoriesPreview";
import Footer from "@/components/Footer";
import NavBar from "@/components/NavBar";

export default function Categories() {
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
            <CategoriesPreview />
        </>
    );
}