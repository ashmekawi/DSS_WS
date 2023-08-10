Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class PersonUpdate
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetPersons(ByVal Titel As String, ByVal NID As String) As DataTable
        If (Titel <> "") Then
            Dim str As String
            Dim x As New DbClass
            Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
            str = "SELECT cso,name0,ID_NUMBER from Person where [SilentPartner] = 0 and  name0 like '%" & Titel & "%' order by CSO"
            Return x.FillDataTable(str, con, "CRCat")
        End If
        If (NID <> "") Then
            Dim str As String
            Dim x As New DbClass
            Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
            str = "SELECT cso,NAME0,BIRTH_DATE,FK_POLICE_STATIFK,OLD_BIRTH_DATE,Old_ID_NUMBER,ID_NUMBER from PERSON 
                   where ID_NUMBER ='" & NID & "' order by name0"
            Return x.FillDataTable(str, con, "Persons")
        End If
        Return Nothing
    End Function
    <WebMethod()>
    Public Function GetFullPerson(ByVal NID As String) As DataTable
        If (NID <> "") Then
            Dim str As String
            Dim x As New DbClass
            Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
            str = "SELECT * from PERSON where ID_NUMBER ='" & NID & "' order by name0"
            Return x.FillDataTable(str, con, "Persons")
        End If
        Return Nothing
    End Function

    <WebMethod()>
    Public Function CheckMerge(ByVal CSO As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "select * from PERSON_Marged where ToFK_PERSONCSO='" & CSO & "'"
        Return x.FillDataTable(str, con, "Merge")
    End Function



    <WebMethod()>
    Public Function Save(ByVal CSO As Integer, ByVal Name0 As String, ByVal IDNumber As String, ByVal Title As String, ByVal UID As String) As String
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        Dim UpdatePersonSTR As String
        UpdatePersonSTR = "exec dbo.sp_append2anylog 'person','cso=" & CSO & "','" & UID & "'"
        Dim save0 As New DbClass
        Dim n As Integer
        Dim nn As Boolean
        Dim str As String
        n = save0.ExecSpAndReValue(UpdatePersonSTR, con)
        If n = 1 Then
            UpdatePersonSTR = "exec dbo.sp_append2anylog 'company_person','fk_personcso=" & CSO & "','" & UID & "'"
            n = save0.ExecSpAndReValue(UpdatePersonSTR, con)
            If n = 1 Then
                str = "update company_person set title='" & Title & "' where fk_personcso = '" & CSO & "' and title is null "
                nn = save0.ExecInsertUpdateDelete(str, con)
                If nn = True Then
                    str = "update person set name0='" & Name0 & "',ID_Number='" & IDNumber & "',zname=Null,cname=Null,pernameid=Null,cstatus=Null,zlen=Null where cso = '" & CSO & "'"
                    nn = save0.ExecInsertUpdateDelete(str, con)
                    If nn = True Then
                        Return "تم الحفظ"
                    Else
                        Return "لم يتم الحفظ فى اشخاص الشركات"
                    End If
                Else
                    Return "لم يتم الحفظ فى الاشخاص"
                End If

            Else
                Return " لم يتم الحفظ لوجود مشكلة فى جدول اشخاص الشركات"
            End If
        Else
            Return "لم يتم الحفظ لوجود مشكلة فى جدول الاشخاص"

        End If











        ' Return "تم الحفظ"
    End Function

    <WebMethod()>
    Public Function SilentPartner(ByVal CSO As Integer, ByVal UID As String) As String
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        Dim Log As String
        Log = "exec dbo.sp_append2anylog 'person','cso=" & CSO & "','" & UID & "'"
        Dim save0 As New DbClass
        Dim n As Integer
        'Dim nn As Boolean
        'Dim str As String
        n = save0.ExecSpAndReValue(Log, con)
        If n = 1 Then
            Dim insert As String
            insert = "Update [CRA00].[dbo].[PERSON] set [SilentPartner] =1 where person.CSO = '" & CSO & "' "
            '"insert into [dbo].[SilentPartner]SELECT isnull(
            '     company_person.FKCompany_TransactionID,0),COMPANY_PERSON.FK_COMPANYCRA_NUM
            '     ,[NAME0],person.OfficeCode,'" & UID & "',GETDATE() 
            '     FROM [CRA00].[dbo].[PERSON] 
            '     left join COMPANY_PERSON on person.CSO=company_person.FK_PERSONCSO
            '     where  person.CSO = '" & CSO & "' "
            n = save0.ExecSpAndReValue(insert, con)
            ' If n = 1 Then
            '   str = "delete from person where cso= '" & CSO & "' "
            '  nn = save0.ExecInsertUpdateDelete(str, con)
            'If nn = True Then
            '    Return "تم حذف الشخص واضافته الى الاشخاص الاعتبارية وعمل LOG"
            'Else
            '    Return "لم يتم الحذف"

            'End If

            ' Else
            '    Return " لم يتم الحفظ فى الاشخاص الاعتبارية"
            'End If
            If n = 1 Then
                Return "تم الحفظ بنجاح"
            Else
                Return "لم يتم الحفظ "
            End If
        Else
            Return "لم يتم حفظ LOG"
        End If
    End Function
    <WebMethod()>
    Public Function InsuranceCheck(ByVal UserID As Int32) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim dt As New DataTable
        str = "  select top (1) *  from [insuranceCheck] where  [Correct] = 0 and (userid is Null or userid = '" & UserID & "')  and (isnull(userid1,0)<> '" & UserID & "') and [ins_Cname] is not null and ins_Cname <>'لايوجد'  "
        dt = x.FillDataTable(str, con, "InsuranceCheck")
        Dim str1 As String
        str1 = "update insuranceCheck set userid = '" & UserID & "' where cso = '" & dt.Rows(0).Item(0) & "'  "
        Dim y As Int32 = x.ExecInsertUpdateDelete(str1, con)
        Return dt
    End Function
    <WebMethod()>
    Public Function InsuranceCheckUpdate(ByVal cso As Int32, ByVal Correct As Int32, ByVal UserID As Int32) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "  update [insuranceCheck] set [Correct] = '" & Correct & "', UserID='" & UserID & "'  where  [cso] = '" & cso & "' "
        Return x.FillDataTable(str, con, "InsuranceCheck")
    End Function


    <WebMethod()>
    Public Function InsuranceCheckCount() As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "  select count(*)  from [insuranceCheck] where  [Correct] = 0 "
        Return x.FillDataTable(str, con, "InsuranceCheck").Rows(0).Item(0)
    End Function


    <WebMethod()>
    Public Function InsuranceCheckCountUserID(ByVal UserID As Int32) As Int32
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "  select count(*)  from [insuranceCheck] where  userid = '" & UserID & "' and correct <>0 "
        Return x.FillDataTable(str, con, "InsuranceCheck").Rows(0).Item(0)
    End Function


    <WebMethod()>
    Public Function PersonData(ByVal ID_Number As String, ByVal Cso As Integer) As DataSet
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("CRA00").ConnectionString
        str = "  EXEC [dbo].[PersonData] @ID_number = N'" & ID_Number & "',@cso=" & Cso & ""
        Return x.FillDataSet(str, con, "PersonData")
    End Function



End Class