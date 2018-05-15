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
        public double cantidadAPagarEnEfectivo;
        public double cantidadAPagarEnTarjeta;


        public FrmPagoVenta(double totalAPagar)
        {
            this.totalAPagar = totalAPagar;
            InitializeComponent();

            this.cantidadAPagarEnEfectivo = 0.0;
            this.cantidadAPagarEnTarjeta = 0.0;
            DeshabilitarLabelsPagoTarjetaYEfectivo();
            DeshabilitaTextBoxsPagoTarjetaYEfectivo();
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
                    Login.tipoPago = 2;
                    Login.Pago = true;
                    this.Hide();
                    this.cantidadAPagarEnTarjeta = Convert.ToDouble(textBox1.Text);
                    this.cantidadAPagarEnEfectivo = Convert.ToDouble(textBox2.Text);
                }

                else
                {
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

                        double cambio = cantidadEntregada - totalAPagar;
                        MessageBox.Show("Cambio: " + cambio.ToString());
                        Login.Pago = true;
                        this.Hide();

                    }
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
                DeshabilitaLimpiaTxtBoxYLabel_Recibio();
                HabilitarLabelsPagoTarjetaYEfectivo();
                LimpiarTextBoxsPagoTarjetaYEfectivo();

                FrmPagoVentaEfectivoTarjeta frmPagoVentaEfectivoTarjeta = new FrmPagoVentaEfectivoTarjeta(totalAPagar);
                frmPagoVentaEfectivoTarjeta.ShowDialog(this);

                textBox1.Text = frmPagoVentaEfectivoTarjeta.CantidadTomadaDeTarjeta.ToString();
                textBox2.Text = frmPagoVentaEfectivoTarjeta.CantidadTomadaDeEfectivo.ToString();

                //Examino para ver si no cero el form al hacer click en la X
                if(    (frmPagoVentaEfectivoTarjeta.CantidadTomadaDeTarjeta == 0.0) && (frmPagoVentaEfectivoTarjeta.CantidadTomadaDeEfectivo== 0.0)    )
                {
                    frmPagoVentaEfectivoTarjeta.Dispose();
                    this.Dispose();
                }


                frmPagoVentaEfectivoTarjeta.Dispose();


                
                
            }

            else
            {
                //Volver el form al estado inicial
                recibio.Enabled = true;
                txtCantidad.Clear();
                txtCantidad.Enabled = true;
                DeshabilitarLabelsPagoTarjetaYEfectivo();
                LimpiarTextBoxsPagoTarjetaYEfectivo();
                DeshabilitaTextBoxsPagoTarjetaYEfectivo();

                this.cantidadAPagarEnTarjeta = 0.0;
                this.cantidadAPagarEnEfectivo = 0.0;
            }
        }

        private void cmbTipoPago_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == (int)Keys.Enter)
            {
                realizarVenta();
            }
        }


        private void HabilitarLabelsPagoTarjetaYEfectivo()
        {
            label2.Enabled = true;
            label4.Enabled = true;
        }

        private void LimpiarTextBoxsPagoTarjetaYEfectivo()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

       
        private void DeshabilitaLimpiaTxtBoxYLabel_Recibio()
        {
            recibio.Enabled = false;
            txtCantidad.Enabled = false;
            txtCantidad.Clear();
        }

        private void DeshabilitarLabelsPagoTarjetaYEfectivo()
        {
            label2.Enabled = false;
            label4.Enabled = false;
        }


        private void DeshabilitaTextBoxsPagoTarjetaYEfectivo()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }
    }
}
