Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class PersonDashboard
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetPersonDashboard() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("Cra_Con").ConnectionString
        str = "select * from V_DashboardMaxDatePerson"
        Dim dt As New DataTable
        dt = x.FillDataTable(str, con, "0")
        Return dt
    End Function
End Class