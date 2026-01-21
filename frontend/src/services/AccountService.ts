import api from './api';
import { RegisterDto, LoginDto, AuthResponseDto } from '../types/account';

export const AccountService = {
  register: async (data: RegisterDto): Promise<AuthResponseDto> => {
    const response = await api.post<AuthResponseDto>('/Account/register', data);
    return response.data;
  },

  login: async (data: LoginDto): Promise<AuthResponseDto> => {
    const response = await api.post<AuthResponseDto>('/Account/login', data);
    return response.data;
  },

  registerAdmin: async (data: RegisterDto): Promise<AuthResponseDto> => {
    const response = await api.post<AuthResponseDto>('/Account/register-admin', data);
    return response.data;
  },
};
