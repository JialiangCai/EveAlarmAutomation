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
                System.Diagnostics.Debug.WriteLine("CaptureLoop:" + this._capture_thread_cancel_requested);
                Bitmap bitmap = CaptureImage();
                alarm_zone_picture_box.Invoke(new Action(() =>
                {
                    alarm_zone_picture_box.Image = bitmap;
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
                g.CopyFromScreen(new Point(this._image_position_x, this._image_position_y), Point.Empty, new Size(this._image_box_width, this._image_box_height));
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