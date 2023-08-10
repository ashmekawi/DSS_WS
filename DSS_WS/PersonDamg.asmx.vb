Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class PersonDamg
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetPerson(ByVal Name0 As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "SELECT top(10000)  CSO, NAME0, ID_NUMBER, SEX, FK_POLICE_STATIFK, BIRTH_DATE, OLD_BIRTH_DATE, OLD_ID_NUMBER FROM PERSON 
                where cname like '" & Name0 & "' and cso not in (select fromCso from dq.dbo.MargeConfirm where FromPage= 2 and done is null) order by LTRIM(rtrim(Cname)),BIRTH_DATE"
        Dim dt As DataTable
        dt = x.FillDataTable(str, con, "Person")
        Return dt


    End Function
    <WebMethod()>
    Public Function GetCompany(ByVal CSO As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "SELECT        COMPANY.OfficeCode, COMPANY.REGISTRATION_NO, COMPANY_PERSON.DATE0, COMPANY_NAME.name0, ACTIVITY_TEXT.ADESC, COMPANY_PERSON.FK_PERSONCSO
FROM            COMPANY_PERSON INNER JOIN
                         COMPANY ON COMPANY_PERSON.FK_COMPANYCRA_NUM = COMPANY.CRA_NUM LEFT OUTER JOIN
                         COMPANY_NAME ON COMPANY.CRA_NUM = COMPANY_NAME.FK_COMPANYCRA_NUM LEFT OUTER JOIN
                         ACTIVITY_TEXT ON COMPANY.CRA_NUM = ACTIVITY_TEXT.FK_COMPANYCRA_NUM
WHERE        (COMPANY_PERSON.FK_PERSONCSO = " & CSO & ")"





        Dim dt As DataTable
        dt = x.FillDataTable(str, con, "Company")
        Return dt


    End Function

End Class