import {createUser, deleteUser, paginationUser, updateUser, getUserById } from "../api/userApi.jsx";

export const fetchUsers = async (page, rowsPerPage, roleFilter) => {
    return await paginationUser(page, rowsPerPage, roleFilter);
};

export const createNewUser = async (createdUser) => {
    return await createUser(createdUser);
};

export const updateExistingUser = async (updatedUser) => {
    await updateUser(updatedUser);
    return updatedUser;
};

export const deleteExistingUser = async (userId) => {
    await deleteUser(userId);
};

export const fetchUserById = async (userId) => {
    return await getUserById(userId);
}
