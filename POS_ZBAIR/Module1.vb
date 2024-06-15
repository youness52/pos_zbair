Imports MySql.Data.MySqlClient
Imports System.Management
Imports System.Diagnostics

Module Module1
    Public cn As MySqlConnection
    Public cmd As MySqlCommand
    Public dr As MySqlDataReader
    Public st As String
    Public usernamelogin As String
    Public lastdate As String
    Public uuid As String

    Public Sub ouvrir()
        Try
            cn = New MySqlConnection(My.Settings.dbcn.ToString & "database=pos")
            cn.Open()
        Catch ex As Exception
            MsgBox("E002 DB C" & ex.Message.ToString)
            dbsettings.ShowDialog()
        End Try
    End Sub

    Public Function getuuid() As String
        Try
            Dim mos As New ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct")
            Dim moc As ManagementObjectCollection = mos.Get()
            For Each mo As ManagementObject In moc
                Return mo("UUID").ToString()
            Next
        Catch ex As Exception
            MsgBox("E006 UUID " & ex.ToString)
        End Try
    End Function

    Public Function criptagee() As String
        Dim newuuid As String = ""
        Dim reversedString As String = ""
        For Each c As Char In getuuid()
            If UCase(c) = "B" Or UCase(c) = "A" Or c = "0" Then
                newuuid &= "X0B"
            Else
                newuuid &= c
            End If
        Next

        For i As Integer = newuuid.Length - 1 To 0 Step -1
            reversedString &= newuuid(i)
        Next

        Return reversedString
    End Function
    Public Sub checkserver()
        Try
            If System.IO.Directory.Exists("C:\xampp") Then
                If System.IO.File.Exists("C:\xampp\xampp-control.exe") Then
                    Process.Start("C:\xampp\xampp-control.exe")
                Else
                    MessageBox.Show("XAMPP control executable introuvable.")
                    End
                End If
            Else
                MessageBox.Show("XAMPP introuvable SVP install xampp server.")
                End
            End If
        Catch ex As Exception
            MsgBox("E009 check server f  " & ex.ToString)
        End Try
    End Sub
    Public Sub reloadimages()
        My.Computer.FileSystem.CopyDirectory("images", "C:\xampp\htdocs\pos\images", True)
    End Sub
End Module
