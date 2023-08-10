Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Syncfusion.XlsIO
Imports System
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class COCIntegration
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GetMaxCert(ByVal ChamberID As String) As String
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "SELECT isnull(max(cast(PrcCrtReq_IssuedSerial as int)),0) FROM [DQ].[dbo].[COCCertificate] where ChamberID =" & ChamberID & ""
        Dim dt As New DataTable
        dt = x.FillDataTable(str, con, "0")
        Return dt.Rows(0).Item(0)
    End Function
    <WebMethod()>
    Public Function GetInterval() As Integer
        Return 10000
    End Function


    <WebMethod()>
    Public Function InsertCert(ByVal dt As DataTable, ByVal ChamberID As String) As Boolean
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim strIns As String
        For i = 0 To dt.Rows.Count


            strIns = "INSERT INTO [dbo].[COCCertificate]
           ([PrcCrtReq_ID]
           ,[PrcCrtReq_Chm_ID]
           ,[PrcCrtReq_ReqDate]
           ,[PrcCrtReq_IssuedTo]
           ,[PrcCrtReq_ApplicantName]
           ,[PrcCrtReq_OfficialPosition]
           ,[PrcCrtReq_DeputationNumber]
           ,[PrcCrtReq_RealEstateOffice]
           ,[PrcCrtReq_IsDeputationPrivate]
           ,[PrcCrtReq_MerchantName]
           ,[PrcCrtReq_TradeName]
           ,[PrcCrtReq_MainBrnComRegNumber]
           ,[PrcCrtReq_MainBrnComRegOffice]
           ,[PrcCrtReq_MainBrnComRegGov_ID]
           ,[PrcCrtReq_CmpTyp_ID]
           ,[PrcCrtReq_Leg_ID]
           ,[PrcCrtReq_StatedCapital]
           ,[PrcCrtReq_IssuedCapital]
           ,[PrcCrtReq_PaidCapital]
           ,[PrcCrtReq_TextPaidCapital]
           ,[PrcCrtReq_CmpDuration]
           ,[PrcCrtReq_CmpStartingFrom]
           ,[PrcCrtReq_TaxIDNumber]
           ,[PrcCrtReq_TaxFileNumber]
           ,[PrcCrtReq_TaxOfficeCode]
           ,[PrcCrtReq_StartingDate]
           ,[PrcCrtReq_SalesTaxRegNumber]
           ,[PrcCrtReq_GovSec_ID]
           ,[PrcCrtReq_BuildingNumber]
           ,[PrcCrtReq_AppartmentNumber]
           ,[PrcCrtReq_FloorNumber]
           ,[PrcCrtReq_Street]
           ,[PrcCrtReq_ZipCode]
           ,[PrcCrtReq_AddressExtraDesc]
           ,[PrcCrtReq_Address]
           ,[PrcCrtReq_BuildingOwner]
           ,[PrcCrtReq_BrnTyp]
           ,[PrcCrtReq_OwnershipType]
           ,[PrcCrtReq_EndOfRentContract]
           ,[PrcCrtReq_PlaceOccupationDate]
           ,[PrcCrtReq_PlaceRegistrationNumber]
           ,[PrcCrtReq_PlaceRealEstateOffice]
           ,[PrcCrtReq_NationalNumber]
           ,[PrcCrtReq_IsIssued]
           ,[PrcCrtReq_IssuedDate]
           ,[PrcCrtReq_IssuedSerial]
           ,[PrcCrtReq_RejectReason]
           ,[PrcCrtReq_Serial]
           ,[PrcCrtReq_TrnNumber]
           ,[PrcCrtReq_IsPaidFor]
           ,[PrcCrtReq_ReceiptNumber]
           ,[PrcCrtReq_ComRegNumber]
           ,[PrcCrtReq_ComRegDate]
           ,[PrcCrtReq_RegCheckingDate]
           ,[PrcCrtReq_RegCheckingStatus]
           ,[PrcCrtReq_RegCheckingNumber]
           ,[PrcCrtReq_SubmittedUser]
           ,[PrcCrtReq_IssuedUser]
           ,[PrcCrtReq_CheckingUser]
           ,[PrcCrtReq_IsDelivered]
           ,[PrcCrtReq_OtherTaxActivity]
           ,[PrcCrtReq_Capital_OriginCurrency]
           ,[PrcCrtReq_Capital_OriginValue]
           ,[PrcCrtReq_Capital_ExgRate]
           ,[PrcCrtReq_Capital_ExgDate]
           ,[PrcCrtReq_SharesNo]
           ,[PrcCrtReq_ShareValue]
           ,[PrcCrtReq_PaidShareValue]
           ,[PrcCrtReq_InKindShareValue]
           ,[PrcCrtReq_IsSpecialService]
           ,[PrcCrqReq_HisCenterAddress]
           ,[PrcCrqReq_HisBranchAddress]
           ,[PrcCrqReq_HisActivities]
           ,[PrcCrqReq_HisStatedCapital]
           ,[PrcCrqReq_HisPaidCapital]
           ,[PrcCrqReq_HisIssuedCapital]
           ,[PrcCrtReq_Name]
           ,[PrcCrtReq_QualCourtName]
           ,[PrcCrtReq_QualCourtNum]
           ,[PrcCrtReq_QualCourtDate]
           ,[PrcCrtReq_DeviseeShareValue]
           ,[PrcCrtReq_ForeignersShareValue]
           ,[PrcCrtReq_Sec_ID]
           ,[PrcCrtReq_IsReEntered]
           ,[PrcCrtReq_IsFromExistingReg]
           ,[PrcCrtReq_ExistingGnReg_ID]
           ,[PrcCrtReq_ReasonForFeesCalculation]
           ,[GovSec_ID]
           ,[GovSec_Chm_ID]
           ,[GovSec_Name]
           ,[GovSec_MainSecID]
           ,[GovSec_IsInCity]
           ,[CmpTyp_ID]
           ,[CmpTyp_MainCmpTyp_ID]
           ,[CmpTyp_Name]
           ,[CmpTyp_Code]
           ,[PrcCrtReqMng_Chm_ID]
           ,[PrcCrtReqMng_ID]
           ,[PrcCrtReqMng_PrcCrtReq_ID]
           ,[PrcCrtReqMng_Management]
           ,[PrcCrtReqMng_IsSingleSignee]
           ,[PrcCrtReqMng_CombinedWith]
           ,[PrcCrtReqMng_Gender]
           ,[PrcCrtReqMng_IDType]
           ,[PrcCrtReqMng_IDNumber]
           ,[PrcCrtReqMng_IDIssuePlace]
           ,[PrcCrtReqMng_BirthDate]
           ,[PrcCrtReqMng_BirthPlace]
           ,[PrcCrtReqMng_Email]
           ,[PrcCrtReqMng_Tel]
           ,[PrcCrtReqMng_Fax]
           ,[PrcCrtReqMng_URL]
           ,[PrcCrtReqMng_Cntry_ID]
           ,[PrcCrtReqMng_Title]
           ,[PrcCrtReqMng_Roles]
           ,[ChamberID])
     VALUES
           ('" & dt.Rows(i).Item(0) & "',
           '" & dt.Rows(i).Item(1) & "',
           '" & dt.Rows(i).Item(2) & "',
           '" & dt.Rows(i).Item(3) & "',
           '" & dt.Rows(i).Item(4) & "',
           '" & dt.Rows(i).Item(5) & "',
           '" & dt.Rows(i).Item(6) & "',
           '" & dt.Rows(i).Item(7) & "',
           '" & dt.Rows(i).Item(8) & "',
           '" & dt.Rows(i).Item(9) & "',
           '" & dt.Rows(i).Item(10) & "',
           '" & dt.Rows(i).Item(11) & "',
           '" & dt.Rows(i).Item(12) & "',
           '" & dt.Rows(i).Item(13) & "',
           '" & dt.Rows(i).Item(14) & "',
           '" & dt.Rows(i).Item(15) & "',
           '" & dt.Rows(i).Item(16) & "',
           '" & dt.Rows(i).Item(17) & "',
           '" & dt.Rows(i).Item(18) & "',
           '" & dt.Rows(i).Item(19) & "',
           '" & dt.Rows(i).Item(20) & "',
           '" & dt.Rows(i).Item(21) & "',
           '" & dt.Rows(i).Item(22) & "',
           '" & dt.Rows(i).Item(23) & "',
           '" & dt.Rows(i).Item(24) & "',
           '" & dt.Rows(i).Item(25) & "',
           '" & dt.Rows(i).Item(26) & "',
           '" & dt.Rows(i).Item(27) & "',
           '" & dt.Rows(i).Item(28) & "',
           '" & dt.Rows(i).Item(29) & "',
           '" & dt.Rows(i).Item(30) & "',
           '" & dt.Rows(i).Item(31) & "',
           '" & dt.Rows(i).Item(32) & "',
           '" & dt.Rows(i).Item(33) & "',
           '" & dt.Rows(i).Item(34) & "',
           '" & dt.Rows(i).Item(35) & "',
           '" & dt.Rows(i).Item(36) & "',
           '" & dt.Rows(i).Item(37) & "',
           '" & dt.Rows(i).Item(38) & "',
           '" & dt.Rows(i).Item(39) & "',
           '" & dt.Rows(i).Item(40) & "',
           '" & dt.Rows(i).Item(41) & "',
           '" & dt.Rows(i).Item(42) & "',
           '" & dt.Rows(i).Item(43) & "',
           '" & dt.Rows(i).Item(44) & "',
           '" & dt.Rows(i).Item(45) & "',
           '" & dt.Rows(i).Item(46) & "',
           '" & dt.Rows(i).Item(47) & "',
           '" & dt.Rows(i).Item(48) & "',
           '" & dt.Rows(i).Item(49) & "',
           '" & dt.Rows(i).Item(50) & "',
           '" & dt.Rows(i).Item(51) & "',
           '" & dt.Rows(i).Item(52) & "',
           '" & dt.Rows(i).Item(53) & "',
           '" & dt.Rows(i).Item(54) & "',
           '" & dt.Rows(i).Item(55) & "',
           '" & dt.Rows(i).Item(56) & "',
           '" & dt.Rows(i).Item(57) & "',
           '" & dt.Rows(i).Item(58) & "',
           '" & dt.Rows(i).Item(59) & "',
           '" & dt.Rows(i).Item(60) & "',
           '" & dt.Rows(i).Item(61) & "',
           '" & dt.Rows(i).Item(62) & "',
           '" & dt.Rows(i).Item(63) & "',
           '" & dt.Rows(i).Item(64) & "',
           '" & dt.Rows(i).Item(65) & "',
           '" & dt.Rows(i).Item(66) & "',
           '" & dt.Rows(i).Item(67) & "',
           '" & dt.Rows(i).Item(68) & "',
           '" & dt.Rows(i).Item(69) & "',
           '" & dt.Rows(i).Item(70) & "',
           '" & dt.Rows(i).Item(71) & "',
           '" & dt.Rows(i).Item(72) & "',
           '" & dt.Rows(i).Item(73) & "',
           '" & dt.Rows(i).Item(74) & "',
           '" & dt.Rows(i).Item(75) & "',
           '" & dt.Rows(i).Item(76) & "',
           '" & dt.Rows(i).Item(77) & "',
           '" & dt.Rows(i).Item(78) & "',
           '" & dt.Rows(i).Item(79) & "',
           '" & dt.Rows(i).Item(80) & "',
           '" & dt.Rows(i).Item(81) & "',
           '" & dt.Rows(i).Item(82) & "',
           '" & dt.Rows(i).Item(83) & "',
           '" & dt.Rows(i).Item(84) & "',
           '" & dt.Rows(i).Item(85) & "',
           '" & dt.Rows(i).Item(86) & "',
           '" & dt.Rows(i).Item(87) & "',
           '" & dt.Rows(i).Item(88) & "',
           '" & dt.Rows(i).Item(89) & "',
           '" & dt.Rows(i).Item(90) & "',
           '" & dt.Rows(i).Item(91) & "',
           '" & dt.Rows(i).Item(92) & "',
           '" & dt.Rows(i).Item(93) & "',
           '" & dt.Rows(i).Item(94) & "',
           '" & dt.Rows(i).Item(95) & "',
           '" & dt.Rows(i).Item(96) & "',
           '" & dt.Rows(i).Item(97) & "',
           '" & dt.Rows(i).Item(98) & "',
           '" & dt.Rows(i).Item(99) & "',
           '" & dt.Rows(i).Item(100) & "',
           '" & dt.Rows(i).Item(101) & "',
           '" & dt.Rows(i).Item(102) & "',
           '" & dt.Rows(i).Item(103) & "',
           '" & dt.Rows(i).Item(104) & "',
           '" & dt.Rows(i).Item(105) & "',
           '" & dt.Rows(i).Item(106) & "',
           '" & dt.Rows(i).Item(107) & "',
           '" & dt.Rows(i).Item(108) & "',
           '" & dt.Rows(i).Item(109) & "',
           '" & dt.Rows(i).Item(110) & "',
           '" & dt.Rows(i).Item(111) & "',
           '" & dt.Rows(i).Item(112) & "',
           '" & dt.Rows(i).Item(113) & "',
           '" & dt.Rows(i).Item(114) & "',
           '" & ChamberID & "')"
            x.ExecInsertUpdateDelete(strIns, con)
        Next
        Return True



    End Function
    <WebMethod()>
    Public Function InsertCertSUZE(ByVal dt As DataTable, ByVal ChamberID As String) As Boolean
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim strIns As String
        ExportExcel(dt)
        For i = 0 To dt.Rows.Count
            Try
                strIns = "INSERT INTO [dbo].COCCertificate
           ( PrcCrtReq_IssuedSerial,
PrcCrtReq_IssuedDate,
PrcCrtReq_IssuedTo ,
PrcCrtReq_MerchantName,
CmpTyp_Name,
PrcCrqReq_HisActivities,
PrcCrqReq_HisPaidCapital,
PrcCrtReq_TaxFileNumber,
PrcCrtReq_CmpStartingFrom ,
PrcCrtReq_BuildingNumber,
PrcCrtReq_AppartmentNumber,
PrcCrtReq_FloorNumber,
PrcCrtReq_Street,
PrcCrtReq_AddressExtraDesc,
PrcCrtReq_PlaceRealEstateOffice,
chamberid
)
     VALUES
           ('" & dt.Rows(i).Item(0) & "',
           '" & dt.Rows(i).Item(1) & "',
           '" & dt.Rows(i).Item(2) & "',
           '" & dt.Rows(i).Item(3) & "',
           '" & dt.Rows(i).Item(4) & "',
           '" & dt.Rows(i).Item(5) & "',
           '" & dt.Rows(i).Item(6) & "',
           '" & dt.Rows(i).Item(7) & "',
           '" & dt.Rows(i).Item(8) & "',
           '" & dt.Rows(i).Item(9) & "',
           '" & dt.Rows(i).Item(10) & "',
           '" & dt.Rows(i).Item(11) & "',
           '" & dt.Rows(i).Item(12) & "',
           '" & dt.Rows(i).Item(13) & "',
           '" & dt.Rows(i).Item(14) & "',
          
            '26')"
                x.ExecInsertUpdateDelete(strIns, con)

            Catch ex As Exception
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter("D:\test.txt", True)
                file.WriteLine(ex.Message)
                file.Close()
            End Try


        Next



        Return True



    End Function
    Public Shared Sub ExportExcel(ByVal dt As DataTable)
        Using excelEngine As ExcelEngine = New ExcelEngine()

            'Initialize Application
            Dim application As IApplication = excelEngine.Excel

            'Set the default application version as Excel 2016
            application.DefaultVersion = ExcelVersion.Excel2016

            'Create a new workbook
            Dim workbook As IWorkbook = application.Workbooks.Create(1)

            'Access first worksheet from the workbook instance
            Dim worksheet As IWorksheet = workbook.Worksheets(0)

            'Exporting DataTable to worksheet

            worksheet.ImportDataTable(dt, True, 1, 1)
            worksheet.UsedRange.AutofitColumns()

            'Save the workbook to disk in xlsx format
            workbook.SaveAs("d:\Output.xlsx")

        End Using
    End Sub


    <WebMethod>
    <Obsolete>
    Public Function GetStr(ByVal ChamberID As Integer) As String
        Dim Order As String
        Dim maxCert As Integer
        Dim response As String
        Dim count As Integer
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim x As New DbClass
        Dim str As String = "SELECT [SelectStr],[MaxCert],[CountCert],[Orderby] FROM [DQ].[dbo].[ChamberRequest] where ChamberID = " & ChamberID & ""
        Dim dt As New DataTable
        dt = x.FillDataTable(str, con, "0")
        maxCert = dt.Rows(0).Item(1)
        count = dt.Rows(0).Item(2)
        Order = dt.Rows(0).Item(3)
        count = maxCert + count
        response = dt.Rows(0).Item(0) + " " + Convert.ToString(maxCert) + " and " + Convert.ToString(count) + " " + Order
        Return x.Encrypt1(response)
    End Function

    <WebMethod>
    <Obsolete>
    Public Function GetConnection(ByVal ChamberID As Integer) As String
        Dim Server As String
        Dim Database As String
        Dim UserID As String
        Dim Password As String
        Dim response As String
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim x As New DbClass
        Dim str As String = "SELECT [serverIP],[Database],[SQLUserID],[SQLPass] FROM [DQ].[dbo].[ChamberRequest] where ChamberID = " & ChamberID & ""
        Dim dt As New DataTable
        dt = x.FillDataTable(str, con, "0")
        Server = dt.Rows(0).Item(0)
        Database = dt.Rows(0).Item(1)
        UserID = dt.Rows(0).Item(2)
        Password = dt.Rows(0).Item(3)
        response = "Server=" + Server + ";Database=" + Database + ";Uid=" + UserID + ";PWD=" + Password
        Return x.Encrypt1(response)
    End Function


End Class
