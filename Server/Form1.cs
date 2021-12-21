using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        Socket socket;
        NetworkStream stream;
        TcpListener listener;
        private void Form1_Load(object sender, EventArgs e)
        {
            listener = new TcpListener(5000);
            listener.Start();

            socket = listener.AcceptSocket();
            stream = new NetworkStream(socket);
            Thread listen = new Thread(ListenSocket);
            listen.Start();
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        void ListenSocket()
        {
            while (socket.Connected)
            {
                Mesaj receivedMessage = (Mesaj)binaryFormatter.Deserialize(stream);
                listBox1.Items.Add(receivedMessage);
            }
        }
        private void btnGonder_Click(object sender, EventArgs e)
        {
            Mesaj message = new Mesaj();
            message.Gonderen = "Server";
            message.GonderimTarihi = DateTime.Now;
            message.Icerik = textBox1.Text;
            listBox1.Items.Add(message);
            binaryFormatter.Serialize(stream,message);
            stream.Flush();

            textBox1.Clear();
            textBox1.Focus();
        }


    }
}
