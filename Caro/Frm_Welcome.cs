using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro
{
    public partial class Frm_Welcome : Form
    {
        public Frm_Welcome()
        {
            InitializeComponent();

            RenderUIWelcome rui = new RenderUIWelcome(this);
            rui.titleForm("GAME CARO");
            rui.titleInput();
            rui.renderInput();
            rui.renderButton("Bắt đầu");
        }
    }
}
