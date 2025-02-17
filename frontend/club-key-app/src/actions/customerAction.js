import {fetchCustomers, createNewCustomer} from "../services/CustomerService.jsx";

export const fetchCustomersAction = async (page, rowsPerPage, typeFilter) => fetchCustomers(page, rowsPerPage, typeFilter);

export const handleCreateUserAction = async (createdCustomer, setCustomers, setSuccessMessage) => {
    try {
        const newCustomer = await createNewCustomer(createdCustomer);
        setCustomers((prevCustomers) => [...prevCustomers, newCustomer]);
        setSuccessMessage(true);
    } catch (error) {
        console.warn("Error al crear cliente:", error);
    }
};

export const handleChangePageAction = (setPage) => (event, newPage) => {
    setPage(newPage);
};

export const handleOpenModalAction = (setOpenModal) => () => {
    setOpenModal(true);
};

export const handleCloseModalAction = (setOpenModal) => () => {
    setOpenModal(false);
};

export const handleSnackbarCloseAction = (setSuccessMessage, handleCloseModal) => () => {
    setSuccessMessage(false);
    handleCloseModal();
};

export const handleFilterChangeAction = (setTypeFilter, setPage) => (event) => {
    setTypeFilter(event.target.value);
    setPage(1);
};

export const handlerResetFilterAction = (setTypeFilter, setPage) => () => {
    setTypeFilter("");
    setPage(1);
};