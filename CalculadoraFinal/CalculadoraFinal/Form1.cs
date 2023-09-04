using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraFinal
{
    public partial class Form1 : Form
    {
        // Declaración de variables
        double Numero1 = 0, Numero2 = 0; // Almacenan los números para las operaciones
        char Operador; // Almacena el operador seleccionado
        string operacionActual = ""; // Almacena la operación actual

        public Form1()
        {
            InitializeComponent();
        }

        // Evento cuando se hace clic en un número
        private void agregarNumero(object sender, EventArgs e)
        {
            var boton = ((Button)sender);

            if (txtResultado.Text == "0")
            {
                txtResultado.Text = "";
            }

            txtResultado.Text += boton.Text;

            // Agregar el número a la operación actual
            operacionActual += boton.Text;
            operaciones.Text = operacionActual;
        }

        // Evento cuando se hace clic en el botón de cero
        private void btnCero_Click(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            Numero1 = Convert.ToDouble(txtResultado.Text);
            Operador = Convert.ToChar(boton.Text);

            operaciones.Text = Numero1 + " " + Operador;
        }

        // Evento cuando se hace clic en el botón de resultado
        private void btnResultado_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtResultado.Text, out Numero2))
            {
                // Realizar operaciones según el operador seleccionado
                if (Operador == '+')
                {
                    txtResultado.Text = (Numero1 + Numero2).ToString();
                    Numero1 = Convert.ToDouble(txtResultado.Text);
                }
                else if (Operador == '-')
                {
                    txtResultado.Text = (Numero1 - Numero2).ToString();
                    Numero1 = Convert.ToDouble(txtResultado.Text);
                }
                else if (Operador == 'X')
                {
                    txtResultado.Text = (Numero1 * Numero2).ToString();
                    Numero1 = Convert.ToDouble(txtResultado.Text);
                }
                else if (Operador == '/')
                {
                    if (Numero2 != 0)
                    {
                        txtResultado.Text = (Numero1 / Numero2).ToString();
                        Numero1 = Convert.ToDouble(txtResultado.Text);
                    }
                    else
                    {
                        MessageBox.Show("No se puede dividir por cero");
                    }
                }

                // Limpiar la operación actual después de la operación
                operacionActual = "";
                operaciones.Text = "";
            }
            else
            {
                MessageBox.Show("Entrada no válida. Asegúrate de ingresar un número válido.");
            }
        }

        // Evento cuando se hace clic en el botón de borrar
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (txtResultado.Text.Length > 1)
            {
                txtResultado.Text = txtResultado.Text.Substring(0, txtResultado.Text.Length - 1);
            }
            else
            {
                txtResultado.Text = "0";
            }
        }

        // Evento cuando se hace clic en el botón de borrar todo
        private void btnBorrarTodo_Click(object sender, EventArgs e)
        {
            Numero1 = 0;
            Numero2 = 0;
            Operador = '\n';
            txtResultado.Text = "0";
            operacionActual = "";
            operaciones.Text = "";
        }

        // Evento cuando se hace clic en el botón de quitar
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            txtResultado.Text = "0";
        }

        // Evento cuando se hace clic en el botón de punto decimal
        private void btnPunto_Click(object sender, EventArgs e)
        {
            // Si no tiene el punto, agregarlo
            if (!txtResultado.Text.Contains("."))
            {
                txtResultado.Text += ".";
            }
        }

        // Evento cuando se hace clic en el botón de cambiar signo
        private void btnSigno_Click(object sender, EventArgs e)
        {
            // Cambiar el signo del número actual
            Numero1 = Convert.ToDouble(txtResultado.Text);
            Numero1 *= -1;
            txtResultado.Text = Numero1.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Evento de presionar tecla en el cuadro de resultado
        private void txtResultado_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, el punto decimal y la tecla de retroceso (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true; // Bloquear la entrada del carácter no válido
            }

            // Permitir solo un punto decimal
            if ((e.KeyChar == '.') && (txtResultado.Text.IndexOf('.') > -1))
            {
                e.Handled = true; // Bloquear el segundo punto decimal
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Evento cuando se hace clic en un operador especial
        private void clickOperador(object sender, EventArgs e)
        {
            var boton = ((Button)sender);
            Numero1 = Convert.ToDouble(txtResultado.Text);
            Operador = Convert.ToChar(boton.Tag);

            // Realizar operaciones especiales (elevar al cuadrado, raíz cuadrada)
            if (Operador == '²')
            {
                Numero1 = Math.Pow(Numero1, 2);
                txtResultado.Text = Numero1.ToString();
            }
            else if (Operador == '√')
            {
                Numero1 = Math.Sqrt(Numero1);
                txtResultado.Text = Numero1.ToString();
            }
            else
            {
                txtResultado.Text = "0";
            }

            // Mostrar la operación en el label "operaciones"
            operacionActual = Numero1 + " " + Operador;
            operaciones.Text = operacionActual;
        }
    }
}
