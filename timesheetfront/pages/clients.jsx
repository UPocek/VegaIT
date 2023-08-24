import Footer from "@/components/Footer";
import NavBar from "@/components/NavBar";

export default function Clients() {
    return (
        <>
            <NavBar />
            <MainArea />
            <Footer />
        </>
    );
}

// eslint-disable-next-line react/no-multi-comp
function MainArea() {
    return (
        <>
            <div class="modal">
                <div class="modal__content">
                    <h2 class="heading">Create new client</h2>
                    <a href="javascript:;" class="modal__close">
                        <span class="modal__icon"></span>
                    </a>
                    <form class="info" action="javascript:;">
                        <ul class="info__form">
                            <li class="info__list">
                                <label class="info__label">Client name:</label>
                                <input type="text" class="in-text" />
                            </li>
                            <li class="info__list">
                                <label class="report__label">Address:</label>
                                <input type="text" class="in-text" />
                            </li>
                            <li class="info__list">
                                <label class="report__label">City:</label>
                                <input type="text" class="in-text" />
                            </li>
                            <li class="info__list">
                                <label class="report__label">Zip/Postal code:</label>
                                <input type="text" class="in-text" />
                            </li>
                            <li class="info__list">
                                <label class="report__label">Country:</label>
                                <select class="info__select">
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
            <div class="wrapper">
                <section class="content">
                    <div class="main-content">
                        <h2 class="main-content__title">Clients</h2>
                        <div class="table-navigation">
                            <a href="javascript:;" class="table-navigation__create btn-modal"><span>Create new client</span></a>
                            <form class="table-navigation__input-container" action="javascript:;">
                                <input type="text" class="table-navigation__search" />
                                <button type="submit" class="icon__search"></button>
                            </form>
                        </div>
                        <div class="alphabet">
                            <ul class="alphabet__navigation">
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">a</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">b</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">c</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">d</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">e</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">f</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">g</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">h</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">i</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button alphabet__button--disabled" href="javascript:;">j</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">k</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">l</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">m</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">n</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button alphabet__button--active" href="javascript:;">o</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">p</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">q</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">r</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">s</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">t</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">u</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">v</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">w</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">x</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">y</a>
                                </li>
                                <li class="alphabet__list">
                                    <a class="alphabet__button" href="javascript:;">z</a>
                                </li>
                            </ul>
                        </div>
                        <div class="accordion">
                            <div class="accordion__intro">
                                <h4 class="accordion__title">Client 1</h4>
                            </div>
                            <form class="accordion__content" action="javascript:;">
                                <div class="info">
                                    <div class="info__form">
                                        <ul class="info__wrapper">
                                            <li class="info__list">
                                                <label class="info__label">Client name:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">Address:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">City:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">Zip/Postal code:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">Country:</label>
                                                <select class="info__select">
                                                    <option value="">All</option>
                                                </select>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="btn-wrap">
                                    <button type="submit" class="btn btn--green"><span>Save changes</span></button>
                                    <button type="button" class="btn btn--red"><span>Delete</span></button>
                                </div>
                            </form>
                        </div>
                        <div class="accordion">
                            <div class="accordion__intro">
                                <h4 class="accordion__title">Client 2</h4>
                            </div>
                            <form class="accordion__content" action="javascript:;">
                                <div class="info">
                                    <div class="info__form">
                                        <ul class="info__wrapper">
                                            <li class="info__list">
                                                <label class="info__label">Client name:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">Address:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">City:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">Zip/Postal code:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">Country:</label>
                                                <select class="info__select">
                                                    <option value="">All</option>
                                                </select>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="btn-wrap">
                                    <button type="submit" class="btn btn--green"><span>Save changes</span></button>
                                    <button type="button" class="btn btn--red"><span>Delete</span></button>
                                </div>
                            </form>
                        </div>
                        <div class="accordion">
                            <div class="accordion__intro">
                                <h4 class="accordion__title">Client 3</h4>
                            </div>
                            <form class="accordion__content" action="javascript:;">
                                <div class="info">
                                    <div class="info__form">
                                        <ul class="info__wrapper">
                                            <li class="info__list">
                                                <label class="info__label">Client name:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">Address:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">City:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">Zip/Postal code:</label>
                                                <input type="text" class="in-text" />
                                            </li>
                                            <li class="info__list">
                                                <label class="report__label">Country:</label>
                                                <select class="info__select">
                                                    <option value="">All</option>
                                                </select>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="btn-wrap">
                                    <button type="submit" class="btn btn--green"><span>Save changes</span></button>
                                    <button type="button" class="btn btn--red"><span>Delete</span></button>
                                </div>
                            </form>
                        </div>
                    </div>
                    <div class="pagination">
                        <ul class="pagination__navigation">
                            <li class="pagination__list">
                                <a class="pagination__button" href="javascript:;">Previous</a>
                            </li>
                            <li class="pagination__list">
                                <a class="pagination__button pagination__button--active" href="javascript:;">1</a>
                            </li>
                            <li class="pagination__list">
                                <a class="pagination__button" href="javascript:;">2</a>
                            </li>
                            <li class="pagination__list">
                                <a class="pagination__button" href="javascript:;">3</a>
                            </li>
                            <li class="pagination__list">
                                <a class="pagination__button" href="javascript:;">4</a>
                            </li>
                            <li class="pagination__list">
                                <a class="pagination__button" href="javascript:;">Next</a>
                            </li>
                        </ul>
                    </div>
                </section>
            </div>
        </>
    );
}