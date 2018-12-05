using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace 学习项目随机选择
{
    public partial class FormMain : Form
    {
        int count = 0;
        List<int> lStudyOrder = new List<int>();
        int currentIndex = 0;
        string pathRecord = Directory.GetCurrentDirectory() + "\\record.ini";

        //添加计时
        DateTime dtStart = new DateTime();
        double usedSec = 0;//已使用的秒数

        //配置表读写“ini”
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filepath);
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder returnvalue, int buffersize, string filepath);

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //读记录
            string strTemp = "";
            GetRecord("学习内容", "count", out strTemp);
            int.TryParse(strTemp, out count);
            string[] strStudy = new string[count];
            for (int i = 0; i < count;i++ )
            {
                GetRecord("学习内容", "Item" + (i + 1), out strTemp);
                clbStudy.Items.Add(strTemp);
            }

            //判断时间是不是当天的，不是的话就清除记录
            GetRecord("相关信息", "时间", out strTemp);
            DateTime dt = DateTime.ParseExact(strTemp, "yyyy/MM/dd", System.Globalization.CultureInfo.CurrentCulture);
            if (DateTime.Today.Subtract(dt).Days==0)
            {
                //今天
                GetRecord("相关信息", "学习顺序", out strTemp);
                string[] strArr = strTemp.Split(',');
                for (int i = 0; i < strArr.Length;i++ )
                {
                    int intTemp = 0;
                    bool res = int.TryParse(strArr[i], out intTemp);
                    if (res == true)
                    {
                        lStudyOrder.Add(intTemp);
                    }
                }

                //学习状态
                btnStudy.Text = "学习开始";
                for (int i = 0; i < lStudyOrder.Count;i++ )
                {
                    GetRecord("相关信息", lStudyOrder[i].ToString(), out strTemp);
                    CheckState cs = (CheckState)Enum.Parse(typeof(CheckState), strTemp);
                    clbStudy.SetItemCheckState(lStudyOrder[i], cs);
                    switch (cs)
                    {
                        case CheckState.Indeterminate:
                            {
                                currentIndex = i;
                                GetRecord("相关信息", "已使用秒数", out strTemp);
                                double.TryParse(strTemp, out usedSec);
                            }
                            break;
                        case CheckState.Checked:
                            {
                                currentIndex = i;
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
            else
            {
                //不是今天
                //删除相关信息
                WriteRecord("相关信息", null, null);
                //日期
                WriteRecord("相关信息", "时间", DateTime.Today.ToString("yyyy/MM/dd"));
                //学习顺序和学习状态
                int[] order = new int[count];
                for (int i = 0; i < order.Length; i++)
                {
                    order[i] = i;
                }
                for (int i = order.Length - 1; i >= 0; i--)
                {
                    Random rd = new Random();
                    int j = rd.Next(i+1);
                    int temp = order[i];
                    order[i] = order[j];
                    order[j] = temp;
                }
                strTemp = "";
                for (int i = 0; i < order.Length;i++ )
                {
                    strTemp += order[i] + ",";
                    lStudyOrder.Add(order[i]);
                    WriteRecord("相关信息", order[i].ToString(), CheckState.Unchecked.ToString());
                }
                strTemp = strTemp.Substring(0, strTemp.Length - 1);
                WriteRecord("相关信息", "学习顺序", strTemp);
                WriteRecord("相关信息", "已使用秒数", "0");
            }
        }

        private void btnStudy_Click(object sender, EventArgs e)
        {
            switch (btnStudy.Text)
            {
                case "学习开始":
                    {
                        clbStudy.SetItemCheckState(lStudyOrder[currentIndex], CheckState.Indeterminate);
                        btnStudy.Text = "学习暂停";
                        dtStart = DateTime.Now;
                    }
                    break;
                case "学习暂停":
                    {
                        btnStudy.Text = "学习开始";
                        TimeSpan ts = DateTime.Now - dtStart;
                        usedSec += ts.TotalSeconds;
                        TimeSpan totalSec = new TimeSpan((long)(usedSec * Math.Pow(10, 7)));
                        txtShowTime.AppendText("该次学习从" + dtStart.ToString("HH:mm:ss") + "到" + DateTime.Now.ToString("HH:mm:ss"));
                        txtShowTime.AppendText("\n");
                        txtShowTime.AppendText("用时：" + ts.ToString() + "，该课程学习共用时：" + totalSec.ToString());
                        txtShowTime.AppendText("\n");
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            clbStudy.SetItemCheckState(lStudyOrder[currentIndex++], CheckState.Checked);
            switch (btnStudy.Text)
            {
                case "学习开始":
                    {
                    }
                    break;
                case "学习暂停":
                    {
                        btnStudy.Text = "学习开始";
                        TimeSpan ts = DateTime.Now - dtStart;
                        usedSec += ts.TotalSeconds;
                        TimeSpan totalSec = new TimeSpan((long)(usedSec * Math.Pow(10, 7)));
                        txtShowTime.AppendText("该次学习从" + dtStart.ToString("HH:mm:ss") + "到" + DateTime.Now.ToString("HH:mm:ss"));
                        txtShowTime.AppendText("\n");
                        txtShowTime.AppendText("用时：" + ts.ToString() + "，该课程学习共用时：" + totalSec.ToString());
                        txtShowTime.AppendText("\n");
                    }
                    break;
                default:
                    break;

            }
            usedSec = 0;

        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();//显示界面
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;//显示在任务栏
        }

        #region 读写记录
        private void WriteRecord(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, pathRecord);
        }

        private void GetRecord(string section, string key, out string value)
        {
            StringBuilder temp = new StringBuilder();
            GetPrivateProfileString(section, key, "", temp, 1024, pathRecord);
            value = temp.ToString();
        }
        #endregion

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string strTemp = "";
            GetRecord("相关信息", "时间", out strTemp);
            DateTime dt = DateTime.ParseExact(strTemp, "yyyy/MM/dd", System.Globalization.CultureInfo.CurrentCulture);
            if (DateTime.Today.Subtract(dt).Days == 0)
            {
                if (btnStudy.Text == "学习暂停")
                {
                    btnStudy_Click(sender, e);
                }
                WriteRecord("相关信息", "已使用秒数", usedSec.ToString());
                for (int i = 0; i < count; i++)
                {
                    switch (clbStudy.GetItemCheckState(i))
                    {
                        case CheckState.Unchecked:
                            {
                                WriteRecord("相关信息", i.ToString(), CheckState.Unchecked.ToString());
                            }
                            break;
                        case CheckState.Indeterminate:
                            {
                                WriteRecord("相关信息", i.ToString(), CheckState.Indeterminate.ToString());
                            }
                            break;
                        case CheckState.Checked:
                            {
                                WriteRecord("相关信息", i.ToString(), CheckState.Checked.ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

        }

    }
}
