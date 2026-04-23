# 🏃 QUICK START - Iniciar en 5 Minutos

## 📋 Requisitos Mínimos
- .NET 7+ SDK
- SQL Server LocalDB (incluido en Visual Studio)

## ⚡ Iniciar Rápido

### 1. Preparar Entorno (2 min)
```bash
# Restaurar paquetes
dotnet restore
```

### 2. Base de Datos (2 min)
```bash
# Crear BD y tablas
cd SystemCalculatorShip.Infrastructure
dotnet ef database update -s ../SystemCalculatorShip.Api
cd ..
```

### 3. Ejecutar (1 min)
```bash
# Ir a API
cd SystemCalculatorShip.Api

# Ejecutar
dotnet run
```

### 4. ¡Accede a la App!
- 🏠 **Home**: https://localhost:7000
- 📱 **Calculadora**: https://localhost:7000/calculator
- 🔐 **Admin**: https://localhost:7000/login
  - User: `admin`
  - Pass: `admin`

---

## 🧪 Probar Rápidamente

### Opción 1: Interfaz Gráfica
1. Ir a https://localhost:7000/calculator
2. Seleccionar país: **India**
3. Peso: **2.5** kg
4. Tipo: **Express**
5. Hacer clic: **Calcular Costo**
6. Ver resultado: **$18.75** ✅

### Opción 2: API con cURL
```bash
curl -X POST https://localhost:7000/api/shipping/calculate \
  -H "Content-Type: application/json" \
  -d '{"weight": 2.5, "countryCode": "IN", "tariffType": "express"}' \
  -k

# Respuesta: {"success": true, "data": {"totalCost": 18.75, ...}}
```

### Opción 3: Swagger
- Acceder a: https://localhost:7000/swagger
- Probar endpoints directamente en la UI

---

## 📌 Principales Puntos de Acceso

| URL | Descripción |
|-----|-------------|
| `https://localhost:7000` | Página de inicio |
| `https://localhost:7000/calculator` | Calculadora pública |
| `https://localhost:7000/login` | Login administrador |
| `https://localhost:7000/admin` | Panel administrativo |
| `https://localhost:7000/swagger` | Documentación API |

---

## 🔑 Credenciales de Prueba

| Campo | Valor |
|-------|-------|
| Usuario | `admin` |
| Contraseña | `admin` |

---

## 💾 Datos Pre-cargados

### Países disponibles
- India (IN)
- United States (US)
- United Kingdom (GB)

### Tarifas por país
**India:**
- Standard: $5.00/kg
- Express: $7.50/kg
- Economy: $3.00/kg

**USA:**
- Standard: $8.00/kg
- Express: $12.00/kg
- Economy: $5.00/kg

**UK:**
- Standard: $10.00/kg
- Express: $15.00/kg
- Economy: $6.00/kg

---

## 🆘 Problemas Comunes

### "Port 7000 already in use"
```bash
dotnet run --urls "https://localhost:7001"
```

### "Connection string error"
Verificar: `SystemCalculatorShip.Api/appsettings.json`
```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SystemCalculatorShipDb;Trusted_Connection=true;"
```

### "Migration failed"
```bash
cd SystemCalculatorShip.Infrastructure
dotnet ef database drop -s ../SystemCalculatorShip.Api
dotnet ef database update -s ../SystemCalculatorShip.Api
```

---

## 📚 Leer Después

1. **README.md** - Documentación completa
2. **SETUP.md** - Instalación detallada
3. **IMPLEMENTATION_SUMMARY.md** - Resumen técnico

---

## ✨ Funciones Clave

### Para Usuarios
- ✅ Calcular costos de envío instantáneamente
- ✅ Seleccionar entre múltiples tarifas
- ✅ Interfaz intuitiva y responsiva

### Para Administradores
- ✅ Crear/Editar/Eliminar países
- ✅ Crear/Editar/Eliminar tarifas
- ✅ Gestión segura con JWT
- ✅ Panel unificado

---

## 🎯 Próximos Pasos

1. **Explorar el código**: Ver la arquitectura limpia
2. **Agregar más países**: Panel admin
3. **Crear nuevas tarifas**: Prueba el CRUD
4. **Revisar API**: Swagger docs
5. **Extender funciones**: Agregar más features

---

**¡Listo para comenzar! Cualquier pregunta, revisar los archivos MD.** 🚀
