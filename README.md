# 🌍 Sistema de Calculadora de Tarifas de Envío Internacional

**Calculadora de tarifas de envío de clase mundial construida con C# y Blazor** 🚀

## 📋 ¿Qué es este proyecto?

Este es un **sistema completo de cálculo de tarifas internacionales** que permite a usuarios:
- **Públicos**: Calcular en tiempo real el costo de envíos a diferentes países
- **Administradores**: Gestionar países, tarifas y tipos de envío mediante un panel administrativo

## ✨ Características Principales

### 🌐 Para Usuarios Públicos
- ✅ **Calculadora Simple**: Selecciona país, peso y tipo de tarifa
- ✅ **Cálculo Instantáneo**: Obtén el precio total al instante
- ✅ **Múltiples Opciones**: Standard, Express, Economy, Premium
- ✅ **Interfaz Intuitiva**: Diseñada para facilidad de uso

### 👨‍💼 Para Administradores
- ✅ **Panel Admin Seguro**: Acceso mediante login JWT
- ✅ **Gestión de Países**: CRUD completo (Crear, Leer, Actualizar, Eliminar)
- ✅ **Gestión de Tarifas**: Crear múltiples tarifas por país
- ✅ **Panel Todo en Uno**: Torres para países y tarifas

## 🏗️ Arquitectura del Sistema

```
┌─────────────────────────────────────────────────────────┐
│                   Blazor Frontend                        │
│  (Pages: Home, Calculator, Login, AdminPanel)           │
└────────────────┬────────────────────────────────────────┘
                 │ HTTP/JSON
┌────────────────▼────────────────────────────────────────┐
│              ASP.NET Core API                            │
│  (Controllers: Shipping, Countries, Tariffs, Auth)      │
└────────────────┬────────────────────────────────────────┘
                 │
┌────────────────▼────────────────────────────────────────┐
│          Application Layer (Business Logic)              │
│  - Services: Calculation, Management, Authentication    │
│  - DTOs: Request/Response models                        │
└────────────────┬────────────────────────────────────────┘
                 │
┌────────────────▼────────────────────────────────────────┐
│         Infrastructure Layer (Data Access)               │
│  - Repositories: Countries, Tariffs                     │
│  - DbContext: Entity Framework Core                     │
│  - Database: SQL Server                                 │
└─────────────────────────────────────────────────────────┘
```

## 🗂️ Estructura de Carpetas

```
SystemCalculatorShip/
├── SystemCalculatorShip.Domain/
│   ├── Entities/              # Country, Tariff, AdminUser
│   ├── Validators/            # Weight, Country, TariffType
│   └── Errors/                # Custom exceptions
│
├── SystemCalculatorShip.Application/
│   ├── DTOs/                  # Data Transfer Objects
│   ├── Services/              # Business logic implementations
│   ├── Interfaces/            # Service contracts & Repositories
│   └── Errors/                # Application exceptions
│
├── SystemCalculatorShip.Infrastructure/
│   ├── Persistence/
│   │   ├── Contexts/          # ApplicationDbContext
│   │   └── Repositories/      # Data access implementations
│   └── Interfaces/            # Repository interfaces
│
└── SystemCalculatorShip.Api/
    ├── Controllers/           # API endpoints
    ├── Middleware/            # Error handling
    ├── Pages/                 # Blazor pages
    ├── Services/              # Blazor services
    └── Program.cs             # Configuration
```

## 📦 Entidades y Base de Datos

### 🌎 Tabla: Países (Countries)
```sql
- Id              INT PRIMARY KEY
- Code            VARCHAR(2) UNIQUE      -- Código ISO (e.g., "IN", "US", "GB")
- Name            VARCHAR(100)           -- Nombre del país
- CreatedAt       DATETIME
- UpdatedAt       DATETIME
- IsActive        BIT DEFAULT 1
```

### 💰 Tabla: Tarifas (Tariffs)
```sql
- Id              INT PRIMARY KEY
- CountryId       INT FOREIGN KEY        -- Referencia a Countries
- Type            VARCHAR(50)            -- standard, express, economy, premium
- RatePerKg       DECIMAL(10,2)          -- Precio por kilogramo
- Currency        VARCHAR(3) DEFAULT USD
- CreatedAt       DATETIME
- UpdatedAt       DATETIME
- IsActive        BIT DEFAULT 1
- UNIQUE CONSTRAINT (CountryId, Type)
```

### 👤 Tabla: Usuarios Admin (AdminUsers)
```sql
- Id              INT PRIMARY KEY
- Username        VARCHAR(50) UNIQUE
- PasswordHash    VARCHAR(255)
- Email           VARCHAR(100)
- CreatedAt       DATETIME
- IsActive        BIT DEFAULT 1
```

## 📊 Datos de Ejemplo (Seed Data)

### Países Iniciales
| Código | País             |
|--------|------------------|
| IN     | India            |
| US     | United States    |
| GB     | United Kingdom   |

### Tarifas Iniciales por País

**India:**
- Standard: $5.00/kg
- Express: $7.50/kg
- Economy: $3.00/kg

**United States:**
- Standard: $8.00/kg
- Express: $12.00/kg
- Economy: $5.00/kg

**United Kingdom:**
- Standard: $10.00/kg
- Express: $15.00/kg
- Economy: $6.00/kg

## 🔌 API Endpoints

### 🌐 Públicos (Sin Autenticación)

#### 1. **Calcular Costo de Envío**
```http
POST /api/shipping/calculate
Content-Type: application/json

{
  "weight": 5,
  "countryCode": "IN",
  "tariffType": "express"
}

Respuesta:
{
  "success": true,
  "data": {
    "weight": 5,
    "country": "India",
    "tariffType": "express",
    "ratePerKg": 7.50,
    "totalCost": 37.50,
    "currency": "USD"
  }
}
```

#### 2. **Obtener Todos los Países**
```http
GET /api/countries

Respuesta:
{
  "success": true,
  "data": [
    { "id": 1, "code": "IN", "name": "India" },
    { "id": 2, "code": "US", "name": "United States" },
    { "id": 3, "code": "GB", "name": "United Kingdom" }
  ]
}
```

### 🔐 Autenticación

#### **Login Admin**
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin"
}

Respuesta:
{
  "success": true,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 3600,
  "message": "Login successful"
}
```

### 👨‍💼 Admin (Requieren Token JWT)

#### **Obtener Todas las Tarifas**
```http
GET /api/tariffs
Authorization: Bearer {token}

Respuesta:
{
  "success": true,
  "data": [
    {
      "id": 1,
      "countryId": 1,
      "countryName": "India",
      "type": "standard",
      "ratePerKg": 5.00,
      "currency": "USD"
    }
  ]
}
```

#### **Crear Nuevo País**
```http
POST /api/countries
Authorization: Bearer {token}
Content-Type: application/json

{
  "code": "FR",
  "name": "France"
}

Respuesta: HTTP 201 Created
```

#### **Actualizar País**
```http
PUT /api/countries/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "New Country Name"
}
```

#### **Eliminar País**
```http
DELETE /api/countries/{id}
Authorization: Bearer {token}

Respuesta: HTTP 200 OK
```

#### **Crear Nueva Tarifa**
```http
POST /api/tariffs
Authorization: Bearer {token}
Content-Type: application/json

{
  "countryId": 1,
  "type": "premium",
  "ratePerKg": 15.00
}
```

#### **Actualizar Tarifa**
```http
PUT /api/tariffs/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "type": "express",
  "ratePerKg": 8.50
}
```

#### **Eliminar Tarifa**
```http
DELETE /api/tariffs/{id}
Authorization: Bearer {token}
```

## 🚀 Guía de Instalación

### Requisitos Previos
- [.NET 7+](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB o instancia completa)
- [Visual Studio](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)

### Pasos de Instalación

#### 1. **Clonar el Repositorio**
```bash
git clone <repository-url>
cd SystemCalculatorShip
```

#### 2. **Restaurar Paquetes NuGet**
```bash
dotnet restore
```

#### 3. **Configurar la Base de Datos**

Editar la cadena de conexión en `SystemCalculatorShip.Api/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SystemCalculatorShipDb;Trusted_Connection=true;"
  }
}
```

#### 4. **Crear la Base de Datos (Migraciones)**
```bash
cd SystemCalculatorShip.Infrastructure
dotnet ef database update -s ../SystemCalculatorShip.Api

# O si prefieres crear la migración primero:
dotnet ef migrations add InitialCreate -s ../SystemCalculatorShip.Api
dotnet ef database update -s ../SystemCalculatorShip.Api
```

#### 5. **Ejecutar el API**
```bash
cd SystemCalculatorShip.Api
dotnet run
# O con HTTPS
dotnet run --urls "https://localhost:7000"
```

El API estará disponible en: `https://localhost:7000`

Swagger UI: `https://localhost:7000/swagger`

## 📱 Uso de la Aplicación

### Para Usuarios Públicos

1. **Ir a la página de inicio**: `https://localhost:7000/`
2. **Hacer clic en "Calcular Ahora"**
3. **Ingresar datos**:
   - Seleccionar país de destino
   - Ingresar peso en kg
   - Elegir tipo de tarifa (Standard, Express, Economy)
4. **Hacer clic en "Calcular Costo"**
5. **Ver el costo total en segundos** ✅

### Para Administradores

1. **Ir a login**: `https://localhost:7000/login`
2. **Ingresar credenciales**:
   - Usuario: `admin`
   - Contraseña: `admin`
3. **Panel Admin abre automáticamente**
4. **Gestionar**:
   - Países: Crear, editar, eliminar
   - Tarifas: Crear, editar, eliminar
5. **Hacer clic en "Logout"** para salir

## 🔐 Seguridad

### Autenticación JWT
- ✅ Token de 1 hora de duración
- ✅ Firma con clave secreta de 32+ caracteres
- ✅ Validación en cada request autenticado

### Protección de Rutas
- ✅ Endpoints admin requieren token válido
- ✅ Endpoints públicos accesibles sin autenticación
- ✅ CORS configurado para evitar acceso no autorizado

### Validación de Datos
- ✅ Validación de peso (positivo)
- ✅ Validación de código de país (2 caracteres)
- ✅ Validación de tipo de tarifa (standard, express, economy, premium)

## 🧪 Testing con Postman/Thunder Client

### 1. Calcular Costo de Envío
```bash
POST http://localhost:7000/api/shipping/calculate
Content-Type: application/json

{
  "weight": 2.5,
  "countryCode": "IN",
  "tariffType": "standard"
}
```

**Resultado Esperado**: `$12.50` (2.5 × $5.00)

### 2. Obtener Token de Admin
```bash
POST http://localhost:7000/api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin"
}
```

### 3. Crear Nuevo País (Con Token)
```bash
POST http://localhost:7000/api/countries
Authorization: Bearer {token_obtenido}
Content-Type: application/json

{
  "code": "MX",
  "name": "Mexico"
}
```

### 4. Crear Tarifa para México
```bash
POST http://localhost:7000/api/tariffs
Authorization: Bearer {token_obtenido}
Content-Type: application/json

{
  "countryId": 4,
  "type": "standard",
  "ratePerKg": 6.50
}
```

## 🎨 Componentes de la Interfaz

### Pages (Páginas Blazor)
- **Index.razor** - Página de inicio con instrucciones
- **Calculator.razor** - Calculadora pública de tarifas
- **Login.razor** - Formulario de login para administradores
- **AdminPanel.razor** - Panel de control administrativo

### Admin Subpages
- **Countries.razor** - Gestión de países (CRUD)
- **Tariffs.razor** - Gestión de tarifas (CRUD)

### Services (Servicios Blazor)
- **ServiceClient** - Client HTTP para comunicación con API

## 📝 Flujo de la Aplicación

### Usuario Público - Calcular Costo

```
1. Usuario accede a /calculator
          ↓
2. Página carga lista de países
   (GET /api/countries)
          ↓
3. Usuario selecciona país, peso y tarifa
          ↓
4. Hace clic en "Calcular Costo"
          ↓
5. API recibe request en POST /api/shipping/calculate
          ↓
6. Validación:
   - Peso > 0 ✓
   - País existe ✓
   - Tarifa existe para ese país ✓
          ↓
7. Cálculo: totalCost = weight × ratePerKg
          ↓
8. Respuesta con costo total
          ↓
9. Usuario ve resultado en pantalla ✅
```

### Administrador - Crear Tarifa

```
1. Administrador accede a /login
          ↓
2. Ingresa credenciales (admin/admin)
          ↓
3. POST /api/auth/login valida y retorna JWT token
          ↓
4. Token se guarda en memoria/localStorage
          ↓
5. Redirige a /admin
          ↓
6. Hace clic en tab "Tariffs"
          ↓
7. Carga lista de tarifas
   (GET /api/tariffs con Bearer token)
          ↓
8. Selecciona país, tipo, y rate
          ↓
9. Hace clic en "Add Tariff"
          ↓
10. POST /api/tariffs (con Bearer token)
           ↓
11. Validación:
    - Token válido ✓
    - País existe ✓
    - Tarifa no existe para este país ✓
    - Rate positivo ✓
           ↓
12. Crea registro en BD
           ↓
13. Retorna 201 Created
           ↓
14. Lista se actualiza ✅
```

## 🛠️ Troubleshooting

### Problema: "Connection string not found"
**Solución**: Verificar `appsettings.json` y asegurarse que SQL Server está corriendo

### Problema: "Migration failed"
**Solución**: 
```bash
# Remover migraciones anteriores
Remove-Migration

# Crear nueva migración
Add-Migration InitialCreate

# Actualizar BD
Update-Database
```

### Problema: "401 Unauthorized en endpoints admin"
**Solución**: Asegurarse de incluir el token en el header:
```
Authorization: Bearer {token}
```

### Problema: "CORS error"
**Solución**: CORS ya está configurado en `Program.cs`. Si aún así tienes problemas, verificar la configuración.

## 📚 Tecnologías Utilizadas

### Backend
- ✅ **ASP.NET Core 7+** - Framework web
- ✅ **Entity Framework Core** - ORM
- ✅ **SQL Server** - Base de datos
- ✅ **JWT** - Autenticación
- ✅ **Swagger/OpenAPI** - Documentación API

### Frontend
- ✅ **Blazor WebAssembly** - Framework SPA
- ✅ **Bootstrap 5** - Estilos CSS
- ✅ **HTTP Client** - Comunicación API

### Arquitectura
- ✅ **Clean Architecture** - Separación de capas
- ✅ **SOLID Principles** - Código limpio
- ✅ **Dependency Injection** - IoC
- ✅ **Repository Pattern** - Abstracción de datos

## 📈 Posibles Mejoras Futuras

- [ ] Autenticación con BD real (no hardcoded)
- [ ] Editar y actualizar entidades completamente
- [ ] Dashboard con estadísticas
- [ ] Búsqueda y filtrado avanzado
- [ ] Exportar reportes (PDF, Excel)
- [ ] Multi-moneda
- [ ] Historial de cálculos
- [ ] Notificaciones en tiempo real
- [ ] Tests unitarios completos
- [ ] Docker containerization

## 👥 Contribuyentes

Este proyecto fue creado como demostración de arquitectura limpia en C#.

## 📄 Licencia

Este proyecto está bajo licencia MIT. Ver `LICENSE` file.

## 📞 Soporte

Para reportar bugs o sugerencias, por favor abrir un issue en el repositorio.

---

## ¡Gracias por usar Shipping Cost Calculator! 🎉

**¡Deseamos que disfrutes la aplicación!**

Cualquier pregunta, no dudes en contactar. 

✨ **¡Feliz envío!** ✨