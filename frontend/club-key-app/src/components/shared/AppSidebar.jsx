import { Box, Drawer, List, ListItem, ListItemIcon, ListItemText, useMediaQuery } from "@mui/material";
import { Link, useLocation } from "react-router-dom";
import { motion } from "framer-motion";
import useSidebarStore from "../../stores/useSidebarStore.jsx";
import routes from "../../routes/Routes.jsx";
import { useAuth } from "../../context/useAuth";

const Sidebar = () => {
    const isMobile = useMediaQuery("(max-width: 768px)");
    const location = useLocation();
    const { open } = useSidebarStore();
    const { user } = useAuth();
    const drawerWidth = isMobile ? "16rem" : "17.5rem";

    return (
        <Drawer
            variant={isMobile ? "temporary" : "persistent"}
            anchor="left"
            open={open}
            ModalProps={{ keepMounted: true }}
            sx={{
                width: drawerWidth,
                flexShrink: 0,
                [`& .MuiDrawer-paper`]: {
                    width: drawerWidth,
                    backgroundColor: "#1976d2",
                    color: "white",
                    marginTop: "4rem",
                    height: "calc(100% - 4rem)",
                    padding: "1.25rem 0",
                    transition: "width 0.3s ease-in-out",
                },
            }}
        >
            <Box sx={{ display: "flex", justifyContent: "center", alignItems: "center", width: "100%", paddingY: "1.5rem" }}>
                <img src="/images/img.png" alt="Logo" style={{ width: "80%", maxWidth: "8rem" }} />
            </Box>
            <List>
                {routes
                    .filter((route) => !route.roles || route.roles.includes(user?.role))
                    .map((route) => (
                        <motion.div key={route.path} initial={{ opacity: 0, x: -20 }} animate={{ opacity: 1, x: 0 }} transition={{ duration: 0.3 }}>
                            <ListItem
                                component={Link}
                                to={route.path}
                                disablePadding
                                onClick={!open}
                                sx={{
                                    textAlign: "center",
                                    paddingY: "1.5rem",
                                    transition: "background 0.3s ease-in-out",
                                    backgroundColor: location.pathname === route.path ? "rgba(255, 255, 255, 0.3)" : "transparent",
                                    "&:hover": { backgroundColor: "rgba(255, 255, 255, 0.2)" },
                                }}
                            >
                                <ListItemIcon sx={{ color: "white", fontSize: "4.5rem", justifyContent: "left", marginLeft: "1.89rem" }}>
                                    {route.icon}
                                </ListItemIcon>
                                <ListItemText primary={route.text} primaryTypographyProps={{ fontSize: "1.29rem", fontWeight: "bold", textAlign: "left" }} />
                            </ListItem>
                        </motion.div>
                    ))}
            </List>
        </Drawer>
    );
};

export default Sidebar;