Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class DamgConfirm
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetData(ByVal UserID As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select top(1) * from dq.dbo.MargedCheck where (UserID = " & UserID & " or UserID  is Null) and [correct] = 0 "
        Dim dt As DataTable
        dt = x.FillDataTable(str, con, "Gafi")
        Dim str1 As String
        str1 = "Update dq.dbo.MargedCheck set UserID = " & UserID & " where [ToFK_PERSONCSO] = '" & dt.Rows(0).Item(0) & "' and  [FromFK_PERSONCSO] = '" & dt.Rows(0).Item(1) & "' "
        Dim save As Int16
        save = x.ExecInsertUpdateDelete(str1, con)
        Return dt


    End Function

    <WebMethod()>
    Public Function GetToData(ByVal Cso As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select NAME0,BIRTH_DATE,ID_NUMBER,FK_POLICE_STATIFK,OLD_BIRTH_DATE,OLD_ID_NUMBER from cra00.dbo.person where cso=" & Cso & ""
        Dim dt As DataTable
        dt = x.FillDataTable(str, con, "Gafi")
        Return dt
    End Function
    <WebMethod()>
    Public Function GetFromData(ByVal Cso As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select top 1 NAME0,BIRTH_DATE,ID_NUMBER,FK_POLICE_STATIFK,OLD_BIRTH_DATE,OLD_ID_NUMBER from CRA00LOG.dbo.PERSONLog where cso=" & Cso & " order by TransDate desc,TransTime"
        Dim dt As DataTable
        dt = x.FillDataTable(str, con, "Gafi")
        Return dt
    End Function

    <WebMethod()>
    Public Function Correct(ByVal From As Int32, ByVal Too As Int32) As Boolean
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim x As New DbClass
        Dim str As String
        str = "update dq.dbo.MargedCheck set Correct = 1 ,[TimeStamp0]='" & DateTime.Now.ToString() & "'  where ToFK_PERSONCSO = '" & Too & "' and FromFK_PERSONCSO = '" & From & "'  "
        Dim save As Boolean
        save = x.ExecInsertUpdateDelete(str, con)
        Return save
    End Function
    <WebMethod()>
    Public Function NotCorrect(ByVal From As Int32, ByVal Too As Int32, ByVal UserID As String) As Boolean
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim con1 As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        Dim x As New DbClass
        Dim str1 As String
        Dim str As String
        str1 = "EXECUTE  [dbo].[rollBackdmg] '" & From & "'  ,'" & Too & "','" & UserID & "'"
        Dim update As Boolean
        update = x.ExecInsertUpdateDelete(str1, con1)
        str = "update dq.dbo.MargedCheck set Correct = 2 ,[TimeStamp0]='" & DateTime.Now.ToString() & "'  where ToFK_PERSONCSO = '" & Too & "' and FromFK_PERSONCSO = '" & From & "'  "
        Dim save As Boolean
        save = x.ExecInsertUpdateDelete(str, con)
        Return save
    End Function

    <WebMethod()>
    Public Function Count() As String
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim x As New DbClass
        Dim str As String
        str = "select count(*) from dq.dbo.MargedCheck where correct =0 "
        Return x.FillDataTable(str, con, "Count").Rows(0).Item(0)
    End Function


    <WebMethod()>
    Public Function CountUserID(ByVal UserID As String) As String
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim x As New DbClass
        Dim str As String
        str = "select count(*) from dq.dbo.MargedCheck where [userID]='" & UserID & "' and correct <> 0 "
        Return x.FillDataTable(str, con, "Count").Rows(0).Item(0)
    End Function

End Class