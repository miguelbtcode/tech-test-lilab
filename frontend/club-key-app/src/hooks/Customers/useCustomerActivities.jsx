import { useState, useEffect } from "react";

import * as customerActivityActions from "../../actions/customerActivityAction.js";

export const useCustomerActivities = (rowsPerPage = 10) => {
    const [customers, setCustomers] = useState([]);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(false);
    const [successMessage, setSuccessMessage] = useState(false);
    const [customerIdFilter, setCustomerIdFilter] = useState("");

    useEffect(() => {
        const getCustomers = async () => {
            setLoading(true);
            try {
                const response = await customerActivityActions.fetchCustomersActivityAction(page, rowsPerPage, customerIdFilter);
                setCustomers(response.data);
                setTotalPages(response.pageCount || 1);
            } catch (error) {
                console.error("Error al obtener clientes:", error.message);
            } finally {
                setLoading(false);
            }
        };

        getCustomers();
    }, [page, customerIdFilter]);

    const handleAddEntrance = (createdEntrance) => customerActivityActions.handleAddEntranceAction(createdEntrance, setSuccessMessage);
    const handleAddExit = (createdExit) => customerActivityActions.handleAddExitAction(createdExit, setSuccessMessage);
    const handleForceRefresh = (customerId) => customerActivityActions.handleForceRefresh(page, rowsPerPage, customerId, setCustomers, setPage);
    const handleFilterChange = customerActivityActions.handleFilterChangeAction(setCustomerIdFilter, setPage);
    const handleResetFilter = customerActivityActions.handlerResetFilterAction(setCustomerIdFilter, setPage);
    const handleSnackbarClose = customerActivityActions.handleSnackbarCloseAction(setSuccessMessage);

    return {
        customers,
        loading,
        page,
        totalPages,
        successMessage,
        customerIdFilter,
        handleFilterChange,
        handleResetFilter,
        handleAddEntrance,
        handleAddExit,
        handleSnackbarClose,
        handleForceRefresh
    };
};