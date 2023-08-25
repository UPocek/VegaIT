import { baseUrl } from "@/pages/_app";
import axios from "axios";
import { useState } from "react";

export default function CategoriesModal({ setShowModal, categories, setCategories }) {
    const [credentialsNotValid, setCredentialsNotValid] = useState(false);

    const [name, setName] = useState('');

    function createNewCategory(e) {
        e.preventDefault();
        if (!credentialsValid(e)) {
            setCredentialsNotValid(true);
            return;
        }
        axios.post(`${baseUrl}/api/category`, { 'name': name })
            .then(response => {
                setCredentialsNotValid(false);
                resetForm();
                const allCategories = [...categories];
                allCategories.push(response.data);
                setCategories(allCategories);
                alert("New category added successfully");
            })
            .catch(_ => setCredentialsNotValid(true));
    }

    function credentialsValid() {
        return name;
    }

    function resetForm() {
        setName('');
    }

    return (
        <div className="modal">
            <div className="modal__content">
                <h2 className="heading">Create new category</h2>
                <button onClick={() => setShowModal(false)} className="modal__close">
                    <span className="modal__icon" />
                </button>
                <form className="info" onSubmit={createNewCategory}>
                    <ul className="info__form">
                        <li className="info__list">
                            <label htmlFor="cname" className="info__label">Category name:</label>
                            <input type="text" name="cname" id="cname" className="in-text" value={name} onChange={(e) => setName(e.currentTarget.value)} />
                        </li>
                    </ul>
                    <div className="btn-wrap">
                        <button type="submit" className="btn btn--green"><span>Save changes</span></button>
                        <button onClick={resetForm} type="button" className="btn btn--red"><span>Reset</span></button>
                    </div>
                    {credentialsNotValid && <p className="error">Name filed must be filled</p>}
                </form>
            </div>
        </div>
    );
}