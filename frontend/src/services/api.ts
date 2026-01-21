import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7228/api', // Update with your backend URL
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;
