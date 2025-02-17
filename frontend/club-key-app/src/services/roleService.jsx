import api from "../api/api.jsx";

const roleBaseApi = "/role";

/**
 * Obtiene todos los roles desde la API.
 * @returns {Promise<Array>} Lista de roles.
 */
export const getAllRoles = async () => {
    try {
        const response = await api.get(`${roleBaseApi}`);
        return response.data;
    } catch (error) {
        console.error("Error al obtener los roles:", error);
        throw new Error(error.response?.data?.message || "Error al obtener los roles.");
    }
};