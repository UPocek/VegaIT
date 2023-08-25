/* eslint-disable react/no-multi-comp */
import Head from 'next/head'
import { Inter } from 'next/font/google'
import styles from '@/styles/Home.module.css'
import NavBar from '@/components/NavBar'
// import { useState } from 'react'
// import axios from 'axios';
// import { baseUrl } from './_app';

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
      </main>
    </>
  )
}

function MainArea() {
  return (
    <div>A</div>
  );
}
