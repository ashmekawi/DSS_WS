Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class GafiCairo
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GafiLogin(ByVal UserID As String, ByVal PWD As String) As Integer
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("Cra_Con").ConnectionString
        str = "SELECT ID from GafiUser where UserID ='" & UserID & "' and PWD =hashbytes('SHA1',N'" & PWD & "')"
        Dim dt As DataTable
        dt = x.FillDataTable(str, con, "Gafi")
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item(0)
        Else
            Return 0
        End If

    End Function

    <WebMethod()>
    Public Function TranType() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("Cra_Con").ConnectionString
        str = "SELECT *  FROM [Cra_Con].[dbo].[GafiTranType]"
        Dim dt As DataTable
        dt = x.FillDataTable(str, con, "Gafi")
        If dt.Rows.Count > 0 Then
            Return dt
        Else
            Return dt
        End If

    End Function
    <WebMethod()>
    Public Function AddTransaction(ByVal Type As Integer, ByVal Text0 As String, ByVal Reg_Num As Integer, ByVal Name0 As String) As Integer
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("Cra_Con").ConnectionString
        Dim result As Integer
        str = "INSERT INTO [dbo].[GafiTransaction] ([Type] ,[Text0],[Reg_Num],[Name0])
     VALUES
           (
           '" & Type & "'
           ,'" & Text0 & "'
           ,'" & Reg_Num & "'
           ,'" & Name0 & "' )"


        Dim save0 As Boolean
        save0 = x.ExecInsertUpdateDelete(str, con)
        If (save0 = True) Then
            Dim dt As New DataTable
            Dim str1 = "select id from GafiTransaction where reg_Num = '" & Reg_Num & "' and text0 = '" & Text0 & "'"
            dt = x.FillDataTable(str1, con, "result")
            result = dt.Rows(0).Item(0)
            Return result
        End If
        Return save0


    End Function

End Class