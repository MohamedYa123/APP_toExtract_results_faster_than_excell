using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace high_school_results
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (stdname.Text == "")
            {
                if (stdnum.Text == "")
                {
                    MessageBox.Show("Please enter student number !");
                    return;
                }
                var stn=Convert.ToInt32(stdnum.Text);
                var x=  seatnumss.IndexOf(stn);
                if (x == -1)
                {
                    MessageBox.Show("Student number not found !");
                    return;
                }
                list.Clear();
                ListViewItem lv = new ListViewItem();
                lv.Text = "1";
                lv.SubItems.Add(stn.ToString());
                lv.SubItems.Add(names2[x]);
                lv.SubItems.Add(Math.Round(degrees[x],2)+"");
                lv.SubItems.Add(Math.Round(degrees[x]/410*100,2)+" %");
                lv.SubItems.Add(stcase[x]);
                list.Add(lv);
            }
            else
            {
                string s = stdname.Text;
                s =fine(s);
                await Task.Run(() => {
                    int nums = 0;
                    List<int> ns = new List<int>();
                    for(int i=0;i<names.Count;i++)
                    {
                        var zx = names[i];
                        if (zx.Contains(s))
                        {
                            ns.Add(i);
                            nums++; 
                            if(nums>=maxnums)
                            {
                                break;
                            }
                        }
                    }
                    list.Clear();
                    for(int i=0;i<ns.Count;i++)
                    {
                        var x = ns[i];
                        ListViewItem lv = new ListViewItem();
                        lv.Text = (i+1)+"";
                        lv.SubItems.Add(seatnumss[x]+"");
                        lv.SubItems.Add(names2[x]);
                        lv.SubItems.Add(Math.Round(degrees[x], 2) + "");
                        lv.SubItems.Add(Math.Round(degrees[x] / 410 * 100,2) + " %");
                        lv.SubItems.Add(stcase[x]);
                        list.Add(lv);
                    }
                });

            }

            listView1.Items.Clear();
            listView1.Items.AddRange(list.ToArray());
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        int maxnums = 5000;
        List<ListViewItem> list = new List<ListViewItem>();
        List<string> names = new List<string>();
        List<string> names2 = new List<string>();
        List<string> stcase = new List<string>();
        List<int> seatnumss = new List<int>();
        List<double> degrees = new List<double>();
        static string path = "high_school_2023.csv";
        int progress = 1;
        int total;
        string fine(string s)
        {
            return s.Replace("أ", "ا").Replace("إ", "ا").Replace("ة", "ه").Replace("ى", "ي").Replace(" ","").Replace("س","ص").Replace("ئ","ى").Replace("ء","ا");
        }
        async Task read()
        {
            await Task.Run(() => {
                var f = File.ReadAllLines(path);
                total = f.Length;
                for (int i = 1; i < f.Length; i++)
                {
                    
                    var x = f[i].Split(',');
                    if (Convert.ToDouble(x[2]) <= 410)
                    {
                        names.Add(fine(x[1]));
                        names2.Add(x[1]);
                        seatnumss.Add(Convert.ToInt32(x[0]));
                        degrees.Add(Convert.ToDouble(x[2]));
                        stcase.Add(x[5]);
                        
                    }
                    progress++;
                }
            });
        }
        void  search(string name)
        {
            list.Clear();

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
          
            await read();
        }
        int yy = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (total > 0)
            {
                progressBar1.Value = (progress * 100) / total;
                if (progressBar1.Value == 100)
                {
                    yy++;
                
                }
            }
            if (yy == 50)
            {
                  progressBar1.Hide();
            }
        }

        private void stdname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                button1.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (stdname.Text == "")
            {
                if (stdnum.Text == "")
                {
                    MessageBox.Show("Please enter student number !");
                    return;
                }
                var stn = Convert.ToInt32(stdnum.Text);
                var x = seatnumss.IndexOf(stn);
                if (x == -1)
                {
                    MessageBox.Show("Student number not found !");
                    return;
                }
                list.Clear();
                for (int i = 0; i < 1000; i++)
                {
                    
                    ListViewItem lv = new ListViewItem();
                    lv.Text = (i+1)+"";
                    lv.SubItems.Add(seatnumss[x] + "");
                    lv.SubItems.Add(names2[x]);
                    lv.SubItems.Add(Math.Round(degrees[x], 2) + "");
                    lv.SubItems.Add(Math.Round(degrees[x] / 410 * 100, 2) + " %");
                    lv.SubItems.Add(stcase[x]);
                    list.Add(lv);
                    x++;
                }
            }
            listView1.Items.Clear();
            listView1.Items.AddRange(list.ToArray());
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
