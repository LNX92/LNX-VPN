Imports DevExpress.XtraSplashScreen
Public Class Form1

    Public Function ShowProgressPanel() As IOverlaySplashScreenHandle
        Dim handle As IOverlaySplashScreenHandle = SplashScreenManager.ShowOverlayForm(Me)
        Return handle
    End Function

    Public Sub CloseProgressPanel(ByVal handle As IOverlaySplashScreenHandle)
        If handle IsNot Nothing Then SplashScreenManager.CloseOverlayForm(handle)
    End Sub

    Public Handle As IOverlaySplashScreenHandle = Nothing
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        PanelControl1.Visible = True
        PanelControl2.Visible = False
        ModuleSystem.ReqCheck()
        ModuleData.LoadVPNData()
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        PanelControl1.Visible = False
        PanelControl2.Visible = True
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModuleConnection.Real_IP_Check()
        ModuleConnection.Load_Check_Network()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ModuleConnection.RealTimeCheckConnection()
    End Sub

    Private Sub RepositoryItemButtonEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemButtonEdit1.ButtonClick
        ModuleConnection.ConnectSelectedTunnel()
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        ModuleConnection.DisconnectTunnel()
    End Sub

    Private Sub AccordionControlElementAbout_Click(sender As Object, e As EventArgs) Handles AccordionControlElementAbout.Click
        AboutForm.ShowDialog()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        ModuleConnection.ApplicationExitDisconnect()
    End Sub
End Class
