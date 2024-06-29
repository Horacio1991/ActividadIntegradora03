using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WpfApp4;

public class ListaProductos : IEnumerable<Producto>
{
    private List<Producto> productos = new List<Producto>();

    // metodo que agrega un producto a la lista
    public void AgregarProducto(Producto producto)
    {
        productos.Add(producto);
    }

    // metodo para eliminar un producto de la lista
    public void EliminarProducto(Producto producto)
    {
        productos.Remove(producto);
    }

    // metodo que modifica un producto de la lista
    public void ModificarProducto(Producto productoModificado)
    {
        // Buscar el producto por su Id
        Producto productoExistente = productos.FirstOrDefault(p => p.Id == productoModificado.Id);

        // Verifica si el producto existe
        if (productoExistente != null)
        {
            // Actualizar las propiedades del producto existente
            productoExistente.Descripcion = productoModificado.Descripcion;
            productoExistente.Precio = productoModificado.Precio;
            productoExistente.Stock = productoModificado.Stock;
        }
        // si no existe lanza un error
        else
        {
            throw new InvalidOperationException("Producto no encontrado para modificar.");
        }
    }

    
    public IEnumerator<Producto> GetEnumerator()
    {
        return productos.GetEnumerator();
    }

  
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
