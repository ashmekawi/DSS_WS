Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class AllComp
    Inherits System.Web.Services.WebService


    <WebMethod()>
    Public Function GetCRANum(ByVal UserID As String) As DataTable
        Dim str As String
        Dim str1 As String
        Dim x As New DbClass
        Dim dt As New DataTable
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "SELECT TOP 1 [FK_COMPANYCRA_NUM] FROM [DQ].[dbo].[SameCompanyDuplicate] where (userid is null or userid='" & UserID & "') and done<>cnt or done>cnt"
        dt = x.FillDataTable(str, con, "BRN")
        str1 = "update SameCompanyDuplicate set userid = '" & UserID & "' where [FK_COMPANYCRA_NUM] = '" & dt.Rows(0).Item(0) & "'"
        x.ExecInsertUpdateDelete(str1, con)
        Return dt

    End Function
    <WebMethod()>
    Public Function GetDamgcnt(ByVal CRANum As Integer) As String
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select cnt from SameCompanyDuplicate where [FK_COMPANYCRA_NUM] ='" & CRANum & "' and cnt<>done "
        Return x.FillDataTable(str, con, "BRN").Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function GetDamgdone(ByVal CRANum As Integer) As String
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select done from SameCompanyDuplicate where [FK_COMPANYCRA_NUM] ='" & CRANum & "'"
        Return x.FillDataTable(str, con, "BRN").Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function DamgUpdateDone(ByVal Done As Integer, ByVal UserID As Integer) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "  update [SameCompanyDuplicate] set Done = " & Done & "  where UserID ='" & UserID & "' "
        x.ExecInsertUpdateDelete(str, con)
        Return 1
    End Function
    <WebMethod()>
    Public Function DamgCount() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select count(*) from SameCompanyDuplicate where cnt<>done"
        Return x.FillDataTable(str, con, "BRN")
    End Function
    <WebMethod()>
    Public Function DamgCountUserID(ByVal UserID As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select count(*) from SameCompanyDuplicate where cnt=done and UserID='" & UserID & "'"
        Return x.FillDataTable(str, con, "BRN")
    End Function
    <WebMethod()>
    Public Function DamgCSOs(ByVal CRANum As Integer) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "select CSO1,CSO2   FROM [DQ].[dbo].[Comp_Same] where [CRA_NUM] = '" & CRANum & "'"
        Return x.FillDataTable(str, con, "BRN")
    End Function
    <WebMethod()>
    Public Function DamgData(ByVal CSO1 As Integer, ByVal CSO2 As Integer) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "select CSO,NAME0,ID_NUMBER,sex,FK_POLICE_STATIFK,[BIRTH_DATE],OLD_BIRTH_DATE,OLD_ID_NUMBER,cname  from Person where CSO in ('" & CSO1 & "','" & CSO2 & "') order by cname"
        Return x.FillDataTable(str, con, "BRN")
    End Function
    <WebMethod()>
    Public Function InsuranceData(ByVal ID_Number As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00-As").ConnectionString
        str = "SELECT *  FROM [CRA00].[dbo].[InsuranceData] where ID_NUMBER ='" & ID_Number & "'  and HaveData in (0,1)"
        Return x.FillDataTable(str, con, "Data")
    End Function
    <WebMethod()>
    Public Function LoopData(ByVal FullName As String, ByVal FamilyName As String, ByVal InsuranceNumber As String, ByVal NationalId As String, ByVal MotherName As String, ByVal Governorate As String, ByVal Zone As String, ByVal Sector As String, ByVal Gender As String, ByVal haveData As String) As Integer
        Dim str As String

        If FullName = "" Then
            FullName = "لايوجد"
        End If

        str = "UPDATE [dbo].[InsuranceData]
   SET 
      [FullName] = '" & FullName & "'
      ,[FamilyName] = '" & FamilyName & "'
      ,[InsuranceNumber] = '" & InsuranceNumber & "'
      ,[NationalId] = '" & NationalId & "'
      ,[MotherName] ='" & MotherName & "'
      ,[Governorate] ='" & Governorate & "'
      ,[Zone] = '" & Zone & "'
      ,[Sector] ='" & Sector & "'
      ,[Gender] = '" & Gender & "'
      ,[HaveData] = '" & haveData & "'
        WHERE ID_NUMBER = '" & NationalId & "'"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00-As").ConnectionString

        Return x.ExecInsertUpdateDelete(str, con)
    End Function
    <WebMethod()>
    Public Function NotDamg(ByVal Cso As String, ByVal CRANum As Integer) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "INSERT INTO [dbo].[TempCSO] ([CSO]) VALUES ('" & Cso & "')"
        x.ExecInsertUpdateDelete(str, con)
        Dim str1 As String
        str1 = "  update [SameCompanyDuplicate] set Done = done+1,[TimeStamp]='" & DateTime.Now.ToString() & "'  where [FK_COMPANYCRA_NUM] ='" & CRANum & "'"
        x.ExecInsertUpdateDelete(str1, con)
        Return 1
    End Function
    <WebMethod()>
    Public Function DamgDone(ByVal FromCso As String, ByVal ToCso As String, ByVal CRANum As Integer, ByVal UserID As Integer) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "EXECUTE  [dbo].[SP_MargePerson_Batch]  " & FromCso & " ," & ToCso & ",'" & UserID & "'"
        x.ExecInsertUpdateDelete(str, con)
        Dim str1 As String
        str1 = "  update [SameCompanyDuplicate] set Done = done+1 ,[TimeStamp]='" & DateTime.Now.ToString() & "' where [FK_COMPANYCRA_NUM] ='" & CRANum & "'"
        Dim con1 As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        x.ExecInsertUpdateDelete(str1, con1)
        Return 1
    End Function
    <WebMethod()>
    Public Function CompanyName(ByVal CRANum As Integer) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "SELECT Top 1 name0,date0 FROM COMPANY_NAME where STATUS = 1 and FK_COMPANYCRA_NUM ='" & CRANum & "'"
        Return x.FillDataTable(str, con, "Name")
    End Function
    <WebMethod()>
    Public Function NIDStatus() As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "select ONOFF from Fun where Name = 'NID'"
        Return x.FillDataTable(str, con, "Name").Rows(0).Item(0)
    End Function
End Class