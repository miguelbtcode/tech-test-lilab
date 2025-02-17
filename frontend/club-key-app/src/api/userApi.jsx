import api from "./api.jsx";
import { handleApiError } from "../utils/handleApiError.js";

const userBaseApi = "/user";

export const paginationUser = async (pageIndex = 1, pageSize = 10, roleId = "") => {
    try {
        const { data } = await api.get(`${userBaseApi}/pagination`, {
            params: {
                PageIndex: pageIndex,
                PageSize: pageSize,
                Role: roleId,
            },
        });
        return data;
    } catch (error) {
        throw new Error(handleApiError(error, "Error en la paginación de usuarios"));
    }
};

export const createUser = async (createdUser) => {
    try {
        const response = await api.post(`${userBaseApi}/register`, createdUser);
        return response.data;
    } catch (error) {
        throw new Error(handleApiError(error, "Error en la creación de usuario"));
    }
};

export const updateUser = async (updatedUser) => {
    try {
        const response = await api.put(`${userBaseApi}/Update`, updatedUser);
        return response.data;
    } catch (error) {
        throw new Error(handleApiError(error, "Error en la actualización de usuario"));
    }
};

export const getUserById = async (id) => {
    try {
        const response = await api.get(`${userBaseApi}/${id}`);
        return response.data;
    } catch (error) {
        throw new Error(handleApiError(error, "Error al obtener el usuario"));
    }
};

export const deleteUser = async (id) => {
    try {
        const response = await api.delete(`${userBaseApi}/Delete`, {
            params: { id },
        });
        return response.data;
    } catch (error) {
        throw new Error(handleApiError(error, "Error al eliminar el usuario"));
    }
};