using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA;
using FISCA.Presentation;

namespace SchedulerClean
{
    public class Program
    {
        [MainMethod]
        public static void Main()
        {
            MotherForm.RibbonBarItems["調代課", "神秘工具組"]["批砍記錄"].Click += (object sender, EventArgs e) => {
                new Main().ShowDialog();
            };
        }
    }
}
