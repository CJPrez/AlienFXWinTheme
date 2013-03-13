using System;
using System.Windows.Interop;
using System.Windows.Forms;
using Microsoft.Win32;
namespace AlienFXWinTheme
{
    partial class Form1
    {
        const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x320;
        const String VALUE_NAME = "AlienFX WinTheme";
        const String keyName = @"Software\Microsoft\Windows\CurrentVersion\Run";

        private WindowsTheme theme;
        private NotifyIcon notifyIcon;
        private MenuItem startupMI;


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

        private void MinimizeToTray()
        {
            this.ShowInTaskbar = false;  // Removes the application from the taskbar
            this.WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void CreateSystemIcon()
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Text = "AlienFX WinTheme";
            notifyIcon.Icon = new System.Drawing.Icon("Main.ico");
            notifyIcon.Visible = true;

            notifyIcon.ContextMenu = new ContextMenu();
            MenuItem mi = new MenuItem("Quit");
            mi.Click += new EventHandler(Quit);
            notifyIcon.ContextMenu.MenuItems.Add(mi);

            startupMI = new MenuItem("Start With Windows");
            startupMI.Click += new EventHandler(startupMIClicked);
            notifyIcon.ContextMenu.MenuItems.Add(startupMI);

            notifyIcon.ContextMenu.Popup += new EventHandler(ContextMenu_Popup);

            /* Enable to allow double clicking the system icon
            ni.DoubleClick +=
                delegate(object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                };
            */
        }

        void ContextMenu_Popup(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                if (key != null)
                {
                    if (key.GetValue(VALUE_NAME) == null)
                        startupMI.Text = "Start With Windows";
                    else
                        startupMI.Text = "Don't Start With Windows";
                }
                else
                {
                    startupMI.Enabled = false;
                }
            }
        }

        void startupMIClicked(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                if (key != null)
                {
                    if (key.GetValue(VALUE_NAME) == null)
                        key.SetValue(VALUE_NAME, Application.ExecutablePath.ToString());
                    else
                        key.DeleteValue(VALUE_NAME);
                }
            }
        }

        private void Quit(object sender, EventArgs e)
        {
            AlienFX.Release();

            notifyIcon.Visible = false;
            Environment.Exit(0);
        }
 
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x320)
            {
                theme.updateColor();
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "Form1";
            theme = new WindowsTheme();

            CreateSystemIcon();
            MinimizeToTray();
        }


        

        #endregion
    }
}

