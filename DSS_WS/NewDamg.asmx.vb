Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class NewDamg
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function DamgData(UserID As Integer) As DamgDataResponse
        Dim result As New DamgDataResponse
        Dim str As String
        str = "SELECT TOP (1) [fromCso],[ToCso] FROM  [DQ].[dbo].[MargeConfirm] where (RevUserID is null  or RevUserID = '" & UserID & "') and done is null"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Dim dt As DataTable
        dt = x.FillDataTable(str, con, "Result")
        result.FromCSO = dt.Rows(0).Item(0)
        result.TOCSO = dt.Rows(0).Item(1)
        Dim Fromstr As String
        Fromstr = "select NAME0,BIRTH_DATE,ID_NUMBER,FK_POLICE_STATIFK,OLD_BIRTH_DATE,OLD_ID_NUMBER from cra00.dbo.person where cso=" & result.FromCSO & ""
        Dim Fromdt As DataTable
        Fromdt = x.FillDataTable(str, con, "Gafi")
        result.FromName = Fromdt.Rows(0).Item(0)
        result.FromBD = Fromdt.Rows(0).Item(1)
        result.FromNID = Fromdt.Rows(0).Item(2)
        result.FromGov = Fromdt.Rows(0).Item(3)
        result.FromOldBD = Fromdt.Rows(0).Item(4)
        result.FromOldNID = Fromdt.Rows(0).Item(5)

        Dim TOstr As String
        TOstr = "select NAME0,BIRTH_DATE,ID_NUMBER,FK_POLICE_STATIFK,OLD_BIRTH_DATE,OLD_ID_NUMBER from cra00.dbo.person where cso=" & result.FromCSO & ""
        Dim TOdt As DataTable
        TOdt = x.FillDataTable(str, con, "Gafi")
        result.TOName = TOdt.Rows(0).Item(0)
        result.TOBD = TOdt.Rows(0).Item(1)
        result.TONID = TOdt.Rows(0).Item(2)
        result.TOGov = TOdt.Rows(0).Item(3)
        result.TOOldBD = TOdt.Rows(0).Item(4)
        result.TOOldNID = TOdt.Rows(0).Item(5)


        Return result
    End Function

End Class