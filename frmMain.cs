using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLTesting
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// Connection string
        /// </summary>
        const string conStr = "Provider={0};Data Source={1};";
        string conn = "";
        string sqlSelection = "";
        OleDbConnection oleConn = null;
        
        private readonly Dictionary<string, string> twoProviderStr = new Dictionary<string, string>
        {
            { "Microsoft.ACE.OLEDB.12.0", "Microsoft.ACE.OLEDB.12.0" } ,
           { "Microsoft.Jet.OLEDB.4.0", "Microsoft.Jet.OLEDB.4.0" } 
    };

        public frmMain()
        {
            InitializeComponent();
            //
            cbbProvider.DataSource = twoProviderStr.ToList();
            cbbProvider.DisplayMember = "Key";
            cbbProvider.ValueMember = "Value";
            InitLoad();
        }

        void InitLoad()
        {
            btnRun.Enabled = false;
            btnConnect.Enabled = true;
            richSQLClause.WordWrap = true;
            richSQLClause.HideSelection = true;
            richSQLClause.Text = "ここにはクエリーを入力して下さい";
            //エベント
            richSQLClause.Enter += RichSQLClause_Enter;
            richSQLClause.Leave += RichSQLClause_Leave;
            txtDataSrc.Text = "";
            txtPassword.Text = "";
            sqlSelection = "";
            oleConn = null;
        }

        private void RichSQLClause_Leave(object sender, EventArgs e)
        {
            if(richSQLClause.Text.Length == 0)
            {
                richSQLClause.Text = "ここにはクエリーを入力して下さい";
            }
        }

        private void RichSQLClause_Enter(object sender, EventArgs e)
        {
            if (richSQLClause.Text.Equals("ここにはクエリーを入力して下さい"))
            {
                richSQLClause.Text = "";
            }
        }

        void Connect()
        {
            conn = conStr.Replace("{0}",cbbProvider.Text).Replace("{1}",txtDataSrc.Text).Replace("{2}", txtPassword.Text);
            try
            {
                oleConn = new OleDbConnection(conn);
                oleConn.Open();
            }catch(Exception e)
            {
                throw e;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Access DB|*.accdb;*.mdb";
            DialogResult result = opf.ShowDialog();
            if(result == DialogResult.OK)
            {
                txtDataSrc.Text = opf.FileName;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            sqlSelection = string.IsNullOrEmpty(sqlSelection) ? richSQLClause.Text : sqlSelection;
            string[] sqlStr = sqlSelection.Split(';');
            try
            {
                foreach(string sql in sqlStr)
                {
                    if (!string.IsNullOrEmpty(sql))
                    {
                        using (OleDbCommand cmd = new OleDbCommand(sql.Trim(), oleConn))
                        {
                            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt != null)
                            {
                                frmShowData frm = new frmShowData();
                                frm.SetData(dt);
                                frm.Show(this);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                FormHelper.fn_display_message(ex.Message, "OE");
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                Connect();
                FormHelper.fn_display_message("データベースの接続に合格しました。", "OI");
                btnRun.Enabled = true;
                btnConnect.Enabled = false;
            }

            catch(Exception ex)
            {
                FormHelper.fn_display_message("データベースの接続に失敗しました。!\n情報にチェックして下さい。\n" + ex.Message, "OE");
                btnRun.Enabled = false;
            }
            
        }

        private void richSQLClause_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void richSQLClause_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                btnRun_Click(null, null);
            }
        }

        private void richSQLClause_SelectionChanged(object sender, EventArgs e)
        {
            sqlSelection = richSQLClause.SelectedText;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            InitLoad();
        }
    }
}
