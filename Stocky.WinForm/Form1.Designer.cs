namespace Stocky.WinForm
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
            this.btnSaveHistoricalData = new System.Windows.Forms.Button();
            this.txtStocks = new System.Windows.Forms.TextBox();
            this.btnShowChart = new System.Windows.Forms.Button();
            this.btnSaveEODData = new System.Windows.Forms.Button();
            this.btnReadTweets = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSaveHistoricalData
            // 
            this.btnSaveHistoricalData.Location = new System.Drawing.Point(262, 79);
            this.btnSaveHistoricalData.Name = "btnSaveHistoricalData";
            this.btnSaveHistoricalData.Size = new System.Drawing.Size(172, 49);
            this.btnSaveHistoricalData.TabIndex = 0;
            this.btnSaveHistoricalData.Text = "Save Historical Data";
            this.btnSaveHistoricalData.UseVisualStyleBackColor = true;
            this.btnSaveHistoricalData.Click += new System.EventHandler(this.btnSaveHistoricalData_Click);
            // 
            // txtStocks
            // 
            this.txtStocks.Location = new System.Drawing.Point(518, 43);
            this.txtStocks.Multiline = true;
            this.txtStocks.Name = "txtStocks";
            this.txtStocks.Size = new System.Drawing.Size(190, 211);
            this.txtStocks.TabIndex = 1;
            // 
            // btnShowChart
            // 
            this.btnShowChart.Location = new System.Drawing.Point(518, 261);
            this.btnShowChart.Name = "btnShowChart";
            this.btnShowChart.Size = new System.Drawing.Size(190, 42);
            this.btnShowChart.TabIndex = 2;
            this.btnShowChart.Text = "Show Charts";
            this.btnShowChart.UseVisualStyleBackColor = true;
            this.btnShowChart.Click += new System.EventHandler(this.btnShowChart_Click);
            // 
            // btnSaveEODData
            // 
            this.btnSaveEODData.Location = new System.Drawing.Point(262, 169);
            this.btnSaveEODData.Name = "btnSaveEODData";
            this.btnSaveEODData.Size = new System.Drawing.Size(172, 50);
            this.btnSaveEODData.TabIndex = 3;
            this.btnSaveEODData.Text = "Save EOD Data";
            this.btnSaveEODData.UseVisualStyleBackColor = true;
            this.btnSaveEODData.Click += new System.EventHandler(this.btnSaveEODData_Click);
            // 
            // btnReadTweets
            // 
            this.btnReadTweets.Location = new System.Drawing.Point(262, 261);
            this.btnReadTweets.Name = "btnReadTweets";
            this.btnReadTweets.Size = new System.Drawing.Size(172, 42);
            this.btnReadTweets.TabIndex = 4;
            this.btnReadTweets.Text = "Read Tweets";
            this.btnReadTweets.UseVisualStyleBackColor = true;
            this.btnReadTweets.Click += new System.EventHandler(this.btnReadTweets_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnReadTweets);
            this.Controls.Add(this.btnSaveEODData);
            this.Controls.Add(this.btnShowChart);
            this.Controls.Add(this.txtStocks);
            this.Controls.Add(this.btnSaveHistoricalData);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveHistoricalData;
        private System.Windows.Forms.TextBox txtStocks;
        private System.Windows.Forms.Button btnShowChart;
        private System.Windows.Forms.Button btnSaveEODData;
        private System.Windows.Forms.Button btnReadTweets;
    }
}

