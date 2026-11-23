using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace FreeRebex
{
    public class MainForm : Form
    {
        private Label topLabel;
        private Button centerButton;
        private PictureBox displayBox;

        public MainForm()
        {
            // Window setup
            this.Text = "FreeRebex";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Top text label
            topLabel = new Label();
            topLabel.Text = "Free robux in 2026";
            topLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            topLabel.Dock = DockStyle.Top;
            topLabel.Height = 60;
            topLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Center button
            centerButton = new Button();
            centerButton.Text = "Click Me To Get";
            centerButton.Size = new Size(140, 45);
            centerButton.Font = new Font("Segoe UI", 11);
            centerButton.Click += CenterButton_Click;

            // Keep button centered when window resizes
            this.Resize += (s, e) => CenterControl(centerButton);

            // PictureBox for stretched image (hidden initially)
            displayBox = new PictureBox();
            displayBox.Dock = DockStyle.Fill;
            displayBox.SizeMode = PictureBoxSizeMode.StretchImage;
            displayBox.Visible = false;

            // Add controls to form
            this.Controls.Add(topLabel);
            this.Controls.Add(centerButton);
            this.Controls.Add(displayBox);

            CenterControl(centerButton);
        }

        private void CenterControl(Control ctrl)
        {
            ctrl.Left = (this.ClientSize.Width - ctrl.Width) / 2;
            ctrl.Top = (this.ClientSize.Height - ctrl.Height) / 2;
        }

        private void CenterButton_Click(object sender, EventArgs e)
        {
            // Hide text and button
            topLabel.Visible = false;
            centerButton.Visible = false;

            Assembly asm = Assembly.GetExecutingAssembly();

            // Load embedded image
            Stream imgStream = asm.GetManifestResourceStream("img.png");
            if (imgStream != null)
            {
                displayBox.Image = Image.FromStream(imgStream);
                displayBox.Visible = true;
            }
            else
            {
                MessageBox.Show("Embedded img.png not found.");
            }

            // Load and play embedded audio
            Stream audStream = asm.GetManifestResourceStream("aud.wav");
            if (audStream != null)
            {
                SoundPlayer player = new SoundPlayer(audStream);
                player.Play();
            }
            else
            {
                MessageBox.Show("Embedded aud.wav not found.");
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
