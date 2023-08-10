Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class PerNameUpdate
    Inherits System.Web.Services.WebService
    <WebMethod()>
    Public Function GetPernameid(ByVal UserID As String) As DataTable
        Dim str As String
        Dim str1 As String
        Dim x As New DbClass
        Dim dt As New DataTable
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "SELECT TOP 1 [pernameid],[birth_date] FROM [DQ].[dbo].[DuplicatePerNameID] where (userid is null or userid='" & UserID & "') and done<>cnt or done>cnt"
        ' str = "SELECT TOP 1 [id_number] FROM [DQ].[dbo].[PersonDuplicateIDNum] where (userid is null or userid='" & UserID & "') and done<>cnt or done>cnt"
        dt = x.FillDataTable(str, con, "BRN")
        str1 = "update DuplicatePerNameID set userid = '" & UserID & "' where [pernameid] = '" & dt.Rows(0).Item(0) & "' and [birth_date] = '" & dt.Rows(0).Item(1) & "'"
        x.ExecInsertUpdateDelete(str1, con)
        Return dt

    End Function
    <WebMethod()>
    Public Function GetPersonData(ByVal PerNameID As Integer, ByVal Birth_Date As Integer) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        '  str = "select CSO,NAME0,ID_NUMBER,sex,FK_POLICE_STATIFK,OLD_BIRTH_DATE,OLD_ID_NUMBER 
        'FROM [CRA00].[dbo].[PERSON] where [pernameid]='" & PerNameID & "' and BIRTH_DATE='" & Birth_Date & "' and CSO not in (select cso from [DQ].dbo.tempcso)"
        str = "SELECT person.CSO , person.name0 ,person.sex,person.FK_POLICE_STATIFK,person.OLD_BIRTH_DATE,person.OLD_ID_NUMBER  FROM [DQ].[dbo].[DuplicateZname]
  inner join cra00.dbo.PERSON on [DuplicateZname].cso = PERSON.cso
  where [DuplicateZname].r = " & PerNameID & " and [DuplicateZname].bith10=" & Birth_Date & ""
        Return x.FillDataTable(str, con, "Data")
    End Function
    <WebMethod()>
    Public Function GetDamgcnt(ByVal PerNameID As Integer, ByVal Birth_Date As Integer) As String
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select cnt from DuplicatePerNameID where [pernameid]='" & PerNameID & "' and BIRTH_DATE='" & Birth_Date & "'"
        Return x.FillDataTable(str, con, "BRN").Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function GetDamgdone(ByVal PerNameID As Integer, ByVal Birth_Date As Integer) As String
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select done from DuplicatePerNameID where [pernameid]='" & PerNameID & "' and BIRTH_DATE='" & Birth_Date & "'"
        Return x.FillDataTable(str, con, "BRN").Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function DamgUpdateDone(ByVal Done As String, ByVal PerNameID As Integer, ByVal Birth_Date As Integer) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "  update [DuplicatePerNameID] set Done = '" & Done & "', nodamg=1,[TimeStamp]='" & DateTime.Now.ToString() & "' where [pernameid]='" & PerNameID & "' and BIRTH_DATE='" & Birth_Date & "'"
        x.ExecInsertUpdateDelete(str, con)
        Return 1
    End Function
    <WebMethod()>
    Public Function DamgCount() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select count(*) from DuplicatePerNameID where cnt<>done"
        Return x.FillDataTable(str, con, "BRN")
    End Function
    <WebMethod()>
    Public Function DamgCountUserID(ByVal UserID As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select count(*) from DuplicatePerNameID where cnt=done and UserID='" & UserID & "' and [DamgDone] > 0"
        Return x.FillDataTable(str, con, "BRN")
    End Function
    <WebMethod()>
    Public Function DamgData(ByVal PerNameID As Integer, ByVal Birth_Date As Integer) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        'str = "select CSO,NAME0,ID_NUMBER,sex,FK_POLICE_STATIFK,BIRTH_DATE,OLD_BIRTH_DATE,OLD_ID_NUMBER  from person where [pernameid]='" & PerNameID & "' and zlen > 10 and  Left(BIRTH_DATE,6) in(left('" & Birth_Date & "',6),190001) order by BIRTH_DATE   --and CSO not in (select cso from [DQ].dbo.tempcso)"
        str = "SELECT distinct person.CSO , person.name0 ,person.ID_NUMBER,person.sex,person.FK_POLICE_STATIFK,person.BIRTH_DATE,person.OLD_BIRTH_DATE,person.OLD_ID_NUMBER,person.Zname  FROM [DQ].[dbo].[DuplicateZname]
        inner join cra00.dbo.PERSON on [DuplicateZname].cso = PERSON.cso
        where [DuplicateZname].r = " & PerNameID & "  and isnull([DuplicateZname].DamgDone,0) <> 1 order by person.Zname,person.BIRTH_DATE"
        Return x.FillDataTable(str, con, "BRN")
    End Function
    <WebMethod()>
    Public Function NotDamg(ByVal Cso As String, ByVal PerNameID As Integer, ByVal Birth_Date As Integer) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "INSERT INTO [dbo].[TempCSO] ([CSO]) VALUES ('" & Cso & "')"
        x.ExecInsertUpdateDelete(str, con)
        Dim str1 As String
        str1 = "  update [DuplicatePerNameID] set Done = done+1,[TimeStamp]='" & DateTime.Now.ToString() & "',[NoDamg]=1  where [pernameid]='" & PerNameID & "' and left(BIRTH_DATE,4)=left('" & Birth_Date & "',4)"
        x.ExecInsertUpdateDelete(str1, con)
        Return 1
    End Function
    <WebMethod()>
    Public Function DamgDone(ByVal FromCso As String, ByVal ToCso As String, ByVal PerNameID As Integer, ByVal Birth_Date As Integer, ByVal UserID As Integer, ByVal FromPage As Integer) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "EXECUTE  [dbo].[SP_MargePerson_Batch]  " & FromCso & " ," & ToCso & ", '" & UserID & "','" & FromPage & "'"
        x.ExecInsertUpdateDelete(str, con)
        Dim str1 As String
        str1 = "  update [DuplicatePerNameID] set Done = done+1 ,[TimeStamp]='" & DateTime.Now.ToString() & "', DamgDone=1 where [pernameid]='" & PerNameID & "' and BIRTH_DATE='" & Birth_Date & "'"
        Dim con1 As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        x.ExecInsertUpdateDelete(str1, con1)
        Dim str2 = "UPDATE [dbo].[DuplicateZname]SET  [DamgDone] =1 WHERE cso = '" & FromCso & "' and r = '" & PerNameID & "'"
        x.ExecInsertUpdateDelete(str2, con1)
        Return 1
    End Function
    <WebMethod()>
    Public Function InsuranceData(ByVal ID_Number As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00-As").ConnectionString
        str = "SELECT *  FROM [InsuranceData] where ID_NUMBER ='" & ID_Number & "'  and HaveData in (0,1)"
        Return x.FillDataTable(str, con, "Data")
    End Function
    <WebMethod()>
    Public Function LoopData(ByVal FullName As String, ByVal FamilyName As String, ByVal InsuranceNumber As String, ByVal NationalId As String, ByVal MotherName As String, ByVal Governorate As String, ByVal Zone As String, ByVal Sector As String, ByVal Gender As String, ByVal haveData As String) As Boolean
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
    Public Function NIDStatus() As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("dq").ConnectionString
        str = "select ONOFF from Fun where Name = 'NID'"
        Return x.FillDataTable(str, con, "Name").Rows(0).Item(0)
    End Function

    <WebMethod()>
    Public Function NewDamgDone(ByVal FromCso As String, ByVal ToCso As String, ByVal PerNameID As Integer, ByVal Birth_Date As Integer, ByVal UserID As Integer, ByVal FromPage As Integer) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "EXECUTE  [dbo].[SP_MargePerson_Done]  " & FromCso & " ," & ToCso & ", '" & UserID & "','" & FromPage & "'"
        x.ExecInsertUpdateDelete(str, con)
        Dim str1 As String
        str1 = "  update [DQ].[dbo].[MargeConfirm] set Done = 1 where done is null and [fromCso] ='" & FromCso & "' and [ToCso] = '" & ToCso & "' and FromPage = '" & FromPage & "' and RevUserID = '" & UserID & "'  "
        Dim con1 As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        x.ExecInsertUpdateDelete(str1, con1)

        Return 1
    End Function
    <WebMethod()>
    Public Function NewNoDamg(ByVal FromCso As String, ByVal ToCso As String, ByVal PerNameID As Integer, ByVal Birth_Date As Integer, ByVal UserID As Integer, ByVal FromPage As Integer) As Int32
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        Dim str1 As String
        str1 = "  update [DQ].[dbo].[MargeConfirm] set Done = 0 where done is null and [fromCso] ='" & FromCso & "' and [ToCso] = '" & ToCso & "' and FromPage = '" & FromPage & "' and RevUserID = '" & UserID & "'  "
        Dim con1 As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        x.ExecInsertUpdateDelete(str1, con1)

        Return 1
    End Function

End Class