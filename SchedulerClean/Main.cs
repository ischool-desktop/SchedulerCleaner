using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Data;
using FISCA.Presentation.Controls;
using K12.Data;

namespace SchedulerClean
{
    public partial class Main : BaseForm
    {
        private string query_template = @"select date_trunc('day',@@Field) date,count(*) count from $scheduler.course_calendar
                where date_trunc('day',@@Field) >= '@@Begin' and date_trunc('day',@@Field) <= '@@End'
                group by date_trunc('day',@@Field)
                order by date_trunc('day',@@Field) desc";

        private string del_template = @"delete from $scheduler.course_calendar 
            where date_trunc('day',@@Field) >= '@@Begin' and date_trunc('day',@@Field) <= '@@End'";

        public Main()
        {
            InitializeComponent();

            dgvPreview.AutoGenerateColumns = false;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                QueryHelper q = new QueryHelper();
                dgvPreview.DataSource = q.Select(getQuerySql());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("刪了就沒救了，你確定？", "Confirm", MessageBoxButtons.OKCancel);

                if (dr == DialogResult.OK)
                {
                    UpdateHelper update = new UpdateHelper();
                    update.Execute(getDeleteSql());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string getQuerySql()
        {
            string output = query_template;
            output = output.Replace("@@Field", getField());
            output = output.Replace("@@Begin", txtBegin.Text);
            output = output.Replace("@@End", txtEnd.Text);

            return output;
        }

        private string getDeleteSql()
        {
            string output = del_template;
            output = output.Replace("@@Field", getField());
            output = output.Replace("@@Begin", txtBegin.Text);
            output = output.Replace("@@End", txtEnd.Text);

            return output;
        }

        private string getField()
        {
            if (chkLastUpdate.Checked)
                return "last_update";
            else if (chkStartTime.Checked)
                return "start_date_time";
            else
                throw new Exception("請選擇要用哪種日期當條件!!!");
        }
    }
}
