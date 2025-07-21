import { API_URL } from '../config/api';

type ApiResponse<T> = {
  success: boolean;
  message: string;
  data: T;
  statusCode: number;
};

let logoutCallback: (() => void) | null = null;

export function setLogoutCallback(callback: () => void) {
  logoutCallback = callback;
}

async function handleResponse<T>(response: Response): Promise<ApiResponse<T>> {
  if (response.status === 401) {
    if (logoutCallback) logoutCallback();
    throw new Error('Sessão expirada. Faça login novamente.');
  }

  const result: ApiResponse<T> = await response.json();

  if (!result.success) {
    throw new Error(result.message || 'Erro ao processar requisição');
  }

  return result;
}

async function request<T>(
  method: 'GET' | 'POST' | 'PUT' | 'DELETE',
  path: string,
  body?: any
): Promise<ApiResponse<T>> {
  const token = localStorage.getItem('token');
  if (!token) throw new Error('Token não encontrado. Faça login novamente.');

  const headers: HeadersInit = {
    'Content-Type': 'application/json',
    Authorization: `Bearer ${token}`,
  };

  const response = await fetch(`${API_URL}${path}`, {
    method,
    headers,
    body: body ? JSON.stringify(body) : undefined,
  });

  return handleResponse<T>(response);
}

export const apiClient = {
  get: <T>(path: string) => request<T>('GET', path),
  post: <T>(path: string, body: any) => request<T>('POST', path, body),
  put: <T>(path: string, body: any) => request<T>('PUT', path, body),
  del: <T>(path: string) => request<T>('DELETE', path),
};
