Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class NullID
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetIDNumber(ByVal UserID As Integer) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "SELECT top(1) [id_number] FROM [DQ].[dbo].[NullIdNumber] where done <> cnt and (userID is null or userID = '" & UserID & "') and isnull([Canceled],0) <>1"
        Dim dt As New DataTable
        dt = x.FillDataTable(str, con, "0")
        Dim str1 As String = "update [DQ].[dbo].[NullIdNumber] set Userid = '" & UserID & "'where id_number = '" & dt.Rows(0).Item(0) & "'"
        x.ExecInsertUpdateDelete(str1, con)
        Return dt
    End Function
    <WebMethod()>
    Public Function GetIDData(ByVal IdNumber As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "Select cso,Name0,BIRTH_DATE,ID_NUMBER  from Person where id_number='" & IdNumber & "'"
        Return x.FillDataTable(str, con, "0")
    End Function

    <WebMethod()>
    Public Function NullID(ByVal Cso As String, ByVal UserID As String) As Boolean
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "[cra00].[dbo].[NullIdNumber] @CSO=" & Cso & ",@userid='" & UserID & "'"
        Return x.ExecInsertUpdateDelete(str, con)
    End Function

    <WebMethod()>
    Public Function UpdateDone(ByVal ID_Number As String) As Boolean
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "update [DQ].[dbo].[NullIdNumber] set done=done+1 , timestamp = '" & DateTime.Now & "' where [id_number] = '" & ID_Number & "'"
        Return x.ExecInsertUpdateDelete(str, con)
    End Function


    <WebMethod()>
    Public Function Canceled(ByVal ID_Number As String) As Boolean
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "update [DQ].[dbo].[NullIdNumber] set canceled=1 , timestamp = '" & DateTime.Now & "' where [id_number] = '" & ID_Number & "'"
        Return x.ExecInsertUpdateDelete(str, con)
    End Function
End Class