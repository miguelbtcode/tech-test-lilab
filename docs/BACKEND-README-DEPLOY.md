# Despliegue del Proyecto con Docker 🐳

Este proyecto utiliza Docker Compose para gestionar los servicios de la aplicación .NET 9.0 y MySQL 8.0. A continuación, las instrucciones para levantar el entorno.

## 📋 Prerrequisitos

- Docker 20.10+
- Docker Compose 2.20+
- Git (opcional)

## 🚀 Pasos de Implementación

### 1. Clonar el repositorio

```bash
git clone https://github.com/miguelbtcode/tech-test-lilab.git
cd repositorio-clonado
```

# Variables de MySQL

MYSQL_ROOT_PASSWORD=rootpassword
MYSQL_DATABASE=miappdb
MYSQL_USER=miappuser
MYSQL_PASSWORD=miapppassword

# Puerto de la API

API_PORT=8080

# Construir e iniciar servicios

docker-compose up --build -d

# Estructura del proyecto

├── db/
│ └── mysql-init.sql # Script de inicialización de BD
├── src/
│ ├── Presentation/ # Capa de presentación
│ │ ├── Dockerfile # Configuración Docker de la API
│ │ └── ...
│ ├── Infrastructure/ # Configuración de la base de datos
│ └── Core/ # Lógica de negocio
├── docker-compose.yml # Orquestación de servicios

## API .NET

## Puerto: 5006 → 8080

### Variables de entorno:

```bash
ConnectionStrings__DefaultConnection="Server=mysql;Database=miappdb;User=miappuser;Password=miapppassword;Pooling=True;Max Pool Size=100;Connection Timeout=60;"
cd repositorio-clonado
```

### Migraciones: Se aplican automáticamente al iniciar

# MySQL

### Versión: 8.0

### Puerto: 3306 (interno) → 3306 (local)

# Volúmenes:

## mysql_data: Datos persistentes

## mysql-init.sql: Script de inicialización
