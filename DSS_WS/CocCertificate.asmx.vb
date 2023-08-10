Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class CocCertificate
    Inherits System.Web.Services.WebService
    <WebMethod()>
    Public Function GetCert(ByVal CertID As String, ByVal ChamberID As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        Dim concoc As String = ConfigurationManager.ConnectionStrings("coc").ConnectionString
        str = " select top(1) * from COCCertificate where PrcCrtReq_IssuedSerial = '" & CertID & "' and [ChamberID] = '" & ChamberID & "'"
        Dim dt As New DataTable
        dt = x.FillDataTable(str, con, "0")
        If dt.Rows.Count > 0 Then
            Return dt
        Else
            If (ChamberID = 19) Then




            End If




            Dim str1 As String
            Dim x1 As New DbClass
            Dim con1 As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
            str1 = " SELECT dbo.PracticeCertificateRequests.PrcCrtReq_ID, dbo.PracticeCertificateRequests.PrcCrtReq_Chm_ID, dbo.PracticeCertificateRequests.PrcCrtReq_ReqDate, dbo.PracticeCertificateRequests.PrcCrtReq_IssuedTo, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_ApplicantName, dbo.PracticeCertificateRequests.PrcCrtReq_OfficialPosition, dbo.PracticeCertificateRequests.PrcCrtReq_DeputationNumber, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_RealEstateOffice, dbo.PracticeCertificateRequests.PrcCrtReq_IsDeputationPrivate, dbo.PracticeCertificateRequests.PrcCrtReq_MerchantName, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_TradeName, dbo.PracticeCertificateRequests.PrcCrtReq_MainBrnComRegNumber, dbo.PracticeCertificateRequests.PrcCrtReq_MainBrnComRegOffice, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_MainBrnComRegGov_ID, dbo.PracticeCertificateRequests.PrcCrtReq_CmpTyp_ID, dbo.PracticeCertificateRequests.PrcCrtReq_Leg_ID, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_StatedCapital, dbo.PracticeCertificateRequests.PrcCrtReq_IssuedCapital, dbo.PracticeCertificateRequests.PrcCrtReq_PaidCapital, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_TextPaidCapital, dbo.PracticeCertificateRequests.PrcCrtReq_CmpDuration, dbo.PracticeCertificateRequests.PrcCrtReq_CmpStartingFrom, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_TaxIDNumber, dbo.PracticeCertificateRequests.PrcCrtReq_TaxFileNumber, dbo.PracticeCertificateRequests.PrcCrtReq_TaxOfficeCode, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_StartingDate, dbo.PracticeCertificateRequests.PrcCrtReq_SalesTaxRegNumber, dbo.PracticeCertificateRequests.PrcCrtReq_GovSec_ID, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_BuildingNumber, dbo.PracticeCertificateRequests.PrcCrtReq_AppartmentNumber, dbo.PracticeCertificateRequests.PrcCrtReq_FloorNumber, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_Street, dbo.PracticeCertificateRequests.PrcCrtReq_ZipCode, dbo.PracticeCertificateRequests.PrcCrtReq_AddressExtraDesc, dbo.PracticeCertificateRequests.PrcCrtReq_Address, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_BuildingOwner, dbo.PracticeCertificateRequests.PrcCrtReq_BrnTyp, dbo.PracticeCertificateRequests.PrcCrtReq_OwnershipType, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_EndOfRentContract, dbo.PracticeCertificateRequests.PrcCrtReq_PlaceOccupationDate, dbo.PracticeCertificateRequests.PrcCrtReq_PlaceRegistrationNumber, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_PlaceRealEstateOffice, dbo.PracticeCertificateRequests.PrcCrtReq_NationalNumber, dbo.PracticeCertificateRequests.PrcCrtReq_IsIssued, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_IssuedDate, dbo.PracticeCertificateRequests.PrcCrtReq_IssuedSerial, dbo.PracticeCertificateRequests.PrcCrtReq_RejectReason, dbo.PracticeCertificateRequests.PrcCrtReq_Serial, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_TrnNumber, dbo.PracticeCertificateRequests.PrcCrtReq_IsPaidFor, dbo.PracticeCertificateRequests.PrcCrtReq_ReceiptNumber, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_ComRegNumber, dbo.PracticeCertificateRequests.PrcCrtReq_ComRegDate, dbo.PracticeCertificateRequests.PrcCrtReq_RegCheckingDate, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_RegCheckingStatus, dbo.PracticeCertificateRequests.PrcCrtReq_RegCheckingNumber, dbo.PracticeCertificateRequests.PrcCrtReq_SubmittedUser, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_IssuedUser, dbo.PracticeCertificateRequests.PrcCrtReq_CheckingUser, dbo.PracticeCertificateRequests.PrcCrtReq_IsDelivered, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_OtherTaxActivity, dbo.PracticeCertificateRequests.PrcCrtReq_Capital_OriginCurrency, dbo.PracticeCertificateRequests.PrcCrtReq_Capital_OriginValue, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_Capital_ExgRate, dbo.PracticeCertificateRequests.PrcCrtReq_Capital_ExgDate, dbo.PracticeCertificateRequests.PrcCrtReq_SharesNo, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_ShareValue, dbo.PracticeCertificateRequests.PrcCrtReq_PaidShareValue, dbo.PracticeCertificateRequests.PrcCrtReq_InKindShareValue, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_IsSpecialService, dbo.PracticeCertificateRequests.PrcCrqReq_HisCenterAddress, dbo.PracticeCertificateRequests.PrcCrqReq_HisBranchAddress, 
                  dbo.PracticeCertificateRequests.PrcCrqReq_HisActivities, dbo.PracticeCertificateRequests.PrcCrqReq_HisStatedCapital, dbo.PracticeCertificateRequests.PrcCrqReq_HisPaidCapital, 
                  dbo.PracticeCertificateRequests.PrcCrqReq_HisIssuedCapital, dbo.PracticeCertificateRequests.PrcCrtReq_Name, dbo.PracticeCertificateRequests.PrcCrtReq_QualCourtName, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_QualCourtNum, dbo.PracticeCertificateRequests.PrcCrtReq_QualCourtDate, dbo.PracticeCertificateRequests.PrcCrtReq_DeviseeShareValue, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_ForeignersShareValue, dbo.PracticeCertificateRequests.PrcCrtReq_Sec_ID, dbo.PracticeCertificateRequests.PrcCrtReq_IsReEntered, 
                  dbo.PracticeCertificateRequests.PrcCrtReq_IsFromExistingReg, dbo.PracticeCertificateRequests.PrcCrtReq_ExistingGnReg_ID, dbo.PracticeCertificateRequests.PrcCrtReq_ReasonForFeesCalculation, 
                  dbo.GovernmentalSections.GovSec_ID, dbo.GovernmentalSections.GovSec_Chm_ID, dbo.GovernmentalSections.GovSec_Name, dbo.GovernmentalSections.GovSec_MainSecID, dbo.GovernmentalSections.GovSec_IsInCity, 
                  dbo.CompanyTypes.CmpTyp_ID, dbo.CompanyTypes.CmpTyp_MainCmpTyp_ID, dbo.CompanyTypes.CmpTyp_Name, dbo.CompanyTypes.CmpTyp_Code, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_Chm_ID, 
                  dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_ID, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_PrcCrtReq_ID, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_Management, 
                  dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_IsSingleSignee, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_CombinedWith, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_Gender, 
                  dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_IDType, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_IDNumber, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_IDIssuePlace, 
                  dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_BirthDate, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_BirthPlace, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_Email, 
                  dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_Tel, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_Fax, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_URL, 
                  dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_Cntry_ID, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_Title, dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_Roles
                  FROM dbo.GovernmentalSections INNER JOIN
                  dbo.PracticeCertificateRequests INNER JOIN
                  dbo.CompanyTypes ON dbo.PracticeCertificateRequests.PrcCrtReq_CmpTyp_ID = dbo.CompanyTypes.CmpTyp_ID ON dbo.GovernmentalSections.GovSec_ID = dbo.PracticeCertificateRequests.PrcCrtReq_GovSec_ID INNER JOIN
                  dbo.PracticeCertificateRequestsManagements ON dbo.PracticeCertificateRequests.PrcCrtReq_ID = dbo.PracticeCertificateRequestsManagements.PrcCrtReqMng_PrcCrtReq_ID
                  WHERE  (dbo.PracticeCertificateRequests.PrcCrtReq_IssuedSerial = '" & CertID & "')"
            Dim dt1 As New DataTable
            dt1 = x1.FillDataTable(str1, concoc, "0")
            Dim strIns As String
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
           ,[PrcCrtReqMng_Roles])
     VALUES
           ('" & dt1.Rows(0).Item(0) & "',
           '" & dt1.Rows(0).Item(1) & "',
           '" & dt1.Rows(0).Item(2) & "',
           '" & dt1.Rows(0).Item(3) & "',
           '" & dt1.Rows(0).Item(4) & "',
           '" & dt1.Rows(0).Item(5) & "',
           '" & dt1.Rows(0).Item(6) & "',
           '" & dt1.Rows(0).Item(7) & "',
           '" & dt1.Rows(0).Item(8) & "',
           '" & dt1.Rows(0).Item(9) & "',
           '" & dt1.Rows(0).Item(10) & "',
           '" & dt1.Rows(0).Item(11) & "',
           '" & dt1.Rows(0).Item(12) & "',
           '" & dt1.Rows(0).Item(13) & "',
           '" & dt1.Rows(0).Item(14) & "',
           '" & dt1.Rows(0).Item(15) & "',
           '" & dt1.Rows(0).Item(16) & "',
           '" & dt1.Rows(0).Item(17) & "',
           '" & dt1.Rows(0).Item(18) & "',
           '" & dt1.Rows(0).Item(19) & "',
           '" & dt1.Rows(0).Item(20) & "',
           '" & dt1.Rows(0).Item(21) & "',
           '" & dt1.Rows(0).Item(22) & "',
           '" & dt1.Rows(0).Item(23) & "',
           '" & dt1.Rows(0).Item(24) & "',
           '" & dt1.Rows(0).Item(25) & "',
           '" & dt1.Rows(0).Item(26) & "',
           '" & dt1.Rows(0).Item(27) & "',
           '" & dt1.Rows(0).Item(28) & "',
           '" & dt1.Rows(0).Item(29) & "',
           '" & dt1.Rows(0).Item(30) & "',
           '" & dt1.Rows(0).Item(31) & "',
           '" & dt1.Rows(0).Item(32) & "',
           '" & dt1.Rows(0).Item(33) & "',
           '" & dt1.Rows(0).Item(34) & "',
           '" & dt1.Rows(0).Item(35) & "',
           '" & dt1.Rows(0).Item(36) & "',
           '" & dt1.Rows(0).Item(37) & "',
           '" & dt1.Rows(0).Item(38) & "',
           '" & dt1.Rows(0).Item(39) & "',
           '" & dt1.Rows(0).Item(40) & "',
           '" & dt1.Rows(0).Item(41) & "',
           '" & dt1.Rows(0).Item(42) & "',
           '" & dt1.Rows(0).Item(43) & "',
           '" & dt1.Rows(0).Item(44) & "',
           '" & dt1.Rows(0).Item(45) & "',
           '" & dt1.Rows(0).Item(46) & "',
           '" & dt1.Rows(0).Item(47) & "',
           '" & dt1.Rows(0).Item(48) & "',
           '" & dt1.Rows(0).Item(49) & "',
           '" & dt1.Rows(0).Item(50) & "',
           '" & dt1.Rows(0).Item(51) & "',
           '" & dt1.Rows(0).Item(52) & "',
           '" & dt1.Rows(0).Item(53) & "',
           '" & dt1.Rows(0).Item(54) & "',
           '" & dt1.Rows(0).Item(55) & "',
           '" & dt1.Rows(0).Item(56) & "',
           '" & dt1.Rows(0).Item(57) & "',
           '" & dt1.Rows(0).Item(58) & "',
           '" & dt1.Rows(0).Item(59) & "',
           '" & dt1.Rows(0).Item(60) & "',
           '" & dt1.Rows(0).Item(61) & "',
           '" & dt1.Rows(0).Item(62) & "',
           '" & dt1.Rows(0).Item(63) & "',
           '" & dt1.Rows(0).Item(64) & "',
           '" & dt1.Rows(0).Item(65) & "',
           '" & dt1.Rows(0).Item(66) & "',
           '" & dt1.Rows(0).Item(67) & "',
           '" & dt1.Rows(0).Item(68) & "',
           '" & dt1.Rows(0).Item(69) & "',
           '" & dt1.Rows(0).Item(70) & "',
           '" & dt1.Rows(0).Item(71) & "',
           '" & dt1.Rows(0).Item(72) & "',
           '" & dt1.Rows(0).Item(73) & "',
           '" & dt1.Rows(0).Item(74) & "',
           '" & dt1.Rows(0).Item(75) & "',
           '" & dt1.Rows(0).Item(76) & "',
           '" & dt1.Rows(0).Item(77) & "',
           '" & dt1.Rows(0).Item(78) & "',
           '" & dt1.Rows(0).Item(79) & "',
           '" & dt1.Rows(0).Item(80) & "',
           '" & dt1.Rows(0).Item(81) & "',
           '" & dt1.Rows(0).Item(82) & "',
           '" & dt1.Rows(0).Item(83) & "',
           '" & dt1.Rows(0).Item(84) & "',
           '" & dt1.Rows(0).Item(85) & "',
           '" & dt1.Rows(0).Item(86) & "',
           '" & dt1.Rows(0).Item(87) & "',
           '" & dt1.Rows(0).Item(88) & "',
           '" & dt1.Rows(0).Item(89) & "',
           '" & dt1.Rows(0).Item(90) & "',
           '" & dt1.Rows(0).Item(91) & "',
           '" & dt1.Rows(0).Item(92) & "',
           '" & dt1.Rows(0).Item(93) & "',
           '" & dt1.Rows(0).Item(94) & "',
           '" & dt1.Rows(0).Item(95) & "',
           '" & dt1.Rows(0).Item(96) & "',
           '" & dt1.Rows(0).Item(97) & "',
           '" & dt1.Rows(0).Item(98) & "',
           '" & dt1.Rows(0).Item(99) & "',
           '" & dt1.Rows(0).Item(100) & "',
           '" & dt1.Rows(0).Item(101) & "',
           '" & dt1.Rows(0).Item(102) & "',
           '" & dt1.Rows(0).Item(103) & "',
           '" & dt1.Rows(0).Item(104) & "',
           '" & dt1.Rows(0).Item(105) & "',
           '" & dt1.Rows(0).Item(106) & "',
           '" & dt1.Rows(0).Item(107) & "',
           '" & dt1.Rows(0).Item(108) & "',
           '" & dt1.Rows(0).Item(109) & "',
           '" & dt1.Rows(0).Item(110) & "',
           '" & dt1.Rows(0).Item(111) & "',
           '" & dt1.Rows(0).Item(112) & "',
           '" & dt1.Rows(0).Item(113) & "',
           '" & dt1.Rows(0).Item(114) & "')"

            x.ExecInsertUpdateDelete(strIns, con)
            Return dt1
        End If

    End Function
    <WebMethod()>
    Public Function DQGetCert(ByVal CertID As String, ByVal ChamberID As String) As DataTable
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "select vlue, lbl from dbo.Coc_GetCert('" & CertID & "','" & ChamberID & "')"
        Dim dt As New DataTable
        dt = x.FillDataTable(str, con, "0")
        Return dt
    End Function
    <WebMethod()>
    Public Function Log(ByVal CertID As String, ByVal UserID As Integer) As Boolean
        Dim str As String
        Dim x As New DbClass
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = "INSERT INTO [dbo].[COCLog]
           ([UserID]
           ,[CertNo]
         )
     VALUES
           (" & UserID & "
           ,'" & CertID & "')"

        Return x.ExecInsertUpdateDelete(str, con)
    End Function
End Class