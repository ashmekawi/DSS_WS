Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class Mosta5rg
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetGov() As DataTable
        Dim str As String
        str = "SELECT [CODE],[ADESC] as gov FROM [CRA00].[dbo].[GOVERNORATE] where code not in (0,88)"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result")
    End Function
    <WebMethod()>
    Public Function GetOffice(ByVal GovID As Integer) As DataTable
        Dim str As String
        str = "SELECT [CODE],[NAME] FROM [CRA00].[dbo].[OFFICE] where   FK_GOVERNORATECODE = '" & GovID & "' and code not in (52,191,200,901,902)"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result")
    End Function
    <WebMethod()>
    Public Function GetCompanies(ByVal OfficeID As Integer, ByVal RegNum As Integer) As DataTable
        Dim str As String
        str = "select cra_num,REGISTRATION_NO,ct.NUMBER0,dbo.intToDate(ct.DATE0)date0,ltrim(rtrim(cn.name0)) name0,ltrim(rtrim(class.ADESC))class,ltrim(rtrim(legal.ADESC))legal
                from company c inner join company_name cn on cn.FK_COMPANYCRA_NUM=c.CRA_NUM and cn.STATUS=1 and cn.FK_COMPANY_NAMECOD in (1,4)
                inner join class on class.CODE=c.FK_CLASSCODE
                inner join LEGAL on LEGAL.CODE=c.FK_LEGALCODE
                inner join Company_Transaction ct on ct.id=c.FKCompany_TransactionID
                where REGISTRATION_NO=" & RegNum & " and c.OfficeCode=" & OfficeID & ""
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result")
    End Function

    <WebMethod()>
    Public Function GetCompaniesByName(ByVal Name0 As String) As DataTable
        Dim str As String
        str = "SELECT [FK_COMPANYCRA_NUM] ,name0  FROM [CRA00].[dbo].[COMPANY_NAME]  where ltrim(rtrim(name0)) like '%" & Name0 & "%'"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result")
    End Function

    <WebMethod()>
    Public Function GetPerson(ByVal Name0 As String) As DataTable
        Dim str As String
        str = "SELECT [CSO] ,name0  FROM [CRA00].[dbo].[Person]  where ltrim(rtrim(name0)) like '%" & Name0 & "%'"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result")
    End Function



    <WebMethod()>
    Public Function GetCompanyByPerson(ByVal CSO As Integer) As DataTable
        Dim str As String
        str = "SELECT        COMPANY_NAME.FK_COMPANYCRA_NUM, COMPANY_NAME.name0, COMPANY_NAME.DATE0, COMPANY_NAME.NUMBER0, COMPANY_PERSON.FK_PERSONCSO
FROM            COMPANY_NAME INNER JOIN
                         COMPANY_PERSON ON COMPANY_NAME.FK_COMPANYCRA_NUM = COMPANY_PERSON.FK_COMPANYCRA_NUM
WHERE        (COMPANY_PERSON.FK_PERSONCSO = '" & CSO & "')"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result")
    End Function

End Class


