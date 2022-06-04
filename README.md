# KODOTI Commerce - Práctica para implementación de microservicios en .NET

Código para practicar la implementación de microservicios. Se usa el curso [Curso Microservicios Net Core 3](https://www.youtube.com/playlist?list=PLNBxBZe3xfYzjwnUgCkht9CNQnwPoRsHp) del usuario [Programación en PHP](https://www.youtube.com/channel/UCNtqjVe11gDAd_NZAqK4JPA/playlists) en [YouTube](https://www.youtube.com)

## Detalle de microservicios

- Identity.Api
  - Usuarios
  - Autenticacion
- Catalog.Api
  - Productos
  - Stock
- Customer.Api
  - Clientes
- Order.Api
  - Órdenes de compra
  - Detalle de órdenes

## Persistencia

Cada microservicio debería tener una BD propia. Para simplificarlo se usará una misma base de datos pero se añadirán schema propios para cada microservicio.

### Migraciones con CLI

> NOTA: Los comandos se ejecutan en la terminal y ubicado en la ruta del proyecto de persistencia.

- Crear Migración: `dotnet ef migrations add Initialize -s ../AAAAAAA.Api`
- Aplicar migraciones: `dotnet ef database update -s ../AAAAAAA.Api`

Información adicional:
- [Microsoft Docs: Descripción general de las migraciones](https://docs.microsoft.com/es-es/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
- [Microsoft Docs: Referencia de herramientas de Entity Framework Core: CLI de .NET Core](https://docs.microsoft.com/es-es/ef/core/cli/dotnet)

## Encapsulamiento

Se crearán tres gateway, uno para Catalog, Customer y Order Api.

## Eventos

Se usará MediatR como mediador para lanzar los comandos. Se enviarán a PaperTrail.
