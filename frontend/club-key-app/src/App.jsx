import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import PrivateRoute from "./routes/PrivateRoute";
import Login from "./components/auth/Login.jsx";
import PublicRoute from "./routes/PublicRoute.jsx";
import routes from "./routes/Routes.jsx";
import Home from "./components/Home.jsx";

function App() {

  return (
      <Router>
          <AuthProvider>
              <Routes>
                  {/* Rutas públicas */}
                  <Route element={<PublicRoute />}>
                      <Route path="/login" element={<Login />} />
                      <Route path="*" element={<Login />} />
                  </Route>

                  {/* Rutas privadas dinámicas */}
                  <Route element={<PrivateRoute />}>
                      <Route path="/" element={<Home />}>
                          {routes.map((route) => (
                              <Route key={route.path} path={route.path} element={route.element} />
                          ))}
                      </Route>
                  </Route>
              </Routes>
          </AuthProvider>
      </Router>
  )
}

export default App;