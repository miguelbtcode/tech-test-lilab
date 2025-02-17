import React from "react"; // 👈 Agrega esto
import { render, screen } from "@testing-library/react";

test("Testing Library funciona correctamente", () => {
    render(<h1>Hello, Testing Library!</h1>);
    expect(screen.getByText("Hello, Testing Library!")).toBeInTheDocument();
});