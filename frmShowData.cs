using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLTesting
{
    public partial class frmShowData : Form
    {
        DataTable dt = null;
        public frmShowData()
        {
            InitializeComponent();
            //dt = new DataTable();
        }

        public void SetData(DataTable _dt)
        {
            dt = _dt;
            dgvData.DataSource = dt;
            Text = _dt.TableName;
        }
    }
}
