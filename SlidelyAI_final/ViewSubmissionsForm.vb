Imports System.Net
Imports Newtonsoft.Json

Public Class ViewSubmissionsForm
    Private currentIndex As Integer = 0
    Private totalEntries As Integer = 0
    Private isEditing As Boolean = False

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        ' Check for Ctrl + P (Previous)
        If keyData = (Keys.Control Or Keys.P) Then
            btnPrevious.PerformClick()
            Return True
        End If

        ' Check for Ctrl + N (Next)
        If keyData = (Keys.Control Or Keys.N) Then
            btnNext.PerformClick()
            Return True
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            LoadSubmission(currentIndex)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < totalEntries - 1 Then
            currentIndex += 1
            LoadSubmission(currentIndex)
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim client As New WebClient()
        Dim url As String = $"http://localhost:3000/delete?index={currentIndex}"
        client.UploadString(url, "DELETE", "")
        MessageBox.Show("Submission deleted successfully")
        ' Reload the submissions
        ViewSubmissionsForm_Load(sender, e)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If isEditing Then
            ' Save the changes
            Dim client As New WebClient()
            Dim url As String = "http://localhost:3000/edit"
            Dim submission = New With {
                .index = currentIndex,
                .name = txtName.Text,
                .email = txtEmail.Text,
                .phone = txtPhone.Text,
                .github_link = txtGithubLink.Text,
                .stopwatch_time = txtStopwatchTime.Text
            }
            Dim json As String = JsonConvert.SerializeObject(submission)
            client.Headers(HttpRequestHeader.ContentType) = "application/json"
            client.UploadString(url, "PUT", json)
            MessageBox.Show("Submission updated successfully")

            ' Disable editing
            SetEditingMode(False)
        Else
            ' Enable editing
            SetEditingMode(True)
        End If
    End Sub

    Private Sub SetEditingMode(editing As Boolean)
        isEditing = editing
        txtName.ReadOnly = Not editing
        txtEmail.ReadOnly = Not editing
        txtPhone.ReadOnly = Not editing
        txtGithubLink.ReadOnly = Not editing
        txtStopwatchTime.ReadOnly = Not editing

        If editing Then
            btnEdit.Text = "Save"
        Else
            btnEdit.Text = "Edit"
        End If
    End Sub

    Private Sub LoadSubmission(index As Integer)
        Dim client As New WebClient()
        Dim url As String = $"http://localhost:3000/read?index={index}"
        Dim json As String = client.DownloadString(url)
        Dim submission = JsonConvert.DeserializeObject(Of Submission)(json)

        txtName.Text = submission.name
        txtEmail.Text = submission.email
        txtPhone.Text = submission.phone
        txtGithubLink.Text = submission.github_link
        txtStopwatchTime.Text = submission.stopwatch_time
        SetEditingMode(False)
    End Sub

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim client As New WebClient()
        Dim url As String = "http://localhost:3000/submissions"
        Dim json As String = client.DownloadString(url)
        totalEntries = JsonConvert.DeserializeObject(Of List(Of Submission))(json).Count
        LoadSubmission(currentIndex)
    End Sub

    Private Class Submission
        Public Property name As String
        Public Property email As String
        Public Property phone As String
        Public Property github_link As String
        Public Property stopwatch_time As String
    End Class

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim email As String = txtSearchEmail.Text
        If Not String.IsNullOrWhiteSpace(email) Then
            SearchByEmail(email)
        Else
            MessageBox.Show("Please enter an email ID to search")
        End If
    End Sub

    Private Sub SearchByEmail(email As String)
        Dim client As New WebClient()
        Dim url As String = $"http://localhost:3000/search?email={email}"
        Try
            Dim json As String = client.DownloadString(url)
            Dim submission = JsonConvert.DeserializeObject(Of Submission)(json)

            txtName.Text = submission.name
            txtEmail.Text = submission.email
            txtPhone.Text = submission.phone
            txtGithubLink.Text = submission.github_link
            txtStopwatchTime.Text = submission.stopwatch_time
            SetEditingMode(False)
        Catch ex As WebException
            MessageBox.Show("Submission not found")
        End Try
    End Sub


End Class
