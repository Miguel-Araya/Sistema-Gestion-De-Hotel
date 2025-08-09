
# Sistema de Gestión Hotelera

Sistema integral para la gestión de un hotel que consta de dos subsistemas principales:

- **Sitio Público:**  
  Página para que los clientes conozcan el hotel, sus servicios, promociones y facilidades, con la capacidad de realizar reservaciones.

- **Módulo Administrativo:**  
  Panel para que los administradores gestionen el contenido del sitio público (textos, imágenes), administren habitaciones, tarifas, temporadas, disponibilidad, y controlen el acceso mediante login/logout.

---

## Funcionalidades Principales

### Sitio Público
- Página de inicio con descripción general del hotel y navegación fácil.
- Visualización de ofertas y promociones destacadas.
- Galería con fotos de las instalaciones y alrededores.
- Información sobre precios por tipo de habitación, incluyendo temporadas.
- Ubicación con mapa interactivo (Google Maps).
- Formulario para realizar reservaciones especificando fechas y tipo de habitación.
- Información de contacto visible para el cliente.

### Módulo Administrativo
- Edición de textos e imágenes de las páginas públicas (Inicio, Sobre Nosotros, Facilidades, Cómo llegar).
- Gestión CRUD de habitaciones (descripción, tarifa, imágenes), temporadas y ofertas.
- Visualización y reporte del estado y disponibilidad diaria de habitaciones.
- Autenticación segura con login y logout para administradores.
- Navegación intuitiva para facilitar la administración.

---

## Tecnologías

- Backend: .NET Core 8  
- Frontend: Angular y Tailwind CSS  
- Base de datos: SQL Server con Entity Framework para migraciones y consultas vía Dapper.  
- Autenticación: JWT con Identity  
- Otros: Google Maps API para ubicación

---

## Configuración Requerida

### Frontend

Editar el archivo `src/app/environments/environment.ts` y completar con la siguiente información:

```typescript
export const environment = {
  production: false,
  cloudName: '',
  uploadPreset: ''
};
````

> Se requiere una cuenta en **Cloudinary** para el procesamiento y almacenamiento de imágenes.
> Completar `cloudName` y `uploadPreset` con los datos correspondientes de tu cuenta Cloudinary.

### Backend (Auth)

Editar el archivo de configuración `HotelAuth.API/HotelAuth/appsettings.json` y completar la sección JWT con tus datos:

```json
{
  "Jwt": {
    "Secret": "",
    "Issuer": "",
    "Audience": ""
  }
}
```

> Estos valores son necesarios para la configuración de la autenticación mediante JWT.

---

## Instalación y Ejecución

1. Clonar repositorio
2. Configurar base de datos y variables de entorno
3. Ejecutar backend (`dotnet run` o desde Visual Studio)
4. Ejecutar frontend (`npm install` y `ng serve`)
5. Acceder al sitio público en `http://localhost:xxxx` y al panel administrativo en `http://localhost:xxxx/login` (ajustar puertos)
