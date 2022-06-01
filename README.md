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

Cada microservicio tendrá una BD propia.

## Encapsulamiento

Se crearán tres gateway, uno para Catalog, Customer y Order Api.
