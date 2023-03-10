using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SłabySaper
{
     partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            Game MyGame = new Game(8,8,10);
            this.Controls.Add(MyGame);

           
            
        }

       

        
    }
}
