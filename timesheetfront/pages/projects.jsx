import ProjectsPreview from "@/components/ProjectsPreview";
import Footer from "@/components/Footer";
import NavBar from "@/components/NavBar";

export default function Projects() {
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
            <ProjectsPreview />
        </>
    );
}