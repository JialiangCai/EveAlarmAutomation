namespace EveAlarmAutomation
{
    partial class EveAlarmAutomationMainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            alarm_automation_button = new Button();
            alarm_automation_settings_button = new Button();
            alarm_zone_picture_box = new PictureBox();
            alarm_logging_box = new TextBox();
            scan_image = new ScanImage(alarm_zone_picture_box, alarm_automation_button);

            FormBorderStyle = FormBorderStyle.FixedSingle; // 添加此行，设置为固定边框
            MaximizeBox = false; // 添加此行，禁止最大化
            TopMost = true; // 保证窗口置顶
            ((System.ComponentModel.ISupportInitialize)alarm_zone_picture_box).BeginInit();
            SuspendLayout();
            // 
            // Alarm Automation Button
            // 
            alarm_automation_button.Location = new Point(225, 362);
            alarm_automation_button.Name = "alarm_automation_button";
            alarm_automation_button.Size = new Size(94, 37);
            alarm_automation_button.TabIndex = 0;
            alarm_automation_button.Text = "Start";
            alarm_automation_button.ForeColor = Color.Green;
            alarm_automation_button.UseVisualStyleBackColor = true;
            alarm_automation_button.Click += scan_image.CaptureButtonClick;
            // 
            // Alarm Automation Settings Button
            // 
            alarm_automation_settings_button.Location = new Point(225, 405);
            alarm_automation_settings_button.Name = "alarm_automation_settings_button";
            alarm_automation_settings_button.Size = new Size(94, 37);
            alarm_automation_settings_button.TabIndex = 1;
            alarm_automation_settings_button.Text = "Settings";
            alarm_automation_settings_button.UseVisualStyleBackColor = true;
            alarm_automation_settings_button.Click += scan_image.SetCaptureAreaButtonClick;
            // 
            // Alarm Zone Picture Box
            // 
            alarm_zone_picture_box.Location = new Point(215, 12);
            alarm_zone_picture_box.Name = "alarm_zone_picture_box";
            alarm_zone_picture_box.Size = new Size(113, 344);
            alarm_zone_picture_box.SizeMode = PictureBoxSizeMode.StretchImage;
            alarm_zone_picture_box.TabIndex = 2;
            alarm_zone_picture_box.TabStop = false;
            // 
            // Alarm Logging Box
            // 
            alarm_logging_box.Location = new Point(12, 12);
            alarm_logging_box.Multiline = true;
            alarm_logging_box.Name = "alarm_logging_box";
            alarm_logging_box.Size = new Size(197, 426);
            alarm_logging_box.TabIndex = 3;
            // 
            // EveAlarm Automation Main Form
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 450);
            Controls.Add(alarm_logging_box);
            Controls.Add(alarm_zone_picture_box);
            Controls.Add(alarm_automation_button);
            Controls.Add(alarm_automation_settings_button);
            Name = "EveAlarmAutomationMainForm";
            Text = "EVE Alarm Automation";
            ((System.ComponentModel.ISupportInitialize)alarm_zone_picture_box).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button alarm_automation_button;
        private Button alarm_automation_settings_button;
        private TextBox alarm_logging_box;
        private PictureBox alarm_zone_picture_box;

        private ScanImage scan_image;
    }
}
