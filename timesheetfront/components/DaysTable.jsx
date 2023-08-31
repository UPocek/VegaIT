import CreateNewTimeEntryForm from "./CreateNewTimeEntryform";
import ExistingTimeEntry from "./ExistingTimeEntry";

export default function DaysTable({ dateSelected, allClients, allProjects, allCategories, allDateEnteries, setAllDateEnteries }) {
    return (
        <div className="tableEnteriesMain">
            {(allClients && allClients.length > 0 && allProjects.length > 0 && allCategories.length > 0) && allDateEnteries.map(entry => <ExistingTimeEntry key={entry.id} date={dateSelected} entry={entry} clients={allClients} projects={allProjects} categories={allCategories} dateEnteries={allDateEnteries} setDateEnteries={setAllDateEnteries} />)}
            {(allClients && allClients.length > 0 && allProjects.length > 0 && allCategories.length > 0) && <CreateNewTimeEntryForm date={dateSelected} clients={allClients} projects={allProjects} categories={allCategories} dateEnteries={allDateEnteries} setDateEnteries={setAllDateEnteries} />}
        </div>
    );
}