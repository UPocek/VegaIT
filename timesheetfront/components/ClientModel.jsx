import { useState } from "react";

export default function ClientModal() {

    const [cities, setCities] = useState([]);
    const [countries, setCountries] = useState([]);

    return (
        <div class="modal">
            <div class="modal__content">
                <h2 class="heading">Create new client</h2>
                <button class="modal__close">
                    <span class="modal__icon" />
                </button>
                <form class="info" action="javascript:;">
                    <ul class="info__form">
                        <li class="info__list">
                            <label htmlFor="cname" class="info__label">Client name:</label>
                            <input type="text" name="cname" id="cname" class="in-text" />
                        </li>
                        <li class="info__list">
                            <label htmlFor="caddress" class="report__label">Address:</label>
                            <input type="text" name="caddress" id="caddress" class="in-text" />
                        </li>
                        <li class="info__list">
                            <label htmlFor="ccity" class="report__label">City:</label>
                            <select name="ccity" id="ccity" class="info__select">
                                <option value="">All</option>
                            </select>
                        </li>
                        <li class="info__list">
                            <label htmlFor="ccountry" class="report__label">Country:</label>
                            <select name="ccountry" id="ccountry" class="info__select">
                                <option value="">All</option>
                            </select>
                        </li>
                    </ul>
                    <div class="btn-wrap">
                        <button type="submit" class="btn btn--green"><span>Save changes</span></button>
                        <button type="button" class="btn btn--red"><span>Delete</span></button>
                    </div>
                </form>
            </div>
        </div>
    );
}