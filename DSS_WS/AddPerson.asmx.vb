Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class AddPerson
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function AddPerson(ByVal NID As String, ByVal Name As String, ByVal Mob As String, ByVal Office As Int32, ByVal UserID As Int32, ByVal TaxID As Int32) As Boolean
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("dq").ConnectionString
        str = "EXEC[dbo].[DQPerson] @NID = N'" & NID & "',@Name = N'" & Name & "',@Mob = N'" & Mob & "',@Office = " & Office & ",	@UserID = " & UserID & ",	@TaxID = " & TaxID & ""
        Return x.ExecInsertUpdateDelete(str, con)
    End Function
End Class