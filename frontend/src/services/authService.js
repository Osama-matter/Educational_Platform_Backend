import axios from 'axios';

const API_BASE = '/api/Account';

export const login = async (email, password) => {
  const response = await axios.post(`${API_BASE}/login`, { email, password });
  return response.data;
};

export const register = async (user) => {
  const response = await axios.post(`${API_BASE}/register`, user);
  return response.data;
};
