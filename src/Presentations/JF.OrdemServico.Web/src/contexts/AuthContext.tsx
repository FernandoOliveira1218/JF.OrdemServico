import { createContext, useState, useContext, useCallback } from 'react';
import type { ReactNode } from 'react';
import { API_URL } from '../config/api';

type AuthContextData = {
  token: string | null;
  login: (login: string, senha: string) => Promise<void>;
  logout: () => void;
  isAuthenticated: boolean;
};

const AuthContext = createContext<AuthContextData>({} as AuthContextData);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [token, setToken] = useState<string | null>(localStorage.getItem('token'));

  const login = async (usuario: string, senha: string) => {
    const response = await fetch(`${API_URL}/Auth/Login`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ login: usuario, senha }),
    });

    if (!response.ok) {
      throw new Error('Usuário ou senha inválidos');
    }

    const dataJson = await response.json();
    localStorage.setItem('token', dataJson.data.token);
    setToken(dataJson.data.token);
  };

  const logout = useCallback(() => {
    localStorage.removeItem('token');
    setToken(null);
  }, []);

  return (
    <AuthContext.Provider value={{ token, login, logout, isAuthenticated: !!token }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
