Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DSS_WS.Module1

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class GGLog
    Inherits System.Web.Services.WebService
    Dim con As String = ConfigurationManager.ConnectionStrings("Cra_Con").ConnectionString
    <WebMethod()>
    Public Function InsuranceInquireLog(ByVal ResponseCode As String, ByVal ResponseMessage As String, ByVal NID As String, ByVal FamilyName As String, ByVal FullName As String, ByVal Gender As String, ByVal Governorate As String, ByVal InsuranceNumber As String, ByVal MotherName As String, ByVal Sector As String, ByVal UserID As String) As Boolean
        Dim x As New DbClass
        Dim str As String
        str = "INSERT INTO [dbo].[InsuranceInquireLog]
           ([ResponseCode]
           ,[ResponseMessage]
           ,[NID]
           ,[FamilyName]
           ,[FullName]
           ,[Gender]
           ,[Governorate]
           ,[InsuranceNumber]
           ,[MotherName]
           ,[Sector]
           ,[UserID])
     VALUES
           ('" & ResponseCode & "',
            '" & ResponseMessage & "',
            '" & NID & "',
            '" & FamilyName & "',
            '" & FullName & "',
            '" & Gender & "',
            '" & Governorate & "',
            '" & InsuranceNumber & "',
            '" & MotherName & "',
            '" & Sector & "',
            '" & UserID & "')"
        Return x.ExecInsertUpdateDelete(str, con)
    End Function

    <WebMethod()>
    Public Function TawkelStatusLog(ByVal alphabet As String, ByVal number As String, ByVal office As String, ByVal year As String, ByVal status As String, ByVal UserID As String) As Integer
        Dim x As New DbClass
        Dim str As String
        str = "INSERT INTO [dbo].[TawkelStatus]
           ([alphabet]
           ,[number]
           ,[office]
           ,[year]
           ,[status]
           ,[UserID])
     VALUES
           ('" & alphabet & "'
           ,'" & number & "'
           ,'" & office & "'
           ,'" & year & "'
           ,'" & status & "'
           ,'" & UserID & "')"
        If x.ExecInsertUpdateDelete(str, con) = True Then
            str = "select max(id) from [dbo].[TawkelStatus] where alphabet ='" & alphabet & "' and year='" & year & "' and UserID = '" & UserID & "' and number = '" & number & "' "
            Dim dt As New DataTable
            dt = x.FillDataTable(str, con, "test")
            If dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows(0).Item(0))
            End If
        Else
            Return 0
        End If
        Return 0
    End Function



    <WebMethod()>
    Public Function TawkelPartnerLog(ByVal TawkelID As String, ByVal custName As String, ByVal custCivilType As String, ByVal custCivilID As String, ByVal customerFromTo As String, ByVal custTypeCode As String, ByVal custTypeNo As String, ByVal Address As String) As Integer
        Dim x As New DbClass
        Dim str As String
        str = "INSERT INTO [dbo].[TawkelPartner]
           ([TawkelID]
           ,[custName]
           ,[custCivilType]
           ,[custCivilID]
           ,[customerFromTo]
           ,[custTypeCode]
           ,[custTypeNo]
           ,[Address])
     VALUES
           ('" & TawkelID & "'
           ,'" & custName & "'
           ,'" & custCivilType & "'
           ,'" & custCivilID & "'
           ,'" & customerFromTo & "'
           ,'" & custTypeCode & "'
           ,'" & custTypeNo & "'
           ,'" & Address & "')"
        If x.ExecInsertUpdateDelete(str, con) = True Then
            Return 1
        Else
            Return 0

        End If
        Return 0

    End Function
End Class