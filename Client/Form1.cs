
using Server;
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

namespace Client
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        TcpClient client;
        NetworkStream stream;

        private void btnBaglan_Click(object sender, EventArgs e)
        {
            client = new TcpClient(txtIp.Text, 5000);
            stream = client.GetStream();

            Thread listener = new Thread(ListenConnect);
            listener.Start();
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        void ListenConnect()
        {
            while (true)
            {
                Mesaj message = (Mesaj)binaryFormatter.Deserialize(stream);
                listBox1.Items.Add(message);
            }
        }
        private void btnGonder_Click(object sender, EventArgs e)
        {
            Mesaj message = new Mesaj();
            message.Gonderen = txtKullaniciAdi.Text;
            message.GonderimTarihi = DateTime.Now;
            message.Icerik = textBox1.Text;
            listBox1.Items.Add(message);

            binaryFormatter.Serialize(stream, message);
            stream.Flush();

            textBox1.Clear();
            textBox1.Focus();
        }


    }
}
