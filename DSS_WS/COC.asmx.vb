Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class COC
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function AddCert(ByVal CertNo As Integer, ByVal CompanyName As String, ByVal CertDate As String, ByVal CertType As String, ByVal OfficeCode As Integer, ByVal UserID As Integer) As Boolean
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "INSERT INTO [dbo].[COCCert]
           ([CertNo]
           ,[CompanyName]
           ,[CertDate]
           ,[CertType]
           ,[OfficeCode]
           ,[UserID])
     VALUES
           ('" & CertNo & "'
           ,'" & CompanyName & "'
           ,'" & CertDate & "'
           ,'" & CertType & "'
           ,'" & OfficeCode & "'
           ,'" & UserID & "')"
        Return x.ExecInsertUpdateDelete(str, con)
    End Function

    <WebMethod()>
    Public Function Login(ByVal UserName As String, ByVal Password As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "SELECT top(1) * From COCUsers where UserName = '" & UserName & "' and Password = '" & Password & "'"
        Dim dt As New DataTable
        dt = x.FillDataTable(str, con, "0")
        Return dt
    End Function
    <WebMethod()>
    Public Function GetCert(ByVal CertID As String, ByVal Office As String) As Integer
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select count(*) from [DQ].[dbo].[COCCert] where id = '" & CertID & "' and [OfficeCode] = '" & Office & "'"
        Dim dt As New DataTable
        dt = x.FillDataTable(str, con, "0")
        Return dt.Rows(0).Item(0)
    End Function



End Class