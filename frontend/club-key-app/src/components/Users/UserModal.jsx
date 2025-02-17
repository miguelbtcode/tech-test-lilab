import { useState, useEffect } from "react";
import PropTypes from "prop-types";
import {
    Dialog, DialogTitle, DialogContent, DialogActions, TextField, Button,
    InputAdornment, IconButton, Checkbox, FormControlLabel, CircularProgress,
    MenuItem, Snackbar
} from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { fetchUserById } from "../../services/userService.jsx";

const UserModal = ({ open, onClose, userId, onCreate, onUpdate, successMessage, handleSnackbarClose }) => {
    const isNewUser = !userId;

    const [formData, setFormData] = useState({
        id: "", name: "", lastName: "", userName: "",
        phoneNumber: "", email: "", role: "", password: "",
    });

    const [loading, setLoading] = useState(false);
    const [showPassword, setShowPassword] = useState(false);
    const [isPasswordEditable, setIsPasswordEditable] = useState(false);
    const [errors, setErrors] = useState({});

    useEffect(() => {
        if (!isNewUser && open) {
            setLoading(true);
            fetchUserById(userId)
                .then(user => {
                    setFormData({
                        id: user.id || userId,
                        name: user.name || "",
                        lastName: user.lastName || "",
                        userName: user.userName || "",
                        phoneNumber: user.phoneNumber || "",
                        email: user.email?.toLowerCase() || "",
                        role: user.roles[0] || "",
                        password: "*****",
                    });
                    setIsPasswordEditable(false);
                })
                .catch(error => console.error("Error al obtener usuario:", error))
                .finally(() => setLoading(false));
        } else if (isNewUser) {
            setFormData({
                id: "", name: "", lastName: "", userName: "",
                phoneNumber: "", email: "", role: "", password: "",
            });

            setIsPasswordEditable(true);
        }
    }, [userId, open, isNewUser]);


    const validateForm = () => {
        let newErrors = {};

        if (!formData.name.trim()) newErrors.name = "El nombre es requerido";
        if (!formData.lastName.trim()) newErrors.lastName = "El apellido es requerido";
        if (!formData.userName.trim()) newErrors.userName = "El usuario es requerido";

        if (!formData.phoneNumber) {
            newErrors.phoneNumber = "El teléfono es requerido";
        } else if (formData.phoneNumber.length !== 9) {
            newErrors.phoneNumber = "Debe tener exactamente 9 dígitos";
        }

        if (!formData.email.trim() || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
            newErrors.email = "Ingrese un correo válido";
        }

        if (isNewUser && !formData.password) {
            newErrors.password = "La contraseña es requerida";
        }else if (isPasswordEditable && !/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?":{}|<>]).{10,}$/.test(formData.password)) {
            newErrors.password = "La contraseña debe tener al menos 10 caracteres, una mayúscula, una minúscula, un número y un símbolo.";
        }

        if (!formData.role) {
            newErrors.role = "El rol es requerido";
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSave = () => {
        if (!validateForm()) return;

        const updatedFormData = {
            ...formData,
            password: formData.password === "*****" || formData.password === "" ? null : formData.password,
        };

        (!isNewUser ? onUpdate : onCreate)(updatedFormData);
    };

    const toggleShowPassword = () => {
        setShowPassword(!showPassword);
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>{!isNewUser ? "Editar Usuario" : "Crear Usuario"}</DialogTitle>
            <DialogContent>
                {loading ? (
                    <CircularProgress color="primary" sx={{ display: "block", margin: "20px auto" }} />
                ) : (
                    <>
                        {[
                            { label: "Nombre", name: "name", type: "text" },
                            { label: "Apellido", name: "lastName", type: "text" },
                            { label: "Usuario", name: "userName", type: "text" },
                        ].map(({ label, name, type }) => (
                            <TextField
                                key={name}
                                fullWidth
                                label={label}
                                variant="outlined"
                                margin="normal"
                                name={name}
                                type={type}
                                value={formData[name]}
                                onChange={handleChange}
                                error={!!errors[name]}
                                helperText={errors[name]}
                            />
                        ))}

                        {/* Teléfono */}
                        <TextField
                            fullWidth label="N. Teléfono" variant="outlined" margin="normal"
                            name="phoneNumber" type="text"
                            value={formData.phoneNumber || ""}
                            onChange={(e) => {
                                const value = e.target.value;
                                if (/^\d*$/.test(value) && value.length <= 9) {
                                    setFormData({ ...formData, phoneNumber: value });
                                }
                            }}
                            error={!!errors.phoneNumber}
                            helperText={errors.phoneNumber}
                        />

                        {/* Email */}
                        <TextField
                            fullWidth label="Email" variant="outlined" margin="normal"
                            name="email" type="email"
                            value={formData.email}
                            onChange={handleChange}
                            error={!!errors.email}
                            helperText={errors.email}
                        />

                        {/* Contraseña */}
                        {!isNewUser && (
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={isPasswordEditable}
                                        onChange={(e) => {
                                            const checked = e.target.checked;
                                            setIsPasswordEditable(checked);
                                            setFormData(prev => ({
                                                ...prev,
                                                password: checked ? "" : "*****",
                                            }));
                                        }}
                                    />
                                }
                                label="Editar contraseña"
                            />
                        )}

                        <TextField
                            fullWidth label="Password" variant="outlined" margin="normal"
                            name="password" type={showPassword ? "text" : "password"}
                            value={formData.password}
                            onChange={handleChange}
                            disabled={!isPasswordEditable}
                            error={!!errors.password && isPasswordEditable}
                            helperText={errors.password && isPasswordEditable ? errors.password : ''}
                            InputProps={{
                                endAdornment: (
                                    <InputAdornment position="end">
                                        <IconButton onClick={toggleShowPassword} edge="end">
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                ),
                            }}
                        />

                        {/* Rol */}
                        <TextField
                            fullWidth label="Rol" variant="outlined" margin="normal"
                            name="role" select value={formData.role} onChange={handleChange}
                        >
                            <MenuItem value="ADMIN">Admin</MenuItem>
                            <MenuItem value="USER">User</MenuItem>
                        </TextField>
                    </>
                )}
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose} color="secondary">Cancelar</Button>
                <Button variant="contained" color="primary" onClick={handleSave}>
                    {isNewUser ? "Crear Usuario" : "Guardar Cambios"}
                </Button>
            </DialogActions>

            <Snackbar
                open={successMessage}
                autoHideDuration={900}
                onClose={handleSnackbarClose}
                message={isNewUser ? "Creado con exito" : "Actualizado con éxito"}
                anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
            />
        </Dialog>
    );
};

UserModal.propTypes = {
    open: PropTypes.bool.isRequired,
    onClose: PropTypes.func.isRequired,
    onCreate: PropTypes.func.isRequired,
    onUpdate: PropTypes.func.isRequired,
    userId: PropTypes.string,
    successMessage: PropTypes.bool,
    handleSnackbarClose: PropTypes.func,
};

export default UserModal;