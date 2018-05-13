using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaAccesoDatos;
using CapaLogicaNegocios;

namespace CapaPresentacion
{
    public partial class FrmPagoVenta : Form
    {
        double totalAPagar;
        public FrmPagoVenta(double totalAPagar)
        {
            this.totalAPagar = totalAPagar;
            InitializeComponent();
        }

        private void FrmPagoVenta_Load(object sender, EventArgs e)
        {
            txtTotalAPagar.Text = totalAPagar.ToString();
            

        }

        private void realizarVenta()
        {

            if (MessageBox.Show("Realizar venta?", "Continuar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if(cmbTipoPago.Text.Equals("Efectivo-Tarjeta"))
                {
                    double suma = Convert.ToDouble(textBox1.Text) + Convert.ToDouble(textBox2.Text);
                    txtCantidad.Text = suma.ToString();
                }

                double cantidadEntregada = Convert.ToDouble(txtCantidad.Text);
                if (cantidadEntregada < totalAPagar)
                {
                    MessageBox.Show("El efectivo ingresado no es suficiente");
                }
                else if (cmbTipoPago.Text.Equals(""))
                {
                    MessageBox.Show("Favor de seleccionar el metodo de pago");
                }
                else
                {
                    if (cmbTipoPago.Text.Equals("Efectivo"))
                    {
                        Login.tipoPago = 0;
                    }
                    else if (cmbTipoPago.Text.Equals("Tarjeta"))
                    {
                        Login.tipoPago = 1;
                    }

                    else if(cmbTipoPago.Text.Equals("Efectivo-Tarjeta"))
                    {
                        Login.tipoPago = 2;
                    }

                    double cambio = cantidadEntregada - totalAPagar;
                    MessageBox.Show("Cambio: " + cambio.ToString());
                    Login.Pago = true;
                    this.Hide();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            realizarVenta();
        }

        private void cmbTipoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string textoSeleccionado = cmbTipoPago.SelectedItem.ToString();
            if (  textoSeleccionado.Equals("Efectivo-Tarjeta")  )
            {
                HabilitaControls_PagoEfectYTarjeta();
                textBox1.Clear();
                textBox2.Clear();
                txtCantidad.Clear();
                txtCantidad.Enabled = false;
                recibio.Enabled = false;
            }

            else
            {  //Dejar en el estado inicial al Form
                DesHabilitaControls_PagoEfectYTarjeta();
                textBox1.Clear();
                textBox2.Clear();
                txtCantidad.Clear();
                txtCantidad.Enabled = true;
                recibio.Enabled = true;
            }
        }

        private void cmbTipoPago_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == (int)Keys.Enter)
            {
                realizarVenta();
            }
        }



        private void HabilitaControls_PagoEfectYTarjeta()
        {
            label2.Enabled = true;
            textBox1.Enabled = true;
            label4.Enabled = true;
            textBox2.Enabled = true;
        }

        private void DesHabilitaControls_PagoEfectYTarjeta()
        {
            label2.Enabled = false;
            textBox1.Enabled = false;
            label4.Enabled = false;
            textBox2.Enabled = false;
        }
    }
}
