
namespace odkladiste
{
    partial class Menu
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.game = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.info = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.rules = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // game
            // 
            this.game.Location = new System.Drawing.Point(320, 200);
            this.game.Name = "game";
            this.game.Size = new System.Drawing.Size(160, 100);
            this.game.TabIndex = 0;
            this.game.Text = "Start";
            this.game.UseVisualStyleBackColor = true;
            this.game.Click += new System.EventHandler(this.game_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(200, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(403, 64);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hungry Horace";
            // 
            // info
            // 
            this.info.Location = new System.Drawing.Point(340, 340);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(120, 80);
            this.info.TabIndex = 2;
            this.info.Text = "Info";
            this.info.UseVisualStyleBackColor = true;
            this.info.Click += new System.EventHandler(this.info_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(40, 340);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(120, 80);
            this.exit.TabIndex = 3;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // rules
            // 
            this.rules.Location = new System.Drawing.Point(640, 340);
            this.rules.Name = "rules";
            this.rules.Size = new System.Drawing.Size(120, 80);
            this.rules.TabIndex = 4;
            this.rules.Text = "Rules";
            this.rules.UseVisualStyleBackColor = true;
            this.rules.Click += new System.EventHandler(this.rules_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rules);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.info);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.game);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Menu";
            this.Text = "Hungry Horace";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button game;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button info;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button rules;
    }
}