using DarkUI.Controls;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{
    partial class EventCommandJoinFaction
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


#region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventCommandJoinFaction));
    this.grpJoinFaction = new DarkUI.Controls.DarkGroupBox();
    this.cmbVariable = new DarkUI.Controls.DarkComboBox();
    this.lblVariable = new System.Windows.Forms.Label();
    this.btnCancel = new DarkUI.Controls.DarkButton();
    this.btnSave = new DarkUI.Controls.DarkButton();
    this.grpJoinFaction.SuspendLayout();
    this.SuspendLayout();
                // 
                // grpJoinFaction
                // 
    this.grpJoinFaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
    this.grpJoinFaction.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
    this.grpJoinFaction.Controls.Add(this.cmbVariable);
    this.grpJoinFaction.Controls.Add(this.lblVariable);
    this.grpJoinFaction.Controls.Add(this.btnCancel);
    this.grpJoinFaction.Controls.Add(this.btnSave);
    this.grpJoinFaction.ForeColor = System.Drawing.Color.Gainsboro;
    this.grpJoinFaction.Location = new System.Drawing.Point(3, 3);
    this.grpJoinFaction.Name = "grpJoinFaction";
    this.grpJoinFaction.Size = new System.Drawing.Size(210, 116);
    this.grpJoinFaction.TabIndex = 17;
    this.grpJoinFaction.TabStop = false;
    this.grpJoinFaction.Text = "Join Faction:";
                // 
                // cmbVariable
                // 
    this.cmbVariable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
    this.cmbVariable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
    this.cmbVariable.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
    this.cmbVariable.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
    this.cmbVariable.ButtonIcon = ((System.Drawing.Bitmap)(resources.GetObject("cmbVariable.ButtonIcon")));
    this.cmbVariable.DrawDropdownHoverOutline = false;
    this.cmbVariable.DrawFocusRectangle = false;
    this.cmbVariable.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
    this.cmbVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
    this.cmbVariable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    this.cmbVariable.ForeColor = System.Drawing.Color.Gainsboro;
    this.cmbVariable.FormattingEnabled = true;
    this.cmbVariable.Location = new System.Drawing.Point(6, 41);
    this.cmbVariable.Name = "cmbVariable";
    this.cmbVariable.Size = new System.Drawing.Size(198, 21);
    this.cmbVariable.TabIndex = 24;
    this.cmbVariable.Text = null;
    this.cmbVariable.TextPadding = new System.Windows.Forms.Padding(2);
                // 
                // lblVariable
                // 
    this.lblVariable.AutoSize = true;
    this.lblVariable.Location = new System.Drawing.Point(6, 25);
    this.lblVariable.Name = "lblVariable";
    this.lblVariable.Size = new System.Drawing.Size(190, 13);
    this.lblVariable.TabIndex = 23;
    this.lblVariable.Text = "Player Variable containing Faction Name:";
                // 
                // btnCancel
                // 
    this.btnCancel.Location = new System.Drawing.Point(129, 83);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
    this.btnCancel.Size = new System.Drawing.Size(75, 23);
    this.btnCancel.TabIndex = 20;
    this.btnCancel.Text = "Cancel";
    this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
                // 
                // btnSave
                // 
    this.btnSave.Location = new System.Drawing.Point(6, 83);
    this.btnSave.Name = "btnSave";
    this.btnSave.Padding = new System.Windows.Forms.Padding(5);
    this.btnSave.Size = new System.Drawing.Size(75, 23);
    this.btnSave.TabIndex = 19;
    this.btnSave.Text = "Ok";
    this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
                // 
                // EventCommandJoinFaction
                // 
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.AutoSize = true;
    this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
    this.Controls.Add(this.grpJoinFaction);
    this.Name = "EventCommandJoinFaction";
    this.Size = new System.Drawing.Size(222, 129);
    this.grpJoinFaction.ResumeLayout(false);
    this.grpJoinFaction.PerformLayout();
    this.ResumeLayout(false);
    
            }

#endregion

        private DarkGroupBox grpJoinFaction;
        private DarkButton btnCancel;
        private DarkButton btnSave;
        private DarkComboBox cmbVariable;
        private System.Windows.Forms.Label lblVariable;
    }
}