export default function ElementsSearch({ searchElements }) {
    return (
        <form className="table-navigation__input-container" onSubmit={searchElements}>
            <input type="text" name="searchText" id="searchText" className="table-navigation__search" placeholder="" />
            <button type="submit" className="icon__search" />
        </form>
    );
}