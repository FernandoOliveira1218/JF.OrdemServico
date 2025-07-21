const BASE_URL = import.meta.env.VITE_API_BASE;
const VERSION = import.meta.env.VITE_API_VERSION;

export const API_URL = `${BASE_URL}/${VERSION}`;

/**
 * Função para obter a URL da API com uma versão específica,
 * útil para chamadas a endpoints que estejam em versões diferentes.
 */

export function apiVersion(version: string): string {
  return `${BASE_URL}/api/${version}`;
}

export function apiUrl(endpoint: string): string {
  return `${API_URL}/${endpoint}`;
}
