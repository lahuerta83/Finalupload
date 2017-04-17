using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Finalupload
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label1.Text = "0";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart startprog = new ThreadStart(ReadFile);

            Thread workprog = new Thread(startprog);

            workprog.Start();

        }

        public delegate void updatebar();


        private void UpdateProgress()
        {
            progressBar1.Value += 1;

            // Here we are just updating a label to show the progressbar value
            label1.Text = Convert.ToString(Convert.ToInt64(label1.Text) + 1);
        }

        private void ReadFile()
        {
            String finalfile = @"c:\\Users\Leigh Ann\Documents\finalfile.txt";

            FileInfo fileSize = new FileInfo(finalfile);
            long size = fileSize.Length;

            long Sizecurrent = 0;
            long incsize = (size / 100);

            StreamReader stream = new StreamReader(new FileStream(finalfile, FileMode.Open));

            char[] buff = new char[10];

            // Read through the file until end of file
            while (!stream.EndOfStream)
            {
                // Add to the current position in the file
                Sizecurrent += stream.Read(buff, 0, buff.Length);


                if (Sizecurrent >= incsize)
                {
                    Sizecurrent -= incsize;
                    progressBar1.Invoke(new updatebar(this.UpdateProgress));
                }
            }

            stream.Close();
            
            MessageBox.Show("Document has been upload!");



        }
    }
}
