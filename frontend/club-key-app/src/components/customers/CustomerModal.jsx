import { useState, useEffect } from "react";
import PropTypes from "prop-types";
import {
    Dialog, DialogTitle, DialogContent, DialogActions, TextField, Button, CircularProgress,
    MenuItem, Snackbar, Alert
} from "@mui/material";
import useErrorSnackbarStore from "../../stores/useErrorSnackbarStore.jsx";

const CustomerModal = ({
                           open,
                           onClose,
                           onCreate,
                           successMessage,
                           handleSnackbarClose
                       }) => {
    const { snackbarOpen, snackbarMessage, hideError } = useErrorSnackbarStore();
    const [formData, setFormData] = useState({
        name: "",
        documentNumber: "",
        type: "",
        phoneNumber: "",
        birthDate: "",
        gender: "",
        email: "",
    });

    const [loading, setLoading] = useState(false);
    const [errors, setErrors] = useState({});


    const genders = [
        { key: 1, name: 'Masculino' },
        { key: 2, name: 'Femenino' },
        { key: 3, name: 'Otro' },
    ];

    useEffect(() => {
        setFormData({
            name: "",
            documentNumber: "",
            type: "",
            phoneNumber: "",
            birthDate: "",
            gender: "",
            email: ""
        });
    }, [open]);

    const validateForm = () => {
        let newErrors = {};

        if (!formData.name.trim()) newErrors.name = "El nombre es requerido";

        if (!formData.documentNumber.trim()) newErrors.documentNumber = "El numero de documento es requerido";
        else if (!/^\d{8}$/.test(formData.documentNumber)) newErrors.documentNumber = "El número de documento debe tener exactamente 8 dígitos";

        if (!formData.type) newErrors.type = "El tipo de miembro es requerido";

        if (!formData.phoneNumber) {
            newErrors.phoneNumber = "El teléfono es requerido";
        } else if (formData.phoneNumber.length !== 9) {
            newErrors.phoneNumber = "Debe tener exactamente 9 dígitos";
        }

        if (!formData.birthDate) {
            newErrors.birthDate = "La fecha de nacimiento es requerida";
        }

        if (!formData.gender) newErrors.gender = "El género es requerido";

        if (!formData.email.trim()) {
            newErrors.email = "El correo es requerido";
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
            newErrors.email = "Ingrese un correo válido";
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
            type: Number(formData.type),
            gender: Number(formData.gender),
        };

        onCreate(updatedFormData);
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>{"Crear cliente"}</DialogTitle>
            <DialogContent>
                {loading ? (
                    <CircularProgress color="primary" sx={{ display: "block", margin: "20px auto" }} />
                ) : (
                    <>
                        {[
                            { label: "Nombre", name: "name", type: "text" },
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

                        <TextField
                            fullWidth
                            label="N. Documento"
                            variant="outlined"
                            margin="normal"
                            name="documentNumber"
                            type="text"
                            value={formData.documentNumber}
                            onChange={(e) => {
                                const value = e.target.value;
                                if (/^\d*$/.test(value) && value.length <= 8) {
                                    setFormData({ ...formData, documentNumber: value });
                                }
                            }}
                            error={!!errors.documentNumber}
                            helperText={errors.documentNumber}
                        />

                        {/* Type */}
                        <TextField
                            fullWidth
                            label="Tipo"
                            variant="outlined"
                            margin="normal"
                            name="type"
                            select
                            value={formData.type || ""}
                            onChange={handleChange}
                            error={!!errors.type}
                            helperText={errors.type}
                        >
                            <MenuItem value="">Seleccionar</MenuItem>
                            <MenuItem value="1">Visitor</MenuItem>
                            <MenuItem value="2">Member</MenuItem>
                        </TextField>

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

                        {/* Fecha de Nacimiento */}
                        <TextField
                            fullWidth
                            label="Fecha de Nacimiento"
                            variant="outlined"
                            margin="normal"
                            type="date"
                            name="birthDate"
                            value={formData.birthDate}
                            onChange={handleChange}
                            error={!!errors.birthDate}
                            helperText={errors.birthDate}
                            InputLabelProps={{
                                shrink: true,
                            }}
                        />

                        {/* Género */}
                        <TextField
                            fullWidth
                            label="Genero"
                            variant="outlined"
                            margin="normal"
                            name="gender"
                            select
                            value={formData.gender || ""}
                            onChange={handleChange}
                            error={!!errors.gender}
                            helperText={errors.gender}
                        >
                            <MenuItem value="">Seleccionar</MenuItem>
                            <MenuItem value="1">Masculino</MenuItem>
                            <MenuItem value="2">Femenino</MenuItem>
                            <MenuItem value="3">Otro</MenuItem>
                        </TextField>

                        {/* Email */}
                        <TextField
                            fullWidth
                            label="Email"
                            variant="outlined"
                            margin="normal"
                            name="email"
                            type="email"
                            value={formData.email}
                            onChange={handleChange}
                            error={!!errors.email}
                            helperText={errors.email}
                        />
                    </>
                )}
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose} color="secondary">Cancelar</Button>
                <Button variant="contained" color="primary" onClick={handleSave}>
                    {"Crear Cliente"}
                </Button>
            </DialogActions>

            <Snackbar
                open={snackbarOpen}
                autoHideDuration={6000}
                onClose={hideError}
            >
                <Alert onClose={hideError} severity="error" sx={{ width: "100%" }}>
                    {snackbarMessage}
                </Alert>
            </Snackbar>

            <Snackbar
                open={successMessage}
                autoHideDuration={900}
                onClose={handleSnackbarClose}
                message="Creado con éxito"
                anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
            />
        </Dialog>
    );
};

CustomerModal.propTypes = {
    open: PropTypes.bool.isRequired,
    onClose: PropTypes.func.isRequired,
    onCreate: PropTypes.func.isRequired,
    successMessage: PropTypes.bool,
    handleSnackbarClose: PropTypes.func,
};

export default CustomerModal;