using System;
using System.Data;
using System.Windows.Forms;

namespace TinhThue
{
    public partial class ThongTin : Form
    {
        private DataTable dataTable = new DataTable(); // Tạo mới một bảng trống
        double[] thueNop = new double[12]; // Biến dùng để lưu tiền thuế phải nộp của tháng
        double tongThue = 0; // Biến để lưu tổng tiền thuế
        double thueTT = 0;    // Biến để lưu thuế thực tế
        double[] luong = new double[12];    //Biến để lưu tiền lương của tháng 
     

        public ThongTin()
        {
            InitializeComponent();

        }
        

        private void loadData()
        {
            double index = Convert.ToDouble(getTongTienThue().ToString());
            double index2 = Convert.ToDouble(getTienThueTT().ToString());
            dataTable.Columns.Add("Tháng", typeof(String));
            dataTable.Columns.Add("Lương", typeof(double));
            dataTable.Columns.Add("Tiền Thuế", typeof(double));
            dataTable.Rows.Add(thang1.Text, luong[0], thueNop[0]);
            dataTable.Rows.Add(thang2.Text, luong[1], thueNop[1]);
            dataTable.Rows.Add(thang3.Text, luong[2], thueNop[2]);
            dataTable.Rows.Add(thang4.Text, luong[3], thueNop[3]);
            dataTable.Rows.Add(thang5.Text, luong[4], thueNop[4]);
            dataTable.Rows.Add(thang6.Text, luong[5], thueNop[5]);
            dataTable.Rows.Add(thang7.Text, luong[6], thueNop[6]);
            dataTable.Rows.Add(thang8.Text, luong[7], thueNop[7]);
            dataTable.Rows.Add(thang9.Text, luong[8], thueNop[8]);
            dataTable.Rows.Add(thang10.Text, luong[9], thueNop[9]);
            dataTable.Rows.Add(thang11.Text, luong[10], thueNop[10]);
            dataTable.Rows.Add(thang12.Text, luong[11], thueNop[11]);
            dataTable.Rows.Add("Tổng thuế TNCN", index);
            dataTable.Rows.Add("Thuế TNCN thực tế", index2);

            if (tongThue > thueTT && tongThue > 0)
            {
                dataTable.Rows.Add("Thuế nhận lại", index - index2);
            }
            else
            {
                dataTable.Rows.Add("Thuế phải nộp", index2 - index);
            }

            this.dataTable = dataTable;
        }//Hàm thêm vào dòng, cột và giá trị cho bảng đã tạo mới
        private double  getTienThue(double luong)
        {
            double tinhTienThueTN = luong - double.Parse(cmdNLD.Text) - (double.Parse(cmdBHYT.Text) * 5 / 100) - double.Parse(cmdPT.Text) * double.Parse(cmdNPT.Text);
            return tinhTienThue(tinhTienThueTN);
        }// Hàm trả về giá trị của tiền thuế thu nhập
        private double tinhTienThue(double tinhTienThueTN)
        {
            if (tinhTienThueTN < 0)
            {
                return 0;
            }
            else
            {
                if (tinhTienThueTN <= 5000000)
                {
                    tinhTienThueTN = tinhTienThueTN * 5 / 100;
                }
                else if (tinhTienThueTN > 5000000 && tinhTienThueTN <= 10000000)
                {
                    tinhTienThueTN = (tinhTienThueTN * 10 / 100) - 250000;
                }
                else if (tinhTienThueTN > 10000000 && tinhTienThueTN <= 18000000)
                {
                    tinhTienThueTN = (tinhTienThueTN * 15 / 100) - 750000;
                }
                else if (tinhTienThueTN > 18000000 && tinhTienThueTN <= 32000000)
                {
                    tinhTienThueTN = (tinhTienThueTN * 20 / 100) - 1650000;
                }
                else if (tinhTienThueTN > 32000000 && tinhTienThueTN <= 52000000)
                {
                    tinhTienThueTN = (tinhTienThueTN * 25 / 100) - 3250000;
                }
                else if (tinhTienThueTN > 5200000 && tinhTienThueTN <= 80000000)
                {
                    tinhTienThueTN = (tinhTienThueTN * 30 / 100) - 5850000;
                }
                else tinhTienThueTN = (tinhTienThueTN * 35 / 100) - 9850000;
                return tinhTienThueTN ;
            }
        }//Hàm tính tiền thuế thu nhập
        
        private double getTongTienThue()
        {
            tongThue = 0;
            for (int i = 0; i < 12; i++)
            {
                tongThue += thueNop[i];
            }
            return this.tongThue;
        }//Hàm tính tổng tiền thuế
        private double getTienThueTT()
        {
            double tongLuong = 0;
            thueTT = 0;
            for (int i = 0; i < 12; i++)
            {
                tongLuong += luong[i];
            }
            thueTT = (tongLuong - Convert.ToDouble(cmdBHYT.Text) - 12 * (Convert.ToDouble(cmdNPT.Text) * Convert.ToDouble(cmdPT.Text)) - 12 * Convert.ToDouble(cmdNLD.Text)) / 12 ;
            return tinhTienThue(thueTT)*12;
        }//Hàm tính tiền thuế thực tế
        private void setTienLuong()
        {
            luong[0] = double.Parse(cmdT1.Text);
            luong[1] = double.Parse(cmdT2.Text);
            luong[2] = double.Parse(cmdT3.Text);
            luong[3] = double.Parse(cmdT4.Text);
            luong[4] = double.Parse(cmdT5.Text);
            luong[5] = double.Parse(cmdT6.Text);
            luong[6] = double.Parse(cmdT7.Text);
            luong[7] = double.Parse(cmdT8.Text);
            luong[8] = double.Parse(cmdT9.Text);
            luong[9] = double.Parse(cmdT10.Text);
            luong[10] = double.Parse(cmdT11.Text);
            luong[11] = double.Parse(cmdT12.Text);
        }// Hàm lưu tiền lương

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                cmdT1.Visible = true;
                cmdTT1.Visible = true;
            }
            if (checkBox1.Checked == false)
            {
                cmdT1.Visible = false;
                cmdTT1.Visible = false;
                cmdT1.Text = "0";
            }
        }//Hàm kiểm tra xem tháng được chọn và hiển thị phần nhập thuế và tiền thuế

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                cmdT2.Visible = true;
                cmdTT2.Visible = true;
            }
            if (checkBox2.Checked == false)
            {
                cmdT2.Visible = false;
                cmdTT2.Visible = false;
                cmdT2.Text = "0";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                cmdT3.Visible = true;
                cmdTT3.Visible = true;
            }
            if (checkBox3.Checked == false)
            {
                cmdT3.Visible = false;
                cmdTT3.Visible = false;
                cmdT3.Text = "0";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                cmdT4.Visible = true;
                cmdTT4.Visible = true;
            }
            if (checkBox4.Checked == false)
            {
                cmdT4.Visible = false;
                cmdTT4.Visible = false;
                cmdT4.Text = "0";
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                cmdT5.Visible = true;
                cmdTT5.Visible = true;
            }
            if (checkBox5.Checked == false)
            {
                cmdT5.Visible = false;
                cmdTT5.Visible = false;
                cmdT5.Text = "0";
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                cmdT6.Visible = true;
                cmdTT6.Visible = true;
            }
            if (checkBox6.Checked == false)
            {
                cmdT6.Visible = false;
                cmdTT6.Visible = false;
                cmdT6.Text = "0";
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                cmdT7.Visible = true;
                cmdTT7.Visible = true;
            }
            if (checkBox7.Checked == false)
            {
                cmdT7.Visible = false;
                cmdTT7.Visible = false;
                cmdT7.Text = "0";
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                cmdT8.Visible = true;
                cmdTT8.Visible = true;
            }
            if (checkBox8.Checked == false)
            {
                cmdT8.Visible = false;
                cmdTT8.Visible = false;
                cmdT8.Text = "0";
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked == true)
            {
                cmdT9.Visible = true;
                cmdTT9.Visible = true;
            }
            if (checkBox9.Checked == false)
            {
                cmdT9.Visible = false;
                cmdTT9.Visible = false;
                cmdT9.Text = "0";
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                cmdT10.Visible = true;
                cmdTT10.Visible = true;
            }
            if (checkBox10.Checked == false)
            {
                cmdT10.Visible = false;
                cmdTT10.Visible = false;
                cmdT10.Text = "0";
            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                cmdT11.Visible = true;
                cmdTT11.Visible = true;
            }
            if (checkBox11.Checked == false)
            {
                cmdT11.Visible = false;
                cmdTT11.Visible = false;
                cmdT11.Text = "0";
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked == true)
            {
                cmdT12.Visible = true;
                cmdTT12.Visible = true;
            }
            if (checkBox12.Checked == false)
            {
                cmdT12.Visible = false;
                cmdTT12.Visible = false;
                cmdT12.Text = "0";
            }
        }
        private void btnOK_Click_1(object sender, EventArgs e)
        {
            if (btnYes.Checked == false && btnNo.Checked == false)
            {
                MessageBox.Show("Bạn đã nộp tiền bảo hiểm chưa?", "THÔNG BÁO!");
            }
            else
            {
                setTienLuong();
                setTienThue();
                loadData();
                BangThue frmBangThue = new BangThue(dataTable, getTongTienThue()  , getTienThueTT() );
                frmBangThue.Show();
                this.Hide();
            }
        }// Kiểm tra tình trạng nộp bảo hiểm lấy giá trị tổng tiền thuế, tiền thuế thực tế, dữ liệu bảng truyền cho bảng thuế 



        private void cmdExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }// Thoát chương trình.
        private void btnYes_CheckedChanged(object sender, EventArgs e)
        {
            if (btnYes.Checked == true)
            {
                btnNo.Visible = false;
                cmdBHYT.ReadOnly = false;
            }
            else btnNo.Visible = true;
        }//Ẩn hiện ô Không

        private void btnNo_CheckedChanged(object sender, EventArgs e)
        {
            if (btnNo.Checked == true)
            {
                btnYes.Visible = false;
                cmdBHYT.ReadOnly = true;
                cmdBHYT.Text = "0";
            }
            else btnYes.Visible = true;
        }//Ân hiện ô có



        private void setTienThue()
        {
            thueNop[0] = getTienThue(double.Parse(cmdT1.Text));
            thueNop[1] = getTienThue(double.Parse(cmdT2.Text));
            thueNop[2] = getTienThue(double.Parse(cmdT3.Text));
            thueNop[3] = getTienThue(double.Parse(cmdT4.Text));
            thueNop[4] = getTienThue(double.Parse(cmdT5.Text));
            thueNop[5] = getTienThue(double.Parse(cmdT6.Text));
            thueNop[6] = getTienThue(double.Parse(cmdT7.Text));
            thueNop[7] = getTienThue(double.Parse(cmdT8.Text));
            thueNop[8] = getTienThue(double.Parse(cmdT9.Text));
            thueNop[9] = getTienThue(double.Parse(cmdT10.Text));
            thueNop[10] = getTienThue(double.Parse(cmdT11.Text));
            thueNop[11] = getTienThue(double.Parse(cmdT12.Text));
        }//Gán giá trị lương cho hàm tiền thuế để tính thuế

       
  

        private void cmdHanMuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }// Hàm có chức năng chỉ cho phép người dùng nhập số

        private void cmdBHYT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdPT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnThang_Click(object sender, EventArgs e)
        {
            if (btnYes.Checked == false && btnNo.Checked == false)
            {
                MessageBox.Show("Bạn đã nộp tiền bảo hiểm chưa?", "THÔNG BÁO!");
            }
            else if (btnYes.Checked == true)
            {
                if (cmdBHYT.Text == "0")
                {
                    MessageBox.Show("Bạn hãy nhập số tiền bảo hiểm đã nộp.", "THÔNG BÁO!");
                }
                else
                    setThueThang();
            }
            else
                setThueThang();

        }//Kiểm tra tình trạng nhập bảo hiểm và lưu giá trị tiền thuế 
        private void setThueThang() 
        {
            setTienThue();
            cmdTT1.Text = thueNop[0].ToString();
            cmdTT2.Text = thueNop[1].ToString();
            cmdTT3.Text = thueNop[2].ToString();
            cmdTT4.Text = thueNop[3].ToString();
            cmdTT5.Text = thueNop[4].ToString();
            cmdTT6.Text = thueNop[5].ToString();
            cmdTT7.Text = thueNop[6].ToString();
            cmdTT8.Text = thueNop[7].ToString();
            cmdTT9.Text = thueNop[8].ToString();
            cmdTT10.Text = thueNop[9].ToString();
            cmdTT11.Text = thueNop[10].ToString();
            cmdTT12.Text = thueNop[11].ToString();
        }
  
        private void cmdT2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void cmdT1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
