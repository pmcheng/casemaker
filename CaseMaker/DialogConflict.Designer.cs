namespace CaseMaker
{
    partial class DialogConflict
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
            this.buttonOverwrite = new System.Windows.Forms.Button();
            this.buttonKeep = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOverwrite
            // 
            this.buttonOverwrite.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonOverwrite.Location = new System.Drawing.Point(12, 87);
            this.buttonOverwrite.Name = "buttonOverwrite";
            this.buttonOverwrite.Size = new System.Drawing.Size(111, 45);
            this.buttonOverwrite.TabIndex = 1;
            this.buttonOverwrite.Text = "Overwrite Demographics";
            this.buttonOverwrite.UseVisualStyleBackColor = true;
            // 
            // buttonKeep
            // 
            this.buttonKeep.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonKeep.Location = new System.Drawing.Point(129, 87);
            this.buttonKeep.Name = "buttonKeep";
            this.buttonKeep.Size = new System.Drawing.Size(121, 45);
            this.buttonKeep.TabIndex = 2;
            this.buttonKeep.Text = "Keep Current Demographics";
            this.buttonKeep.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(256, 88);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 45);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 76);
            this.label1.TabIndex = 4;
            this.label1.Text = "The demographic information for the incoming image conflicts with the current dem" +
                "ographics.  ";
            // 
            // DialogConflict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(356, 146);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonKeep);
            this.Controls.Add(this.buttonOverwrite);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogConflict";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Warning";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOverwrite;
        private System.Windows.Forms.Button buttonKeep;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
    }
}