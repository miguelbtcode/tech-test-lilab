# 🌟 React + Vite Project

Este proyecto implementa una arquitectura moderna para aplicaciones web usando las siguientes herramientas y convenciones:

## 🛠 Stack Tecnológico Frontend

### **Core**

- **Framework**: [React 18](https://react.dev/) (Componentes funcionales con Hooks)
- **Bundler**: [Vite 4](https://vitejs.dev/) (Entorno de desarrollo ultrarrápido)

### **Gestión de Estado**

- **Librería**: [Zustand](https://zustand-demo.pmnd.rs/)
  _Implementación eficiente con slices y configuración optimizada_

### **Enrutamiento**

- **Navegación**: [React Router v6](https://reactrouter.com/)  
  _Sistema de rutas dinámicas con loaders y acciones_

### **Estilos**

- **Metodología**: [CSS Modules](https://github.com/css-modules/css-modules)
  \_Componentes estilizados con scoped CSS

### **Formularios**

- **Validación**: [JS Vanilla + Reach Hooks](https://zod.dev/)  
  \_Validación esquemática con PropTypes y gestion de estado de los inputs con useState hook

### **Comunicación API**

- **Cliente HTTP**: [Axios](https://axios-http.com/)  
  _Interceptores globales y manejo de errores centralizado_

### **UI Components**

- **Biblioteca**: [MUI (Material-UI) v5](https://mui.com/)  
  _Componentes accesibles con theming personalizado_
- **Iconos**: [Material Icons](https://react-icons.github.io/react-icons/)

### **Calidad de Código**

- **Linting**: [ESLint](https://eslint.org/) (Configuración Airbnb extendida)
- **Formateo**: [Prettier](https://prettier.io/)  
  _Integración con Husky para pre-commit hooks_
- **Testing**:
  - Unitario: [Jest](https://jestjs.io/) + [React Testing Library](https://testing-library.com/docs/react-testing-library/intro/)
  - Componentes: [Storybook](https://storybook.js.org/) (Documentación interactiva)

### **Infraestructura**

- **Package Manager**: [npm](https://www.npmjs.com/) (v9+)
- **Entornos**: Configuración multi-environment (dev, staging, prod)
- **CI/CD**: Github Actions (Linting, tests y despliegue automático)

## 🚀 Guía de Despliegue

### **Requisitos**

```bash
node >=18.16.0
npm >=9.5.0
```

## 📋 Prerrequisitos

- [Node.js](https://nodejs.org/) v20 o superior
- [npm](https://www.npmjs.com/) v12.0 o superior
- Editor de código (Recomendado: [VS Code](https://code.visualstudio.com/))

## 🛠️ Configuración Inicial

### 1. Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/tu-repositorio.git
cd repositorio
```

# 2. Instalar dependencias

```bash
npm install
# o usando yarn
yarn install
```

# 3. Variables de entorno (opcional)

### Crear archivo .env en la raíz:

```bash
VITE_API_BASE_URL=http://localhost:5173
VITE_MODE=development
```

# 4. Ejecutar el Proyecto

## Desarrollo

```bash
npm run dev
# o
yarn dev
```

# 4.2. Pruebas en local

## Abrir en navegador: http://localhost:5173

# 5. Producción

```bash
npm run build
npm run preview
# o
yarn build
yarn previ
```

# 6. Estructura del proyecto

├── public/ # Assets estáticos
├── src/
│ ├── api/ # Peticion de apis al backend.
│ ├── actions/ # Logica de negocio que son usados por los customHooks.
│ ├── assets/ # Imágenes, fuentes, etc.
│ ├── components/ # Componentes reutilizables
│ ├── hooks/ # Custom Hooks
│ ├── routes/ # Configuración de rutas
│ ├── services/ # Administrador de estados locales entre componente padre e hijo.
│ ├── stores/ # Estado globales de la app.
│ ├── styles/ # Estilos globales
│ ├── utils/ # Funciones utilitarios o helpers
│ ├── App.jsx # Componente raíz
│ └── main.jsx # Punto de entrada
├── .eslintrc.cjs # Configuración ESLint
├── .gitignore  
└── vite.config.js # Configuración de Vite

# 7. Comandos utiles

Comando | Descripción
npm run dev | Inicia servidor de desarrollo
npm run build | Crea versión para producción
npm run lint | Ejecuta linter (ESLint)
npm run test | Ejecuta pruebas unitarias
npm run format | Formatea código (Prettier)

# 8. Extra

Este README incluye:

- Emojis visuales para mejor legibilidad
- Instrucciones claras paso a paso
- Estructura de proyecto detallada
- Solución de problemas comunes
- Sección de contribución estándar
- Compatible con Vite 4+ y React 18+
- Adaptable para TypeScript (solo cambiar extensiones a .tsx)
- Formato consistente para todos los entornos (Windows/Linux/Mac)
