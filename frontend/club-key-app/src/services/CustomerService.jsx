import {createCustomer, getCustomersActivity, paginationCustomer, createEntrance, createExit } from "../api/customerApi.jsx";

export const fetchCustomers = async (page, rowsPerPage, typeFilter) => {
    return await paginationCustomer(page, rowsPerPage, typeFilter);
};

export const createNewCustomer = async (createdCustomer) => {
    return await createCustomer(createdCustomer);
};

export const fetchCustomersActivity = async (page, rowsPerPage, customerId) => {
    return await getCustomersActivity(page, rowsPerPage, customerId);
}

export const createNewEntrance = async (createdEntrance) => {
    return await createEntrance(createdEntrance);
};

export const createNewExit = async (createdExit) => {
    return await createExit(createdExit);
};