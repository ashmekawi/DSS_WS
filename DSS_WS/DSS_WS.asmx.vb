Imports System.ComponentModel
Imports System.Web.Services


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class DSS_WS
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function DailyReport() As DataTable
        Dim str As String

        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("MService").ConnectionString
        str = "select isnull(t1.dm,t6.dtotal) date1,isnull(t1.m,0)most5rag,isnull(t2.s,0)shahda,isnull(t3.t,0)tagdid,isnull(t4.e,0)est3lam,
                isnull(t7.updateadd,0)updateadd,isnull(t8.addP,0)addPerson,isnull(t9.deleteP,0)deletePerson,
                isnull(t5.done,0)done,isnull(t6.total,0)total
                 from
                    (SELECT COUNT(*) total,CAST( CreateDate as date) dtotal 
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 
                  group by CAST(CreateDate as date)) t6 full join
 
 
                 (SELECT COUNT(*) m,CAST( CreateDate as date) dm
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and InProgress not in (201,230)
                  and RequestType=1
                  group by CAST(CreateDate as date)) t1 on t6.dtotal=t1.dm full join
                  (SELECT COUNT(*) s,CAST( CreateDate as date) ds
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and InProgress not in (201,230)
                  and RequestType=2
                  group by CAST(CreateDate as date)) t2
                  on t6.dtotal=t2.ds full join 
                  (SELECT COUNT(*) t,CAST( CreateDate as date) dt
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and InProgress not in (201,230)
                  and RequestType=3
                  group by CAST(CreateDate as date)) t3
                  on t6.dtotal=t3.dt full join 
                  (SELECT COUNT(*) e,CAST( CreateDate as date) de
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and InProgress not in (201,230)
                  and RequestType=4
                  group by CAST(CreateDate as date))
                   t4 on t6.dtotal=t4.de  full join
                   (SELECT COUNT(*) updateadd,CAST( CreateDate as date) dupdateadd
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and requesttype = 12 
                  group by CAST(CreateDate as date)) t7 on t6.dtotal=t7.dupdateadd full join
   
                   (SELECT COUNT(*) addP,CAST( CreateDate as date) dAddP
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and requesttype = 13 
                  group by CAST(CreateDate as date)) t8 on t6.dtotal=t8.daddP full join
   
                   (SELECT COUNT(*) deleteP,CAST( CreateDate as date) dDeleteP
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and requesttype = 14 
                  group by CAST(CreateDate as date)) t9 on t6.dtotal=t9.dDeleteP
 
                  full join 
                   (SELECT COUNT(*) done,CAST( CreateDate as date) ddone
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and InProgress not in (201,230)
                  group by CAST(CreateDate as date)) t5 on t6.dtotal=t5.ddone 

                  where t6.dtotal is not null
                    
                  order by 1 desc"
        Return x.FillDataTable(str, con, "Report")


    End Function
    <WebMethod()>
    Public Function DailyReportByDay(ByVal date0 As Date) As DataTable
        Dim str As String

        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("MService").ConnectionString
        str = "select isnull(t1.dm,t6.dtotal) date1,isnull(t1.m,0)most5rag,isnull(t2.s,0)shahda,isnull(t3.t,0)tagdid,isnull(t4.e,0)est3lam,
                isnull(t7.updateadd,0)updateadd,isnull(t8.addP,0)addPerson,isnull(t9.deleteP,0)deletePerson,
                isnull(t5.done,0)done,isnull(t6.total,0)total
                 from
                    (SELECT COUNT(*) total,CAST( CreateDate as date) dtotal 
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 
                  group by CAST(CreateDate as date)) t6 full join
 
 
                 (SELECT COUNT(*) m,CAST( CreateDate as date) dm
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and InProgress not in (201,230)
                  and RequestType=1
                  group by CAST(CreateDate as date)) t1 on t6.dtotal=t1.dm full join
                  (SELECT COUNT(*) s,CAST( CreateDate as date) ds
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and InProgress not in (201,230)
                  and RequestType=2
                  group by CAST(CreateDate as date)) t2
                  on t6.dtotal=t2.ds full join 
                  (SELECT COUNT(*) t,CAST( CreateDate as date) dt
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and InProgress not in (201,230)
                  and RequestType=3
                  group by CAST(CreateDate as date)) t3
                  on t6.dtotal=t3.dt full join 
                  (SELECT COUNT(*) e,CAST( CreateDate as date) de
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and InProgress not in (201,230)
                  and RequestType=4
                  group by CAST(CreateDate as date))
                   t4 on t6.dtotal=t4.de  full join
                   (SELECT COUNT(*) updateadd,CAST( CreateDate as date) dupdateadd
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and requesttype = 12 
                  group by CAST(CreateDate as date)) t7 on t6.dtotal=t7.dupdateadd full join
   
                   (SELECT COUNT(*) addP,CAST( CreateDate as date) dAddP
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and requesttype = 13 
                  group by CAST(CreateDate as date)) t8 on t6.dtotal=t8.daddP full join
   
                   (SELECT COUNT(*) deleteP,CAST( CreateDate as date) dDeleteP
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and requesttype = 14 
                  group by CAST(CreateDate as date)) t9 on t6.dtotal=t9.dDeleteP
 
                  full join 
                   (SELECT COUNT(*) done,CAST( CreateDate as date) ddone
                  FROM [CraMService].[dbo].[Cra_MSRequest]where Req_Source=1 and InProgress not in (201,230)
                  group by CAST(CreateDate as date)) t5 on t6.dtotal=t5.ddone 

                  where t6.dtotal like '%" & date0 & "%'
                    
                  order by 1"
        Return x.FillDataTable(str, con, "Report")


    End Function
    <WebMethod()>
    Public Function LateReq() As DataTable
        Dim str As String

        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("MService").ConnectionString
        str = "select OfficeName,StatusOfRequest,total, CONVERT(nvarchar(27),TheLatestDate, 120) as TheLatestDate from DSS_LateReq"
        Return x.FillDataTable(str, con, "Report")


    End Function
    <WebMethod()>
    Public Function Count_Perm_Ren() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        str = "SELECT [Expr1] ,[t] ,[done] ,[amnni_No] ,[amnni_yes],[mostab3ad] ,[Nesba] FROM [CRA00].[dbo].[Aya-Count_Perm_Ren] order by 7 desc, 3 desc"
        Return x.FillDataTable(str, con, "Report")


    End Function
    <WebMethod()>
    Public Function CRALogin(ByVal UserName As String, ByVal Password As String) As DataTable

        Dim EncPwd = Encrptstr(Password, 0) 'encrypt the password
        Dim PwdL = Len(EncPwd) 'get password lenght
        EncPwd = Chr(64 + PwdL) & EncPwd
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        'str = "select * from operator where officecode = 200 "
        str = "select name,password,number0,full_name from operator where name = N'" & UserName & "' and [password]=hashbytes('SHA1',N'" & Password & "')  and status = 'enable' "
        Return x.FillDataTable(str, con, "Login")
    End Function
    <WebMethod()>
    Public Function CRALogin200(ByVal UserName As String, ByVal Password As String) As DataTable

        Dim EncPwd = Encrptstr(Password, 0) 'encrypt the password
        Dim PwdL = Len(EncPwd) 'get password lenght
        EncPwd = Chr(64 + PwdL) & EncPwd
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        'str = "select * from operator where officecode = 200 "
        str = "select name,password,number0,full_name from operator where name = N'" & UserName & "' and [password]=hashbytes('SHA1',N'" & Password & "')  and status = 'enable' and officecode = 200"
        Return x.FillDataTable(str, con, "Login")
    End Function
    <WebMethod()>
    Public Function GetCRCat() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("ITDAFinance").ConnectionString
        str = "SELECT [ID], [Cat] FROM [ITDAFinance].[dbo].[CRCat]"
        Return x.FillDataTable(str, con, "CRCat")
    End Function
    <WebMethod()>
    Public Function GetCRService(ByVal CRCatID As Integer) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("ITDAFinance").ConnectionString
        str = "SELECT *  FROM [ITDAFinance].[dbo].[CRServices]where crcat = '" & CRCatID & "'"
        Return x.FillDataTable(str, con, "CRServices")
    End Function
    <WebMethod()>
    Public Function ListOfOffices() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("ITDAFinance").ConnectionString
        str = "SELECT code, name  FROM [cra00].[dbo].[office] where code not in (0,200,52,901,902,191)"
        Return x.FillDataTable(str, con, "Offices")
    End Function
    <WebMethod()>
    Public Function GetOfficeDE() As DataTable
        Dim total As Integer = 0
        Dim dt As New DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("MService").ConnectionString
        str = "SELECT [office] as OfficeName ,[most5rag] ,[shahda] ,[tagdid] ,[est3lam] ,[updateadd] ,[addPerson] ,[deletePerson] ,[Total]
               FROM [CRA00].[dbo].[Aya_Dss_TotalReqOffice] order by total desc  "
        Return x.FillDataTable(str, con, "Result")

    End Function
    <WebMethod()>
    Public Function Prem_Reg(ByVal OfficeID As Integer) As DataTable

        Dim dt As New DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("MService").ConnectionString
        str = "SELECT distinct [REGISTRATION_NO] ,co.[NUMBER0] , dbo.intToDate(co.DATE0)as Date0 FROM [CRA00].[dbo].[COMPANY] co
inner join COMPANY_PERSON F on co.CRA_NUM=F.FK_COMPANYCRA_NUM
	inner join person P on F.FK_PERSONCSO=P.cso
where co.officecode='" & OfficeID & "'   and  LEFT(flgs,1)=0 and CRA_NUM not in (select FK_COMPANYCRA_NUM from cOMPANY_RELEASE)
 AND (p.Death_Status <> 'Y')
order by 1"
        Return x.FillDataTable(str, con, "Result")

    End Function
    <WebMethod()>
    Public Function DSS_UserVsPC() As DataTable

        Dim dt As New DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("MService").ConnectionString
        str = "SELECT [CODE],[office],[pc],[users] FROM [CRA00].[dbo].[DSS_UserVsPC] order by 3 desc"
        Return x.FillDataTable(str, con, "Result")

    End Function
    <WebMethod()>
    Public Function DEPaymentReport(date0 As String) As DataTable
        Dim dt As New DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "select * from Aya_Dss_DEPaymentReport where DateToFilter like '%" & date0 & "%'"
        Return x.FillDataTable(str, con, "Result")
    End Function
    <WebMethod()>
    Public Function IncomeByOffice(ByVal OfficeID As Integer) As DataTable
        Dim str As String
        str = "SELECT * from Aya_Dss_TotalIncome where  Officecode='" & OfficeID & "' order by 2"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("ITDAFinance").ConnectionString
        Return x.FillDataTable(str, con, "Result")
    End Function
    <WebMethod()>
    Public Function IncomeByOfficeMortagaging(ByVal OfficeID As Integer) As DataTable
        Dim str As String
        str = "SELECT * from Aya_Dss_TotalWithoutMortgaging where  Officecode='" & OfficeID & "' order by 2"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("ITDAFinance").ConnectionString
        Return x.FillDataTable(str, con, "Result")
    End Function
    <WebMethod()>
    Public Function WorkingDay() As Integer
        Dim str As String
        str = "select top(1) count(cnt)m,cnt from(select count(distinct date0)cnt from cra_con.dbo.printlog where date0>= 20220101 and date0<=convert(varchar,getdate(),112)group by issuoffice)a group by cnt order by 1 desc"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result").Rows(0).Item(1)
    End Function
    <WebMethod()>
    Public Function ReportMerg(ByVal date0 As String) As Integer
        Dim str As String
        str = "SELECT count(*)
                FROM [CRA00].[dbo].[PERSON_Marged]where cast(cratimestamp as date) = '" & date0 & "' and CraUserID in (2000000252,
                2000000253,
                2000000250,
                2000000239,670000002,2000000205)"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result").Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function PersonWithMob() As Integer
        Dim str As String
        str = "SELECT count(*)
  FROM [CRA00].[dbo].[PersonConnect]where FK_PERSONCSO in (select cso from PERSON) and ISNUMERIC(adesc)=1  and len(Adesc)=11"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result").Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function silentpartner() As Integer
        Dim str As String
        str = "SELECT count(*)
  FROM [CRA00].[dbo].[PERSONLog]where UserID=444 and TransDate=cast(GETDATE() as int)"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result").Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function CountPerson() As Integer
        Dim str As String
        str = "SELECT count(*)
  FROM [CRA00].[dbo].[PERSON]"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        Return x.FillDataTable(str, con, "Result").Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function NIDToLoop() As DataTable
        Dim str As String
        str = "SELECT top 1000 i1.ID_NUMBER FROM [CRA00-As].[dbo].[InsuranceData] i1 inner join [CRA00-As].[dbo].[InsuranceData] i2
        On i1.ID_NUMBER=i2.id_number and i1.HaveData is null"

        'str = "SELECT distinct [ID_NUMBER]  FROM [DQ].[dbo].[TempItax]"
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00-As").ConnectionString
        Return x.FillDataTable(str, con, "Result")
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
    Public Function GetCRAUser(ByVal OfficeCode As Integer) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        str = "SELECT [NUMBER0] ,[FULL_NAME] FROM [CRA00].[dbo].[OPERATOR] where officecode = '" & OfficeCode & "'"
        Return x.FillDataTable(str, con, "Users")
    End Function
    <WebMethod()>
    Public Function DSSCapital() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        str = "SELECT * from [DSS_Capital]"
        Return x.FillDataTable(str, con, "Capital")
    End Function
    <WebMethod()>
    Public Function DSSTransactons() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        str = "SELECT * from [DSS_Transaction]"
        Return x.FillDataTable(str, con, "Capital")
    End Function

    <WebMethod()>
    Public Function CountBRN() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        str = "SELECT        COUNT(*) AS 'Count', OFFICE.NAME
FROM            COMPANY INNER JOIN
                         OFFICE ON COMPANY.OfficeCode = OFFICE.CODE
WHERE        (COMPANY.FK_CLASSCODE NOT IN (1, 3)) AND (COMPANY.FK_COMPANYCRA_NUM IS NULL)
GROUP BY OFFICE.NAME
order by 1 desc"
        Return x.FillDataTable(str, con, "BRN")
    End Function
    <WebMethod()>
    Public Function MainBranshes(ByVal Name0 As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("cra00").ConnectionString
        str = "SELECT o.NAME,c.REGISTRATION_NO,c.NUMBER0,c.DATE0,cn.name0,t.ADESC,CLASS.ADESC as 'classadesc'
  FROM [CRA00].[dbo].[COMPANY]c
  inner join COMPANY_NAME cn 
  on c.CRA_NUM=cn.FK_COMPANYCRA_NUM
  inner join OFFICE o on o.CODE=c.OfficeCode
  inner join TYPE t on c.FK_TYPECODE=t.CODE
  inner join ADDRESS a on a.FK_COMPANYCRA_NUM=c.CRA_NUM
  inner join CLASS on CLASS.CODE=c.FK_CLASSCODE
  
  where cn.name0 like '%" & Name0 & "%' and c.FK_CLASSCODE in (1,3)"
        Return x.FillDataTable(str, con, "BRN")
    End Function


    <WebMethod()>
    Public Function Damg(ByVal UserID As String) As String
        Dim str As String
        Dim str1 As String
        Dim x As New DbClass
        Dim dt As New DataTable
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "SELECT TOP 1 [id_number] FROM [DQ].[dbo].[PersonDuplicateIDNum] where (userid is null or userid='" & UserID & "') and done<>cnt or done>cnt"
        dt = x.FillDataTable(str, con, "BRN")
        str1 = "update PersonDuplicateIDNum set userid = '" & UserID & "' where id_number = '" & dt.Rows(0).Item(0) & "'  "
        x.ExecInsertUpdateDelete(str1, con)
        Return dt.Rows(0).Item(0)
    End Function



    <WebMethod()>
    Public Function DamgData(ByVal ID_Number As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "select cso,NAME0,BIRTH_DATE,sex,FK_POLICE_STATIFK,OLD_BIRTH_DATE,OLD_ID_NUMBER from person where id_number='" & ID_Number & "' "
        Return x.FillDataTable(str, con, "BRN")
    End Function


    <WebMethod()>
    Public Function NotDamg(ByVal Cso As String, ByVal ID_Number As String) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "INSERT INTO [dbo].[TempCSO] ([CSO]) VALUES ('" & Cso & "')"
        x.ExecInsertUpdateDelete(str, con)
        Dim str1 As String
        str1 = "  update [PersonDuplicateIDNum] set Done = done+1,[TimeStamp]='" & DateTime.Now.ToString() & "'  where id_number='" & ID_Number & "'"
        x.ExecInsertUpdateDelete(str1, con)
        Return 1
    End Function

    <WebMethod()>
    Public Function DamgDone(ByVal FromCso As String, ByVal ToCso As String, ByVal ID_Number As String, ByVal UserID As Integer, ByVal FromPage As Int32) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "EXECUTE  [dbo].[SP_MargePerson_Batch]  " & FromCso & " ," & ToCso & ",'" & UserID & "','" & FromPage & "'"

        x.ExecInsertUpdateDelete(str, con)
        Dim str1 As String
        str1 = "  update [PersonDuplicateIDNum] set Done = done+1 ,[TimeStamp]='" & DateTime.Now.ToString() & "' where id_number='" & ID_Number & "'"
        Dim con1 As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        x.ExecInsertUpdateDelete(str1, con1)
        Return 1
    End Function
    <WebMethod()>
    Public Function DamgCount() As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select count(*) from PersonDuplicateIDNum where cnt<>done"
        Return x.FillDataTable(str, con, "BRN")
    End Function
    <WebMethod()>
    Public Function DamgCountUserID(ByVal UserID As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select count(*) from PersonDuplicateIDNum where cnt=done and UserID='" & UserID & "'"
        Return x.FillDataTable(str, con, "BRN")
    End Function
    <WebMethod()>
    Public Function GetDamgcnt(ByVal ID_Number As String) As String
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select cnt from PersonDuplicateIDNum where id_number='" & ID_Number & "'"
        Return x.FillDataTable(str, con, "BRN").Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function GetDamgdone(ByVal ID_Number As String) As String
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select done from PersonDuplicateIDNum where id_number='" & ID_Number & "'"
        Return x.FillDataTable(str, con, "BRN").Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function DamgUpdateDone(ByVal Done As String, ByVal ID_Number As String) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "  update [PersonDuplicateIDNum] set Done = '" & Done & "' where id_number='" & ID_Number & "'"
        x.ExecInsertUpdateDelete(str, con)
        Return 1
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
    Public Function F_Statistic(ByVal OfficeID As Integer, ByVal StartYear As Integer, ByVal EndYear As Integer) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("ITDAFinance").ConnectionString
        str = "SELECT cat.Cat,[dbo].[CRServices].[Service],sum(d.cashno) as [Count]
  FROM [ITDAFinance].[dbo].[CRCashInDetails] d inner join [ITDAFinance].[dbo].[CRCashIn2]cin
  on d.[CRCashID]=cin.[ID]
  right join [dbo].[CRServices] on [dbo].[CRServices].id=d.[CRServiceID]
  inner join CRCat cat on cat.ID= [CRServices].CRCat
  where cin.[OfficeID]='" & OfficeID & "' and ( (cin.cyear = '" & StartYear & "'  and cin.cmonth between 7 and 12) 
  or (cin.cyear = '" & EndYear & "'  and cin.cmonth between 1 and 6) )
  group by [dbo].[CRServices].[Service], cat.Cat
  order by 1,2"
        Return x.FillDataTable(str, con, "Data")
    End Function

    <WebMethod()>
    Public Function DEReport(ByVal Date0 As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("MService").ConnectionString
        str = "select a.Adesc,isnull(a.cnt,0) allReq,isnull(b.cnt,0) complete, isnull(a.cnt,0)-isnull(b.cnt,0) NotComplete
,isnull(c.cnt,0) NotPaid,isnull(d.cnt,0) cancelled
  from
(SELECT c.Adesc,count(*) cnt,c.ID
  FROM [CraMService].[dbo].[Cra_MSRequest]r inner join code_RequestType c on c.ID=r.RequestType
  where r.Req_Source=1 --and cast( CreateDate as date)='" & Date0 & "' --and InProgress not in (201,230,231)
  group by c.Adesc,c.ID) a left join
  (SELECT c.Adesc,count(*) cnt ,c.id
  FROM [CraMService].[dbo].[Cra_MSRequest]r inner join code_RequestType c on c.ID=r.RequestType
  where r.Req_Source=1 and InProgress not in (201,230,231)
  group by c.Adesc,c.ID)b on a.ID=b.ID left join
   (SELECT c.Adesc,count(*) cnt ,c.id
  FROM [CraMService].[dbo].[Cra_MSRequest]r inner join code_RequestType c on c.ID=r.RequestType
  where r.Req_Source=1  and InProgress  in (201,230) and r.CancelReason is null
  group by c.Adesc,c.ID)c on a.ID=c.ID left join
   (SELECT c.Adesc,count(*) cnt ,c.id
  FROM [CraMService].[dbo].[Cra_MSRequest]r inner join code_RequestType c on c.ID=r.RequestType
  where r.Req_Source=1  and InProgress  in (230) and r.CancelReason is not null
  group by c.Adesc,c.ID)d on a.ID=d.ID
  order by a.ID"
        Return x.FillDataTable(str, con, "Data")
    End Function



    <WebMethod()>
    Public Function DEReportInterval(ByVal Date1 As String, ByVal Date2 As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("MService").ConnectionString
        str = "SELECT * FROM [dbo].[DEReport] ('" & Date1 & "','" & Date2 & "')"
        Return x.FillDataTable(str, con, "Data")
    End Function

End Class

