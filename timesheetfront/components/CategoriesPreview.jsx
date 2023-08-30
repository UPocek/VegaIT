/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect, useState } from "react";
import axios from "axios";
import { baseUrl } from "@/pages/_app";
import ElementsSearch from "./ElementsSearch";
import Alphabet from "./Alphabet";
import Pagination from "./Pagination";
import CategoriesModal from "./CategoriesModal";
import CategoriesCard from "./CategoriesCard";
import { useRouter } from "next/router";

export default function CategoriesPreview() {

    const router = useRouter();

    const [showModal, setShowModal] = useState(false);
    const [searchLetter, setSearchLetter] = useState('');
    const [categories, setCategories] = useState([]);
    const [firstLetters, setFirstLetters] = useState([]);
    const [searchQuery, setSearchQuery] = useState('');

    useEffect(() => {
        axios.get(`${baseUrl}/api/category/all`)
            .then(response => setCategories(response.data))
            .catch(error => console.log(error));
    }, []);

    useEffect(() => {
        updateLetters(categories);
    }, [categories]);

    useEffect(() => {
        const search = router.query.search;
        if (search == null || search.length <= 0) return;
        if (search.length == 1 && searchLetter == '') {
            setSearchLetter(search);
        }
        if (searchQuery == '') {
            setSearchQuery(search);
        }
    }, [router.query.search]);

    function updateLetters(dataForLetters) {
        let letters = [];
        for (let element of dataForLetters) {
            letters.push(element.name[0].toLowerCase());
        }
        // eslint-disable-next-line no-undef
        setFirstLetters([...new Set(letters)]);
    }

    function searchCategories(e) {
        e.preventDefault();
        setSearchLetter('');
        setSearchQuery(e.target.searchText.value);
        router.replace(`/categories?search=${e.target.searchText.value}`);
    }

    function searchCategoriesByLetter(letter) {
        setSearchLetter(letter);
        setSearchQuery(letter);
        router.replace(`/categories?search=${letter}`);
    }

    function fulfillsSearch(client) {
        if (searchQuery == '') return true;
        return client.name.toLowerCase().startsWith(searchQuery.toLowerCase());
    }

    return (
        <>
            {showModal && <CategoriesModal setShowModal={setShowModal} categories={categories} setCategories={setCategories} />}
            <div className="wrapper">
                <section className="content">
                    <div className="main-content">
                        <h2 className="main-content__title">Categories</h2>
                        <div className="table-navigation">
                            <button onClick={() => setShowModal(true)} className="table-navigation__create btn-modal"><span>Create new category</span></button>
                            <ElementsSearch searchElements={searchCategories} />
                        </div>
                        <Alphabet searchByLetter={searchCategoriesByLetter} searchLetter={searchLetter} firstLetters={firstLetters} />
                        {categories.filter(category => fulfillsSearch(category)).map(category => <CategoriesCard key={category.id} categoryId={category.id} categoryName={category.name} categories={categories} setCategories={setCategories} />)}
                    </div>
                    <Pagination elements={categories} setElements={setCategories} />
                </section>
            </div>
        </>
    );
}