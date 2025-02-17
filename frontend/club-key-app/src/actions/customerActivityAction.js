import {
    fetchCustomersActivity,
    createNewEntrance,
    createNewExit
} from "../services/CustomerService.jsx";

export const fetchCustomersActivityAction = async (page, rowsPerPage, customerId) =>  {
    return await fetchCustomersActivity(page, rowsPerPage, customerId);
}

export const handleForceRefresh = async (page, rowsPerPage, customerId, setCustomers, setPage) => {
    const customers = await fetchCustomersActivity(page, rowsPerPage, customerId);
    setCustomers(customers.data);
    setPage(1);
    return customers;
}

export const handleFilterChangeAction = (setCustomerIdFilter, setPage) => (event) => {
    setCustomerIdFilter(event.target.value);
    setPage(1);
};

export const handlerResetFilterAction = (setCustomerIdFilter, setPage) => () => {
    setCustomerIdFilter("");
    setPage(1);
};

export const handleAddEntranceAction = async (createdEntrance, setSuccessMessage) => {
    try{
        const response = await createNewEntrance(createdEntrance);
        setSuccessMessage(true);
        return response;
    }catch (error) {
        console.warn("Error al crear la entrada:", error);
    }
}

export const handleAddExitAction = async (createdExit, setSuccessMessage) => {
    try{
        const response = await createNewExit(createdExit);
        setSuccessMessage(true);
        return response;
    }catch (error) {
        console.warn("Error al crear la salida:", error);
    }
}

export const handleSnackbarCloseAction = (setSuccessMessage) => () => {
    setSuccessMessage(false);
};