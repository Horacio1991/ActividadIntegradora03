using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ListaProductos listaProductos = new ListaProductos();

        public MainWindow()
        {
            InitializeComponent();

            // Agregar productos de ejemplo
            listaProductos.AgregarProducto(new Producto { Id = 1, Descripcion = "Play Station 5", Precio = 999999, Stock = 100 });
            listaProductos.AgregarProducto(new Producto { Id = 2, Descripcion = "XBOX ONE", Precio = 869999, Stock = 50 });

            // Vincular la lista de productos con la DataGrid
            //dgProductos.ItemsSource = listaProductos;
            ActualizarDataGrid();
        }

        private void ActualizarDataGrid()
        {
            var productos = new List<Producto>();
            foreach (var producto in listaProductos)
            {
                productos.Add(producto);
            }
            dgProductos.ItemsSource = productos;
            dgProductos.Items.Refresh();
        }

        // Boton para agregar producto
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                // Validar y convertir los valores de los TextBox
                if (int.TryParse(txtId.Text, out int id) &&
                    decimal.TryParse(txtPrecio.Text, out decimal precio) &&
                    int.TryParse(txtStock.Text, out int stock) &&
                    !string.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    // Crear un nuevo producto
                    Producto nuevoProducto = new Producto
                    {
                        Id = id,
                        Descripcion = txtDescripcion.Text,
                        Precio = precio,
                        Stock = stock
                    };

                    // Agregar el producto a la lista
                    listaProductos.AgregarProducto(nuevoProducto);

                    // Actualizar la DataGrid
                    //dgProductos.Items.Refresh();
                    ActualizarDataGrid();

                    // Limpiar los TextBox
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese valores válidos en todos los campos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}");
            }
        }

        // boton para borrar un producto
        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verificar si hay un producto seleccionado
                if (dgProductos.SelectedItem is Producto selectedProducto)
                {
                    // Eliminar el producto de la lista
                    listaProductos.EliminarProducto(selectedProducto);

                    // Actualizar la DataGrid
                    //dgProductos.Items.Refresh();
                    ActualizarDataGrid();
                }
                else
                {
                    MessageBox.Show("Seleccione un producto para eliminar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el producto: {ex.Message}");
            }
        }


        // Boton para modificar un producto
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgProductos.SelectedItem is Producto selectedProducto)
                {
                    if (int.TryParse(txtId.Text, out int id) &&
                        decimal.TryParse(txtPrecio.Text, out decimal precio) &&
                        int.TryParse(txtStock.Text, out int stock) &&
                        !string.IsNullOrWhiteSpace(txtDescripcion.Text))
                    {
                        Producto productoModificado = new Producto
                        {
                            Id = id,
                            Descripcion = txtDescripcion.Text,
                            Precio = precio,
                            Stock = stock
                        };

                        listaProductos.ModificarProducto(productoModificado);
                        ActualizarDataGrid();
                        ClearTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese valores válidos en todos los campos.");
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un producto para modificar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar el producto: {ex.Message}");
            }
        }

        // para mejor usabilidad, al hacer click en un producto de la grid, se completan los campos de los textBox
        private void dgProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verificar si hay un producto seleccionado en la DataGrid
            if (dgProductos.SelectedItem is Producto selectedProducto)
            {
                // Mostrar los datos del producto seleccionado en los TextBox
                txtId.Text = selectedProducto.Id.ToString();
                txtDescripcion.Text = selectedProducto.Descripcion;
                txtPrecio.Text = selectedProducto.Precio.ToString();
                txtStock.Text = selectedProducto.Stock.ToString();
            }
            else
            {
                // Limpiar los texbox si no hay producto seleccionado
                ClearTextBoxes();
            }
        }

        // Limpiar los textBoxs
        private void ClearTextBoxes()
        {
            txtId.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }


    }
}
