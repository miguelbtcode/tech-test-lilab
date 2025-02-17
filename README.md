# tech-test-lilab

Name: Miguel Angel Barreto Torres

## Frontend: https://club-key-site.netlify.app
## Backend: https://clubkey-webapi-dme9c3ddahduacft.centralus-01.azurewebsites.net/swagger/index.html
## Backend (Scalar): https://clubkey-webapi-dme9c3ddahduacft.centralus-01.azurewebsites/Scalar

# Sistema de Gestión de Accesos para un Club Recreativo

## Descripción
Se necesita desarrollar un sistema para gestionar los accesos de los clientes a un club recreativo. Actualmente, el acceso al club no está automatizado, por lo que el personal será responsable de registrar tanto las entradas como las salidas de los visitantes.

El personal autorizado deberá iniciar sesión en el sistema utilizando un nombre de usuario y una contraseña. Se requerirá un usuario administrador con privilegios para realizar tareas como consultas, registro, modificación y eliminación de usuarios registrados.

Cuando el personal acceda al sistema, deberá poder registrar a los clientes, los cuales se clasifican en dos tipos:

- **Visitantes**: Clientes que realizan un pago por visita.
- **Miembros**: Clientes que cuentan con una membresía que incluye un número fijo de visitas al mes y un pago mensual.

## Casos de uso

### 1. Iniciar Sesión
**Actor Principal**: Personal autorizado.

**Descripción**: El personal ingresa al sistema utilizando su nombre de usuario y contraseña para acceder a las funcionalidades del sistema.

**Flujo Principal**:
1. El usuario accede a la página de inicio de sesión.
2. Ingresa su correo electrónico y contraseña.
3. El frontend envía las credenciales al backend para validación.
4. El backend verifica las credenciales y genera un token o sesión.
5. El frontend almacena la sesión y redirige al usuario al panel de control.

### 2. Administrar Usuarios
**Actor Principal**: Usuario administrador.

**Descripción**: El administrador puede realizar tareas de consulta, registro, modificación y eliminación de usuarios registrados en el sistema.

**Flujo Principal**:
1. El administrador accede al formulario de registro de usuario en la interfaz web.
2. Ingresa los datos del nuevo usuario (nombre, correo electrónico, contraseña, rol).
3. El frontend valida los datos ingresados.
4. El backend procesa la solicitud y guarda el nuevo usuario en la base de datos.
5. El frontend muestra una confirmación del registro exitoso.

### 3. Registrar Cliente
**Actor Principal**: Personal autorizado.

**Descripción**: El personal registra a un nuevo cliente en el sistema, proporcionando los detalles relevantes como nombre, tipo de cliente (visitante, miembro), información de contacto, etc.

**Flujo Principal**:
1. El personal autorizado accede al formulario de registro de cliente en la interfaz web.
2. Ingresa los datos del cliente (nombre, tipo de cliente, detalles de contacto, etc.).
3. El frontend valida los datos ingresados.
4. El backend procesa la solicitud y guarda el nuevo cliente en la base de datos.
5. El frontend muestra una confirmación del registro exitoso.

### 4. Registrar Entrada
**Actor Principal**: Personal autorizado.

**Descripción**: El personal registra la entrada de un cliente al club, asociando la entrada con el cliente correspondiente y registrando la fecha y hora de ingreso.

**Flujo Principal**:
1. El personal autorizado selecciona el cliente correspondiente en la interfaz web.
2. Ingresa la hora de entrada.
3. El frontend envía los datos al backend para registro.
4. El backend guarda la hora de entrada en la base de datos.
5. El frontend actualiza la vista con la información actualizada.

### 5. Registrar Salida
**Actor Principal**: Personal autorizado.

**Descripción**: El personal registra la salida de un cliente del club, asociando la salida con el cliente correspondiente y registrando la fecha y hora de salida.

**Flujo Principal**:
1. El personal autorizado selecciona el cliente correspondiente en la interfaz web.
2. Ingresa la hora de salida.
3. El frontend envía los datos al backend para registro.
4. El backend guarda la hora de salida en la base de datos.
5. El frontend actualiza la vista con la información actualizada.

### 6. Consultar Registro de Accesos
**Actor Principal**: Personal autorizado.

**Descripción**: El personal puede consultar el registro de accesos al club para un cliente específico, mostrando las entradas y salidas registradas, junto con las fechas y horas correspondientes.

**Flujo Principal**:
1. El administrador accede a la sección de Consulta de Accesos en la interfaz web.
2. Consulta y filtra los detalles por visitante y las entradas y salidas.
3. El frontend envía los campos para filtrar los datos.
4. El backend procesa la solicitud y actualiza los datos de la consulta.
5. El frontend muestra los datos actualizados.

## Puntos a Considerar

### Backend
- **Endpoints RESTful**: Debe enfocarse en el desarrollo de los endpoints RESTful necesarios para cumplir con los objetivos planteados.
- **Modelos de Datos y Base de Datos**: Definir los modelos de datos necesarios e implementar una base de datos apropiada para el requerimiento y proporcionar scripts de migración de esquema y datos iniciales si es necesario.
- **Pruebas Unitarias**: Escribir pruebas unitarias para asegurar la funcionalidad correcta del backend.
- **Guía de Despliegue y Uso**: Proveer una guía clara sobre cómo desplegar y utilizar la API (puede incluirlo en el README).
- **Seguridad**: Implementar medidas de seguridad para la protección y resguardo de los datos.
- **Control de Roles**: Implementar un sistema de control de roles que permita definir diferentes niveles de acceso (por ejemplo, administrador y personal autorizado).

### Frontend
- **Componentes Reutilizables**: Desarrollar componentes reutilizables y modulares siguiendo buenas prácticas de desarrollo en ReactJS.
- **Gestión de Estado**: Implementar gestión de estado global utilizando Redux o Context API.
- **Rutas**: Implementar enrutamiento utilizando React Router, asegurando que la navegación sea fluida y que se manejen adecuadamente las rutas protegidas y públicas.
- **Interacción con el Backend**: Realizar llamadas a los endpoints del backend utilizando Axios o Fetch API, manejando adecuadamente las respuestas y errores.
- **Estilos y Responsividad**: Implementar estilos utilizando CSS-in-JS con Styled Components o CSS Modules, asegurando que la aplicación sea visualmente atractiva y responsiva.
- **Pruebas Unitarias y de Integración**: Escribir pruebas unitarias y de integración utilizando Jest y React Testing Library para asegurar que los componentes funcionen correctamente.
- **Guía de Despliegue y Uso**: Proveer una guía clara sobre cómo desplegar y utilizar la aplicación frontend (puede incluirlo en el README).
- **Seguridad**: Implementar medidas de seguridad básicas como la validación de entradas y la protección contra XSS (Cross-Site Scripting).
