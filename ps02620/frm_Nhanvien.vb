Imports System.Data.SqlClient
Imports System.Data.DataTable
Public Class frm_Nhanvien
    Dim Database As New DataTable
    Dim chuoiconnect As String = "workstation id=tinpvps02620.mssql.somee.com;packet size=4096;user id=ps02620;pwd=pvt28494;data source=tinpvps02620.mssql.somee.com;persist security info=False;initial catalog=tinpvps02620"
    Dim connect As SqlConnection = New SqlConnection(chuoiconnect)

    Private Sub dgvhienthi_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvhienthi.CellContentClick
        Dim index As Integer = dgvhienthi.CurrentCell.RowIndex
        txtmanv.Text = dgvhienthi.Item(0, index).Value
        txttennv.Text = dgvhienthi.Item(1, index).Value
        txtpass.Text = dgvhienthi.Item(2, index).Value
    End Sub

    Private Sub frm_Quanlythanhvien_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect) ' Tạo đối tượng kết nối tới DB  Online
        ' Câu truy vấn để get dữ liệu
        Dim Query1 As SqlDataAdapter = New SqlDataAdapter("select * from NhanVien", connect)
        'Kết nối mở ra
        connect.Open()
        'Đổ dữ liệu vào đối tượng database
        Query1.Fill(Database)
        'Hiển thị dữ liệu ra Datagridview
        dgvhienthi.DataSource = Database.DefaultView
    End Sub

    Private Sub btnthem_Click(sender As Object, e As EventArgs) Handles btnthem.Click
        ' Tạo đối tượng kết nối
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect)
        'Tạo query câu truy vấn Insert into
        Dim Query2 As String = "insert into NhanVien values (@Ma_NhanVien, @Ten_NhanVien, @Password)"
        'Tạo đối tượng để thực thi câu truy vấn với DB ONline
        Dim ExecuteQuery1 As New SqlCommand(Query2, connect)
        'Kết nối mở ra
        connect.Open()

        Try
            'Truyền giá trị trong các ô textbox cho các biến tương ứng
            ExecuteQuery1.Parameters.AddWithValue("@Ma_NhanVien", txtmanv.Text)
            ExecuteQuery1.Parameters.AddWithValue("@Ten_NhanVien", txttennv.Text)
            ExecuteQuery1.Parameters.AddWithValue("@Password", txtpass.Text)

            'Exucute là ghi dữ liệu vào Database
            MessageBox.Show("Thêm thành công")
            ExecuteQuery1.ExecuteNonQuery()
        Catch ex As Exception
            'Nếu thêm không được thì hiển thị thông báo
            MessageBox.Show("Không thêm được!")

        End Try
        'Refresh bang
        Dim Query3 As SqlDataAdapter = New SqlDataAdapter("select * from NhanVien", connect)
        Database.Clear()

        Query3.Fill(Database)
        dgvhienthi.DataSource = Database.DefaultView
    End Sub

    Private Sub btnsua_Click(sender As Object, e As EventArgs) Handles btnsua.Click
        Dim ketnoi1 As New SqlConnection(chuoiconnect)
        ketnoi1.Open()
        Dim Stradd1 As String = "Update Nhanvien Set Password = @Password, Ten_NhanVien = @Ten_NhanVien where Ma_NhanVien = @Ma_NhanVien"
        Dim com As New SqlCommand(Stradd1, ketnoi1)
        Try
            com.Parameters.AddWithValue("@Ma_NhanVien", txtmanv.Text)
            com.Parameters.AddWithValue("@Ten_NhanVien", txttennv.Text)
            com.Parameters.AddWithValue("@Password", txtpass.Text)
            com.ExecuteNonQuery()
            ketnoi1.Close()
            MessageBox.Show("Sửa thành công")
        Catch ex As Exception
            MessageBox.Show("Sửa không thành công")
        End Try
        Database.Clear()
        dgvhienthi.DataSource = Database
        dgvhienthi.DataSource = Nothing
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect)
        Dim Query1 As SqlDataAdapter = New SqlDataAdapter("select * from NhanVien", connect)

        connect.Open()
        Query1.Fill(Database)
        dgvhienthi.DataSource = Database.DefaultView
    End Sub

    Private Sub btnxoa_Click(sender As Object, e As EventArgs) Handles btnxoa.Click
        Dim ketnoi As New SqlConnection(chuoiconnect)
        ketnoi.Open()
        Dim xoadl As String = "Delete from NhanVien Where Ma_NhanVien = @Ma_NhanVien"
        Dim lenh As New SqlCommand(xoadl, ketnoi)
        Try
            lenh.Parameters.AddWithValue("@Ma_NhanVien", txtmanv.Text)
            lenh.ExecuteNonQuery()
            ketnoi.Close()
        Catch ex As Exception
            MessageBox.Show("Xoá không thành công")
        End Try
        Database.Clear()
        dgvhienthi.DataSource = Database
        dgvhienthi.DataSource = Nothing
        LoadData()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtmanv.Clear()
        txttennv.Clear()
        txtpass.Clear()
    End Sub

    Private Sub btnthoat_Click(sender As Object, e As EventArgs) Handles btnthoat.Click
        Me.Close()
    End Sub
End Class