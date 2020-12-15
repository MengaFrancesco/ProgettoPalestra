using System;
using System.Drawing;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.QrCode;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.IO;
using System.Timers;
using ZXing.Aztec.Internal;

namespace Progetto_Palestra.QR_Reader
{


    public partial class QR_Reader : Form
    {
        public FilterInfoCollection capturedev;
        public VideoCaptureDevice finalframe;

        private static System.Timers.Timer aTimer;

        string QR_code;

        public QR_Reader()
        {
            InitializeComponent();
            
            capturedev = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Dev in capturedev)
            {
                comboBox1.Items.Add(Dev.Name);
            }
            comboBox1.SelectedIndex = -1;

            QR_code = "";
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            }
            catch (Exception ex)
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            finalframe = new VideoCaptureDevice(capturedev[comboBox1.SelectedIndex].MonikerString);
            finalframe.NewFrame += Finalframe_NewFrame;
            finalframe.Start();
            SetTimer();
        }

        private void Finalframe_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            }
            catch (Exception)
            {
            }
            
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            BarcodeReader red = new BarcodeReader();
            if (pictureBox1.Image != null)
            {
                try
                {
                    Result res = red.Decode((Bitmap)pictureBox1.Image);
                    string dec = res.ToString();
                    if(QR_code=="")
                    MessageBox.Show(dec);
                    QR_code = dec;

                    aTimer.Stop();
                    aTimer.Dispose();
                }
                catch (Exception ex)
                { }
            }
        }

        private void QR_Reader_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (finalframe != null)
                if (finalframe.IsRunning == true)
                {
                    finalframe.Stop();
                }
        }
    }
}
