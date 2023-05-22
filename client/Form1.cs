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
using System.Net.Sockets;

namespace client
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
        private async void connect()
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 49300);
            networkStream = tcpClient.GetStream();
            streamReader= new StreamReader(networkStream);
            streamWriter= new StreamWriter(networkStream);
            streamWriter.AutoFlush = true;
            button2.Enabled = true;
            while (true)
            {
                string msg= await streamReader.ReadLineAsync();
                txtReceivedMsg.Text += $"Server : {msg}{Environment.NewLine}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            streamWriter.WriteLine(txtMsg.Text);
            txtReceivedMsg.Text += $"Client : {txtMsg.Text}{Environment.NewLine}";
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
