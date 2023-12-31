/* eslint-disable react/no-multi-comp */
import Head from 'next/head'
import { Inter } from 'next/font/google'
import styles from '@/styles/Home.module.css'
import NavBar from '@/components/NavBar'
import { useState } from 'react'
import TableNavigation from '@/components/TableNavigation'
import TableData from '@/components/TableData'
import Footer from '@/components/Footer'

const inter = Inter({ subsets: ['latin'] })

export default function Home() {
  return (
    <>
      <Head>
        <title>Uros Pocek</title>
        <meta name="description" content="TimeSheet app for VegaIT" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <main className={`${styles.main} ${inter.className}`}>
        <NavBar />
        <MainArea />
        <Footer />
      </main>
    </>
  )
}

function MainArea() {
  const [dateToShow, setDateToShow] = useState(new Date());

  return (
    <div className="wrapper min100">
      <section className="main-content">
        <h2 className="main-content__title">Timesheet</h2>
        <TableNavigation dateToShow={dateToShow} setDateToShow={setDateToShow} />
        <TableData today={new Date()} dateToShow={dateToShow} />
      </section>
    </div>
  );
}
