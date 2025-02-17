import HomeIcon from "@mui/icons-material/Home";
import PeopleIcon from "@mui/icons-material/People";
import Home from "../components/Home.jsx";
import Users from "../components/Users/User.jsx";
import Groups2Icon from '@mui/icons-material/Groups2';
import AppRegistrationIcon from '@mui/icons-material/AppRegistration';
import Customer from "../components/customers/Customer.jsx";
import Activity from "../components/activity/Activity.jsx";

const routes = [
    {
        path: "/home",
        element: <Home />,
        text: "Inicio",
        icon: <HomeIcon fontSize={"large"} />,
        isPrivate: true,
        roles: ["ADMIN", "USER"],
    },
    {
        path: "/customers",
        element: <Customer />,
        text: "Clientes",
        icon: <Groups2Icon fontSize={"large"} />,
        isPrivate: true,
        roles: ["USER"],
    },
    {
        path: "/usuarios",
        element: <Users />,
        text: "Usuarios",
        icon: <PeopleIcon fontSize={"large"} />,
        isPrivate: true,
        roles: ["ADMIN"],
    },
    {
        path: "/activity",
        element: <Activity />,
        text: "Activity",
        icon: <AppRegistrationIcon fontSize={"large"} />,
        isPrivate: true,
        roles: ["USER"],
    },
];

export default routes;