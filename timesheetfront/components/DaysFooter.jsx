import Link from "next/link";

export default function DaysFooter({ total }) {
    return (
        <div className="table-navigation">
            <Link href="/" className="table-navigation__prev"><span>back to monthly view</span></Link>
            <div className="table-navigation__next">
                <span className="table-navigation__text">Total:</span>
                <span>{total}</span>
            </div>
        </div>
    );
}