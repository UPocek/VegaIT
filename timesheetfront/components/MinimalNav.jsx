import Image from "next/image";
import Link from "next/link";

export default function MinimalNav() {
    return (
        <header className="header">
            <div className="inner-wrap">
                <Link href="/login" className="logo">
                    <Image src="/images/logo-white.png" width={115} height={54} alt="logo" />
                </Link>
            </div>
        </header>
    );
}