Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class DamgRiview
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function DamdRiviewData() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "SELECT      top(1000)  PERSON_Marged.FromFK_PERSONCSO AS [From], CRA00LOG.dbo.PERSONLog.NAME0 AS FromName, CRA00LOG.dbo.PERSONLog.ID_NUMBER AS [From ID], PERSON_Marged.ToFK_PERSONCSO AS [To], 
                         PERSON.NAME0 AS [To name], PERSON.ID_NUMBER AS [To ID], OPERATOR.NAME AS [User name], PERSON_Marged.CraTimeStamp
FROM            PERSON_Marged INNER JOIN
                         OPERATOR ON PERSON_Marged.CraUserID = OPERATOR.NUMBER0 INNER JOIN
                         PERSON ON PERSON_Marged.ToFK_PERSONCSO = PERSON.CSO INNER JOIN
                         CRA00LOG.dbo.PERSONLog ON PERSON_Marged.FromFK_PERSONCSO = CRA00LOG.dbo.PERSONLog.CSO"



        Dim dt As DataTable
        dt = x.FillDataTable(str, con, "Person")
        Return dt
    End Function


End Class