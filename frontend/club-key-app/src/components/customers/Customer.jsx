import {
    Box,
    Table,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Typography,
    CircularProgress,
    Button,
    FormControl,
    InputLabel, IconButton, TableBody, Select, MenuItem, Pagination,
} from "@mui/material";
import CustomerModal from "./CustomerModal.jsx";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import CancelIcon from "@mui/icons-material/Cancel";
import {useCustomers} from "../../hooks/Customers/useCustomers.jsx";

const Customer = () => {
    const {
        customers, loading, page, totalPages,
        openModal, successMessage, typeFilter,
        handleCreateCustomer, handleChangePage, handleOpenModal,
        handleCloseModal, handleSnackbarClose, handleFilterChange, handlerResetFilter,
    } = useCustomers(10);

    const customerTypes = [
        {
            key: 1,
            name: 'Visitante'
        },
        {
            key: 2,
            name: 'Miembro'
        },
    ];

    return (
        <Box sx={{ p: 3 }}>
            <Paper sx={{ p: 3, boxShadow: 3 }}>
                <Typography variant="h5" sx={{ mb: 2 }}>
                    Gestión de Clientes
                </Typography>

                {loading && (
                    <Box sx={{ display: "flex", justifyContent: "center", my: 3 }}>
                        <CircularProgress color="primary" />
                    </Box>
                )}

                <Box sx={{ display: "flex", alignItems: "center", gap: 2, mb: 3 }}>
                    <FormControl sx={{ minWidth: 200 }}>
                        <InputLabel>Filtro</InputLabel>
                        <Select value={typeFilter} onChange={handleFilterChange} variant="outlined">
                            <MenuItem value="">Todos</MenuItem>
                            {customerTypes.map((customerType) => (
                                <MenuItem key={customerType.key} value={customerType.key}>
                                    {customerType.name}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <Button variant="outlined" color="secondary" onClick={handlerResetFilter}>
                        Borrar
                    </Button>

                    <Box sx={{ flexGrow: 1 }} />

                    {/* Botón para agregar usuario */}
                    <Button
                        variant="contained"
                        color="primary"
                        onClick={() => handleOpenModal()}
                    >
                        Registrar Entrada
                    </Button>
                </Box>


                {!loading && customers.length > 0 && (
                    <>
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
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {customers.map((customer) => (
                                        <TableRow key={customer.id}>
                                            <TableCell>{customer.name}</TableCell>
                                            <TableCell>{customer.documentNumber}</TableCell>
                                            <TableCell>{customer.type}</TableCell>
                                            <TableCell>{customer.phoneNumber}</TableCell>
                                            <TableCell>{customer.birthDate}</TableCell>
                                            <TableCell>{customer.gender}</TableCell>
                                            <TableCell>{customer.email}</TableCell>

                                            <TableCell>
                                                <IconButton color={customer.isActive ?  "success" : "error"}>
                                                    {customer.isActive ? <CheckCircleIcon /> : <CancelIcon />}
                                                </IconButton>
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </>
                )}

                {/* Mensaje cuando no hay usuarios */}
                {!loading && customers.length === 0 && (
                    <Typography variant="body1" align="center" sx={{ mt: 2 }}>
                        No hay clientes disponibles.
                    </Typography>
                )}

                <Box sx={{ display: "flex", justifyContent: "center", mt: 2 }}>
                    <Pagination
                        count={totalPages}
                        page={page}
                        onChange={handleChangePage}
                        color="primary"
                    />
                </Box>

            </Paper>

            {/* Modal para editar usuario */}
            <CustomerModal
                open={openModal}
                onClose={handleCloseModal}
                onCreate={handleCreateCustomer}
                successMessage={successMessage}
                handleSnackbarClose={handleSnackbarClose}
            />
        </Box>
    );
};

export default Customer;