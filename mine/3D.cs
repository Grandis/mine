using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mine
{
    public partial class _3D : Form
    {
        double[,] data3D;
        int count;

        public _3D(double[,] data3D, int count)
        {
            InitializeComponent();
            this.data3D = data3D;
            this.count = count;
        }
    }
}
