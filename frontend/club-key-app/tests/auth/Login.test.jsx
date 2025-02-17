/**
 * @jest-environment jsdom
 */

import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import Login from "../../src/components/auth/Login";
import { useAuth } from "../../src/context/useAuth";

jest.mock("../../src/context/useAuth");

test("Renderiza el login correctamente y permite ingresar credenciales", () => {
    const mockLogin = jest.fn();
    useAuth.mockReturnValue({ login: mockLogin });

    render(
        <MemoryRouter>
            <Login />
        </MemoryRouter>
    );

    expect(screen.getByText(/iniciar sesión/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/email/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/contraseña/i)).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /ingresar/i })).toBeInTheDocument();

    fireEvent.change(screen.getByLabelText(/email/i), { target: { value: "test@example.com" } });
    fireEvent.change(screen.getByLabelText(/contraseña/i), { target: { value: "password123" } });
    fireEvent.click(screen.getByRole("button", { name: /ingresar/i }));

    expect(mockLogin).toHaveBeenCalledTimes(1);
    expect(mockLogin).toHaveBeenCalledWith({
        email: "test@example.com",
        password: "password123",
    });
});