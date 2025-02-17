import {
    Box,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Pagination,
    IconButton,
    Typography,
    CircularProgress,
    DialogTitle,
    DialogContent,
    Dialog,
    DialogActions,
    Button,
    FormControl,
    InputLabel,
    Select,
    MenuItem
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import CancelIcon from "@mui/icons-material/Cancel";
import UserModal from "./UserModal.jsx";
import { useUsers } from "../../hooks/Users/useUsers.jsx";
import { useRoles } from "../../hooks/Users/useRoles.jsx";

const Users = () => {

    const { roles, loading: loadingRoles } = useRoles();

    const {
        users, loading, page, totalPages,
        openModal, selectedUser, successMessage, confirmDelete,
        roleFilter,
        handleCreateUser, handleUpdateUser, handleChangePage, handleOpenModal,
        handleCloseModal, handleSnackbarClose, handleConfirmDelete, handleCancelDelete,
        handleDeleteUser, handleFilterChange, handlerResetFilter
    } = useUsers(10);

    return (
        <Box sx={{ p: 3 }}> {/* Padding general */}
            <Paper sx={{ p: 3, boxShadow: 3 }}> {/* Contenedor con sombra */}
                <Typography variant="h5" sx={{ mb: 2 }}>
                    Gestión de Usuarios
                </Typography>

                {/* Cargando */}
                {loading && loadingRoles && (
                    <Box sx={{ display: "flex", justifyContent: "center", my: 3 }}>
                        <CircularProgress color="primary" />
                    </Box>
                )}

                {/* Filtros */}
                <Box sx={{ display: "flex", alignItems: "center", gap: 2, mb: 3 }}>
                    <FormControl sx={{ minWidth: 200 }}>
                        <InputLabel>Filtro</InputLabel>
                        <Select value={roleFilter} onChange={handleFilterChange} variant="outlined">
                            <MenuItem value="">Todos</MenuItem>
                            {roles.map((role) => (
                                <MenuItem key={role.id} value={role.id}>
                                    {role.name}
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
                        Agregar Usuario
                    </Button>
                </Box>



                {/* Tabla de usuarios */}
                {!loading && users.length > 0 && (
                    <>
                        <TableContainer component={Paper}>
                            <Table>
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Nombre</TableCell>
                                        <TableCell>UserName</TableCell>
                                        <TableCell>Email</TableCell>
                                        <TableCell>Phone Number</TableCell>
                                        <TableCell>Activo</TableCell>
                                        <TableCell align="right">Acciones</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {users.map((user) => (
                                        <TableRow key={user.id}>
                                            <TableCell>{user.name}</TableCell>
                                            <TableCell>{user.userName}</TableCell>
                                            <TableCell>{user.email}</TableCell>
                                            <TableCell>{user.phoneNumber}</TableCell>
                                            <TableCell>
                                                <IconButton color={user.isActive ?  "success" : "error"}>
                                                    {user.isActive ? <CheckCircleIcon /> : <CancelIcon />}
                                                </IconButton>
                                            </TableCell>

                                            <TableCell align="right">
                                                <IconButton color="primary" disabled={!user.isActive} onClick={() => handleOpenModal(user.id)}>
                                                    <EditIcon />
                                                </IconButton>
                                                <IconButton color="error" disabled={!user.isActive} onClick={() => handleConfirmDelete(user.id)}>
                                                    <DeleteIcon />
                                                </IconButton>
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </>
                )}

                {!loading && users.length === 0 && (
                    <Typography variant="body1" align="center" sx={{ mt: 2 }}>
                        No hay usuarios disponibles.
                    </Typography>
                )}

                {/* Paginación */}
                <Box sx={{ display: "flex", justifyContent: "center", mt: 2 }}>
                    <Pagination
                        count={totalPages}
                        page={page}
                        onChange={handleChangePage}
                        color="primary"
                    />
                </Box>
            </Paper>

            <UserModal
                open={openModal}
                onClose={handleCloseModal}
                userId={selectedUser}
                onCreate={handleCreateUser}
                onUpdate={handleUpdateUser}
                successMessage={successMessage}
                handleSnackbarClose={handleSnackbarClose}
            />

            <Dialog open={confirmDelete} onClose={handleCancelDelete}>
                <DialogTitle>Confirmar Eliminación</DialogTitle>
                <DialogContent>
                    <Typography>
                        ¿Estás seguro de que deseas eliminar este usuario? Esta acción no se puede deshacer.
                    </Typography>
                    {loading && (
                        <Box sx={{ display: "flex", justifyContent: "center", mt: 2 }}>
                            <CircularProgress color="error" />
                        </Box>
                    )}
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleCancelDelete} color="secondary">
                        Cancelar
                    </Button>
                    <Button onClick={handleDeleteUser} color="error" variant="contained">
                        Eliminar
                    </Button>
                </DialogActions>
            </Dialog>
        </Box>
    );
};

export default Users;