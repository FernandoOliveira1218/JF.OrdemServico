import { apiClient } from './apiClient';

export type Chamado = {
  id: string;
  titulo: string;
  descricao: string;
  status: string;
  clienteNome: string;
  dataAbertura: string;
};

export const ChamadoService = {
  async listar(filtros: any = {}): Promise<Chamado[]> {
    const queryParams = new URLSearchParams();

    Object.entries(filtros).forEach(([key, value]) => {
      if (value !== undefined && value !== null && value !== '') {
        const stringValue = typeof value === 'object' ? JSON.stringify(value) : value.toString();
        queryParams.append(key, stringValue);
      }
    });

    const response = await apiClient.get<Chamado[]>(`/Chamado?${queryParams.toString()}`);
    return response.data;
  },

  async buscarPorId(id: string): Promise<Chamado> {
    const response = await apiClient.get<Chamado>(`/Chamado/${id}`);
    return response.data;
  },

  async criar(data: Partial<Chamado>): Promise<Chamado> {
    const response = await apiClient.post<Chamado>(`/Chamado`, data);
    return response.data;
  },

  async atualizar(id: string, data: Partial<Chamado>): Promise<Chamado> {
    const response = await apiClient.put<Chamado>(`/Chamado/${id}`, data);
    return response.data;
  },

  async finalizar(id: string): Promise<void> {
    await apiClient.post<void>(`/Chamado/${id}/finalizar`, {});
  },

  async deletar(id: string): Promise<void> {
    await apiClient.del<void>(`/Chamado/${id}`);
  },
};