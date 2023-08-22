using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro
{
    class RenderUIWelcome
    {
        private Control ctr;

        public RenderUIWelcome(Control ctr)
        {
            this.ctr = ctr;
        }

        public void titleForm(string title)
        {
            Label lbl = new Label();
            lbl.Text = title;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Width = 200;
            lbl.Height = 50;
            lbl.Left = 180;
            lbl.Top = 20;
            lbl.Font = new Font("Arial", 20);
            ctr.Controls.Add(lbl);
        }

        public void titleInput()
        {
            Label lbl = new Label();
            lbl.Text = "Số cấp của ma trận :";
            lbl.Top = 120;
            lbl.Left = 100;
            lbl.Width =200;
            lbl.Font = new Font("Arial", 14);
            ctr.Controls.Add(lbl);
        }

        public void renderInput()
        {
            TextBox tb = new TextBox();
            tb.Name = "txtLevel";
            tb.Width = 150;
            tb.Height = 40;
            tb.Font = new Font("Arial", 14);
            tb.Top = 120;
            tb.Left = 320;
            tb.KeyPress += new KeyPressEventHandler(txt_KeyPress);
            ctr.Controls.Add(tb);
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        public void renderButton(string name)
        {
            Button btn = new Button();
            btn.Text = name;
            btn.Font = new Font("Arial", 14);
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Top = 190;
            btn.Left = 230;
            btn.Width = 100;
            btn.Height = 40;
            btn.Click += new EventHandler(bt_Click);
            ctr.Controls.Add(btn);
        }

        private void bt_Click(object sender, EventArgs e)
        {
            if(isEnter())
            {
                Frm_Caro frm = new Frm_Caro(getLevel());
                ctr.Hide();
                frm.ShowDialog();
                ctr.Show();
            }   
        }

        private bool isEnter()
        {
            foreach(Control item in ctr.Controls)
            {
                if (item.GetType() == typeof(TextBox) && item.Name == "txtLevel")
                {
                    TextBox txt = (TextBox)item;
                    if (item.Text.Length <= 0)
                        MessageBox.Show("Bạn phải nhập số cấp của ma trận");
                    else
                    {
                        int level = int.Parse(txt.Text);
                        if (level < 3 || level > 50)
                            MessageBox.Show("Giá trị không hợp lệ");
                        else
                            return true;
                    }    
                }
            }
            return false;
        }

        private int getLevel()
        {
            int result = 0;
            foreach (Control item in ctr.Controls)
            {
                if (item.GetType() == typeof(TextBox))
                {
                    TextBox txt = (TextBox)item;
                    return Convert.ToInt32(txt.Text);
                }
            }
            return result;
        }
    }
}
