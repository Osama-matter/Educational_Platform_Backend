export interface RegisterDto {
  username: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

export interface AuthResponseDto {
  token: string;
  isSuccess: boolean;
  message: string;
}
