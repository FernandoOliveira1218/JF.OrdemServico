import { useEffect, useState } from 'react';
import { ChamadoService, type Chamado } from '../../services/ChamadoService';

export default function ChamadoPage() {
  const [filtros, setFiltros] = useState({
    clienteId: '',
    status: '',
    prioridade: '',
    dataInicio: '',
    dataFim: '',
  });

  const [chamados, setChamados] = useState<Chamado[]>([]);
  const [loading, setLoading] = useState(false);

  const carregarChamados = async () => {
    setLoading(true);
    try {
      const resultado = await ChamadoService.listar(filtros);
      setChamados(resultado);
    } catch (err) {
      console.error('Erro ao buscar chamados:', err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    carregarChamados();
  }, []);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    setFiltros({ ...filtros, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    carregarChamados();
  };

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Chamados</h1>

      <form onSubmit={handleSubmit} className="grid grid-cols-1 md:grid-cols-4 gap-4 mb-6">
        <input
          type="text"
          name="clienteId"
          value={filtros.clienteId}
          onChange={handleInputChange}
          placeholder="ID do Cliente"
          className="border p-2 rounded"
        />

        <select
          name="status"
          value={filtros.status}
          onChange={handleInputChange}
          className="border p-2 rounded"
        >
          <option value="">Todos os status</option>
          <option value="Aberto">Aberto</option>
          <option value="Finalizado">Finalizado</option>
        </select>

        <input
          type="date"
          name="dataInicio"
          value={filtros.dataInicio}
          onChange={handleInputChange}
          className="border p-2 rounded"
        />

        <input
          type="date"
          name="dataFim"
          value={filtros.dataFim}
          onChange={handleInputChange}
          className="border p-2 rounded"
        />

        <button
          type="submit"
          className="col-span-1 md:col-span-4 bg-blue-600 text-white rounded px-4 py-2 hover:bg-blue-700"
        >
          Filtrar
        </button>
      </form>

      {loading ? (
        <p>Carregando...</p>
      ) : (
        <table className="w-full border text-sm">
          <thead>
            <tr className="bg-gray-100">
              <th className="p-2 text-left">Título</th>
              <th className="p-2 text-left">Cliente</th>
              <th className="p-2 text-left">Status</th>
              <th className="p-2 text-left">Data de Abertura</th>
              <th className="p-2">Ações</th>
            </tr>
          </thead>
          <tbody>
            {chamados.map((chamado) => (
              <tr key={chamado.id} className="border-t">
                <td className="p-2">{chamado.titulo}</td>
                <td className="p-2">{chamado.clienteNome}</td>
                <td className="p-2">{chamado.status}</td>
                <td className="p-2">
                  {new Date(chamado.dataAbertura).toLocaleDateString()}
                </td>
                <td className="p-2">
                  <button className="text-blue-600 hover:underline mr-3">Editar</button>
                  <button className="text-green-600 hover:underline">Finalizar</button>
                </td>
              </tr>
            ))}

            {chamados.length === 0 && (
              <tr>
                <td colSpan={5} className="text-center py-4">
                  Nenhum chamado encontrado.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      )}
    </div>
  );
}
