import Router from "next/router";

export function getUserAccessToken() {
    return localStorage.getItem('accessToken');
}
export function getUserRefreshToken() {
    return localStorage.getItem('refreshToken');
}
export function getUserRole() {
    return parseJwt(getUserAccessToken())['role'];
}
export function getUserEmail() {
    return parseJwt(getUserAccessToken())['email'];
}
export function getUserUsername() {
    return parseJwt(getUserAccessToken())['unique_name'] || 'anonymous';
}
export function getUserName() {
    return parseJwt(getUserAccessToken())['given_name'] || 'anonymous';
}

function parseJwt(token) {
    let jsonPayload = {}
    try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
    } catch (e) {
        logOut();
    }
    return JSON.parse(jsonPayload);
}

export function logOut() {
    localStorage.clear()
    Router.push('/login');
}

export function getQueryVariable(variable) {
    let query = window.location.search.substring(1);
    let vars = query.split("&");
    for (let i = 0; i < vars.length; i++) {
        let pair = vars[i].split("=");
        if (pair[0] == variable) { return pair[1]; }
    }
    return (false);
}

export function roleIdToName(roles, roleId) {
    for (let role of roles) {
        if (role.id == roleId) {
            return role.name
        }
    }
}

export function getMonthNameFromIndex(index) {
    const month = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    return month[index];
}

export function getMonthShortNameFromIndex(index) {
    const month = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    return month[index];
}

export function getWeekDayNameFromIndex(index) {
    const month = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    return month[index];
}

export function getWeekDayShortNameFromIndex(index) {
    const month = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
    return month[index];
}

export function datesEqual(firstDay, secondDay) {
    return firstDay.getFullYear() == secondDay.getFullYear() && firstDay.getMonth() == secondDay.getMonth() && firstDay.getDate() == secondDay.getDate();
}

export function datesCompare(first, second) {
    return first > second ? 1 : first == second ? 0 : -1;
}