import React, { createContext, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { loginUser, logoutUser, getUserProfile } from "../services/authService";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            fetchUserProfile(token);
        }
    }, []);

    const fetchUserProfile = async (token) => {
        try {
            const profile = await getUserProfile(token);
            setUser({ token, role: profile.roles[0] });
        } catch (error) {
            console.error("Error al obtener perfil:", error);
            logout();
        }
    };

    const login = async (credentials) => {
        try {
            const token = await loginUser(credentials);
            localStorage.setItem("token", token);
            await fetchUserProfile(token);
            navigate("/home");
        } catch (error) {
            console.error(error.message);
        }
    };

    const logout = () => {
        localStorage.removeItem("token");
        setUser(null);
        navigate("/login");
    };

    return (
        <AuthContext.Provider value={{ user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};