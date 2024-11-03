import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-hot-toast";
import type { ApiResponse } from "../types";

const DEVELOPMENT_BASE_API_URL = "http://localhost:5000/api";
const BASE_URL = import.meta.env.VITE_API_URL || DEVELOPMENT_BASE_API_URL;

export const api = axios.create({
  baseURL: BASE_URL,
  timeout: 10_000,
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error: AxiosError) => {
    console.error("Request error:", error);
    return Promise.reject(error);
  }
);

api.interceptors.response.use(
  (response: AxiosResponse) => {
    const responseData: ApiResponse<unknown> = {
      data: response.data,
      status: response.status,
      message: response.data?.message,
    };
    return responseData as
      | AxiosResponse<unknown, unknown>
      | Promise<AxiosResponse<unknown, unknown>>;
  },

  (error: AxiosError) => {
    let message;
    if (error.response?.data) {
      message = (error.response?.data as { message: string }).message;
    } else {
      message = "An error occurred";
    }

    if (error.response?.status === 401) {
      localStorage.removeItem("token");
      window.location.href = "/";
      toast.error("Session expired. Please login again.");
    } else if (error.response?.status === 403) {
      toast.error("You do not have permission to perform this action");
    } else if (error.response?.status === 404) {
      toast.error("Resource not found");
    } else if (error.response?.status && error.response?.status >= 500) {
      toast.error("Server error. Please try again later.");
    } else {
      toast.error(message);
    }

    return Promise.reject(error);
  }
);
