Imports System.IO
Imports System.Net
Imports System.IO.Compression
Imports DevExpress.XtraEditors
Imports System.Text.RegularExpressions

Public Module ModuleSystem
    Public Sub ReqInstallRebootMessageBox()
        Dim args As XtraMessageBoxArgs = New XtraMessageBoxArgs() With {.Caption = "Uyarı", .Text = "Gereksinimler yüklenmiştir. Bilgisayarınız 15 saniye içerisinde yeniden başlatılacaktır.", .Buttons = New DialogResult() {DialogResult.OK}, .AutoCloseOptions = New AutoCloseOptions() With {.Delay = 15000, .ShowTimerOnDefaultButton = True}}
        If XtraMessageBox.Show(args) = DialogResult.OK Then
            Dim proc As New Process

            proc.StartInfo.FileName = "C:\LNX VPN\System\Reboot.bat"
            proc.StartInfo.UseShellExecute = False
            proc.StartInfo.RedirectStandardOutput = True
            proc.StartInfo.CreateNoWindow = True
            proc.Start()

            proc.WaitForExit()
        End If
    End Sub
    Public Sub ReqCheck()
        If (System.IO.Directory.Exists("C:\Program Files\WireGuard")) Then
            If (System.IO.File.Exists("C:\Program Files\WireGuard\wireguard.exe")) Then
                'Nothing
            Else
                Form1.PanelControl1.Visible = False
                Form1.PanelControl2.Visible = True

                Dim proc As New Process

                proc.StartInfo.FileName = "C:\LNX VPN\System\InstallReq.bat"
                proc.StartInfo.UseShellExecute = False
                proc.StartInfo.RedirectStandardOutput = True
                proc.StartInfo.CreateNoWindow = True
                proc.Start()

                proc.WaitForExit()

                For Each prog As Process In Process.GetProcesses
                    If prog.ProcessName = "wireguard" Then
                        prog.Kill()
                    End If
                Next

                ReqInstallRebootMessageBox()
            End If
        Else
            Form1.PanelControl1.Visible = False
            Form1.PanelControl2.Visible = True

            Dim proc As New Process

            proc.StartInfo.FileName = "C:\LNX VPN\System\InstallReq.bat"
            proc.StartInfo.UseShellExecute = False
            proc.StartInfo.RedirectStandardOutput = True
            proc.StartInfo.CreateNoWindow = True
            proc.Start()

            proc.WaitForExit()

            For Each prog As Process In Process.GetProcesses
                If prog.ProcessName = "wireguard" Then
                    prog.Kill()
                End If
            Next

            ReqInstallRebootMessageBox()
        End If
    End Sub
End Module
