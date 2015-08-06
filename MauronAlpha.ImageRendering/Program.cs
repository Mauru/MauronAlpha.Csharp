using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


public class MainForm : System.Windows.Forms.Form {
	private System.ComponentModel.Container components;
	public MainForm() {
		InitializeComponent();
		CenterToScreen();
		SetStyle(ControlStyles.ResizeRedraw, true);
	}

	protected override void Dispose(bool disposing) {
		if (disposing) {
			if (components != null) {
				components.Dispose();
			}
		}
		base.Dispose(disposing);
	}
	private void InitializeComponent() {
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.ClientSize = new System.Drawing.Size(292, 273);
		this.Text = "Fun with graphics";
		this.Resize += new System.EventHandler(this.Form1_Resize);
		this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
	}
	[STAThread]
	static void Main() {
		Application.Run(new MainForm());
	}

	private void MainForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e) {
		Graphics g = this.CreateGraphics();
		// Make a big red pen.
		Pen p = new Pen(Color.Red, 7);
		g.DrawLine(p, 1, 1, 100, 100);
	}

	private void Form1_Resize(object sender, System.EventArgs e) {
	}
}