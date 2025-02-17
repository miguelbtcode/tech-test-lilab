import {
    Box
} from "@mui/material";
import { Outlet } from "react-router-dom";
import AppNavBar from "./shared/AppNavbar.jsx";
import Sidebar from "./shared/AppSidebar.jsx";
import useSidebarStore from "../stores/useSidebarStore.jsx";
import {useEffect} from "react";

const drawerWidthLeft = 2.5;
const drawerWidthRight = 15.62;

const Home = () => {

    const { open, openDrawer } = useSidebarStore();

    useEffect(() => {
        openDrawer();
    }, [openDrawer]);

    return (
        <Box sx={{ display: "flex"}}>
            {/* Navbar */}
            <AppNavBar />

            {/* Sidebar */}
            <Sidebar />

            {/* Contenido principal centrado */}
            <Box
                component="main"
                sx={{
                    flexGrow: 1,
                    width: "100vw",
                    height: "100vh",
                    display: "flex",
                    justifyContent: "center",
                    alignItems: "center",
                    transition: "margin 0.3s ease-in-out",
                    marginLeft: open ? `${drawerWidthLeft}rem` : "0px",
                    marginRight: open ? "3rem" : `${drawerWidthRight}rem`,
                }}
            >
                {/* Contenedor del contenido hijo */}
                <Box
                    sx={{
                        width: "100%",
                        minHeight: "85vh",
                        backgroundColor: "white",
                        borderRadius: 2,
                        boxShadow: 0,
                        transition: "margin 0.3s ease-in-out",
                        marginX: "auto",
                    }}
                >
                    <Outlet />
                </Box>
            </Box>
        </Box>
    );
};

export default Home;