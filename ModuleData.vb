Imports System.Data.SQLite
Public Module ModuleData
    Dim SQLiteConnectionString As SQLite.SQLiteConnection = New SQLiteConnection("Data Source=C:\LNX VPN\Data\VPN.db;Version=3;New=False;Compress=True;")

    Public Sub SelectedTunnelInformation()
        Dim FID = CType(Form1.GridView1.GetFocusedRowCellValue("id").ToString(), String)
        Dim FNAME = CType(Form1.GridView1.GetFocusedRowCellValue("Name").ToString(), String)
        Dim FFILE = CType(Form1.GridView1.GetFocusedRowCellValue("File").ToString(), String)
        Dim FNETWORKNAME = CType(Form1.GridView1.GetFocusedRowCellValue("NetworkName").ToString(), String)

        ModuleConnection.SelectedTunnelServiceID = FID
        ModuleConnection.SelectedTunnelServiceName = FNAME
        ModuleConnection.SelectedTunnelServiceFile = FFILE
        ModuleConnection.SelectedTunnelNetworkName = FNETWORKNAME
    End Sub
    Public Sub LoadVPNData()
        Dim sqlite_cmd As SQLiteCommand
        sqlite_cmd = SQLiteConnectionString.CreateCommand()
        sqlite_cmd.CommandText = "SELECT * FROM VPNList;"

        Dim sqlite_dataadapter As New SQLite.SQLiteDataAdapter
        sqlite_dataadapter.SelectCommand = sqlite_cmd
        Dim dt As New DataTable

        sqlite_dataadapter.Fill(dt)
        SQLiteConnectionString.Open()

        Form1.GridControl1.DataSource = dt
        SQLiteConnectionString.Close()
    End Sub
End Module
