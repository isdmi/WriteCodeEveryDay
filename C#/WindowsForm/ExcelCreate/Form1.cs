using ClosedXML.Excel;
using System.Runtime.InteropServices;

namespace ExcelCreate
{
    public partial class Form1 : Form
    {
        // グローバルホットキー登録用
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 100;
        private const int VK_F9 = 0x78;  // F9キー

        public Form1()
        {
            InitializeComponent();

            // ホットキー登録（F9）
            RegisterHotKey(this.Handle, HOTKEY_ID, 0, VK_F9);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            if (m.Msg == WM_HOTKEY)
            {
                if (m.WParam.ToInt32() == HOTKEY_ID)
                {
                    SaveDesktopToExcel();
                }
            }

            base.WndProc(ref m);
        }

        private void SaveDesktopToExcel()
        {
            try
            {
                // デスクトップ全体をキャプチャ
                Bitmap bmp = CaptureDesktop();

                // Excel 保存（ClosedXML）
                SaveImageToExcel(bmp);

                MessageBox.Show("デスクトップ画像を Excel (ClosedXML) に保存しました。");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラー: {ex.Message}");
            }
        }

        private Bitmap CaptureDesktop()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;

            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0, bounds.Size);
            }

            return bitmap;
        }

        private void SaveImageToExcel(Bitmap bmp)
        {
            string path = $"DesktopCapture_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            // 一時ファイルとして画像を保存
            string tempImgPath = System.IO.Path.GetTempFileName() + ".png";
            bmp.Save(tempImgPath);

            // ClosedXML で Excel 作成
            using (var workbook = new XLWorkbook())
            {
                var sheet = workbook.Worksheets.Add("Capture");

                // 画像を貼り付け
                var picture = sheet.AddPicture(tempImgPath)
                                   .MoveTo(sheet.Cell("A1"))
                                   .WithSize(800, 450); // 任意のサイズに調整

                workbook.SaveAs(path);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);
            base.OnFormClosed(e);
        }
    }
}
