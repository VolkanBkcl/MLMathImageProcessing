
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MLMathImageProcessing
{
    public class MainForm : Form
    {
        PictureBox picture;
        Button btnLoad, btnGray, btnRecognizeDigit;
        Label lblResult, lblTitle;
        DigitRecognitionService digitRecognitionService;
        Panel topPanel, resultPanel;
        FlowLayoutPanel buttonPanel;

        public MainForm()
        {
            digitRecognitionService = new DigitRecognitionService();
            InitializeUI();
        }

        void InitializeUI()
        {
            Text = "ML Math Image Processing - Sayı Tanıma";
            Width = 1000;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(240, 240, 240);

            topPanel = new Panel();
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 60;
            topPanel.BackColor = Color.FromArgb(52, 73, 94);
            Controls.Add(topPanel);

            lblTitle = new Label();
            lblTitle.Text = "ML Math Image Processing - Sayı Tanıma Uygulaması";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 15);
            topPanel.Controls.Add(lblTitle);

            resultPanel = new Panel();
            resultPanel.Dock = DockStyle.Bottom;
            resultPanel.Height = 80;
            resultPanel.BackColor = Color.FromArgb(236, 240, 241);
            resultPanel.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(resultPanel);

            lblResult = new Label();
            lblResult.Text = "Sonuç: -";
            lblResult.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblResult.ForeColor = Color.FromArgb(44, 62, 80);
            lblResult.AutoSize = false;
            lblResult.Dock = DockStyle.Fill;
            lblResult.TextAlign = ContentAlignment.MiddleCenter;
            resultPanel.Controls.Add(lblResult);

            buttonPanel = new FlowLayoutPanel();
            buttonPanel.Dock = DockStyle.Top;
            buttonPanel.Height = 70;
            buttonPanel.BackColor = Color.FromArgb(236, 240, 241);
            buttonPanel.Padding = new Padding(10);
            Controls.Add(buttonPanel);

            picture = new PictureBox();
            picture.Dock = DockStyle.Fill;
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.BackColor = Color.White;
            picture.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(picture);

            btnLoad = CreateStyledButton("📁 Görüntü Yükle", Color.FromArgb(52, 152, 219));
            btnGray = CreateStyledButton("🎨 Gri Tonlama", Color.FromArgb(46, 204, 113));
            btnRecognizeDigit = CreateStyledButton("🔢 Sayı Tanı", Color.FromArgb(231, 76, 60));

            btnLoad.Click += LoadImage;
            btnGray.Click += ConvertGray;
            btnRecognizeDigit.Click += RecognizeDigit;

            buttonPanel.Controls.Add(btnLoad);
            buttonPanel.Controls.Add(btnGray);
            buttonPanel.Controls.Add(btnRecognizeDigit);
        }

        Button CreateStyledButton(string text, Color backColor)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Width = 150;
            btn.Height = 45;
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Margin = new Padding(5);
            
            btn.MouseEnter += (s, e) => {
                btn.BackColor = ControlPaint.Light(backColor, 0.1f);
            };
            btn.MouseLeave += (s, e) => {
                btn.BackColor = backColor;
            };
            
            return btn;
        }

        void LoadImage(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.bmp|All Files|*.*";
            open.Title = "Görüntü Seçin";
            
            if(open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picture.Image?.Dispose();
                    picture.Image = new Bitmap(Image.FromFile(open.FileName));
                    lblResult.Text = "Sonuç: Görüntü yüklendi. Sayı tanıma için 'Sayı Tanı' butonuna tıklayın.";
                    lblResult.ForeColor = Color.FromArgb(52, 152, 219);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Görüntü yüklenirken hata oluştu:\n{ex.Message}", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void ConvertGray(object sender, EventArgs e)
        {
            if(picture.Image == null)
            {
                MessageBox.Show("Lütfen önce bir görüntü yükleyin!", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Bitmap original = new Bitmap(picture.Image);
                Bitmap bmp = new Bitmap(original.Width, original.Height);

                for(int i = 0; i < bmp.Width; i++)
                {
                    for(int j = 0; j < bmp.Height; j++)
                    {
                        Color p = original.GetPixel(i, j);
                        int avg = (p.R + p.G + p.B) / 3;
                        bmp.SetPixel(i, j, Color.FromArgb(avg, avg, avg));
                    }
                }

                picture.Image?.Dispose();
                picture.Image = bmp;
                original.Dispose();
                
                lblResult.Text = "Sonuç: Görüntü gri tonlamaya dönüştürüldü.";
                lblResult.ForeColor = Color.FromArgb(46, 204, 113);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İşlem sırasında hata oluştu:\n{ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void RecognizeDigit(object sender, EventArgs e)
        {
            if(picture.Image == null)
            {
                MessageBox.Show("Lütfen önce bir görüntü yükleyin!", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnRecognizeDigit.Enabled = false;
                lblResult.Text = "Sonuç: Analiz ediliyor...";
                lblResult.ForeColor = Color.FromArgb(241, 196, 15);
                Application.DoEvents();

                Bitmap currentImage = new Bitmap(picture.Image);
                string result = digitRecognitionService.RecognizeDigit(currentImage);

                lblResult.Text = $"Sonuç: Tanınan Sayı = {result}";
                lblResult.ForeColor = Color.FromArgb(231, 76, 60);
                
                MessageBox.Show($"Tanınan Sayı: {result}\n\nNot: Bu basit bir tahmin algoritmasıdır. Daha iyi sonuçlar için eğitilmiş ML modeli kullanılabilir.", 
                    "Sayı Tanıma Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                currentImage.Dispose();
            }
            catch (Exception ex)
            {
                lblResult.Text = $"Hata: {ex.Message}";
                lblResult.ForeColor = Color.FromArgb(192, 57, 43);
                MessageBox.Show($"Sayı tanıma sırasında hata oluştu:\n{ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRecognizeDigit.Enabled = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            picture.Image?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
