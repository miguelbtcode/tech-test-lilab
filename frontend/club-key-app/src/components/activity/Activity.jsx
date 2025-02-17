import {
    Box,
    Table,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Typography,
    Button,
    FormControl,
    InputLabel,
    IconButton,
    TableBody,
    Select,
    MenuItem,
    Pagination,
    Collapse,
    Dialog,
    DialogTitle,
    DialogContent,
    TextField,
    DialogActions,
    Checkbox,
    FormControlLabel,
    Snackbar,
    Alert
} from "@mui/material";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import CancelIcon from "@mui/icons-material/Cancel";
import { useCustomerActivities } from "../../hooks/Customers/useCustomerActivities.jsx";
import { format } from 'date-fns';
import React, { useState } from "react";
import useErrorSnackbarStore from "../../stores/useErrorSnackbarStore.jsx";

const Activity = () => {
    const {
        customers,
        loading,
        page,
        totalPages,
        successMessage,
        customerIdFilter,
        handleFilterChange,
        handlerResetFilter,
        handleAddEntrance,
        handleAddExit,
        handleSnackbarClose,
        handleForceRefresh,
    } = useCustomerActivities(10);

    const [expandedRows, setExpandedRows] = useState({});
    const { snackbarOpen, snackbarMessage, hideError, showError } = useErrorSnackbarStore();
    const [modalEntranceOpen, setModalEntranceOpen] = useState(false);
    const [modalExitOpen, setModalExitOpen] = useState(false);
    const [selectedCustomerId, setSelectedCustomerId] = useState("");
    const [entryDate, setEntryDate] = useState("");
    const [enableReason, setEnableReason] = useState(false);

    const handleRowClick = (customerId) => {
        setExpandedRows(prevState => ({
            ...prevState,
            [customerId]: !prevState[customerId]
        }));
    };

    const handleOpenModalEntrance = () => {
        setModalEntranceOpen(true);
        setSelectedCustomerId("");
        const currentDate = new Date();
        const formattedDate = currentDate.toISOString().slice(0, 16);
        setEntryDate(formattedDate);
    };

    const handleOpenModalExit = () => {
        setModalExitOpen(true);
        setSelectedCustomerId("");
        const currentDate = new Date();
        const formattedDate = currentDate.toISOString().slice(0, 16);
        setEntryDate(formattedDate);
    };

    const handleCloseModalEntrance = () => {
        setModalEntranceOpen(false);
        setSelectedCustomerId("");
        setEntryDate("");
        setEnableReason(false);
    };

    const handleCloseModalExit = () => {
        setModalExitOpen(false);
        setSelectedCustomerId("");
        setEntryDate("");
        setEnableReason(false);
    };

    const handleRegisterEntry = async (e) => {

        if (!selectedCustomerId) {
            const errorMessage = "Selecciona un cliente";
            if (snackbarMessage !== errorMessage) {
                showError(errorMessage);
            }
            return;
        }

        const selectedCustomer = customers.find(c => c.id === selectedCustomerId);
        if (!selectedCustomer) {
            const errorMessage = "El cliente seleccionado no se encuentra en la lista.";
            if (snackbarMessage !== errorMessage) {
                showError(errorMessage);
            }
            return;
        }

        const formattedEntryDate = new Date(entryDate).toISOString();
        const formData = {
            customerId: selectedCustomer.id,
            entryDate: formattedEntryDate
        };

        if (e.target.value === 'Entrance') {
            await handleAddEntrance(formData);

            setTimeout(() => {
                setModalEntranceOpen(false);
            }, 1600);

            await handleForceRefresh(selectedCustomerId);

        } else {
            await handleAddExit(formData);

            setTimeout(() => {
                setModalExitOpen(false);
            }, 1600);

            await handleForceRefresh(selectedCustomerId);

            handleCloseModalExit();
        }
    };

    return (
        <Box sx={{ p: 3 }}>
            <Paper sx={{ p: 3, boxShadow: 3 }}>
                <Typography variant="h5" sx={{ mb: 2 }}>
                    Registro de entradas y salidas
                </Typography>
                <Box sx={{ display: "flex", alignItems: "center", gap: 2, mb: 3 }}>
                    <FormControl sx={{ minWidth: 200 }}>
                        <InputLabel id="filter-label">Filtro</InputLabel>
                        <Select
                            labelId="filter-label"
                            id="customer-filter"
                            value={customerIdFilter}
                            onChange={handleFilterChange}
                            variant="outlined"
                        >
                            <MenuItem value="">Todos</MenuItem>
                            {customers.map((customerType) => (
                                <MenuItem key={customerType.id} value={customerType.id}>
                                    {customerType.name}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <Button variant="outlined" color="secondary" onClick={handlerResetFilter}>
                        Borrar
                    </Button>
                    <Box sx={{ flexGrow: 1 }} />
                    <Button variant="contained" color="success" onClick={handleOpenModalEntrance}>
                        Registrar Entrada
                    </Button>
                    <Button variant="contained" color="error" onClick={handleOpenModalExit}>
                        Registrar Salida
                    </Button>
                </Box>

                {!loading && customers.length > 0 && (
                    <TableContainer component={Paper}>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell>Nombre</TableCell>
                                    <TableCell>N. Documento</TableCell>
                                    <TableCell>Tipo</TableCell>
                                    <TableCell>N. Telefono</TableCell>
                                    <TableCell>BirthDate</TableCell>
                                    <TableCell>Genero</TableCell>
                                    <TableCell>Email</TableCell>
                                    <TableCell>Activo</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {customers.map((customer) => (
                                    <React.Fragment key={customer.id}>
                                        <TableRow
                                            sx={{ cursor: "pointer" }}
                                            onClick={() => handleRowClick(customer.id)}
                                        >
                                            <TableCell>{customer.name}</TableCell>
                                            <TableCell>{customer.documentNumber}</TableCell>
                                            <TableCell>{customer.type === 1 ? "Visitante" : "Miembro"}</TableCell>
                                            <TableCell>{customer.phoneNumber}</TableCell>
                                            <TableCell>{format(new Date(customer.birthDate), 'dd/MM/yyyy')}</TableCell>
                                            <TableCell>
                                                {customer.gender === 1 ? 'Masculino' : customer.gender === 2 ? 'Femenino' : 'Otro'}
                                            </TableCell>
                                            <TableCell>{customer.email}</TableCell>
                                            <TableCell>
                                                <IconButton color={customer.isActive ? "success" : "error"}>
                                                    {customer.isActive ? <CheckCircleIcon /> : <CancelIcon />}
                                                </IconButton>
                                            </TableCell>
                                        </TableRow>

                                        <TableRow key={`expandable-${customer.id}`}>
                                            <TableCell colSpan={10} sx={{ paddingBottom: 0, paddingTop: 0 }}>
                                                <Collapse in={expandedRows[customer.id]} timeout="auto" unmountOnExit>
                                                    <Box sx={{ padding: 2 }}>
                                                        {customer.entrances.length > 0 && (
                                                            <>
                                                                <Typography variant="h6">Entradas:</Typography>
                                                                <Table>
                                                                    <TableHead>
                                                                        <TableRow>
                                                                            <TableCell>N. Autogenerado</TableCell>
                                                                            <TableCell>Ultima actividad</TableCell>
                                                                            <TableCell>Fecha y hora</TableCell>
                                                                        </TableRow>
                                                                    </TableHead>
                                                                    <TableBody>
                                                                        {customer.entrances.map((entrance) => (
                                                                            <TableRow key={`entrance-${entrance.id}`}>
                                                                                <TableCell>{entrance.id}</TableCell>
                                                                                <TableCell>
                                                                                    {entrance.isLastStatus ? (
                                                                                        <CheckCircleIcon color="success" />
                                                                                    ) : (
                                                                                        <CancelIcon color="error" />
                                                                                    )}
                                                                                </TableCell>
                                                                                <TableCell>
                                                                                    {format(new Date(entrance.entranceTime), 'dd/MM/yyyy HH:mm:ss')}
                                                                                </TableCell>
                                                                            </TableRow>
                                                                        ))}
                                                                    </TableBody>
                                                                </Table>
                                                            </>
                                                        )}

                                                        {customer.exits.length > 0 && (
                                                            <>
                                                                <Typography variant="h6">Salidas:</Typography>
                                                                <Table>
                                                                    <TableHead>
                                                                        <TableRow>
                                                                            <TableCell>N. Autogenerado</TableCell>
                                                                            <TableCell>Ultima actividad</TableCell>
                                                                            <TableCell>Fecha y hora</TableCell>
                                                                        </TableRow>
                                                                    </TableHead>
                                                                    <TableBody>
                                                                        {customer.exits.map((exit) => (
                                                                            <TableRow key={`exit-${exit.id}`}>
                                                                                <TableCell>{exit.id}</TableCell>
                                                                                <TableCell>
                                                                                    {exit.isLastStatus ? (
                                                                                        <CheckCircleIcon color="success" />
                                                                                    ) : (
                                                                                        <CancelIcon color="error" />
                                                                                    )}
                                                                                </TableCell>
                                                                                <TableCell>
                                                                                    {format(new Date(exit.exitTime), 'dd/MM/yyyy HH:mm:ss')}
                                                                                </TableCell>
                                                                            </TableRow>
                                                                        ))}
                                                                    </TableBody>
                                                                </Table>
                                                            </>
                                                        )}
                                                    </Box>
                                                </Collapse>
                                            </TableCell>
                                        </TableRow>
                                    </React.Fragment>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                )}

                {!loading && customers.length === 0 && (
                    <Typography variant="body1" align="center" sx={{ mt: 2 }}>
                        No hay clientes disponibles.
                    </Typography>
                )}

                <Box sx={{ display: "flex", justifyContent: "center", mt: 2 }}>
                    <Pagination count={totalPages} page={page} color="primary" />
                </Box>

                {/* Entrance modal */}
                <Dialog open={modalEntranceOpen} onClose={handleCloseModalEntrance}>
                    <DialogTitle>Registrar Entrada</DialogTitle>
                    <DialogContent sx={{ padding: 4 }}>
                        <FormControl fullWidth sx={{ mb: 2 }}>
                            <InputLabel id="cliente-label">Cliente</InputLabel>
                            <Select
                                value={selectedCustomerId}
                                onChange={(e) => setSelectedCustomerId(e.target.value)}
                                label="Cliente"
                                variant="outlined"
                                labelId="cliente-label"
                                id="cliente"
                            >
                                {customers.map((customer) => (
                                    <MenuItem key={customer.id} value={customer.id}>
                                        {customer.name}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>

                        <FormControlLabel
                            control={
                                <Checkbox
                                    checked={enableReason}
                                    onChange={(e) => setEnableReason(e.target.checked)}
                                    color="primary"
                                />
                            }
                            label="(*) Modificar fecha"
                        />

                        <TextField
                            fullWidth
                            label="Fecha y hora de entrada"
                            variant="outlined"
                            margin="normal"
                            type="datetime-local"
                            value={entryDate}
                            disabled={!enableReason}
                            onChange={(e) => setEntryDate(e.target.value)}
                            InputLabelProps={{
                                shrink: true
                            }}
                        />
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleCloseModalEntrance} color="primary">
                            Cancelar
                        </Button>
                        <Button onClick={handleRegisterEntry} color="primary" value={'Entrance'}>
                            Registrar
                        </Button>
                    </DialogActions>

                    <Snackbar open={snackbarOpen} autoHideDuration={4000} onClose={hideError}>
                        <Alert onClose={hideError} severity="error" sx={{ width: "100%" }}>
                            {snackbarMessage}
                        </Alert>
                    </Snackbar>

                    <Snackbar
                        open={successMessage}
                        autoHideDuration={1600}
                        onClose={handleSnackbarClose}
                        message="Creado con éxito"
                        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
                    />
                </Dialog>

                {/* Exit modal */}
                <Dialog open={modalExitOpen} onClose={handleCloseModalExit}>
                    <DialogTitle>Registrar Salida</DialogTitle>
                    <DialogContent sx={{ padding: 4 }}>
                        <FormControl fullWidth sx={{ mb: 2 }}>
                            <InputLabel>Cliente</InputLabel>
                            <Select
                                value={selectedCustomerId}
                                onChange={(e) => setSelectedCustomerId(e.target.value)}
                                label="Cliente"
                                variant="outlined"
                            >
                                {customers.map((customer) => (
                                    <MenuItem key={customer.id} value={customer.id}>
                                        {customer.name}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>

                        <FormControlLabel
                            control={
                                <Checkbox
                                    checked={enableReason}
                                    onChange={(e) => setEnableReason(e.target.checked)}
                                    color="primary"
                                />
                            }
                            label="(*) Modificar fecha"
                        />

                        <TextField
                            fullWidth
                            label="Fecha y hora de salida"
                            variant="outlined"
                            margin="normal"
                            type="datetime-local"
                            value={entryDate}
                            disabled={!enableReason}
                            onChange={(e) => setEntryDate(e.target.value)}
                            InputLabelProps={{
                                shrink: true
                            }}
                        />
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleCloseModalExit} color="primary">
                            Cancelar
                        </Button>
                        <Button onClick={handleRegisterEntry} color="primary" value={'Exit'}>
                            Registrar
                        </Button>
                    </DialogActions>

                    <Snackbar open={snackbarOpen} autoHideDuration={4000} onClose={hideError}>
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
            </Paper>
        </Box>
    );
};

export default Activity;