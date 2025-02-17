import { useState, useEffect } from "react";
import * as userActions from "../../actions/userActions";

export const useUsers = (rowsPerPage = 10) => {
    const [users, setUsers] = useState([]);
    const [page, setPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(false);
    const [openModal, setOpenModal] = useState(false);
    const [selectedUser, setSelectedUser] = useState(null);
    const [successMessage, setSuccessMessage] = useState(false);
    const [confirmDelete, setConfirmDelete] = useState(false);
    const [roleFilter, setRoleFilter] = useState("");

    useEffect(() => {
        const getUsers = async () => {
            setLoading(true);
            try {
                const response = await userActions.fetchUsersAction(page, rowsPerPage, roleFilter);
                setUsers(response.data);
                setTotalPages(response.pageCount || 1);
            } catch (error) {
                console.error("Error al obtener usuarios:", error.message);
            } finally {
                setLoading(false);
            }
        };

        getUsers();
    }, [page, roleFilter]);

    const handleCreateUser = (createdUser) => userActions.handleCreateUserAction(createdUser, setUsers, setSuccessMessage);
    const handleUpdateUser = (updatedUser) => userActions.handleUpdateUserAction(updatedUser, setUsers, setSuccessMessage);
    //const handleDeleteUser = () => userActions.handleDeleteUserAction(selectedUser, page, rowsPerPage, roleFilter, setUsers, setPage, setConfirmDelete, setSelectedUser);

    const handleDeleteUser = async () => {
        try {
            setLoading(true);

            setTimeout(async () => {
                await userActions.handleDeleteUserAction(selectedUser, page, rowsPerPage, roleFilter, setUsers, setPage, setConfirmDelete, setSelectedUser);

                setLoading(false);
            }, 1000);
        } catch (error) {
            console.error("Error al eliminar usuario:", error);
            setLoading(false);
        }
    };

    const handleChangePage = userActions.handleChangePageAction(setPage);
    const handleOpenModal = userActions.handleOpenModalAction(setSelectedUser, setOpenModal);
    const handleCloseModal = userActions.handleCloseModalAction(setOpenModal, setSelectedUser);
    const handleSnackbarClose = userActions.handleSnackbarCloseAction(setSuccessMessage, handleCloseModal);
    const handleConfirmDelete = userActions.handleConfirmDeleteAction(setSelectedUser, setConfirmDelete);
    const handleCancelDelete = userActions.handleCancelDeleteAction(setConfirmDelete, setSelectedUser);
    const handleFilterChange = userActions.handleFilterChangeAction(setRoleFilter, setPage);
    const handlerResetFilter = userActions.handlerResetFilterAction(setRoleFilter, setPage);

    return {
        users,
        loading,
        page,
        totalPages,
        openModal,
        selectedUser,
        successMessage,
        confirmDelete,
        roleFilter,
        handleCreateUser,
        handleUpdateUser,
        handleChangePage,
        handleOpenModal,
        handleCloseModal,
        handleSnackbarClose,
        handleConfirmDelete,
        handleCancelDelete,
        handleDeleteUser,
        handleFilterChange,
        handlerResetFilter,
    };
};