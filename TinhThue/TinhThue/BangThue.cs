using System;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace TinhThue
{
    public partial class BangThue : Form
    {

        double tongTienThue = 0;  // Biến lưu tổng tiền thuế 
        double tienThueTT = 0;    // Biến lưu tiền thuế thực tê
        public BangThue()
        {
            InitializeComponent();
        }
        public BangThue(DataTable dataTable, double tongTienThue, double tienThueTT)
        {
            InitializeComponent();
            dgv.DataSource = dataTable;
            this.tongTienThue = tongTienThue; 
            this.tienThueTT = tienThueTT;
            chiTiet();
        } // Hàm dùng để gán giá trị của bảng, tổng tiền thuế , tiền thuế tực tế được truyền trừ bảng thông tin

        private void Export_Excel(string path)
        {
            Excel.Application ex = new Excel.Application();
            ex.Application.Workbooks.Add(Type.Missing);
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                ex.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
            }
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    ex.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value;
                }
            }
            ex.Columns.AutoFit();
            ex.ActiveWorkbook.SaveCopyAs(path);
            ex.ActiveWorkbook.Saved = true;

        }// Hàm để xuất bảng ra file excel

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            ThongTin FrmThongTin = new ThongTin();
            FrmThongTin.Show();
            this.Close();
        }//Hàm để tạo mới form thông tin khi ấn nút quay lại

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có muốn tạo file excel không?", "THÔNG BÁO!", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                SaveFileDialog s = new SaveFileDialog();
                s.Title = "Exprot Excel";
                s.Filter = "Excel (*.xlsx)|*.xlsx | Excel 2003 (*.xls)|*.xls";
                if (s.ShowDialog() == DialogResult.OK)
                {
                    Export_Excel(s.FileName);
                    MessageBox.Show("Bạn đã xuất ra file excel thành công!!");
                }
                else MessageBox.Show("Bạn đã xuất ra file excel không thành công!!");
            }
            else
            {

            }


        } //Hàm để đặt tên và lưu file excel

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }//Hàm để thoát
        private void chiTiet() 
        {
            txt1.Text = tongTienThue.ToString();
            txt2.Text = tienThueTT.ToString();
            if (tongTienThue > tienThueTT && tongTienThue > 0)
            {
                txtResult.Text = "Thuế nhận lại: ";
                txt3.Text = (tongTienThue - tienThueTT).ToString();
            }
            else 
            {
                txtResult.Text = "Thuế phải nộp: ";
                txt3.Text = (tienThueTT - tongTienThue).ToString();
            }
        }//Hàm để hiển thị bảng chi tiét thuế
        
    }
}
