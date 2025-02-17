export const handleApiError = (error, defaultMessage) => {
    return error.response.data.name || defaultMessage;
};