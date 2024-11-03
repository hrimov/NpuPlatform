import { api } from "./api";
import type {
  AuthResponse,
  LoginCredentials,
  RegisterCredentials,
  User,
} from "../types/auth";
import { ApiResponse } from "../types";

export const getCurrentUser = async (): Promise<User | null> => {
  try {
    const response = await api.get<ApiResponse<User>>("/auth/me");
    const userData = response.data as unknown as User;
    return userData;
  } catch (error) {
    console.error(error);
    return null;
  }
};

export const login = async (credentials: LoginCredentials) => {
  const response = await api.post<AuthResponse>("/auth/login", credentials);
  if (response.data?.token) {
    const token = response.data.token;
    localStorage.setItem("token", token);
    const user = await getCurrentUser();
    if (user) {
      localStorage.setItem("user", JSON.stringify(user));
      return { token, user };
    }
  }
  throw new Error("Invalid response format");
};

export const register = async (credentials: RegisterCredentials) => {
  const response = await api.post<AuthResponse>("/auth/register", credentials);
  if (response.data?.username) {
    return response.data;
  }
  throw new Error("Invalid response format");
};

export const logout = () => {
  localStorage.removeItem("token");
  localStorage.removeItem("user");
  window.location.href = "/login";
};
