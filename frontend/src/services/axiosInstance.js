import axios from 'axios';
import { getToken, removeToken } from './jwtService';

const instance = axios.create();

instance.interceptors.request.use(
  (config) => {
    const token = getToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

instance.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response) {
      if (error.response.status === 401) {
        removeToken();
        window.location.href = '/login';
      } else if (error.response.status === 403) {
        window.location.href = '/access-denied';
      }
    }
    return Promise.reject(error);
  }
);

export default instance;
