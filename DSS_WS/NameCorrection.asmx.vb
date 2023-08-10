Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class NameCorrection
    Inherits System.Web.Services.WebService


    <WebMethod()>
    Public Function GetID(ByVal Part As Int32, ByVal Name0 As String) As DataTable
        Dim str As String
        str = "SELECT top (1) [id],[Cname],[Part1],[Part2],[Part3],[Part4],[Part5],[Part6] FROM [CRA00].[dbo].[PerName]  where part" & Part & " like'%" & Name0 & "%' and Done = 0"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        Return x.FillDataTable(str, con, "CRCat")
    End Function
    <WebMethod()>
    Public Function GetCsos(ByVal pernameid As Int32) As DataTable
        Dim str As String
        str = "SELECT cso,Name0,Birth_Date fROM PERSON where pernameid ='" & pernameid & "'"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        Return x.FillDataTable(str, con, "CRCat")
    End Function

    <WebMethod()>
    Public Function Exl(ByVal pernameid As Int32, ByVal UserID As String) As Boolean
        Dim str As String
        str = "update [CRA00].[dbo].[PerName] set done =2 ,UserID = '" & UserID & "', timestamp = '" & DateTime.Now & "' where id = '" & pernameid & "'"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        Return x.ExecInsertUpdateDelete(str, con)
    End Function

    <WebMethod()>
    Public Function Correction(ByVal pernameid As Int32, ByVal NewName As String, ByVal UserID As Int32) As Boolean
        Dim str As String
        Dim str1 As String
        Dim str2 As String
        str2 = "update [CRA00].[dbo].[PerName] set done =1 ,UserID = '" & UserID & "', timestamp = '" & DateTime.Now & "' where id = '" & pernameid & "'"
        str1 = "exec [dbo].[Sp_Append2AnyLog] 'Person','pernameid=" & pernameid & "' ,'" & UserID & "'"
        str = "update person set name0='" & NewName & "' where pernameid = '" & pernameid & "'"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        Dim str4 As String
        str4 = "delete from pername where id = " & pernameid & ""
        x.ExecInsertUpdateDelete(str4, con)
        x.ExecInsertUpdateDelete(str2, con)
        x.ExecInsertUpdateDelete(str1, con)
        Return x.ExecInsertUpdateDelete(str, con)
    End Function
    <WebMethod()>
    Public Function UpdatePerName(ByVal CSO As Int32) As Boolean
        Dim str As String

        str = "[dbo].[SP_SetPersonCname] " & CSO & ""
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        Return x.ExecInsertUpdateDelete(str, con)
    End Function
End Class