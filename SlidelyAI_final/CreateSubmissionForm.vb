Imports System.Net
Imports Newtonsoft.Json

Public Class CreateSubmissionForm
    ' Remove redundant Timer1 declaration
    Private stopwatchTime As TimeSpan = TimeSpan.Zero
    Private stopwatchRunning As Boolean = False

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.T) Then
            btnToggleStopwatch.PerformClick()
            Return True
        End If
        If keyData = (Keys.Control Or Keys.S) Then
            btnSubmit.PerformClick()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatchRunning Then
            Timer1.Stop()
            stopwatchRunning = False
        Else
            Timer1.Start()
            stopwatchRunning = True
        End If
    End Sub

    ' Ensure this event handler is correct
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        stopwatchTime = stopwatchTime.Add(TimeSpan.FromSeconds(1))
        lblStopwatchTime.Text = stopwatchTime.ToString("hh\:mm\:ss")
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim client As New WebClient()
        Dim url As String = "http://localhost:3000/submit"
        Dim submission = New With {
            .name = txtName.Text,
            .email = txtEmail.Text,
            .phone = txtPhone.Text,
            .github_link = txtGithubLink.Text,
            .stopwatch_time = lblStopwatchTime.Text
        }
        Dim json As String = JsonConvert.SerializeObject(submission)
        client.Headers(HttpRequestHeader.ContentType) = "application/json"
        client.UploadString(url, json)
        MessageBox.Show("Submission saved successfully")
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged

    End Sub
End Class
