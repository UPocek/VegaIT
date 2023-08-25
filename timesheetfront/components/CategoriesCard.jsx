/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable jsx-a11y/no-static-element-interactions */
import { useState } from "react";
import axios from "axios";
import { baseUrl } from "@/pages/_app";

export default function CategoriesCard({ categoryId, categoryName, categories, setCategories }) {
    const [name, setName] = useState(categoryName);

    const [showAccordion, setShowAccordion] = useState(false);

    function updateCategory(e) {
        e.preventDefault();
        if (!credentialsValid(e)) {
            alert("Invalid new data");
            return;
        }
        axios.put(`${baseUrl}/api/category/${categoryId}`, { 'name': name })
            .then(response => {
                const allCategories = categories.filter(p => p.id != categoryId);
                allCategories.push(response.data);
                setCategories(allCategories);
                alert("Category updated successfully");
            })
            .catch(error => console.log(error));
    }

    function credentialsValid() {
        return name.length > 1 && (name != categoryName)
    }

    function deleteCategory() {
        axios.delete(`${baseUrl}/api/category/${categoryId}`)
            .then(_ => {
                const remainingCategories = categories.filter(p => p.id != categoryId);
                setCategories(remainingCategories);
                alert("Category deleted successfully");
            })
            .catch(error => console.log(error));
    }

    return (
        <div className="accordion">
            <div className="accordion__intro" onClick={() => setShowAccordion(!showAccordion)}>
                <h4 className="accordion__title">{categoryName}</h4>
            </div>
            <form className={`accordion__content ${showAccordion ? 'show_accordion' : 'hide_accordion'}`} onSubmit={updateCategory}>
                <div className="info">
                    <div className="info__form">
                        <ul className="info__wrapper">
                            <li className="info__list">
                                <label htmlFor="cname" className="info__label">Category name:</label>
                                <input type="text" name="cname" id="cname" className="in-text" value={name} onChange={(e) => setName(e.currentTarget.value)} />
                            </li>
                        </ul>
                    </div>
                </div>
                <div className="btn-wrap">
                    <button type="submit" className="btn btn--green"><span>Save changes</span></button>
                    <button onClick={deleteCategory} type="button" className="btn btn--red"><span>Delete</span></button>
                </div>
            </form>
        </div>
    );
}