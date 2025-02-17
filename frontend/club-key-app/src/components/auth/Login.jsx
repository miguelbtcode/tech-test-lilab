import React, { useState } from "react";
import { useAuth } from "../../context/useAuth";
import { TextField, Button, Container, Typography, Box } from "@mui/material";

const Login = () => {
    const { login } = useAuth();
    const [credentials, setCredentials] = useState({ email: "", password: "" });

    const handleChange = (e) => {
        setCredentials({ ...credentials, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        login(credentials);
    };

    return (
        <Container maxWidth="xs">
            <Box sx={{ display: "flex", flexDirection: "column", alignItems: "center", mt: 8 }}>
                <Typography variant="h4" gutterBottom>
                    Iniciar Sesión
                </Typography>
                <form onSubmit={handleSubmit} style={{ width: "100%" }}>
                    <TextField
                        fullWidth
                        label="Email"
                        name="email"
                        variant="outlined"
                        margin="normal"
                        onChange={handleChange}
                    />
                    <TextField
                        fullWidth
                        label="Contraseña"
                        name="password"
                        type="password"
                        variant="outlined"
                        margin="normal"
                        onChange={handleChange}
                    />
                    <Button fullWidth variant="contained" color="primary" type="submit" sx={{ mt: 2 }}>
                        Ingresar
                    </Button>
                </form>
            </Box>
        </Container>
    );
};

export default Login;