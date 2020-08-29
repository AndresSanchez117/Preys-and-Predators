namespace ProyectoFinal
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
            this.PictureBoxImage = new System.Windows.Forms.PictureBox();
            this.ButtonLoad = new System.Windows.Forms.Button();
            this.OpenFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            this.ButtonAddPrey = new System.Windows.Forms.Button();
            this.ButtonAddPredator = new System.Windows.Forms.Button();
            this.ButtonAddObjective = new System.Windows.Forms.Button();
            this.ButtonSimulate = new System.Windows.Forms.Button();
            this.LabelPreyCount = new System.Windows.Forms.Label();
            this.LabelPredatorCount = new System.Windows.Forms.Label();
            this.LabelPreySpeed = new System.Windows.Forms.Label();
            this.LabelPredatorSpeed = new System.Windows.Forms.Label();
            this.LabelPredatorRange = new System.Windows.Forms.Label();
            this.LabelStopEvent = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBoxImage
            // 
            this.PictureBoxImage.Location = new System.Drawing.Point(12, 12);
            this.PictureBoxImage.Name = "PictureBoxImage";
            this.PictureBoxImage.Size = new System.Drawing.Size(910, 700);
            this.PictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBoxImage.TabIndex = 0;
            this.PictureBoxImage.TabStop = false;
            this.PictureBoxImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxImage_MouseClick);
            // 
            // ButtonLoad
            // 
            this.ButtonLoad.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ButtonLoad.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLoad.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonLoad.Location = new System.Drawing.Point(965, 647);
            this.ButtonLoad.Name = "ButtonLoad";
            this.ButtonLoad.Size = new System.Drawing.Size(196, 65);
            this.ButtonLoad.TabIndex = 1;
            this.ButtonLoad.Text = "Cargar Entorno";
            this.ButtonLoad.UseVisualStyleBackColor = false;
            this.ButtonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // OpenFileDialogImage
            // 
            this.OpenFileDialogImage.FileName = "openFileDialog1";
            // 
            // ButtonAddPrey
            // 
            this.ButtonAddPrey.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonAddPrey.Enabled = false;
            this.ButtonAddPrey.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAddPrey.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonAddPrey.Location = new System.Drawing.Point(965, 306);
            this.ButtonAddPrey.Name = "ButtonAddPrey";
            this.ButtonAddPrey.Size = new System.Drawing.Size(196, 63);
            this.ButtonAddPrey.TabIndex = 2;
            this.ButtonAddPrey.Text = "Agregar Presas";
            this.ButtonAddPrey.UseVisualStyleBackColor = false;
            this.ButtonAddPrey.Click += new System.EventHandler(this.ButtonAddPrey_Click);
            // 
            // ButtonAddPredator
            // 
            this.ButtonAddPredator.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonAddPredator.Enabled = false;
            this.ButtonAddPredator.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAddPredator.Location = new System.Drawing.Point(965, 375);
            this.ButtonAddPredator.Name = "ButtonAddPredator";
            this.ButtonAddPredator.Size = new System.Drawing.Size(196, 70);
            this.ButtonAddPredator.TabIndex = 3;
            this.ButtonAddPredator.Text = "Agregar Depredadores";
            this.ButtonAddPredator.UseVisualStyleBackColor = false;
            this.ButtonAddPredator.Click += new System.EventHandler(this.ButtonAddPredator_Click);
            // 
            // ButtonAddObjective
            // 
            this.ButtonAddObjective.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonAddObjective.Enabled = false;
            this.ButtonAddObjective.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonAddObjective.Location = new System.Drawing.Point(965, 451);
            this.ButtonAddObjective.Name = "ButtonAddObjective";
            this.ButtonAddObjective.Size = new System.Drawing.Size(196, 62);
            this.ButtonAddObjective.TabIndex = 4;
            this.ButtonAddObjective.Text = "Agregar Objetivo";
            this.ButtonAddObjective.UseVisualStyleBackColor = false;
            this.ButtonAddObjective.Click += new System.EventHandler(this.ButtonAddObjective_Click);
            // 
            // ButtonSimulate
            // 
            this.ButtonSimulate.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonSimulate.Enabled = false;
            this.ButtonSimulate.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonSimulate.Location = new System.Drawing.Point(965, 519);
            this.ButtonSimulate.Name = "ButtonSimulate";
            this.ButtonSimulate.Size = new System.Drawing.Size(196, 60);
            this.ButtonSimulate.TabIndex = 5;
            this.ButtonSimulate.Text = "Iniciar Simulación";
            this.ButtonSimulate.UseVisualStyleBackColor = false;
            this.ButtonSimulate.Click += new System.EventHandler(this.ButtonSimulate_Click);
            // 
            // LabelPreyCount
            // 
            this.LabelPreyCount.AutoSize = true;
            this.LabelPreyCount.Font = new System.Drawing.Font("Book Antiqua", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPreyCount.Location = new System.Drawing.Point(947, 22);
            this.LabelPreyCount.Name = "LabelPreyCount";
            this.LabelPreyCount.Size = new System.Drawing.Size(0, 28);
            this.LabelPreyCount.TabIndex = 6;
            // 
            // LabelPredatorCount
            // 
            this.LabelPredatorCount.AutoSize = true;
            this.LabelPredatorCount.Font = new System.Drawing.Font("Book Antiqua", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPredatorCount.Location = new System.Drawing.Point(947, 134);
            this.LabelPredatorCount.Name = "LabelPredatorCount";
            this.LabelPredatorCount.Size = new System.Drawing.Size(0, 28);
            this.LabelPredatorCount.TabIndex = 7;
            // 
            // LabelPreySpeed
            // 
            this.LabelPreySpeed.AutoSize = true;
            this.LabelPreySpeed.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPreySpeed.Location = new System.Drawing.Point(947, 61);
            this.LabelPreySpeed.Name = "LabelPreySpeed";
            this.LabelPreySpeed.Size = new System.Drawing.Size(0, 24);
            this.LabelPreySpeed.TabIndex = 8;
            // 
            // LabelPredatorSpeed
            // 
            this.LabelPredatorSpeed.AutoSize = true;
            this.LabelPredatorSpeed.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPredatorSpeed.Location = new System.Drawing.Point(948, 207);
            this.LabelPredatorSpeed.Name = "LabelPredatorSpeed";
            this.LabelPredatorSpeed.Size = new System.Drawing.Size(0, 24);
            this.LabelPredatorSpeed.TabIndex = 9;
            // 
            // LabelPredatorRange
            // 
            this.LabelPredatorRange.AutoSize = true;
            this.LabelPredatorRange.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPredatorRange.Location = new System.Drawing.Point(948, 171);
            this.LabelPredatorRange.Name = "LabelPredatorRange";
            this.LabelPredatorRange.Size = new System.Drawing.Size(0, 24);
            this.LabelPredatorRange.TabIndex = 10;
            // 
            // LabelStopEvent
            // 
            this.LabelStopEvent.AutoSize = true;
            this.LabelStopEvent.Font = new System.Drawing.Font("Book Antiqua", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelStopEvent.ForeColor = System.Drawing.Color.Maroon;
            this.LabelStopEvent.Location = new System.Drawing.Point(946, 247);
            this.LabelStopEvent.Name = "LabelStopEvent";
            this.LabelStopEvent.Size = new System.Drawing.Size(0, 28);
            this.LabelStopEvent.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 727);
            this.Controls.Add(this.LabelStopEvent);
            this.Controls.Add(this.LabelPredatorRange);
            this.Controls.Add(this.LabelPredatorSpeed);
            this.Controls.Add(this.LabelPreySpeed);
            this.Controls.Add(this.LabelPredatorCount);
            this.Controls.Add(this.LabelPreyCount);
            this.Controls.Add(this.ButtonSimulate);
            this.Controls.Add(this.ButtonAddObjective);
            this.Controls.Add(this.ButtonAddPredator);
            this.Controls.Add(this.ButtonAddPrey);
            this.Controls.Add(this.ButtonLoad);
            this.Controls.Add(this.PictureBoxImage);
            this.Name = "Form1";
            this.Text = "Sanchez Salcedo Andres - Proyecto Final";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxImage;
        private System.Windows.Forms.Button ButtonLoad;
        private System.Windows.Forms.OpenFileDialog OpenFileDialogImage;
        private System.Windows.Forms.Button ButtonAddPrey;
        private System.Windows.Forms.Button ButtonAddPredator;
        private System.Windows.Forms.Button ButtonAddObjective;
        private System.Windows.Forms.Button ButtonSimulate;
        private System.Windows.Forms.Label LabelPreyCount;
        private System.Windows.Forms.Label LabelPredatorCount;
        private System.Windows.Forms.Label LabelPreySpeed;
        private System.Windows.Forms.Label LabelPredatorSpeed;
        private System.Windows.Forms.Label LabelPredatorRange;
        private System.Windows.Forms.Label LabelStopEvent;
    }
}

