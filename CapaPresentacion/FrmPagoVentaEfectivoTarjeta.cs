using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmPagoVentaEfectivoTarjeta : Form
    {
        private double totalAPagar;
        private double cantidadTomadaDeTarjeta;
        private double cantidadTomadaDeEfectivo;

        //-----------properties
        public double TotalAPagar
        {
            set { totalAPagar = value; }
            get { return totalAPagar;  }
        }

        public double CantidadTomadaDeTarjeta
        {
            set { cantidadTomadaDeTarjeta = value; }
            get { return cantidadTomadaDeTarjeta; }
        }

        public double CantidadTomadaDeEfectivo
        {
            set { cantidadTomadaDeEfectivo = value; }
            get { return cantidadTomadaDeEfectivo; }
        }


        //-----------constructor
        public FrmPagoVentaEfectivoTarjeta(double totalAPagar)
        {
            InitializeComponent();
            this.TotalAPagar = totalAPagar;
            this.CantidadTomadaDeTarjeta = 0.0;
            this.cantidadTomadaDeEfectivo = 0.0;


        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(AlgunasValidaciones())
                {
                    //Todo va bien
                    double cambio = Convert.ToDouble(textBox1.Text) - Convert.ToDouble(textBox2.Text);
                    MessageBox.Show("Cambio en efectivo: " + cambio);

                    this.CantidadTomadaDeTarjeta = Convert.ToDouble(textBox3.Text);
                    this.CantidadTomadaDeEfectivo = Convert.ToDouble(textBox2.Text);

                    this.Visible = false;
                }
              
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }


        private bool AlgunasValidaciones()
        {
            bool esOk = true;

            double cantidadEntregadaEfectivo = Convert.ToDouble(textBox1.Text);
            double cantidadATomarEfectivo = Convert.ToDouble(textBox2.Text);
            double cantidadATomarTarjeta = Convert.ToDouble(textBox3.Text);


            //todos lo capturado es positivo
            if ((cantidadEntregadaEfectivo >= 0.0) && (cantidadATomarEfectivo >= 0.0) && (cantidadATomarTarjeta >= 0.0))
                esOk = true;
            else
                throw new System.InvalidOperationException("No se permiten valores negativos");


            //Validacion
            if (cantidadATomarEfectivo <= cantidadEntregadaEfectivo)
                esOk = true;
            else
                throw new InvalidOperationException("La condición cantidadATomarEfectivo <= cantidadEntregadaEfectivo no se cumple");


            //Validacion
            if ((cantidadATomarEfectivo + cantidadATomarTarjeta) == this.TotalAPagar)
                esOk = true;
            else
                throw new InvalidOperationException("No se cubre la cantidad que se debe pagar ");
          
            return (esOk);  //siempre sera true si llega a este punto
        }
    }
}
