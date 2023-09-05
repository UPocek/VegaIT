import { memo } from "react";

const Alphabet = memo(function Alphabet({ searchByLetter, searchLetter, firstLetters }) {
    const alphabetChacacters = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];

    return (
        <div className="alphabet">
            <ul className="alphabet__navigation">
                {alphabetChacacters.map(letter => <li key={letter} className="alphabet__list"> <button onClick={() => searchByLetter(letter)} className={`alphabet__button ${searchLetter == letter ? 'alphabet__button--active' : ''}  ${!firstLetters.includes(letter) ? 'alphabet__button--disabled' : ''}`}>{letter}</button> </li>)}
            </ul>
        </div>
    );
});

export default Alphabet;