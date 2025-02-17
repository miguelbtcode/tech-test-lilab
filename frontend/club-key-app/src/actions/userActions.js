import { createNewUser, updateExistingUser, deleteExistingUser, fetchUsers } from "../services/userService.jsx";

export const fetchUsersAction = async (page, rowsPerPage, roleFilter) => fetchUsers(page, rowsPerPage, roleFilter);

export const handleCreateUserAction = async (createdUser, setUsers, setSuccessMessage) => {
    try {
        const newUser = await createNewUser(createdUser);
        setUsers((prevUsers) => [...prevUsers, newUser]);
        setSuccessMessage(true);
    } catch (error) {
        console.error("Error al crear usuario:", error);
    }
};

export const handleUpdateUserAction = async (updatedUser, setUsers, setSuccessMessage) => {
    try {
        await updateExistingUser(updatedUser);
        setUsers((prevUsers) =>
            prevUsers.map((user) => (user.id === updatedUser.id ? { ...user, ...updatedUser } : user))
        );
        setSuccessMessage(true);
    } catch (error) {
        console.error("Error al actualizar usuario:", error);
    }
};

export const handleDeleteUserAction = async (selectedUser, page, rowsPerPage, roleFilter, setUsers, setPage, setConfirmDelete, setSelectedUser) => {
    try {
        await deleteExistingUser(selectedUser);
        const updatedUsers = await fetchUsers(page, rowsPerPage, roleFilter);
        setUsers(updatedUsers.data);
        setPage(1);
        setConfirmDelete(false);
        setSelectedUser(null);
    } catch (error) {
        console.error("Error al eliminar usuario:", error);
    }
};

export const handleChangePageAction = (setPage) => (event, newPage) => {
    setPage(newPage);
};

export const handleOpenModalAction = (setSelectedUser, setOpenModal) => (userId) => {
    setSelectedUser(userId);
    setOpenModal(true);
};

export const handleCloseModalAction = (setOpenModal, setSelectedUser) => () => {
    setOpenModal(false);
    setSelectedUser(null);
};

export const handleSnackbarCloseAction = (setSuccessMessage, handleCloseModal) => () => {
    setSuccessMessage(false);
    handleCloseModal();
};

export const handleConfirmDeleteAction = (setSelectedUser, setConfirmDelete) => (userId) => {
    setSelectedUser(userId);
    setConfirmDelete(true);
};

export const handleCancelDeleteAction = (setConfirmDelete, setSelectedUser) => () => {
    setConfirmDelete(false);
    setSelectedUser(null);
};

export const handleFilterChangeAction = (setRoleFilter, setPage) => (event) => {
    setRoleFilter(event.target.value);
    setPage(1);
};

export const handlerResetFilterAction = (setRoleFilter, setPage) => () => {
    setRoleFilter("");
    setPage(1);
};