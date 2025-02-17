import api from "./api.jsx";
import { handleApiError } from "../utils/handleApiError.js";
import useErrorSnackbarStore from "../stores/useErrorSnackbarStore.jsx";

const customerBaseApi = "/customer";

const handleError = (error, message) => {
    const { showError } = useErrorSnackbarStore.getState();
    const errorMessage = handleApiError(error, message);
    showError(errorMessage);
    throw new Error(errorMessage);
};

export const paginationCustomer = async (pageIndex = 1, pageSize = 10, type = null) => {
    try {
        const { data } = await api.get(`${customerBaseApi}/pagination`, {
            params: {
                PageIndex: pageIndex,
                PageSize: pageSize,
                Type: type,
            },
        });
        return data;
    } catch (error) {
        handleError(error, "Error en la paginación de usuarios");
    }
};

export const createCustomer = async (createdCustomer) => {
    try {
        const response = await api.post(`${customerBaseApi}/register`, createdCustomer);
        return response.data;
    } catch (error) {
        handleError(error, "Error en la creación de cliente");
    }
};

export const getCustomersActivity = async (pageIndex = 1, pageSize = 10, customerId = "", sort = "default") => {
    try {

        const response = await api.get(`${customerBaseApi}/activity`, {
            params: {
                CustomerId: customerId,
                PageIndex: pageIndex,
                PageSize: pageSize,
                Sort: sort
            },
        });
        return response.data;
    } catch (error) {
        throw new Error(handleApiError(error, "Error al listar las actividades del cliente"));
    }
};

export const createEntrance = async (createdEntrance) => {
    try {
        const response = await api.post(`${customerBaseApi}/entrance/register`, {
            customerId: createdEntrance.customerId,
            entranceTime: createdEntrance.entryDate
        });
        return response.data;
    } catch (error) {
        handleError(error, "Error en la creación de entrance");
    }
};

export const createExit = async (createdExit) => {
    try {
        const response = await api.post(`${customerBaseApi}/exit/register`, {
            customerId: createdExit.customerId,
            exitTime: createdExit.entryDate
        });
        return response.data;
    } catch (error) {
        handleError(error, "Error en la creación de exit");
    }
};