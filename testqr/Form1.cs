using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testqr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(txtValor.Text, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer( new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imageTemporal, new Size( new Point(200, 200)));
            panelResultado.BackgroundImage = imagen;

            imagen.Save("imagen.png", ImageFormat.Png);
            btnGuardar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Image imagenFinal = (Image)panelResultado.BackgroundImage.Clone();

            SaveFileDialog dialogGuardar = new SaveFileDialog();
            dialogGuardar.AddExtension = true;
            dialogGuardar.Filter = "Image PNG (*.png)|*.png";
            dialogGuardar.ShowDialog();

            if (!string.IsNullOrEmpty(dialogGuardar.FileName))
            {
                imagenFinal.Save(dialogGuardar.FileName, ImageFormat.Png);
            }

            imagenFinal.Dispose();
        }
    }
}
