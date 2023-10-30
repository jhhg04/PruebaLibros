# PruebaLibros
1. Aplicacion WEB realizada en NET core 7 - MVC
2. API realizada en NET core 7
3. Se debe actualizar la conexion a la base de datos en el archivo appsettings.json del proyecto PruebaLibros.Presentacion.ApiREST
4. Se debe crear una BD en SQL SERVER de nombre ApiTest, las tablas se crean automaticamente por medio de la migracion realiazada por Entity Framework
5. El API regsitra logs de enventos y/o los incluidos por codigo en la ubicacion logs/log-system-xxxx.txt del proyecto API.
6. Los valores de entrada se validan a traves de fluentValidation.
7. Se usa estilos visuales sobre Bootstrap 5
8. Se uso plugin de JQuery DataTable

Patrones de desarrollo de software y complementos de mejores practicas de desarrollo utilizados
1. UnitOfWork
2. Specification
3. IConfig
4. AutoMapper
5. Inyeccion de dependencias
6. FluentValidation
7. Middleware para control de errores
8. Repositorio generico para acceso a bases de datos
9. Repositorio generico para peticiones WEB HttpClient
10. Serilog para manejo de logs a nivel de arhivos de texto
11. CodeFirst en Entity Framework
