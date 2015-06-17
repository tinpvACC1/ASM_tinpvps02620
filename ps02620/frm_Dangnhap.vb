Imports System.Data.SqlClient

Public Class frm_Dangnhap

    Private Sub btndangnhap_Click(sender As Object, e As EventArgs) Handles btndangnhap.Click
        Dim chuoiketnoi As String = "workstation id=tinpvps02620.mssql.somee.com;packet size=4096;user id=ps02620;pwd=pvt28494;data source=tinpvps02620.mssql.somee.com;persist security info=False;initial catalog=tinpvps02620"

        Dim KetNoi As SqlConnection = New SqlConnection(chuoiketnoi)
        Dim sqlAdapter As New SqlDataAdapter("select * from NhanVien where Ma_NhanVien='" & txttaikhoan.Text & "' and Password='" & txtmatkhau.Text & "' ", KetNoi)
        Dim tb As New DataTable

        Try
            KetNoi.Open()
            sqlAdapter.Fill(tb)
            If tb.Rows.Count > 0 Then
                MessageBox.Show("Chúc mừng bạn đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                frm_Main.Show()
                Me.Hide()
            Else
                MessageBox.Show("Tên đăng nhập không đúng! Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frm_Dangnhap_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

End Class
