import { BrowserRouter, Routes, Route } from "react-router-dom"
import LoginPage from "../pages/Login"
import Dashboard from "../pages/DashBoard"
import ChamadosPage from '../pages/Chamados/ChamadoIndex.tsx';
import ClientesPage from '../pages/Clientes';
import PrivateRoute from "./PrivateRoute.tsx"

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/dashboard" element={
          <PrivateRoute>
            <Dashboard />
          </PrivateRoute>
        } />
        <Route path="/chamados" element={
          <PrivateRoute>
            <ChamadosPage />
          </PrivateRoute>
        }
        />
        <Route path="/clientes" element={
          <PrivateRoute>
            <ClientesPage />
          </PrivateRoute>
        }
        />
      </Routes>
    </BrowserRouter>
  )
}
