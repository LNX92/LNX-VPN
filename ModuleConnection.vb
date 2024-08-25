Imports System.Text.RegularExpressions
Imports System.Data.SQLite

Public Module ModuleConnection
    Public Real_IP As String
    Public Real_City As String
    Public Real_Country As String

    Public Check_RealTimeConnection As String
    Public RealTimeIP As String
    Public RealTimeCity As String
    Public RealTimeCountry As String
    Public VPNIP As String

    Public SelectedTunnelServiceID As String
    Public SelectedTunnelServiceName As String
    Public SelectedTunnelServiceFile As String
    Public SelectedTunnelNetworkName As String

    Public VPNState As Integer = 0
    Public TryConnect As Integer = 1

    Public Sub ApplicationExitDisconnect()
        If (SelectedTunnelNetworkName <> "") Then
            System.IO.File.WriteAllText("C:\LNX VPN\System\Disconnect.bat", CStr("cmd.exe /c wireguard /uninstalltunnelservice " & SelectedTunnelNetworkName))
            Dim procDisconnect As New Process

            procDisconnect.StartInfo.FileName = "C:\LNX VPN\System\Disconnect.bat"
            procDisconnect.StartInfo.UseShellExecute = False
            procDisconnect.StartInfo.RedirectStandardOutput = True
            procDisconnect.StartInfo.CreateNoWindow = True
            procDisconnect.Start()

            procDisconnect.WaitForExit()

            Application.ExitThread()
        Else
            Application.ExitThread()
        End If
    End Sub
    Public Sub DisconnectTunnel()
        Form1.Handle = Form1.ShowProgressPanel
        VPNState = 0

        System.IO.File.WriteAllText("C:\LNX VPN\System\Disconnect.bat", CStr("cmd.exe /c wireguard /uninstalltunnelservice " & SelectedTunnelNetworkName))
        Dim procDisconnect As New Process

        procDisconnect.StartInfo.FileName = "C:\LNX VPN\System\Disconnect.bat"
        procDisconnect.StartInfo.UseShellExecute = False
        procDisconnect.StartInfo.RedirectStandardOutput = True
        procDisconnect.StartInfo.CreateNoWindow = True
        procDisconnect.Start()

        procDisconnect.WaitForExit()

        SelectedTunnelServiceID = ""
        SelectedTunnelServiceName = ""
        SelectedTunnelServiceFile = ""
        SelectedTunnelNetworkName = ""

        Form1.PictureEdit1.Image = Image.FromFile("C:\LNX VPN\Images\Default512.png")
        Form1.SimpleButton3.Visible = False

        RealTimeCheckConnection()
        VPNState = 0
        Form1.CloseProgressPanel(Form1.Handle)
    End Sub
    Public Sub ConnectSelectedTunnel()
        Form1.Timer1.Stop()
        Form1.Handle = Form1.ShowProgressPanel

        Form1.PanelControl1.Visible = False
        Form1.PanelControl2.Visible = True
        VPNState = 0
        VPNIP = ""

        If (SelectedTunnelNetworkName <> "") Then
            System.IO.File.WriteAllText("C:\LNX VPN\System\Disconnect.bat", CStr("cmd.exe /c wireguard /uninstalltunnelservice " & SelectedTunnelNetworkName))
            Dim procDisconnect As New Process

            procDisconnect.StartInfo.FileName = "C:\LNX VPN\System\Disconnect.bat"
            procDisconnect.StartInfo.UseShellExecute = False
            procDisconnect.StartInfo.RedirectStandardOutput = True
            procDisconnect.StartInfo.CreateNoWindow = True
            procDisconnect.Start()

            procDisconnect.WaitForExit()
        Else
            'Nothing
        End If

        ModuleData.SelectedTunnelInformation()

        System.IO.File.WriteAllText("C:\LNX VPN\System\Connect.bat", CStr("cmd.exe /c wireguard /installtunnelservice " & Chr(34) & SelectedTunnelServiceFile & Chr(34)))

        Dim proc As New Process

        proc.StartInfo.FileName = "C:\LNX VPN\System\Connect.bat"
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.CreateNoWindow = True
        proc.Start()

        proc.WaitForExit()

        If (My.Computer.Network.Ping("www.google.com", 2500)) Then
            Dim VPNCHECKPROC As New Process

            VPNCHECKPROC.StartInfo.FileName = "C:\LNX VPN\System\Check.bat"
            VPNCHECKPROC.StartInfo.UseShellExecute = False
            VPNCHECKPROC.StartInfo.RedirectStandardOutput = True
            VPNCHECKPROC.StartInfo.CreateNoWindow = True
            VPNCHECKPROC.Start()

            VPNCHECKPROC.WaitForExit()

            Dim sOutput As String
            Using oStreamReader As System.IO.StreamReader = VPNCHECKPROC.StandardOutput
                sOutput = oStreamReader.ReadToEnd()
            End Using

            TempForm.VPNRichTextBox.Clear()
            TempForm.VPNRichTextBox.Text = sOutput

            For i As Integer = 0 To TempForm.VPNRichTextBox.Lines.Count - 1
                Dim rOutput As String = TempForm.VPNRichTextBox.Lines(i)
                Dim m As Match = Regex.Match(rOutput, CStr(Chr(34) & "ip" + Chr(34) & ": " & Chr(34) & "(\d+.\d+.\d+.\d+)"), RegexOptions.Compiled)
                Dim splittedLine() As String = rOutput.Split

                If m.Success = True Then 'Match found.
                    Dim CheckVPNIp As String = m.Groups(1).ToString()
                    VPNIP = CheckVPNIp
                End If
            Next

            If (VPNIP <> Real_IP) Then
                SelectedTunnelInformation()
                VPNState = 1
                Form1.PictureEdit1.Image = Image.FromFile("C:\LNX VPN\Images\Success512.png")
                Form1.SimpleButton3.Visible = True
                RealTimeCheckConnection()
                Form1.Timer1.Start()
                Form1.CloseProgressPanel(Form1.Handle)
                Exit Sub
            Else
                System.IO.File.WriteAllText("C:\LNX VPN\System\Disconnect.bat", CStr("cmd.exe /c wireguard /uninstalltunnelservice " & SelectedTunnelNetworkName))
                Dim procDisconnect As New Process

                procDisconnect.StartInfo.FileName = "C:\LNX VPN\System\Disconnect.bat"
                procDisconnect.StartInfo.UseShellExecute = False
                procDisconnect.StartInfo.RedirectStandardOutput = True
                procDisconnect.StartInfo.CreateNoWindow = True
                procDisconnect.Start()

                procDisconnect.WaitForExit()
                Form1.PictureEdit1.Image = Image.FromFile("C:\LNX VPN\Images\Failed512.png")
                VPNState = 0
                SelectedTunnelNetworkName = ""
                Form1.SimpleButton3.Visible = False
            End If
        Else
            System.IO.File.WriteAllText("C:\LNX VPN\System\Disconnect.bat", CStr("cmd.exe /c wireguard /uninstalltunnelservice " & SelectedTunnelNetworkName))
            Dim procDisconnect As New Process

            procDisconnect.StartInfo.FileName = "C:\LNX VPN\System\Disconnect.bat"
            procDisconnect.StartInfo.UseShellExecute = False
            procDisconnect.StartInfo.RedirectStandardOutput = True
            procDisconnect.StartInfo.CreateNoWindow = True
            procDisconnect.Start()

            procDisconnect.WaitForExit()
            Form1.PictureEdit1.Image = Image.FromFile("C:\LNX VPN\Images\Failed512.png")

            VPNState = 0
            SelectedTunnelNetworkName = ""
            Form1.SimpleButton3.Visible = False
        End If

        RealTimeCheckConnection()
        Form1.Timer1.Start()
        Form1.CloseProgressPanel(Form1.Handle)
    End Sub
    Public Sub RealTime_Country_Check()
        For i As Integer = 0 To TempForm.RealTimeIP_RichTextBox.Lines.Count - 1
            Dim rOutput As String = TempForm.RealTimeIP_RichTextBox.Lines(i)
            Dim m As Match = Regex.Match(rOutput, CStr(Chr(34) & "country" + Chr(34) & ": " & Chr(34) & "(\w+)"), RegexOptions.Compiled)
            Dim splittedLine() As String = rOutput.Split

            If m.Success = True Then 'Match found.
                Dim CheckRealCountry As String = m.Groups(1).ToString()
                RealTimeCountry = CheckRealCountry
            End If
        Next

        Form1.LabelControl1.Text += CStr(", " & RealTimeCountry)
    End Sub
    Public Sub RealTime_City_Check()
        For i As Integer = 0 To TempForm.RealTimeIP_RichTextBox.Lines.Count - 1
            Dim rOutput As String = TempForm.RealTimeIP_RichTextBox.Lines(i)
            Dim m As Match = Regex.Match(rOutput, CStr(Chr(34) & "city" + Chr(34) & ": " & Chr(34) & "(\w+)"), RegexOptions.Compiled)
            Dim splittedLine() As String = rOutput.Split

            If m.Success = True Then 'Match found.
                Dim CheckRealCountry As String = m.Groups(1).ToString()
                RealTimeCity = CheckRealCountry
            End If
        Next

        Form1.LabelControl1.Text += CStr(", " & RealTimeCity)
    End Sub
    Public Sub RealTimeCheckConnection()
        Form1.LabelControl1.Text = ""
        RealTimeIP = ""

        If (My.Computer.Network.IsAvailable) Then
            Dim proc As New Process

            proc.StartInfo.FileName = "C:\LNX VPN\System\Check.bat"
            proc.StartInfo.UseShellExecute = False
            proc.StartInfo.RedirectStandardOutput = True
            proc.StartInfo.CreateNoWindow = True
            proc.Start()

            proc.WaitForExit()

            Dim sOutput As String
            Using oStreamReader As System.IO.StreamReader = proc.StandardOutput
                sOutput = oStreamReader.ReadToEnd()
            End Using

            TempForm.RealTimeIP_RichTextBox.Clear()
            TempForm.RealTimeIP_RichTextBox.Text = sOutput

            For i As Integer = 0 To TempForm.RealTimeIP_RichTextBox.Lines.Count - 1
                Dim rOutput As String = TempForm.RealTimeIP_RichTextBox.Lines(i)
                Dim m As Match = Regex.Match(rOutput, CStr(Chr(34) & "ip" + Chr(34) & ": " & Chr(34) & "(\d+.\d+.\d+.\d+)"), RegexOptions.Compiled)
                Dim splittedLine() As String = rOutput.Split

                If m.Success = True Then 'Match found.
                    Dim CheckRealIP As String = m.Groups(1).ToString()
                    RealTimeIP = CheckRealIP
                End If
            Next

            Form1.LabelControl1.Text += CStr(RealTimeIP)
            RealTime_City_Check()
            RealTime_Country_Check()
        Else
            Form1.SimpleButton1.Enabled = False
            Form1.LabelControl1.Text = "Ag baglantısı bulunamadı"
            Form1.LabelControl1.ForeColor = Color.Salmon
            Form1.PictureEdit1.Image = Image.FromFile("C:\LNX VPN\Images\Failed512.png")

            If (VPNState = 1) Then
                DisconnectTunnel()
            End If
        End If
    End Sub
    Public Sub Load_Check_Network()
        If (Real_IP <> "") Then
            Form1.Timer1.Start()
        Else
            Form1.SimpleButton1.Enabled = False
            Form1.LabelControl1.Text = "Ag baglantısı bulunamadı"
            Form1.LabelControl1.ForeColor = Color.Salmon
            Form1.PictureEdit1.Image = Image.FromFile("C:\LNX VPN\Images\Failed512.png")

            Form1.Timer1.Start()
        End If
    End Sub
    Public Sub Real_Country_Check()
        For i As Integer = 0 To TempForm.RealIP_RichTextBox.Lines.Count - 1
            Dim rOutput As String = TempForm.RealIP_RichTextBox.Lines(i)
            Dim m As Match = Regex.Match(rOutput, CStr(Chr(34) & "country" + Chr(34) & ": " & Chr(34) & "(\w+)"), RegexOptions.Compiled)
            Dim splittedLine() As String = rOutput.Split

            If m.Success = True Then 'Match found.
                Dim CheckRealCountry As String = m.Groups(1).ToString()
                Real_Country = CheckRealCountry
            End If
        Next

        Form1.LabelControl1.Text += CStr(", " & Real_Country)
    End Sub
    Public Sub Real_City_Check()
        For i As Integer = 0 To TempForm.RealIP_RichTextBox.Lines.Count - 1
            Dim rOutput As String = TempForm.RealIP_RichTextBox.Lines(i)
            Dim m As Match = Regex.Match(rOutput, CStr(Chr(34) & "city" + Chr(34) & ": " & Chr(34) & "(\w+)"), RegexOptions.Compiled)
            Dim splittedLine() As String = rOutput.Split

            If m.Success = True Then 'Match found.
                Dim CheckRealCity As String = m.Groups(1).ToString()
                Real_City = CheckRealCity
            End If
        Next

        Form1.LabelControl1.Text += CStr(", " & Real_City)
    End Sub
    Public Sub Real_IP_Check()

        Dim proc As New Process

        proc.StartInfo.FileName = "C:\LNX VPN\System\Check.bat"
        proc.StartInfo.UseShellExecute = False
        proc.StartInfo.RedirectStandardOutput = True
        proc.StartInfo.CreateNoWindow = True
        proc.Start()

        proc.WaitForExit()

        Dim sOutput As String
        Using oStreamReader As System.IO.StreamReader = proc.StandardOutput
            sOutput = oStreamReader.ReadToEnd()
        End Using

        TempForm.RealIP_RichTextBox.Clear()
        TempForm.RealIP_RichTextBox.Text = sOutput

        For i As Integer = 0 To TempForm.RealIP_RichTextBox.Lines.Count - 1
            Dim rOutput As String = TempForm.RealIP_RichTextBox.Lines(i)
            Dim m As Match = Regex.Match(rOutput, CStr(Chr(34) & "ip" + Chr(34) & ": " & Chr(34) & "(\d+.\d+.\d+.\d+)"), RegexOptions.Compiled)
            Dim splittedLine() As String = rOutput.Split

            If m.Success = True Then 'Match found.
                Dim CheckRealIP As String = m.Groups(1).ToString()
                Real_IP = CheckRealIP
            End If
        Next

        Form1.LabelControl1.Text = Real_IP

        Real_City_Check()
        Real_Country_Check()
    End Sub
End Module
