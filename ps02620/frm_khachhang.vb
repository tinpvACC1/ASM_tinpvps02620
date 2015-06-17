Imports System.Data.SqlClient
Imports System.Data.DataTable
Public Class frm_khachhang
    Dim Database As New DataTable
    Dim chuoiconnect As String = "workstation id=tinpvps02620.mssql.somee.com;packet size=4096;user id=ps02620;pwd=pvt28494;data source=tinpvps02620.mssql.somee.com;persist security info=False;initial catalog=tinpvps02620"
    Dim connect As SqlConnection = New SqlConnection(chuoiconnect)

    Private Sub dgvhienthi_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvhienthi.CellContentClick
        Dim index As Integer = dgvhienthi.CurrentCell.RowIndex
        txtmakh.Text = dgvhienthi.Item(0, index).Value
        txttenkh.Text = dgvhienthi.Item(1, index).Value
        txtsodt.Text = dgvhienthi.Item(2, index).Value
        txtdiachi.Text = dgvhienthi.Item(3, index).Value
    End Sub

    Private Sub frm_khachhang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect) ' Tạo đối tượng kết nối tới DB  Online
        ' Câu truy vấn để get dữ liệu
        Dim Query1 As SqlDataAdapter = New SqlDataAdapter("select * from KhachHang", connect)
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
        Dim Query2 As String = "insert into KhachHang values (@Ma_KhachHang, @Ten_KhachHang, @SoDT, @DiaChi)"
        'Tạo đối tượng để thực thi câu truy vấn với DB ONline
        Dim ExecuteQuery1 As New SqlCommand(Query2, connect)
        'Kết nối mở ra
        connect.Open()

        Try
            'Truyền giá trị trong các ô textbox cho các biến tương ứng
            ExecuteQuery1.Parameters.AddWithValue("@Ma_KhachHang", txtmakh.Text)
            ExecuteQuery1.Parameters.AddWithValue("@Ten_KhachHang", txttenkh.Text)
            ExecuteQuery1.Parameters.AddWithValue("@SoDT", txtsodt.Text)
            ExecuteQuery1.Parameters.AddWithValue("@DiaChi", txtdiachi.Text)

            'Exucute là ghi dữ liệu vào Database
            MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ExecuteQuery1.ExecuteNonQuery()
        Catch ex As Exception
            'Nếu thêm không được thì hiển thị thông báo
            MessageBox.Show("Không thêm được! Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
        'Refresh bang
        Dim Query3 As SqlDataAdapter = New SqlDataAdapter("select * from KhachHang", connect)
        Database.Clear()

        Query3.Fill(Database)
        dgvhienthi.DataSource = Database.DefaultView
    End Sub

    Private Sub btnsua_Click(sender As Object, e As EventArgs) Handles btnsua.Click
        Dim ketnoi1 As New SqlConnection(chuoiconnect)
        ketnoi1.Open()
        Dim Stradd1 As String = "Update KhachHang Set SoDT = @SoDT, DiaChi = @DiaChi, Ten_KhachHang = @Ten_KhachHang where Ma_KhachHang = @Ma_KhachHang"
        Dim com As New SqlCommand(Stradd1, ketnoi1)
        Try
            com.Parameters.AddWithValue("@Ma_KhachHang", txtmakh.Text)
            com.Parameters.AddWithValue("@Ten_KhachHang", txttenkh.Text)
            com.Parameters.AddWithValue("@SoDT", txtsodt.Text)
            com.Parameters.AddWithValue("@DiaChi", txtdiachi.Text)
            com.ExecuteNonQuery()
            ketnoi1.Close()
            MessageBox.Show("Update thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Update không thành công! Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Database.Clear()
        dgvhienthi.DataSource = Database
        dgvhienthi.DataSource = Nothing
        LoadData()
    End Sub

    Private Sub LoadData()
        Dim connect As SqlConnection = New SqlConnection(chuoiconnect)
        Dim Query1 As SqlDataAdapter = New SqlDataAdapter("select * from KhachHang", connect)

        connect.Open()
        Query1.Fill(Database)
        dgvhienthi.DataSource = Database.DefaultView
    End Sub

    Private Sub btnxoa_Click(sender As Object, e As EventArgs) Handles btnxoa.Click
        Dim ketnoi As New SqlConnection(chuoiconnect)
        ketnoi.Open()
        Dim xoadl As String = "Delete from KhachHang Where Ma_KhachHang = @Ma_KhachHang"
        Dim lenh As New SqlCommand(xoadl, ketnoi)
        Try
            lenh.Parameters.AddWithValue("@Ma_KhachHang", txtmakh.Text)
            lenh.ExecuteNonQuery()
            ketnoi.Close()
        Catch ex As Exception
            MessageBox.Show("Xóa không thành công! Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Database.Clear()
        dgvhienthi.DataSource = Database
        dgvhienthi.DataSource = Nothing
        LoadData()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtmakh.Clear()
        txttenkh.Clear()
        txtsodt.Clear()
        txtdiachi.Clear()
    End Sub

    Private Sub btnthoat_Click(sender As Object, e As EventArgs) Handles btnthoat.Click
        Me.Close()
    End Sub
End Class