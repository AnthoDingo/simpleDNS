using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace simpleDNS
{
    public class ControlWriter : TextWriter
    {
        private Control textbox;
        public ControlWriter(Control textbox)
        {
            textbox.Invoke((Action)delegate
            {
                this.textbox = textbox;
            });
        }

        public override void Write(char value)
        {
            //textbox.Text += value;
            /*
            if (value != "\r\n")
            {
                textbox.Text += String.Format("[{0:g}] {1}", DateTime.Now, value);
            }
            */
        }

        public override void Write(string value)
        {
            try
            {
                if (value != "\r\n")
                {
                    textbox.Text += String.Format("[{0:g}] {1}", DateTime.Now, value);
                }
                else
                {
                    textbox.Text += value;
                }
            } catch
            {
                textbox.Invoke((Action)delegate
                {
                    Write(value);
                });
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
