using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace citrix_winform_demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Channel.Initialize();
        }

        public AppChannel Channel { get; set; }

        private delegate void DisplayLogelegate(string m);

        public void ShowLog(string m)
        {
            this.textLogBox.Invoke(new DisplayLogelegate(DisplayLog), m);
        }

        private void DisplayLog(string m)
        {
            this.textLogBox.AppendText(m);
            this.textLogBox.AppendText(Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Channel.Send(this.button1.Text);
        }
    }
}
