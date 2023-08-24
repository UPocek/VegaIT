import { getUserAccessToken, logOut } from '@/helper/helper';
import '@/styles/globals.css'
import axios from 'axios';

export const baseUrl = 'https://localhost:7190';

axios.interceptors.request.use(
  config => {
    const token = getUserAccessToken();
    if (token && !config.headers['skip']) {
      config.headers['Authorization'] = 'Bearer ' + token
    }
    // config.headers['Content-Type'] = 'application/json';
    return config
  },
  error => {
    // eslint-disable-next-line no-undef
    Promise.reject(error)
  }
)

axios.interceptors.response.use(
  response => {
    return response;
  },
  async function (error) {
    if (error.response && error.response.status === 401) {
      logOut();
      // eslint-disable-next-line no-undef
      return Promise.reject(error)
    }
  }
)

export default function App({ Component, pageProps }) {
  return <Component {...pageProps} />
}
