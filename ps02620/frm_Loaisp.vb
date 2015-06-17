Imports System.Data.SqlClient
Imports System.Data.DataTable
Public Class frm_Loaisp
    Dim Database As New DataTable
    Dim chuoiconnect As String = "workstation id=tinpvps02620.mssql.somee.com;packet size=4096;user id=ps02620;pwd=pvt28494;data source=tinpvps02620.mssql.somee.com;persist security info=False;initial catalog=tinpvps02620"
    Dim connect As SqlConnection = New SqlConnection(chuoiconnect)

    Private Sub dgvhienthi_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvhienthi.CellContentClick
        Dim index As Integer = dgvhienthi.CurrentCell.RowIndex
        txtmaloai.Text = dgvhienthi.Item(0, index).Value
        txttenloai.Text = dgvhienthi.Item(1, index).Value
    End Sub

    Private Sub frm_Loaisp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect) ' Tạo đối tượng kết nối tới DB  Online
        ' Câu truy vấn để get dữ liệu
        Dim Query1 As SqlDataAdapter = New SqlDataAdapter("select * from LoaiSanPham", connect)
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
        Dim Query2 As String = "insert into LoaiSanPham values (@MaLoai, @TenLoai)"
        'Tạo đối tượng để thực thi câu truy vấn với DB ONline
        Dim ExecuteQuery1 As New SqlCommand(Query2, connect)
        'Kết nối mở ra
        connect.Open()

        Try
            'Truyền giá trị trong các ô textbox cho các biến tương ứng
            ExecuteQuery1.Parameters.AddWithValue("@MaLoai", txtmaloai.Text)
            ExecuteQuery1.Parameters.AddWithValue("@TenLoai", txttenloai.Text)

            'Exucute là ghi dữ liệu vào Database
            MessageBox.Show("Thêm thành công")
            ExecuteQuery1.ExecuteNonQuery()
        Catch ex As Exception
            'Nếu thêm không được thì hiển thị thông báo
            MessageBox.Show("Không thêm được!")

        End Try
        'Refresh bang
        Dim Query3 As SqlDataAdapter = New SqlDataAdapter("select * from LoaiSanPham", connect)
        Database.Clear()

        Query3.Fill(Database)
        dgvhienthi.DataSource = Database.DefaultView
    End Sub

    Private Sub btnsua_Click(sender As Object, e As EventArgs) Handles btnsua.Click
        Dim ketnoi1 As New SqlConnection(chuoiconnect)
        ketnoi1.Open()
        Dim Stradd1 As String = "Update LoaiSanPham Set TenLoai = @TenLoai where MaLoai = @MaLoai"
        Dim com As New SqlCommand(Stradd1, ketnoi1)
        Try
            com.Parameters.AddWithValue("@MaLoai", txtmaloai.Text)
            com.Parameters.AddWithValue("@TenLoai", txttenloai.Text)
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
        Dim Query1 As SqlDataAdapter = New SqlDataAdapter("select * from LoaiSanPham", connect)

        connect.Open()
        Query1.Fill(Database)
        dgvhienthi.DataSource = Database.DefaultView
    End Sub

    Private Sub btnxoa_Click(sender As Object, e As EventArgs) Handles btnxoa.Click
        Dim ketnoi As New SqlConnection(chuoiconnect)
        ketnoi.Open()
        Dim xoadl As String = "Delete from LoaiSanPham Where MaLoai = @MaLoai"
        Dim lenh As New SqlCommand(xoadl, ketnoi)
        Try
            lenh.Parameters.AddWithValue("@MaLoai", txtmaloai.Text)
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
        txtmaloai.Clear()
        txttenloai.Clear()
    End Sub

    Private Sub btnthoat_Click(sender As Object, e As EventArgs) Handles btnthoat.Click
        Me.Close()
    End Sub
End Class