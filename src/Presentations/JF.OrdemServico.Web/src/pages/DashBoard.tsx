import { useAuth } from '../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';

export default function Dashboard() {
    const { logout } = useAuth();
    const navigate = useNavigate();

    return (
        <div className="min-h-screen bg-gray-100 p-6">
            <div className="max-w-4xl mx-auto bg-white p-6 rounded shadow">
                <h1 className="text-2xl font-bold mb-4">Bem-vindo ao Painel</h1>

                <div className="flex gap-4 mb-6">
                    <button onClick={() => navigate('/chamados')} className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"          >
                        Chamados
                    </button>

                    <button onClick={() => navigate('/clientes')} className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"          >
                        Clientes
                    </button>
                </div>

                <button onClick={logout} className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700"        >
                    Sair
                </button>
            </div>
        </div>
    );
}
