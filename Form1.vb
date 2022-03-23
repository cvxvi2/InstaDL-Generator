Public Class Form1
    Dim spath As String = Nothing
    Dim detsubs As Integer = 0
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Application.Exit()

    End Sub

    Sub log(ByVal txt As String)
        Try
            TextBox2.AppendText(Environment.NewLine & txt)
        Catch ex As Exception

        End Try
    End Sub

    Function doesdirectoryexist(ByVal directory As String)
        log("Checking path for " & directory.ToString)
        Try
            If My.Computer.FileSystem.DirectoryExists(directory) Then
                log("Check complete.")
                Return True
            Else
                log("Check failure.")
                Return False
            End If
        Catch ex As Exception
            log("Check failure with ex: " & ex.Message.ToString)
            Return False
        End Try
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        log("Validating R/W access...")
        log("Validating R/W access...0 of 4")
        My.Computer.FileSystem.WriteAllText(FileIO.SpecialDirectories.Temp & "\BottomPart.txt", My.Resources.BottomPart, False)
        log("Validating R/W access...1 of 4")
        My.Computer.FileSystem.WriteAllText(FileIO.SpecialDirectories.Temp & "\folderpart.txt", My.Resources.folderpart, False)
        log("Validating R/W access...2 of 4")
        My.Computer.FileSystem.WriteAllText(FileIO.SpecialDirectories.Temp & "\TopPart.txt", My.Resources.TopPart, False)
        log("Validating R/W access...3 of 4")
        My.Computer.FileSystem.WriteAllText(FileIO.SpecialDirectories.Temp & "\filepart.txt", My.Resources.FilePart, False)
        log("Validating R/W access...4 of 4")
        log("Ready.")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FolderBrowserDialog1.ShowDialog()
        If doesdirectoryexist(FolderBrowserDialog1.SelectedPath) = True Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath

        Else
            MsgBox("Directory does not exist.", 0 + 16)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If CheckBox1.Checked = True Then
            If detsubs = 0 Then
                MsgBox("Subs need scanning first.", 0 + 64)
            Else
                ProgressBar1.Value = 0
                If doesdirectoryexist(TextBox1.Text) Then
                    spath = TextBox1.Text
                    genInstaDL(spath)
                    For Each dirs In My.Computer.FileSystem.GetDirectories(spath, FileIO.SearchOption.SearchAllSubDirectories)
                        spath = dirs
                        genInstaDL(dirs)
                        ProgressBar1.Increment(1)
                    Next
                    TextBox3.AppendText(Environment.NewLine & "[SubGen] Complete")
                    ProgressBar1.Value = ProgressBar1.Maximum
                Else
                    MsgBox("Directory does not exist or not present.", 0 + 16)
                End If
            End If
        Else
            genInstaDL(TextBox1.Text)
        End If


    End Sub


    Sub genInstaDL(ByVal path As String)
        If doesdirectoryexist(path) = True Then
            'spath = TextBox1.Text
            Try
                log("Generate HTML...")
                My.Computer.FileSystem.WriteAllText(path & "\index.html", My.Resources.TopPart, False)
                log("Detect folders...")
                For Each dirs In My.Computer.FileSystem.GetDirectories(path, FileIO.SearchOption.SearchTopLevelOnly)

                    log("Create for " & dirs.ToString)
                    Dim als23 = My.Resources.folderpart
                    Dim d2 = dirs.Replace(path, Nothing)
                    d2 = d2.Replace("\", Nothing)
                    d2 = d2.Replace(TextBox1.Text, Nothing)
                    als23 = als23.Replace("$FOLDERNAME", d2)
                    als23 = als23.Replace("$PATHNAME", d2)
                    My.Computer.FileSystem.WriteAllText(path & "\index.html", Environment.NewLine & als23, True)
                Next
                For Each files In My.Computer.FileSystem.GetFiles(path, FileIO.SearchOption.SearchTopLevelOnly)
                    If files.ToString.Contains("index.html") Then
                    Else
                        log("Create for " & files.ToString)
                        Dim als23 = My.Resources.FilePart
                        Dim d2 = files.Replace(path, Nothing)
                        d2 = d2.Replace("\", Nothing)
                        d2 = d2.Replace(TextBox1.Text, Nothing)
                        als23 = als23.Replace("$FILENAME", d2)
                        als23 = als23.Replace("$FILEPATH", d2)
                        If files.ToString.Contains(".exe") Then
                            als23 = als23.Replace("$FILETYPE", "Application")
                            als23 = als23.Replace("images/File.png", "images/application.png")
                        ElseIf files.ToString.Contains(".inf") Then
                            als23 = als23.Replace("$FILETYPE", "Installation Information")

                        ElseIf files.ToString.Contains(".iso") Then
                            als23 = als23.Replace("$FILETYPE", "ISO Image")
                            als23 = als23.Replace("images/File.png", "images/ISO.png")
                        Else
                            als23 = als23.Replace("$FILETYPE", "File")
                        End If
                        My.Computer.FileSystem.WriteAllText(path & "\index.html", Environment.NewLine & als23, True)
                    End If
                Next
                My.Computer.FileSystem.WriteAllText(path & "\index.html", Environment.NewLine & My.Resources.BottomPart, True)
            Catch ex As Exception
                log("Error generating: " & ex.Message.ToString)
            End Try
            log("Generating CSS folder...")
            Try
                My.Computer.FileSystem.CreateDirectory(path & "\css")
                If doesdirectoryexist(path & "\css") Then
                    log("Generating CSS files...0 of 6")
                    My.Computer.FileSystem.WriteAllText(path & "\css\animate.css", My.Resources.animate_css, False)
                    log("Generating CSS files...1 of 6")
                    My.Computer.FileSystem.WriteAllText(path & "\css\bootstrap.css", My.Resources.bootstrap_css, False)
                    log("Generating CSS files...2 of 6")
                    My.Computer.FileSystem.WriteAllText(path & "\css\flexslider.css", My.Resources.flexslider_css, False)
                    log("Generating CSS files...3 of 6")
                    My.Computer.FileSystem.WriteAllText(path & "\css\icomoon.css", My.Resources.icomoon_css, False)
                    log("Generating CSS files...4 of 6")
                    My.Computer.FileSystem.WriteAllText(path & "\css\magnific-popup.css", My.Resources.magnific_popup_css, False)
                    log("Generating CSS files...5 of 6")
                    My.Computer.FileSystem.WriteAllText(path & "\css\style.css", My.Resources.style_css, False)
                    log("Generating JS folder...")
                    My.Computer.FileSystem.CreateDirectory(path & "\js")
                    If doesdirectoryexist(path & "\js") = True Then
                        log("Generating JS files...0 of 10")
                        My.Computer.FileSystem.WriteAllText(path & "\js\bootstrap.min.js", My.Resources.bootstrap_min, False)
                        log("Generating JS files...1 of 10")
                        My.Computer.FileSystem.WriteAllText(path & "\js\jquery.countTo.js", My.Resources.jquery_countTo, False)
                        log("Generating JS files...2 of 10")
                        My.Computer.FileSystem.WriteAllText(path & "\js\jquery.easing.1.3.js", My.Resources.jquery_easing_1_3, False)
                        log("Generating JS files...3 of 10")
                        My.Computer.FileSystem.WriteAllText(path & "\js\jquery.magnific-popup.min.js", My.Resources.jquery_magnific_popup_min, False)
                        log("Generating JS files...4 of 10")
                        My.Computer.FileSystem.WriteAllText(path & "\js\jquery.min.js", My.Resources.jquery_min, False)
                        log("Generating JS files...5 of 10")
                        My.Computer.FileSystem.WriteAllText(path & "\js\jquery.waypoints.min.js", My.Resources.jquery_waypoints_min, False)
                        log("Generating JS files...6 of 10")
                        My.Computer.FileSystem.WriteAllText(path & "\js\magnific-popup-options.js", My.Resources.magnific_popup_options, False)
                        log("Generating JS files...7 of 10")
                        My.Computer.FileSystem.WriteAllText(path & "\js\main.js", My.Resources.main, False)
                        log("Generating JS files...8 of 10")
                        My.Computer.FileSystem.WriteAllText(path & "\js\modernizr-2.6.2.min.js", My.Resources.modernizr_2_6_2_min, False)
                        log("Generating JS files...9 of 10")
                        My.Computer.FileSystem.WriteAllText(path & "\js\respond.min.js", My.Resources.respond_min, False)
                        log("Generating JS files...10 of 10")
                        log("Generating Images...")
                        My.Computer.FileSystem.CreateDirectory(path & "\images")
                        If doesdirectoryexist(path & "\images") = True Then
                            My.Resources.folder.Save(path & "\images\folder.png")
                            My.Resources.File.Save(path & "\images\file.png")
                            My.Resources.application.Save(path & "\images\application.png")
                            My.Resources.ISO.Save(path & "\images\iso.png")
                        Else

                        End If
                        log("done!")




                    Else
                        log("Error generating js folder. Check permissions and try again.")
                    End If
                Else
                End If
            Catch ex As Exception
                log("Error generating: " & ex.Message.ToString)
            End Try
        Else
            MsgBox("Directory does not exist.", 0 + 16)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If doesdirectoryexist(TextBox1.Text) Then
            spath = TextBox1.Text
            For Each dirs In My.Computer.FileSystem.GetDirectories(spath, FileIO.SearchOption.SearchAllSubDirectories)
                TextBox3.AppendText(Environment.NewLine & "[Detect sub] . . . " & dirs)
                detsubs = detsubs + 1
                If detsubs > ProgressBar1.Maximum Then
                    ProgressBar1.Maximum = detsubs
                End If
                ProgressBar1.Increment(1)
            Next
            TextBox3.AppendText(Environment.NewLine & "[Detect sub] Complete, detected " & detsubs)
        Else
            MsgBox("Directory does not exist or not present.", 0 + 16)
        End If

    End Sub
End Class
