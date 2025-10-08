using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveAlarmAutomation
{
    internal class ScanImage
    {
        public ScanImage(PictureBox alarm_zone_picture_box,Button alarm_automation_button)
        {
            this.alarm_zone_picture_box = alarm_zone_picture_box;
            this.alarm_automation_button = alarm_automation_button;
            this._image_box_width = 100;
            this._image_box_height = 200;
            this._image_position_x = 0;
            this._image_position_y = 0;

            this._capture_thread = null;
            this._capture_thread_cancel_requested = false;
        }

        private void CaptureLoop()
        {
            while (!this._capture_thread_cancel_requested)
            {
                Bitmap bitmap = CaptureImage();
                alarm_zone_picture_box.Invoke(new Action(() =>
                {
                    Bitmap? oldBitmap = alarm_zone_picture_box.Image as Bitmap;
                    alarm_zone_picture_box.Image = bitmap;
                    oldBitmap?.Dispose();
                }));
            }
            this.alarm_zone_picture_box.Image = null;
        }


        private void StartContinuousCapture(int intervalMs = 10)
        {
            if (this._capture_thread != null && this._capture_thread.IsAlive)
            {
                return; // Already running
            }
            this._capture_thread_cancel_requested = false;
            this._capture_thread = new Thread(CaptureLoop)
            {
                IsBackground = true
            };
            this._capture_thread.Start();
        }

        private void StopContinuousCapture()
        {
            if (this._capture_thread != null && this._capture_thread.IsAlive)
            {
                this._capture_thread_cancel_requested = true;
            }
        }

        private Bitmap CaptureImage()
        {
            Bitmap bitmap = new Bitmap(this._image_box_width, this._image_box_height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                try
                {
                    Point source_point = new Point(this._image_position_x, this._image_position_y);
                    Size image_size = new Size(this._image_box_width, this._image_box_height);
                    g.CopyFromScreen(source_point, Point.Empty, image_size);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error capturing screen: " + ex.Message);
                }
            }
            return bitmap;
        }

        public void CaptureButtonClick(object sender, EventArgs e)
        {
            if (this._capture_thread != null && this._capture_thread.IsAlive)
            {
                StopContinuousCapture();
                this.alarm_automation_button.Text = "Start";
                alarm_automation_button.ForeColor = Color.Green;
            }
            else
            {
                StartContinuousCapture();
                this.alarm_automation_button.Text = "Stop";
                alarm_automation_button.ForeColor = Color.Red;
            }
        }

        private void CreateCaptureAreaForm()
        {
            Form CaptureAreaForm = new Form();
            CaptureAreaForm.Text = "Capture Area Settings";
            CaptureAreaForm.Size = new Size(300, 250);
            CaptureAreaForm.StartPosition = FormStartPosition.CenterParent;
            Label widthLabel = new Label() { Text = "Width:", Location = new Point(10, 20), AutoSize = true };
            TextBox widthTextBox = new TextBox() { Text = this._image_box_width.ToString(), Location = new Point(100, 20), Width = 100 };
            Label heightLabel = new Label() { Text = "Height:", Location = new Point(10, 60), AutoSize = true };
            TextBox heightTextBox = new TextBox() { Text = this._image_box_height.ToString(), Location = new Point(100, 60), Width = 100 };
            Label posXLabel = new Label() { Text = "Position X:", Location = new Point(10, 100), AutoSize = true };
            TextBox posXTextBox = new TextBox() { Text = this._image_position_x.ToString(), Location = new Point(100, 100), Width = 100 };
            Label posYLabel = new Label() { Text = "Position Y:", Location = new Point(10, 140), AutoSize = true };
            TextBox posYTextBox = new TextBox() { Text = this._image_position_y.ToString(), Location = new Point(100, 140), Width = 100 };
            Button saveButton = new Button() { Text = "Save", Location = new Point(100, 170), Width = 80 };
            saveButton.Click += (s, e) =>
            {
                if (int.TryParse(widthTextBox.Text, out int width) &&
                    int.TryParse(heightTextBox.Text, out int height) &&
                    int.TryParse(posXTextBox.Text, out int posX) &&
                    int.TryParse(posYTextBox.Text, out int posY))
                {
                    this._image_box_width = width;
                    this._image_box_height = height;
                    this._image_position_x = posX;
                    this._image_position_y = posY;
                    CaptureAreaForm.Close();
                }
                else
                {
                    MessageBox.Show("Please enter valid integer values.");
                }
            };

            CaptureAreaForm.Controls.Add(widthLabel);
            CaptureAreaForm.Controls.Add(heightLabel);
            CaptureAreaForm.Controls.Add(posXLabel);
            CaptureAreaForm.Controls.Add(posYLabel);
            CaptureAreaForm.Controls.Add(widthTextBox);
            CaptureAreaForm.Controls.Add(heightTextBox);
            CaptureAreaForm.Controls.Add(posXTextBox);
            CaptureAreaForm.Controls.Add(posYTextBox);
            CaptureAreaForm.Controls.Add(saveButton);

            CaptureAreaForm.Show();
        }

        public void SetCaptureAreaButtonClick(object sender, EventArgs e)
        {
            this.CreateCaptureAreaForm();
        }

        public void SaveScreenButtonClick(object sender, EventArgs e)
        {
            Bitmap bitmap = CaptureImage();
            alarm_zone_picture_box.Image = bitmap;
        }

        private PictureBox alarm_zone_picture_box;
        private Button alarm_automation_button;
        private int _image_box_width;
        private int _image_box_height;
        private int _image_position_x;
        private int _image_position_y;
        private Thread? _capture_thread;
        private volatile bool _capture_thread_cancel_requested;
    }
}