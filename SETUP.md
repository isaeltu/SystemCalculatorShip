# 🚀 Guía Completa de Instalación - Sistema de Cálculo de Tarifas

## 📋 Tabla de Contenidos
1. [Requisitos Previos](#requisitos-previos)
2. [Instalación del Proyecto](#instalación-del-proyecto)
3. [Configuración de la Base de Datos](#configuración-de-la-base-de-datos)
4. [Ejecución de la Aplicación](#ejecución-de-la-aplicación)
5. [Verificación de la Instalación](#verificación-de-la-instalación)
6. [Primeros Pasos](#primeros-pasos)

## 📦 Requisitos Previos

### Software Requerido

```bash
# Verificar versiones
.NET --version          # Debe ser 7.0 o superior
dotnet --version        # Alternativa
```

Si no tienes .NET instalado:
1. Ir a https://dotnet.microsoft.com/download
2. Descargar .NET 7 SDK o superior
3. Ejecutar el instalador

### Base de Datos

#### Opción 1: SQL Server LocalDB (Recomendado para desarrollo)
```bash
# En Windows, LocalDB suele venir con Visual Studio
# Verificar si está instalado:
sqllocaldb info

# Si no lo tienes, instalar:
# Descargar SQL Server Express desde:
# https://www.microsoft.com/en-us/sql-server/sql-server-downloads
```

#### Opción 2: SQL Server Express
Descargar desde: https://www.microsoft.com/en-us/sql-server/sql-server-downloads

#### Opción 3: Docker (Alternativa)
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword123!" -p 1433:1433 mcr.microsoft.com/mssql/server:latest
```

### Herramientas Recomendadas

- **Visual Studio 2022** (Community Edition es gratis)
  - https://visualstudio.microsoft.com/vs/
  
- **VS Code** (Alternativa lightweight)
  - https://code.visualstudio.com/
  - Extensiones: C# DevKit, SQL Server
  
- **Git**
  - https://git-scm.com/

## 📥 Instalación del Proyecto

### Paso 1: Clonar el Repositorio
```bash
git clone <repository-url>
cd SystemCalculatorShip
```

### Paso 2: Restaurar Dependencias NuGet
```bash
dotnet restore
```

Este comando descarga todos los paquetes NuGet necesarios (puede tomar 1-2 minutos).

**Output esperado:**
```
Determining projects to restore...
Restored C:\...\SystemCalculatorShip.Domain\SystemCalculatorShip.Domain.csproj
Restored C:\...\SystemCalculatorShip.Application\SystemCalculatorShip.Application.csproj
Restored C:\...\SystemCalculatorShip.Infrastructure\SystemCalculatorShip.Infrastructure.csproj
Restored C:\...\SystemCalculatorShip.Api\SystemCalculatorShip.Api.csproj
```

## 🗄️ Configuración de la Base de Datos

### Paso 1: Verificar la Cadena de Conexión

Abrir `SystemCalculatorShip.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SystemCalculatorShipDb;Trusted_Connection=true;"
  }
}
```

**Opciones de conexión:**

#### LocalDB (Por defecto)
```
Server=(localdb)\mssqllocaldb;Database=SystemCalculatorShipDb;Trusted_Connection=true;
```

#### SQL Server Express
```
Server=.\SQLEXPRESS;Database=SystemCalculatorShipDb;Trusted_Connection=true;
```

#### SQL Server (Instancia completa)
```
Server=your-server-name;Database=SystemCalculatorShipDb;Trusted_Connection=true;
```

#### SQL Server (Con autenticación)
```
Server=your-server-name;Database=SystemCalculatorShipDb;User Id=sa;Password=YourPassword;
```

### Paso 2: Crear la Base de Datos con Migraciones

```bash
# Abrir PowerShell en la carpeta del proyecto

# Navegar a Infrastructure (donde está el DbContext)
cd SystemCalculatorShip.Infrastructure

# Crear la migración inicial
dotnet ef migrations add InitialCreate -s ../SystemCalculatorShip.Api

# Aplicar la migración a la BD
dotnet ef database update -s ../SystemCalculatorShip.Api
```

**Output esperado:**
```
Build started...
Build succeeded.
Done. To undo this action, use 'ef migrations remove'
Applying migration '20240423120000_InitialCreate'.
Done.
```

### Verificar que la BD se creó correctamente

#### En SQL Server Management Studio (SSMS)
1. Conectarse a `(localdb)\mssqllocaldb`
2. Expandir "Databases"
3. Ver `SystemCalculatorShipDb`
4. Expandir y verificar tablas:
   - Countries
   - Tariffs
   - AdminUsers

#### Verificar datos de ejemplo
```sql
-- Conectarse a la BD SystemCalculatorShipDb
SELECT * FROM Countries;
SELECT * FROM Tariffs;
```

Deberías ver 3 países y 9 tarifas pre-cargadas.

## ▶️ Ejecución de la Aplicación

### Opción 1: Desde Visual Studio

1. Abrir `SystemCalculatorShip.sln`
2. Right-click en `SystemCalculatorShip.Api` → "Set as Startup Project"
3. Presionar F5 o hacer clic en "Start"
4. Esperar que se abra el navegador en `https://localhost:7000`

### Opción 2: Desde Terminal

```bash
# Ir a la carpeta del API
cd SystemCalculatorShip.Api

# Ejecutar
dotnet run

# O con especificación de puerto
dotnet run --urls "https://localhost:7000"
```

**Output esperado:**
```
info: SystemCalculatorShip.Api.Program[0]
      Now listening on: https://localhost:7000
info: SystemCalculatorShip.Api.Program[0]
      Application started. Press Ctrl+C to exit.
```

### Acceder a la Aplicación

- **Página Principal**: https://localhost:7000
- **Calculadora**: https://localhost:7000/calculator
- **Login Admin**: https://localhost:7000/login
- **API Swagger**: https://localhost:7000/swagger/index.html

## ✅ Verificación de la Instalación

### Test 1: Verificar Server Corriendo
```bash
curl https://localhost:7000/ -k
# Si funciona, verás HTML de la página principal
```

### Test 2: Test API - Obtener Países
```bash
curl https://localhost:7000/api/countries -k

# Respuesta esperada:
# {
#   "success": true,
#   "data": [
#     { "id": 1, "code": "IN", "name": "India" },
#     { "id": 2, "code": "US", "name": "United States" },
#     { "id": 3, "code": "GB", "name": "United Kingdom" }
#   ]
# }
```

### Test 3: Test API - Calcular Costo
```bash
curl -X POST https://localhost:7000/api/shipping/calculate \
  -H "Content-Type: application/json" \
  -d '{"weight": 2.5, "countryCode": "IN", "tariffType": "standard"}' \
  -k

# Respuesta esperada:
# {
#   "success": true,
#   "data": {
#     "weight": 2.5,
#     "country": "India",
#     "tariffType": "standard",
#     "ratePerKg": 5,
#     "totalCost": 12.5,
#     "currency": "USD"
#   }
# }
```

### Test 4: Test API - Login Admin
```bash
curl -X POST https://localhost:7000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username": "admin", "password": "admin"}' \
  -k

# Respuesta esperada:
# {
#   "success": true,
#   "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
#   "expiresIn": 3600,
#   "message": "Login successful"
# }
```

## 🎯 Primeros Pasos

### Para Usuarios Públicos
1. Acceder a https://localhost:7000
2. Hacer clic en "Calcular Ahora"
3. Seleccionar país, peso y tipo de tarifa
4. Ver el costo total calculado

### Para Administradores
1. Acceder a https://localhost:7000/login
2. Ingrese las credenciales:
   - Usuario: `admin`
   - Contraseña: `admin`
3. Ver el Admin Panel
4. Crear, editar o eliminar países y tarifas

## 🧪 Testing en Postman

### 1. Importar Collection
Crear una nueva collection "Shipping API" con estos requests:

#### GET - Obtener Países
```
GET https://localhost:7000/api/countries
Headers: None requeridos
```

#### POST - Calcular Precio
```
POST https://localhost:7000/api/shipping/calculate
Content-Type: application/json

Body:
{
  "weight": 2.5,
  "countryCode": "IN",
  "tariffType": "standard"
}
```

#### POST - Login
```
POST https://localhost:7000/api/auth/login
Content-Type: application/json

Body:
{
  "username": "admin",
  "password": "admin"
}
```

#### GET - Obtener Tarifas (Requiere Token)
```
GET https://localhost:7000/api/tariffs
Headers:
  Authorization: Bearer {token}
```

## 🛠️ Solución de Problemas Comunes

### Error: "Connection string not found"
```
Causa: appsettings.json no tiene DefaultConnection
Solución: Asegúrate que appsettings.json tiene la cadena de conexión
```

### Error: "LocalDB not installed"
```
Solución 1: Instalar SQL Server Express
Solución 2: Usar Docker (ver arriba)
Solución 3: Cambiar a SQL Server remoto
```

### Error: "Port 7000 already in use"
```
Solución: Usar otro puerto
dotnet run --urls "https://localhost:7001"
```

### Error: "Migration failed"
```
Solución:
1. Eliminar migrations anteriores (si existen)
2. Crear nueva: dotnet ef migrations add InitialCreate
3. Aplicar: dotnet ef database update
```

### Error: "401 Unauthorized"
```
Causa: Token JWT inválido o expirado
Solución: Obtener nuevo token desde /api/auth/login
```

## 📊 Estructura de Carpetas Creada

```
SystemCalculatorShip/
├── SystemCalculatorShip.Domain/          # Entidades y reglas de negocio
├── SystemCalculatorShip.Application/     # Lógica de aplicación
├── SystemCalculatorShip.Infrastructure/  # Acceso a datos
├── SystemCalculatorShip.Api/             # Controllers y configuración
│   ├── Controllers/                      # Endpoints API
│   ├── Pages/                            # Páginas Blazor
│   ├── Services/                         # Servicios Blazor
│   ├── Middleware/                       # Middleware personalizado
│   └── Program.cs                        # Configuración
├── README.md                             # Documentación
└── SETUP.md                              # Guía de instalación (este archivo)
```

## 🎓 Próximos Pasos

Después de una instalación exitosa:

1. **Explorar la Calculadora**: Prueba diferentes países y tarifas
2. **Acceder al Admin**: Login y gestiona datos
3. **Revisar el Código**: Analiza la arquitectura limpia implementada
4. **Leer Documentación**: Ver README.md para detalles
5. **Expandir Funcionalidad**: Agrega nuevos países/tarifas

## 📞 Contacto y Soporte

Si encuentras problemas durante la instalación:
1. Verificar que todos los requisitos están instalados
2. Revisar los logs en la consola
3. Consultar la sección "Troubleshooting"
4. Abrir un issue en el repositorio

---

**¡Instalación exitosa! Ahora puedes comenzar a usar el Shipping Cost Calculator.** 🎉

Para más detalles, consulta [README.md](README.md)
