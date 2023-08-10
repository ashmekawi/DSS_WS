Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Correction
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetData(ByVal UserID As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select top(1) * from dq.dbo.Compare where (UserID = " & UserID & " or UserID  is Null) and correct = 0 "
        Dim dt As DataTable
        dt = x.FillDataTable(str, con, "Gafi")
        Dim str1 As String
        str1 = "Update dq.dbo.Compare set UserID = " & UserID & " where [id] = '" & dt.Rows(0).Item(0) & "' "
        Dim save As Int16
        save = x.ExecInsertUpdateDelete(str1, con)
        Return dt


    End Function

    <WebMethod()>
    Public Function Correct(ByVal id As String) As Integer
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim str1 As String
        str1 = "Update dq.dbo.Compare set correct = 1 ,timestamp = getdate() where [id] = '" & id & "' "
        Dim save As Int16
        save = x.ExecInsertUpdateDelete(str1, con)
        Return save


    End Function

    <WebMethod()>
    Public Function NotCorrect(ByVal id As String) As Integer
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim str1 As String
        str1 = "Update dq.dbo.Compare set correct = 2 ,timestamp = getdate() where [id] = '" & id & "' "
        Dim save As Int16
        save = x.ExecInsertUpdateDelete(str1, con)
        Return save
    End Function

End Class