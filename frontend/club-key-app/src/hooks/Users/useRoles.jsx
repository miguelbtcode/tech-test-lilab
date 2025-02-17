import { useState, useEffect } from "react";
import { getAllRoles } from "../../services/roleService.jsx";

export const useRoles = () => {
    const [roles, setRoles] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchRoles = async () => {
            setLoading(true);
            try {
                const rolesData = await getAllRoles();
                setRoles(rolesData);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchRoles();
    }, []);

    return { roles, loading, error };
};