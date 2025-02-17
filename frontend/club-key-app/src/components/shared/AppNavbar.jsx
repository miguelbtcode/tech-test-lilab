import { AppBar, Toolbar, IconButton, Typography, Avatar, Menu, MenuItem } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { useState } from "react";
import useSidebarStore from "../../stores/useSidebarStore.jsx";
import { useAuth } from "../../context/useAuth";

const AppNavBar = () => {
    const [anchorEl, setAnchorEl] = useState(null);
    const { toggleDrawer } = useSidebarStore();
    const { logout } = useAuth();

    const handleMenuOpen = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleMenuClose = () => {
        setAnchorEl(null);
    };

    const handleLogout = () => {
        handleMenuClose();
        logout();
    };

    return (
        <AppBar position="fixed" sx={{ backgroundColor: "#1976d2", zIndex: 1300 }}>
            <Toolbar>
                {/* Botón de menú para abrir el Sidebar */}
                <IconButton color="inherit" edge="start" onClick={toggleDrawer}>
                    <MenuIcon />
                </IconButton>

                {/* Título de la aplicación */}
                <Typography variant="h6" sx={{ flexGrow: 1 }}>
                    Mi App
                </Typography>

                <Avatar onClick={handleMenuOpen} sx={{ cursor: "pointer" }} />
                <Menu anchorEl={anchorEl} open={Boolean(anchorEl)} onClose={handleMenuClose}>
                    <MenuItem onClick={handleLogout}>Cerrar sesión</MenuItem>
                </Menu>
            </Toolbar>
        </AppBar>
    );
};

export default AppNavBar;