export default function TableTotal({ total }) {
    return (
        <div className="table-navigation">
            <div className="table-navigation__next">
                <span className="table-navigation__text">Total:</span>
                <span>{total}</span>
            </div>
        </div>
    );
}