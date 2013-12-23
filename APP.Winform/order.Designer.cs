namespace APP.Winform
{
    partial class order
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.checi = new System.Windows.Forms.DataGridViewLinkColumn();
            this.fazhan = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ToStation = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Lishi = new System.Windows.Forms.DataGridViewLinkColumn();
            this.shangwu = new System.Windows.Forms.DataGridViewLinkColumn();
            this.tedeng = new System.Windows.Forms.DataGridViewLinkColumn();
            this.yideng = new System.Windows.Forms.DataGridViewLinkColumn();
            this.erdeng = new System.Windows.Forms.DataGridViewLinkColumn();
            this.gaojiruanwo = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ruanwo = new System.Windows.Forms.DataGridViewLinkColumn();
            this.yingwo = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ruanzuo = new System.Windows.Forms.DataGridViewLinkColumn();
            this.yingzuo = new System.Windows.Forms.DataGridViewLinkColumn();
            this.wuzuo = new System.Windows.Forms.DataGridViewLinkColumn();
            this.qita = new System.Windows.Forms.DataGridViewLinkColumn();
            this.orderbutton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ordertiket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(32, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(32, 34);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(127, 292);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(185, 8);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(111, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyUp);
            // 
            // listView2
            // 
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(185, 34);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(127, 292);
            this.listView2.TabIndex = 4;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Tile;
            this.listView2.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView2_ItemSelectionChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(337, 8);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(572, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checi,
            this.fazhan,
            this.ToStation,
            this.Lishi,
            this.shangwu,
            this.tedeng,
            this.yideng,
            this.erdeng,
            this.gaojiruanwo,
            this.ruanwo,
            this.yingwo,
            this.ruanzuo,
            this.yingzuo,
            this.wuzuo,
            this.qita,
            this.orderbutton,
            this.ordertiket});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(12, 67);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(1162, 568);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // checi
            // 
            this.checi.HeaderText = "车次";
            this.checi.Name = "checi";
            // 
            // fazhan
            // 
            this.fazhan.HeaderText = "发站";
            this.fazhan.Name = "fazhan";
            // 
            // ToStation
            // 
            this.ToStation.HeaderText = "到站";
            this.ToStation.Name = "ToStation";
            // 
            // Lishi
            // 
            this.Lishi.HeaderText = "历时";
            this.Lishi.Name = "Lishi";
            // 
            // shangwu
            // 
            this.shangwu.HeaderText = "商务座";
            this.shangwu.Name = "shangwu";
            // 
            // tedeng
            // 
            this.tedeng.HeaderText = "特等座";
            this.tedeng.Name = "tedeng";
            // 
            // yideng
            // 
            this.yideng.HeaderText = "一等座";
            this.yideng.Name = "yideng";
            // 
            // erdeng
            // 
            this.erdeng.HeaderText = "二等座";
            this.erdeng.Name = "erdeng";
            // 
            // gaojiruanwo
            // 
            this.gaojiruanwo.HeaderText = "高级软卧";
            this.gaojiruanwo.Name = "gaojiruanwo";
            // 
            // ruanwo
            // 
            this.ruanwo.HeaderText = "软卧";
            this.ruanwo.Name = "ruanwo";
            // 
            // yingwo
            // 
            this.yingwo.HeaderText = "硬卧";
            this.yingwo.Name = "yingwo";
            // 
            // ruanzuo
            // 
            this.ruanzuo.HeaderText = "软座";
            this.ruanzuo.Name = "ruanzuo";
            // 
            // yingzuo
            // 
            this.yingzuo.HeaderText = "硬座";
            this.yingzuo.Name = "yingzuo";
            // 
            // wuzuo
            // 
            this.wuzuo.HeaderText = "无座";
            this.wuzuo.Name = "wuzuo";
            this.wuzuo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.wuzuo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // qita
            // 
            this.qita.HeaderText = "其它";
            this.qita.Name = "qita";
            this.qita.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qita.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // orderbutton
            // 
            this.orderbutton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.orderbutton.HeaderText = "预定";
            this.orderbutton.Name = "orderbutton";
            this.orderbutton.Text = "预定";
            this.orderbutton.UseColumnTextForButtonValue = true;
            // 
            // ordertiket
            // 
            this.ordertiket.HeaderText = "Orderkey";
            this.ordertiket.Name = "ordertiket";
            this.ordertiket.Visible = false;
            // 
            // order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 690);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox1);
            this.Name = "order";
            this.Text = "order";
            this.Load += new System.EventHandler(this.order_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewLinkColumn checi;
        private System.Windows.Forms.DataGridViewLinkColumn fazhan;
        private System.Windows.Forms.DataGridViewLinkColumn ToStation;
        private System.Windows.Forms.DataGridViewLinkColumn Lishi;
        private System.Windows.Forms.DataGridViewLinkColumn shangwu;
        private System.Windows.Forms.DataGridViewLinkColumn tedeng;
        private System.Windows.Forms.DataGridViewLinkColumn yideng;
        private System.Windows.Forms.DataGridViewLinkColumn erdeng;
        private System.Windows.Forms.DataGridViewLinkColumn gaojiruanwo;
        private System.Windows.Forms.DataGridViewLinkColumn ruanwo;
        private System.Windows.Forms.DataGridViewLinkColumn yingwo;
        private System.Windows.Forms.DataGridViewLinkColumn ruanzuo;
        private System.Windows.Forms.DataGridViewLinkColumn yingzuo;
        private System.Windows.Forms.DataGridViewLinkColumn wuzuo;
        private System.Windows.Forms.DataGridViewLinkColumn qita;
        private System.Windows.Forms.DataGridViewButtonColumn orderbutton;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordertiket;
    }
}