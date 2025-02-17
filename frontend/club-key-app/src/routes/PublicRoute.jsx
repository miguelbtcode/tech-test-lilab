import { useEffect, useState } from "react";
import { Navigate, Outlet } from "react-router-dom";
import api from "../api/api.jsx";

const PublicRoute = () => {
    const [isValid, setIsValid] = useState(null);
    const token = localStorage.getItem("token");

    useEffect(() => {
        const checkToken = async () => {
            if (!token) {
                setIsValid(false);
                return;
            }

            try {
                const response = await api.get("/account/profile", {
                    headers: { Authorization: `Bearer ${token}` },
                });

                if (response.status === 200) {
                    setIsValid(true);
                } else {
                    setIsValid(false);
                    localStorage.removeItem("token");
                }
            } catch (error) {
                setIsValid(false);
                console.error(error);
                localStorage.removeItem("token");
            }
        };

        checkToken();
    }, [token]);

    if (isValid === null) return <p>Cargando...</p>;

    return isValid ? <Navigate to="/home" /> : <Outlet />;
};

export default PublicRoute;