
namespace Frogger
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.life1Picture = new System.Windows.Forms.PictureBox();
            this.life2Picture = new System.Windows.Forms.PictureBox();
            this.life3Picture = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subTitleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.life1Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.life2Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.life3Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // life1Picture
            // 
            this.life1Picture.BackColor = System.Drawing.Color.Transparent;
            this.life1Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.life1Picture.Location = new System.Drawing.Point(13, 13);
            this.life1Picture.Name = "life1Picture";
            this.life1Picture.Size = new System.Drawing.Size(60, 56);
            this.life1Picture.TabIndex = 0;
            this.life1Picture.TabStop = false;
            // 
            // life2Picture
            // 
            this.life2Picture.BackColor = System.Drawing.Color.Transparent;
            this.life2Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.life2Picture.Location = new System.Drawing.Point(94, 13);
            this.life2Picture.Name = "life2Picture";
            this.life2Picture.Size = new System.Drawing.Size(60, 56);
            this.life2Picture.TabIndex = 1;
            this.life2Picture.TabStop = false;
            // 
            // life3Picture
            // 
            this.life3Picture.BackColor = System.Drawing.Color.Transparent;
            this.life3Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.life3Picture.Location = new System.Drawing.Point(175, 13);
            this.life3Picture.Name = "life3Picture";
            this.life3Picture.Size = new System.Drawing.Size(60, 56);
            this.life3Picture.TabIndex = 2;
            this.life3Picture.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Showcard Gothic", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(-54, 153);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(668, 109);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Frogger";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subTitleLabel
            // 
            this.subTitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.subTitleLabel.Font = new System.Drawing.Font("Showcard Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subTitleLabel.Location = new System.Drawing.Point(-9, 262);
            this.subTitleLabel.Name = "subTitleLabel";
            this.subTitleLabel.Size = new System.Drawing.Size(577, 107);
            this.subTitleLabel.TabIndex = 4;
            this.subTitleLabel.Text = "Press \"Space\" to Start      Press \"Esc\" to Exit";
            this.subTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(550, 500);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.life3Picture);
            this.Controls.Add(this.life2Picture);
            this.Controls.Add(this.life1Picture);
            this.Controls.Add(this.subTitleLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.life1Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.life2Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.life3Picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.PictureBox life1Picture;
        private System.Windows.Forms.PictureBox life2Picture;
        private System.Windows.Forms.PictureBox life3Picture;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subTitleLabel;
    }
}

