' NOTE: You can use the "Rename" command on the context menu to change the class name "NewDamgService" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select NewDamgService.svc or NewDamgService.svc.vb at the Solution Explorer and start debugging.
Public Class NewDamgService
    Implements INewDamgService


    Public Function DamgData(UserID As Integer) As DamgDataResponse Implements INewDamgService.DamgData
aa:
        Try
            Dim result As New DamgDataResponse
            Dim str As String
            str = "SELECT TOP (1) [fromCso],[ToCso],[FromPage] FROM  [DQ].[dbo].[MargeConfirm] where (RevUserID is null  or RevUserID = '" & UserID & "') and done is null"
            Dim x As New DbClass
            Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
            Dim dt As DataTable
            dt = x.FillDataTable(str, con, "Result")
            result.FromCSO = dt.Rows(0).Item(0)
            result.TOCSO = dt.Rows(0).Item(1)
            result.Page = dt.Rows(0).Item(2)

            x.ExecInsertUpdateDelete("UPDATE [DQ].[dbo].[MargeConfirm] SET [RevUserID] = '" & UserID & "' WHERE [fromCso] ='" & result.FromCSO & "' and [ToCso] = '" & result.TOCSO & "' and [FromPage] ='" & result.Page & "' and ISNULL(Done,2)=2", con)
            Dim Fromstr As String
            Fromstr = "select NAME0,BIRTH_DATE,ID_NUMBER,FK_POLICE_STATIFK,OLD_BIRTH_DATE,OLD_ID_NUMBER from cra00.dbo.person where cso=" & result.FromCSO & ""
            Dim Fromdt As DataTable
            Fromdt = x.FillDataTable(Fromstr, con, "Gafi")
            If (Fromdt.Rows.Count = 0) Then
                x.ExecInsertUpdateDelete("UPDATE [DQ].[dbo].[MargeConfirm] SET [Done] = '0' WHERE [fromCso] ='" & result.FromCSO & "' and [ToCso] = '" & result.TOCSO & "' and [FromPage] ='" & result.Page & "' and ISNULL(Done,2)=2", con)
                GoTo aa
            End If

            result.FromName = Fromdt.Rows(0).Item(0).ToString()
            result.FromBD = Fromdt.Rows(0).Item(1).ToString()
            result.FromNID = Fromdt.Rows(0).Item(2).ToString()
            result.FromGov = Fromdt.Rows(0).Item(3).ToString()
            result.FromOldBD = Fromdt.Rows(0).Item(4).ToString()
            result.FromOldNID = Fromdt.Rows(0).Item(5).ToString()

            Dim TOstr As String
            TOstr = "select NAME0,BIRTH_DATE,ID_NUMBER,FK_POLICE_STATIFK,OLD_BIRTH_DATE,OLD_ID_NUMBER from cra00.dbo.person where cso=" & result.TOCSO & ""
            Dim TOdt As DataTable
            TOdt = x.FillDataTable(TOstr, con, "Gafi")
            result.TOName = TOdt.Rows(0).Item(0).ToString()
            result.TOBD = TOdt.Rows(0).Item(1).ToString()
            result.TONID = TOdt.Rows(0).Item(2).ToString()
            result.TOGov = TOdt.Rows(0).Item(3).ToString()
            result.TOOldBD = TOdt.Rows(0).Item(4).ToString()
            result.TOOldNID = TOdt.Rows(0).Item(5).ToString()


            Return result

        Catch ex As Exception

        End Try

    End Function
End Class
