import { useState, useEffect } from "react";
import * as customerActions from "../../actions/customerAction";

export const useCustomers = (rowsPerPage = 10) => {
    const [customers, setCustomers] = useState([]);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(false);
    const [openModal, setOpenModal] = useState(false);
    const [successMessage, setSuccessMessage] = useState(false);
    const [typeFilter, setTypeFilter] = useState("");

    useEffect(() => {
        const getCustomers = async () => {
            setLoading(true);
            try {
                const response = await customerActions.fetchCustomersAction(page, rowsPerPage, typeFilter);
                setCustomers(response.data);
                setTotalPages(response.pageCount || 1);
            } catch (error) {
                console.error("Error al obtener clientes:", error.message);
            } finally {
                setLoading(false);
            }
        };

        getCustomers();
    }, [page, typeFilter]);

    const handleCreateCustomer = (createdCustomer) => customerActions.handleCreateUserAction(createdCustomer, setCustomers, setSuccessMessage);

    const handleChangePage = customerActions.handleChangePageAction(setPage);
    const handleOpenModal = customerActions.handleOpenModalAction(setOpenModal);
    const handleCloseModal = customerActions.handleCloseModalAction(setOpenModal);
    const handleSnackbarClose = customerActions.handleSnackbarCloseAction(setSuccessMessage, handleCloseModal);
    const handleFilterChange = customerActions.handleFilterChangeAction(setTypeFilter, setPage);
    const handlerResetFilter = customerActions.handlerResetFilterAction(setTypeFilter, setPage);

    return {
        customers,
        loading,
        page,
        totalPages,
        openModal,
        successMessage,
        typeFilter,
        handleCreateCustomer,
        handleChangePage,
        handleOpenModal,
        handleCloseModal,
        handleSnackbarClose,
        handleFilterChange,
        handlerResetFilter,
    };
};