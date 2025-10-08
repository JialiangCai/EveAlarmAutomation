using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveAlarmAutomation
{
    internal class ScanImage
    {
        public ScanImage(PictureBox alarm_zone_picture_box)
        {
            this.alarm_zone_picture_box = alarm_zone_picture_box;
            this._image_box_width = 100;
            this._image_box_height = 200;
            this._image_position_x = 0;
            this._image_position_y = 0;
        }

        public Bitmap CapturePicture()
        {
            Bitmap bitmap = new Bitmap(this._image_box_width, this._image_box_height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(new Point(this._image_position_x, this._image_position_y), Point.Empty, new Size(this._image_box_width, this._image_box_height));
            }
            return bitmap;
        }

        public void SaveScreenButtonClick(object sender, EventArgs e)
        {
            Bitmap bitmap = CapturePicture();
            alarm_zone_picture_box.Image = bitmap;
        }

        private PictureBox alarm_zone_picture_box;
        private int _image_box_width;
        private int _image_box_height;
        private int _image_position_x;
        private int _image_position_y;

    }
}