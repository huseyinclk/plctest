using PlcCommon.S7.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Plc device = null;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CpuType ty = CpuType.S71200;

                if(!string.IsNullOrEmpty(comboBox1.Text))
                {
                    Enum.TryParse<CpuType>(comboBox1.Text, out ty);
                }

                device = new Plc(ty, textip.Text, Convert.ToInt16(numrank.Value), Convert.ToInt16(numslot.Value));
                device.Open();
                if (device.IsConnected && device.IsAvailable)
                    label1.Text = "Bağlantı başarılı";
                else
                    label1.Text = string.Format("Bağlanılamadı IsConnected:{0}, IsAvailable:{1}", device.IsConnected, device.IsAvailable);
            }
            catch (Exception exc)
            {
                label1.Text = string.Format("Bağlantı sağlanamadı: {0}", exc.Message);
                label2.Text = exc.StackTrace;
            }
            //timer1.Enabled = true;


            /*
              PLCPin pin1 = new PLCPin(391, "25100125");
            pin1.ElmaslamaAdres = new PinAdres() { Ad = "KOYO_YANAK_025125_ELMASLAMA_TIMESPAN", Adres = "DB1.DBD16" };
            pin1.BeklemeAdres = new PinAdres() { Ad = "KOYO_YANAK_025125_BEKLEME_TIMESPAN", Adres = "DB1.DBD32" };
            //PLCPin pin2 = new PLCPin(948, "25100129");
            //pin2.ElmaslamaAdres = new PinAdres() { Ad = "NISSEI_YANAK_025129_ELMASLAMA_TIMESPAN", Adres = "DB1.DBD20" };
            //pin2.BeklemeAdres = new PinAdres() { Ad = "NISSEI_YANAK_025129_BEKLEME_TIMESPAN", Adres = "DB1.DBD36" };
            //PLCPin pin3 = new PLCPin(968, "25200240");
            //pin3.ElmaslamaAdres = new PinAdres() { Ad = "SMSB_PUNTASIZ_025240_BEKLEME_TIMESPAN", Adres = "DB1.DBD40" };
            //pin3.BeklemeAdres = new PinAdres() { Ad = "SMSB_PUNTASIZ_025240_ELMASLAMA_TIMESPAN", Adres = "DB1.DBD24" };
            //PLCPin pin4 = new PLCPin(223, "25200222");
            //pin4.ElmaslamaAdres = new PinAdres() { Ad = "KOYO_PUNTASIZ_025222_ELMASLAMA_TIMESPAN", Adres = "DB1.DBD28" };
            //pin4.BeklemeAdres = new PinAdres() { Ad = "KOYO_PUNTASIZ_025222_BEKLEME_TIMESPAN", Adres = "DB1.DBD44" };
            PLCDevice device = new PLCDevice("192.168.145.2", "YP_HAT_2");
            device.Add(pin1);
            //device.Add(pin2);
            //device.Add(pin3);
            //device.Add(pin4);
            device.ConnectionClosed += device_ConnectionClosed;
            device.Open();
            devices.Add(device);
            devices.TrimExcess();
             */
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label1.Text = device.Read("DB1.DBD0").ToString();
            label2.Text = device.Read("DB1.DBD4").ToString();
            label3.Text = device.Read("DB1.DBD80").ToString();
            label4.Text = device.Read("DB1.DBD36").ToString();
            label5.Text = device.Read("DB1.DBD40").ToString();
            label6.Text = device.Read("DB1.DBD24").ToString();
            label7.Text = device.Read("DB1.DBD28").ToString();
            label8.Text = device.Read("DB1.DBD44").ToString();
            timer1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var names = Enum.GetNames(typeof(CpuType));
            foreach (string s in names)
            {
                comboBox1.Items.Add(s);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (device == null)
                {
                    label1.Text = "Önce bağlantı açılmalıdır!";
                    return;
                }
                if (!device.IsConnected)
                {
                    label1.Text = "Cihaza bağlanılmamış";
                    return;
                }
                label5.Text = string.Format("okunan değer:{0}", device.Read(textAdres.Text).ToString());
            }
            catch (Exception exc)
            {
                label1.Text = string.Format("Bağlantı sağlanamadı: {0}", exc.Message);
                label2.Text = exc.StackTrace;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (device == null)
                {
                    label1.Text = "Önce bağlantı açılmalıdır!";
                    return;
                }
                if (device.IsConnected)
                {
                    label1.Text = "Cihaza bağlanılmamış";
                    return;
                }

                device.Write(textAdres.Text, textBox3.Text);

                label5.Text = "yazıldı";
            }
            catch (Exception exc)
            {
                label1.Text = string.Format("Bağlantı sağlanamadı: {0}", exc.Message);
                label2.Text = exc.StackTrace;
            }
        }
    }
}
