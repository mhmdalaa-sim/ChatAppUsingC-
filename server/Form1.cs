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
using System.Net;
using System.Net.Sockets;

namespace server
{
    public partial class Form1 : Form
    {
        StreamReader streamReader;
        StreamWriter streamWriter;
        NetworkStream networkStream;
        public Form1()
        {
            InitializeComponent();
        }
        private async void start()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener tcpListener = new TcpListener(ip, 49300);
            tcpListener.Start();
            TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
            networkStream= tcpClient.GetStream();
            streamReader= new StreamReader(networkStream);
            streamWriter= new StreamWriter(networkStream);
            streamWriter.AutoFlush= true;
            button1.Enabled= true;
            while (true)
            {
                string msg = await streamReader.ReadLineAsync();
                txtRecrivedMsg.Text += $"Client : {msg}{Environment.NewLine}"; 
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            streamWriter.WriteLine(txtMsg.Text);
            txtRecrivedMsg.Text += $"Server : {txtMsg.Text}{Environment.NewLine}";
            txtMsg.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            streamReader.Close();
            streamWriter.Close();
            networkStream.Close();
        }
    }
}
