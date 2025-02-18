# willinn-backend-template

RESTful API Willinn Backen API Template desarrollada en .NET 8 para una sencilla gestión de usuarios. Como parte de la
prueba técnica para Trainee, se desarrollo un CRUD para la gestion de los mismos, ademas de aplicar los principios de
**Clean Architecture**, gestion de Tokens con JWT, Seeder para data inicial en desarrollo y docker-compose para fácil
instanciación.

## Visión General de Clean Architecture

**Clean Architecture** separa la lógica de la aplicación en capas distintas, lo que mejora la mantenibilidad,
testabilidad y portabilidad del sistema:

- **Capa de Presentación**: Maneja las interacciones del usuario y la presentación (por ejemplo, controladores API).
- **Capa de Aplicación**: Contiene la lógica de negocio central y se encarga de interactuar con la capa de dominio.
- **Capa de Dominio**: Representa las entidades centrales y las reglas de negocio de la aplicación.
- **Capa de Persistencia**: Gestiona el acceso y almacenamiento de los datos (por ejemplo, Entity Framework Core).

Esta separación mejora la escalabilidad y facilita las pruebas unitarias de cada capa de manera independiente.

## Estructura del Proyecto

El proyecto sigue la estructura de **Clean Architecture**, con cada capa representada por una carpeta separada:

- **Api**: Contiene el código de la capa de presentación, incluyendo los controladores de la API.
- **Core**: Contiene la lógica de negocio de la aplicación, interfaces y reglas de la capa de aplicación.
- **Data**: Contiene el código de la capa de persistencia, incluyendo el acceso a datos y configuraciones de Entity
- Framework Core.
- **Services**: Contiene los servicios de la aplicación que median entre las capas de aplicación y persistencia.

## Ejecución de la Aplicación

### Requisitos Previos

- **.NET 8 SDK** ([Descargar .NET](https://dotnet.microsoft.com/en-us/download/dotnet/8.0))
- **Docker Desktop** ([Descargar Docker](https://www.docker.com/)) (opcional, si prefieres usar Docker Compose)
1. Clona el repositorio:

   ```bash
   git clone https://github.com/jgnacio/Willinn-backend-api-template.git
    ```
2. Abre una terminal en el directorio del proyecto.

### Con Docker Compose

* Ejecuta el siguiente comando para iniciar la aplicación y la base de datos:
    * Para Desarrollo:
   ```bash
   docker-compose -f docker-compose-develop.yml up --build
   ```
   * Para Producción:
   ```bash
   docker-compose up --build
   ``` 
   
#### Acceso a la API (Contenedor):
```bash
http://localhost:5001
```
#### Swagger (Contenedor):
```bash
  http://localhost:5001/swagger/index.html
  ```

### Con .NET SDK
1. Restaura los paquetes NuGet:
```bash
dotnet restore
```
2. Construye la aplicación:
```bash
dotnet build
```
3. Ejecuta la aplicación:
```bash
dotnet run
```
#### Acceso a la API:
```bash
http://localhost:8081
```

- Asegurate de no tener ese puerto ocupado

### Usuario de Prueba
En el entorno de desarrollo se proporciona un usuario de prueba para hacer peticiones:
```bash
User="WillinUserGuest"
Email="WillinnGuest@gmail.com"
Passwd="Guest"
```

