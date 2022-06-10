namespace TX_ESC_04SessionRememberV2
{
    partial class ProjectInstaller
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
            this.RenibderServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ReminderServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // RenibderServiceProcessInstaller
            // 
            this.RenibderServiceProcessInstaller.Password = null;
            this.RenibderServiceProcessInstaller.Username = null;
            // 
            // ReminderServiceInstaller
            // 
            this.ReminderServiceInstaller.DisplayName = "tx_esc_04 Session Reminder";
            this.ReminderServiceInstaller.ServiceName = "tx_esc_04 Session Reminder";
         
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.RenibderServiceProcessInstaller,
            this.ReminderServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller RenibderServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ReminderServiceInstaller;
    }
}