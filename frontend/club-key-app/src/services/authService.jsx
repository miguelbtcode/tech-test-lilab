import api from "../api/api.jsx";

const authBaseApi = "/account";

export const loginUser = async (credentials) => {
    try {
        const { data } = await api.post(`${authBaseApi}/login`, credentials);
        return data.token;
    } catch (error) {
        throw new Error(error.response?.data?.message || "Error de autenticación");
    }
};

export const logoutUser = () => {
    sessionStorage.removeItem("token");
};


export const getUserProfile = async (token) => {
    try {
        const response = await api.get("/account/profile", {
            headers: { Authorization: `Bearer ${token}` },
        });
        return response.data;
    } catch (error) {
        console.error("Error al obtener el perfil del usuario:", error);
        throw error;
    }
};