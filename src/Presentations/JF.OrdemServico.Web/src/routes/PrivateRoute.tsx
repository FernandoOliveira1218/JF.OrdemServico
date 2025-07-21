import { Navigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import type { ReactNode } from 'react';

type Props = {
  children: ReactNode;
};

export default function PrivateRoute({ children }: Props) {
  const { isAuthenticated } = useAuth();
  
  return isAuthenticated ? children : <Navigate to="/" />;
}
