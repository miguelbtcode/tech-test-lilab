import "@testing-library/jest-dom"; // Agrega matchers como toBeInTheDocument()

// Soluciona el error de `TextEncoder`
import { TextEncoder, TextDecoder } from "util";
global.TextEncoder = TextEncoder;
global.TextDecoder = TextDecoder;