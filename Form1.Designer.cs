partial class ImageProcessing
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
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageFileName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.grayScaleButton = new System.Windows.Forms.Button();
            this.negativeButton = new System.Windows.Forms.Button();
            this.switchButton = new System.Windows.Forms.Button();
            this.revertOriginalButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Detect = new System.Windows.Forms.Button();
            this.componentLabelingButton = new System.Windows.Forms.Button();
            this.contrastTracker = new System.Windows.Forms.TrackBar();
            this.contrastButton = new System.Windows.Forms.Button();
            this.thresholdTracker = new System.Windows.Forms.TrackBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.clearInputButton = new System.Windows.Forms.Button();
            this.clearOutputButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.DilationButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTracker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTracker)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadImageButton.Location = new System.Drawing.Point(20, 11);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(143, 30);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load input image...";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Bitmap files (*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg)|*.bmp;*.gif;*.jpg;*.png;*.ti" +
    "ff;*.jpeg";
            this.openImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // imageFileName
            // 
            this.imageFileName.Font = new System.Drawing.Font("Arial", 10.74627F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageFileName.Location = new System.Drawing.Point(167, 14);
            this.imageFileName.Name = "imageFileName";
            this.imageFileName.ReadOnly = true;
            this.imageFileName.Size = new System.Drawing.Size(513, 24);
            this.imageFileName.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(504, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // applyButton
            // 
            this.applyButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyButton.Location = new System.Drawing.Point(7, 106);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(143, 25);
            this.applyButton.TabIndex = 7;
            this.applyButton.Text = "Apply Threshold";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyThresholdButton_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.Filter = "Bitmap file (*.bmp)|*.bmp";
            this.saveImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(7, 74);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(143, 25);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save Output (BMP)";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(530, 35);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(513, 512);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(530, 555);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(512, 18);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            // 
            // grayScaleButton
            // 
            this.grayScaleButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grayScaleButton.Location = new System.Drawing.Point(7, 75);
            this.grayScaleButton.Name = "grayScaleButton";
            this.grayScaleButton.Size = new System.Drawing.Size(143, 25);
            this.grayScaleButton.TabIndex = 8;
            this.grayScaleButton.Text = "Apply Grayscale";
            this.grayScaleButton.UseVisualStyleBackColor = true;
            this.grayScaleButton.Click += new System.EventHandler(this.grayScaleButton_Click);
            // 
            // negativeButton
            // 
            this.negativeButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.negativeButton.Location = new System.Drawing.Point(7, 11);
            this.negativeButton.Name = "negativeButton";
            this.negativeButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.negativeButton.Size = new System.Drawing.Size(143, 25);
            this.negativeButton.TabIndex = 9;
            this.negativeButton.Text = "Apply Negative";
            this.negativeButton.UseVisualStyleBackColor = true;
            this.negativeButton.Click += new System.EventHandler(this.negativeButton_Click);
            // 
            // switchButton
            // 
            this.switchButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.switchButton.Location = new System.Drawing.Point(7, 11);
            this.switchButton.Name = "switchButton";
            this.switchButton.Size = new System.Drawing.Size(143, 25);
            this.switchButton.TabIndex = 10;
            this.switchButton.Text = "Switch Images";
            this.switchButton.UseVisualStyleBackColor = true;
            this.switchButton.Click += new System.EventHandler(this.switchButton_Click);
            // 
            // revertOriginalButton
            // 
            this.revertOriginalButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.revertOriginalButton.Location = new System.Drawing.Point(7, 42);
            this.revertOriginalButton.Name = "revertOriginalButton";
            this.revertOriginalButton.Size = new System.Drawing.Size(143, 25);
            this.revertOriginalButton.TabIndex = 13;
            this.revertOriginalButton.Text = "Revert to Original";
            this.revertOriginalButton.UseVisualStyleBackColor = true;
            this.revertOriginalButton.Click += new System.EventHandler(this.revertOriginalButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.LavenderBlush;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DilationButton);
            this.panel1.Controls.Add(this.Detect);
            this.panel1.Controls.Add(this.componentLabelingButton);
            this.panel1.Controls.Add(this.contrastTracker);
            this.panel1.Controls.Add(this.contrastButton);
            this.panel1.Controls.Add(this.thresholdTracker);
            this.panel1.Controls.Add(this.applyButton);
            this.panel1.Controls.Add(this.grayScaleButton);
            this.panel1.Controls.Add(this.negativeButton);
            this.panel1.Location = new System.Drawing.Point(7, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 247);
            this.panel1.TabIndex = 19;
            // 
            // Detect
            // 
            this.Detect.Font = new System.Drawing.Font("Arial", 10F);
            this.Detect.Location = new System.Drawing.Point(7, 170);
            this.Detect.Name = "Detect";
            this.Detect.Size = new System.Drawing.Size(142, 23);
            this.Detect.TabIndex = 25;
            this.Detect.Text = "Detect";
            this.Detect.UseVisualStyleBackColor = true;
            this.Detect.Click += new System.EventHandler(this.Detect_Click);
            // 
            // componentLabelingButton
            // 
            this.componentLabelingButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.componentLabelingButton.Location = new System.Drawing.Point(7, 138);
            this.componentLabelingButton.Name = "componentLabelingButton";
            this.componentLabelingButton.Size = new System.Drawing.Size(143, 25);
            this.componentLabelingButton.TabIndex = 24;
            this.componentLabelingButton.Text = "Label Objects";
            this.componentLabelingButton.UseVisualStyleBackColor = true;
            this.componentLabelingButton.Click += new System.EventHandler(this.componentLabelingButton_Click);
            // 
            // contrastTracker
            // 
            this.contrastTracker.Location = new System.Drawing.Point(153, 43);
            this.contrastTracker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.contrastTracker.Maximum = 100;
            this.contrastTracker.Minimum = -100;
            this.contrastTracker.Name = "contrastTracker";
            this.contrastTracker.Size = new System.Drawing.Size(102, 45);
            this.contrastTracker.TabIndex = 23;
            this.contrastTracker.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // contrastButton
            // 
            this.contrastButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contrastButton.Location = new System.Drawing.Point(7, 43);
            this.contrastButton.Name = "contrastButton";
            this.contrastButton.Size = new System.Drawing.Size(143, 25);
            this.contrastButton.TabIndex = 22;
            this.contrastButton.Text = "Apply Contrast";
            this.contrastButton.UseVisualStyleBackColor = true;
            this.contrastButton.Click += new System.EventHandler(this.contrastButton_Click);
            // 
            // thresholdTracker
            // 
            this.thresholdTracker.Location = new System.Drawing.Point(150, 106);
            this.thresholdTracker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.thresholdTracker.Maximum = 20;
            this.thresholdTracker.Name = "thresholdTracker";
            this.thresholdTracker.Size = new System.Drawing.Size(102, 45);
            this.thresholdTracker.TabIndex = 21;
            this.thresholdTracker.TickStyle = System.Windows.Forms.TickStyle.None;
            this.thresholdTracker.Value = 5;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.clearInputButton);
            this.panel2.Controls.Add(this.clearOutputButton);
            this.panel2.Controls.Add(this.revertOriginalButton);
            this.panel2.Controls.Add(this.saveButton);
            this.panel2.Controls.Add(this.switchButton);
            this.panel2.Location = new System.Drawing.Point(6, 28);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(164, 172);
            this.panel2.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 10.74627F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 110);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 17);
            this.label7.TabIndex = 23;
            this.label7.Text = "Clear:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clearInputButton
            // 
            this.clearInputButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearInputButton.Location = new System.Drawing.Point(7, 131);
            this.clearInputButton.Name = "clearInputButton";
            this.clearInputButton.Size = new System.Drawing.Size(70, 25);
            this.clearInputButton.TabIndex = 15;
            this.clearInputButton.Text = "All";
            this.clearInputButton.UseVisualStyleBackColor = true;
            this.clearInputButton.Click += new System.EventHandler(this.clearInputButton_Click);
            // 
            // clearOutputButton
            // 
            this.clearOutputButton.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearOutputButton.Location = new System.Drawing.Point(82, 131);
            this.clearOutputButton.Name = "clearOutputButton";
            this.clearOutputButton.Size = new System.Drawing.Size(68, 25);
            this.clearOutputButton.TabIndex = 14;
            this.clearOutputButton.Text = "Output";
            this.clearOutputButton.UseVisualStyleBackColor = true;
            this.clearOutputButton.Click += new System.EventHandler(this.clearOutputButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Location = new System.Drawing.Point(9, 70);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(272, 583);
            this.panel3.TabIndex = 14;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.label3);
            this.panel8.Controls.Add(this.panel1);
            this.panel8.Location = new System.Drawing.Point(2, 7);
            this.panel8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(268, 283);
            this.panel8.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 13.97015F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 22);
            this.label3.TabIndex = 21;
            this.label3.Text = "IMAGE PROCESSING";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.panel2);
            this.panel7.Location = new System.Drawing.Point(2, 293);
            this.panel7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(176, 210);
            this.panel7.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 13.97015F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 22);
            this.label4.TabIndex = 22;
            this.label4.Text = "OTHERS";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.LoadImageButton);
            this.panel4.Controls.Add(this.imageFileName);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1358, 55);
            this.panel4.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 13.97015F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(831, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(479, 22);
            this.label6.TabIndex = 21;
            this.label6.Text = "IMAGE PROCESSING PROGRAM - PLAYING CARDS";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 664);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1358, 35);
            this.panel5.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 11.8209F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(433, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(524, 18);
            this.label5.TabIndex = 21;
            this.label5.Text = "Created by: Calvin Poleij (5667747) and Leon de Reeder (5530962), 2016";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 13.97015F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(193, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 22);
            this.label1.TabIndex = 19;
            this.label1.Text = "INPUT IMAGE";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 13.97015F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(719, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 22);
            this.label2.TabIndex = 20;
            this.label2.Text = "OUTPUT IMAGE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Controls.Add(this.pictureBox2);
            this.panel6.Controls.Add(this.progressBar);
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.pictureBox1);
            this.panel6.Location = new System.Drawing.Point(285, 70);
            this.panel6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1064, 583);
            this.panel6.TabIndex = 21;
            // 
            // DilationButton
            // 
            this.DilationButton.Font = new System.Drawing.Font("Arial", 11F);
            this.DilationButton.Location = new System.Drawing.Point(7, 200);
            this.DilationButton.Name = "DilationButton";
            this.DilationButton.Size = new System.Drawing.Size(142, 23);
            this.DilationButton.TabIndex = 26;
            this.DilationButton.Text = "Dilation";
            this.DilationButton.UseVisualStyleBackColor = true;
            this.DilationButton.Click += Dilation_Click;
            // 
            // ImageProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(1358, 699);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "ImageProcessing";
            this.ShowIcon = false;
            this.Text = "Image Processing Program - Playing Card Recognition";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTracker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTracker)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button LoadImageButton;
    private System.Windows.Forms.OpenFileDialog openImageDialog;
    private System.Windows.Forms.TextBox imageFileName;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button applyButton;
    private System.Windows.Forms.SaveFileDialog saveImageDialog;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.ProgressBar progressBar;

    // Controls that were added by us.
    private System.Windows.Forms.Button grayScaleButton;
    private System.Windows.Forms.Button negativeButton;
    private System.Windows.Forms.Button switchButton;
    private System.Windows.Forms.Button revertOriginalButton;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TrackBar thresholdTracker;
    private System.Windows.Forms.Panel panel6;
    private System.Windows.Forms.Panel panel7;
    private System.Windows.Forms.Panel panel8;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button clearOutputButton;
    private System.Windows.Forms.Button clearInputButton;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TrackBar contrastTracker;
    private System.Windows.Forms.Button contrastButton;
    private System.Windows.Forms.Button componentLabelingButton;
    private System.Windows.Forms.Button Detect;
    private System.Windows.Forms.Button DilationButton;
}

