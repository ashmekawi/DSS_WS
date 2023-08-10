Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Amny
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetActivity() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "SELECT  [code] ,[Adesc] ,[amni] FROM [CRA00].[dbo].[ACTIVITY] where code <>0 and amni1 = 0 and left(act6_num,2)<>99"
        'x.ExecInsertUpdateDelete(str, con)
        Return x.FillDataTable(str, con, "0")
    End Function

    <WebMethod()>
    Public Function AmnyYes(ByVal Code As Int32, ByVal UserID As Int32) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "update [CRA00].[dbo].[ACTIVITY] Set amni1 =1, amniUser = '" & UserID & "' where [code]='" & Code & "'"

        Return x.ExecInsertUpdateDelete(str, con)
    End Function
    <WebMethod()>
    Public Function AmnyNo(ByVal Code As Int32, ByVal UserID As Int32) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "update [CRA00].[dbo].[ACTIVITY] Set amni1 =2, amniUser = '" & UserID & "' where [code]='" & Code & "'"
        '
        Return x.ExecInsertUpdateDelete(str, con)
    End Function

    <WebMethod()>
    Public Function AmnyCount() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "Select * From [dbo].[AmniCount]()"
        Return x.FillDataTable(str, con, "Count")
    End Function

End Class






