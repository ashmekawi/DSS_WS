Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography

Public Class DbClass
    Public Function Encrypt1(ByVal x As String)

        Try
            Dim textToEncrypt As String = x
            Dim ToReturn As String = ""
            Dim publickey As String = "12345678"
            Dim secretkey As String = "87654321"
            Dim secretkeyByte As Byte() = {}
            secretkeyByte = System.Text.Encoding.UTF8.GetBytes(secretkey)
            Dim publickeybyte As Byte() = {}
            publickeybyte = System.Text.Encoding.UTF8.GetBytes(publickey)
            Dim MS As MemoryStream = Nothing
            Dim cs As CryptoStream = Nothing
            Dim inputbyteArray As Byte() = System.Text.Encoding.UTF8.GetBytes(textToEncrypt)
            Using des As DESCryptoServiceProvider = New DESCryptoServiceProvider()
                MS = New MemoryStream()
                cs = New CryptoStream(MS, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write)
                cs.Write(inputbyteArray, 0, inputbyteArray.Length)
                cs.FlushFinalBlock()
                ToReturn = Convert.ToBase64String(MS.ToArray())
            End Using
            Return ToReturn

        Catch ex As Exception

            Throw New Exception(ex.Message, ex.InnerException)

        End Try


    End Function
    Function FillDataTable(ByVal Lpsql As String, ByVal LpDbConnect As String, ByVal LpTrnsName As String) As DataTable
        Dim DT As New System.Data.DataTable
        Using connection As New SqlConnection(LpDbConnect)
            Dim command As SqlCommand = connection.CreateCommand()
            command.CommandText = Lpsql
            Try
                connection.Open()
                Dim da As New SqlDataAdapter(command)
                DT.TableName = LpTrnsName
                da.Fill(DT)
                connection.Close()
                da = Nothing
            Catch ex As Exception
                ' MsgBox("خطأ فى النظام" & " " & ex.Message)
                DT.Columns.Add("ID", GetType(String))
                DT.Rows.Add("-99")
            End Try
        End Using
        FillDataTable = DT
    End Function
    Function FillDataSet(ByVal Lpsql As String, ByVal LpDbConnect As String, ByVal LpTrnsName As String) As DataSet
        Dim DT As New System.Data.DataSet
        Using connection As New SqlConnection(LpDbConnect)
            Dim command As SqlCommand = connection.CreateCommand()
            command.CommandText = Lpsql
            Try
                connection.Open()
                Dim da As New SqlDataAdapter(command)

                da.Fill(DT)
                connection.Close()
                da = Nothing
            Catch ex As Exception
                ' MsgBox("خطأ فى النظام" & " " & ex.Message)

            End Try
        End Using
        FillDataSet = DT
    End Function
    Function ReXml(ByVal Lpsql As String, ByVal LpDbConnect As String, ByVal LpTrnsName As String) As String
        Dim DT As New System.Data.DataTable
        Dim LpXml As String
        Using connection As New SqlConnection(LpDbConnect)
            Dim command As SqlCommand = connection.CreateCommand()
            command.CommandText = Lpsql
            Try
                connection.Open()
                Dim da As New SqlDataAdapter(command)
                DT.TableName = LpTrnsName
                da.Fill(DT)
                connection.Close()
                da = Nothing
            Catch ex As Exception
                ' MsgBox("خطأ فى النظام" & " " & ex.Message)

            End Try
        End Using
        LpXml = ToStringAsXml(DT)
        DT = Nothing
        ReXml = LpXml
    End Function
    Public Function ToStringAsXml(ByVal LpdT As DataTable) As String
        Dim sw As New IO.StringWriter()
        LpdT.WriteXml(sw, XmlWriteMode.WriteSchema, False)
        ToStringAsXml = sw.ToString
    End Function
    Public Function ReXmlDoc(ByVal Lpstr As String) As System.Xml.XmlDocument
        Dim xmlDocument As System.Xml.XmlDocument
        xmlDocument = New System.Xml.XmlDocument
        xmlDocument.LoadXml(Lpstr)
        Return xmlDocument
    End Function
    Public Function ExecInsertUpdateDelete(ByVal LPSql As String, ByVal LpDbConnect As String) As Boolean
        Dim Vld As Boolean
        Using connection As New SqlConnection(LpDbConnect)
            Dim command As SqlCommand = connection.CreateCommand()
            command.CommandText = LPSql
            Try
                connection.Open()
                command.ExecuteNonQuery()
                connection.Close()
                Vld = True
            Catch ex As Exception
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter("E:\testInsert.txt", True)
                file.WriteLine(ex.Message)
                file.WriteLine(LPSql)
                file.WriteLine(LpDbConnect)
                file.Close()
                Vld = False
            End Try
        End Using
        ExecInsertUpdateDelete = Vld
    End Function
    Public Function ExecSpAndReValue(ByVal Lsql As String, ByVal LpDbConnect As String) As String
        Dim Lpv As String
        Using connection As New SqlConnection(LpDbConnect)
            Dim command As SqlCommand = connection.CreateCommand()
            command.CommandText = Lsql
            Try
                connection.Open()
                Lpv = command.ExecuteScalar
                Lpv = 1
            Catch ex As Exception
                Lpv = "-99"
            End Try
        End Using
        Return Lpv
    End Function
    Public Function ReSqlFromVar(ByVal v As String, ByVal Sprt As String) As String
        Dim CrntValue As String
        CrntValue = "Null"
        If v <> "" Then
            CrntValue = Sprt & v & Sprt
        End If
        ReSqlFromVar = CrntValue
    End Function
    Public Function ValidatePassword(ByVal pwd As String,
      Optional ByVal minLength As Integer = 8,
      Optional ByVal numUpper As Integer = 2,
      Optional ByVal numLower As Integer = 2,
      Optional ByVal numNumbers As Integer = 2,
      Optional ByVal numSpecial As Integer = 0,
      Optional ByVal numAraChar As Integer = 0,
      Optional ByVal numAraNum As Integer = 0) As Boolean

        ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
        Dim upper As New System.Text.RegularExpressions.Regex("[A-Z]")
        Dim lower As New System.Text.RegularExpressions.Regex("[a-z]")
        Dim number As New System.Text.RegularExpressions.Regex("[0-9]")
        ' Special is "none of the above".
        Dim special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")
        Dim arabChar As New System.Text.RegularExpressions.Regex("^[\u0621-\u064A0-9 ]+$")
        Dim ArabNumeric As New System.Text.RegularExpressions.Regex("^[\u0621-\u064A\u0660-\u0669]+$ ")
        ' Check the length.
        If Len(pwd) < minLength Then Return False
        ' Check for minimum number of occurrences.
        If upper.Matches(pwd).Count < numUpper Then Return False
        If lower.Matches(pwd).Count < numLower Then Return False
        If number.Matches(pwd).Count < numNumbers Then Return False
        If special.Matches(pwd).Count < numSpecial Then Return False
        If arabChar.Matches(pwd).Count < numAraChar Then Return False
        If ArabNumeric.Matches(pwd).Count < numAraNum Then Return False
        ' Passed all checks.
        Return True
    End Function
    Public Function IsValidEmailFormat(ByVal s As String) As Boolean
        Return Regex.IsMatch(s, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")
    End Function
    Public Function IsValidIDNumber(ByVal LpStr As String) As Boolean
        If Len(LpStr) < 14 Then
            Return False
            Exit Function
        End If
        Dim LpChekDgt = Val(Mid(LpStr, 14, 1))
        LpStr = (Mid(LpStr, 1, 13))
        Dim IsValid As Boolean = False
        Dim i As Integer
        Dim Res As Integer
        For i = 0 To 12
            Res = Res + (Val(Mid(LpStr, i + 1, 1)) * Val(Mid("2765432765432", i + 1, 1)))
        Next
        Res = 11 - (Res Mod 11)
        If Res > 9 Then Res = Res Mod 10
        If Res = 0 And LpChekDgt = 9 Then
            Res = 9
        End If
        If Res = LpChekDgt Then
            IsValid = True
        End If
        Return IsValid
    End Function
    Public Function ValidateDate(ByVal checkInputValue As String) As Integer
        Dim DateAsInteger As Integer
        Dim x As DateTime = Convert.ToDateTime(checkInputValue)
        checkInputValue = Format(x, "yyyyMMdd")
        DateAsInteger = Integer.Parse(checkInputValue)
        Return DateAsInteger
    End Function
    'Function UploadBookingAttachDoc(ByVal lpBookingID As String, ByVal LpRequestID As String, ByVal LpCustID As String, LpBookingPath As String, ByVal Lfiles As String, Lpconstr As String) As String
    '    'Dim Lfiles As String
    '    Dim DocQ As Integer = 0
    '    Dim fname() As String
    '    Dim i As Integer
    '    'Lfiles = GetFileInpath(LpBookingPath, lpBookingID)
    '    If Lfiles = "" Then Return "-99"
    '    fname = Lfiles.Split("|")
    '    For i = 0 To UBound(fname)
    '        If Trim(fname(i)) <> "" Then
    '            If Dir(LpBookingPath & "\" & fname(i), vbNormal) <> "" Then
    '                DocQ = DocQ + 1
    '                If Not AppendDoc2BookingInDB(LpBookingPath & "\" & fname(i), lpBookingID, LpRequestID, LpCustID, DocQ, Lpconstr) Then
    '                    DocQ = DocQ - 1
    '                End If
    '            End If
    '        End If
    '    Next
    '    Return DocQ.ToString
    'End Function
    Function GetFileInpath(LpPath As String, LpPtrn As String) As String
        Dim Fs() As String
        Dim LFilsStr As String = ""
        Fs = System.IO.Directory.EnumerateFiles(LpPath, LpPtrn & "*.*").ToArray
        Dim i As Integer
        Dim p As Integer
        Dim ext As String = ""

        For i = 0 To UBound(Fs)
            p = InStr(Fs(i), ".")
            If p > 0 Then
                If InStr(Fs(i), LpPtrn) > 0 Then
                    LFilsStr = LFilsStr & Fs(i) & IIf(i < UBound(Fs), "|", "")
                End If
            End If
        Next

        Return LFilsStr
    End Function
    Function AppendDoc2BookingInDB(ByVal LPFileName As String, ByVal LpBookingID As String, ByVal LpRequestID As String, ByVal LpCustID As String, ByVal Lpseq As Integer, ByVal LpDbConnect As String) As Boolean
        Dim Lsql As String
        Dim imgid As String
        Dim BookingID As String
        Dim LFDatetime As String

        LFDatetime = Format(FileDateTime(LPFileName), "yyyyMMddhhmmss")
        BookingID = LpBookingID
        If CLng(BookingID) = 0 Then
            BookingID = Format(Now, "yyyyMMdd") & "00000"
        End If
        Lsql = "exec sp_AttachDoc2Booking " & BookingID & "," & IIf(LpRequestID = "", "0", LpRequestID) & "," & LpCustID & "," & Lpseq & ",'" & LPFileName & "'," & LFDatetime
        imgid = ExecSpAndReValue(Lsql, LpDbConnect)
        If imgid = "-99" Then
            Return False
        End If
        Lsql = "update  BookingAttached set  img=@img where id=" & imgid
        AppendDoc2BookingInDB = InserFile(LPFileName, imgid, "ID", "@img", Lsql, LpDbConnect)
    End Function
    Function InserFile(ByVal FName As String, ByVal IDValue As String, ByVal IDFldname As String, ByVal ImgFldName As String, ByVal Lpsql As String, ByVal LpDbConnect As String) As Boolean
        Dim Vld As Boolean
        Dim fs As FileStream = New FileStream(FName, FileMode.Open, FileAccess.Read)
        Dim r As BinaryReader = New BinaryReader(fs)
        Dim FileByteArray(fs.Length - 1) As Byte
        r.Read(FileByteArray, 0, CInt(fs.Length))
        Using connection As New SqlConnection(LpDbConnect)
            Dim command As SqlCommand = connection.CreateCommand()
            command.CommandText = Lpsql
            Try
                command.Parameters.AddWithValue(ImgFldName, FileByteArray)
                connection.Open()
                command.ExecuteNonQuery()
                connection.Close()
                Vld = True
            Catch ex As Exception
                Vld = False
            End Try
            fs.Close()
            command.Dispose()
            connection.Dispose()
            fs.Dispose()
        End Using
        InserFile = Vld
    End Function
    Function IsFound(ByVal Lsql As String, ByVal LpDbConnect As String) As String
        Dim Lpv As String
        Lpv = ""

        Using connection As New SqlConnection(LpDbConnect)
            Dim command As SqlCommand = connection.CreateCommand()
            command.CommandText = Lsql
            Try
                connection.Open()
                If Not IsDBNull(command.ExecuteScalar) Then
                    Lpv = command.ExecuteScalar
                Else
                    Lpv = ""
                End If
            Catch ex As Exception
                Lpv = "-99"
            End Try
        End Using

        Return Lpv
    End Function
    Function UploadBookingAttachDoc(ByVal lpBookingID As String, ByVal LpRequestID As String, ByVal LpCustID As String, LpBookingPath As String, ByVal LpTrgtPath As String, ByVal Lfiles As String, Lpconstr As String) As String
        Dim LSql As String
        Dim DocQ As Integer = 0
        Dim BookingID As String
        Dim fname() As String
        Dim LFDatetime As String
        Dim TrgtFName As String = ""
        Dim TrgtSubDir As String
        Dim imgid As String
        Dim i As Integer
        'Lfiles = GetFileInpath(LpBookingPath, lpBookingID)
        If Lfiles = "" Then Return "-4"
        BookingID = lpBookingID
        TrgtSubDir = Format(Now, "yyyyMMdd")
        If Not IsFileOrDirValid(LpTrgtPath & "\" & TrgtSubDir, FileAttribute.Directory) Then
            If Not IsDirCreated(LpTrgtPath & "\" & TrgtSubDir) Then
                Return "-5"
            End If
        End If
        If Val(BookingID) = 0 Then
            BookingID = Format(Now, "yyyyMMdd") & "00000"
        End If
        fname = Lfiles.Split("|")
        For i = 0 To UBound(fname)
            If Trim(fname(i)) <> "" Then

                If IsFileOrDirValid(LpBookingPath & "\" & fname(i), FileAttribute.Normal) Then
                    DocQ = DocQ + 1
                    LFDatetime = Format(FileDateTime(LpBookingPath & "\" & fname(i)), "yyyyMMddhhmmss")
                    LSql = "exec sp_AttachDoc2Booking " & BookingID & "," & IIf(LpRequestID = "", "0", LpRequestID) & "," & LpCustID & "," & DocQ & ",'" & fname(i) & "'," & LFDatetime
                    imgid = ExecSpAndReValue(LSql, Lpconstr)
                    If imgid = "-99" Then
                        Return False
                    End If
                    TrgtFName = LpTrgtPath & "\" & TrgtSubDir & "\" & imgid & ReFileExt(fname(0))
                    '   LSql = "exec sp_copyfile 'copy " & LpBookingPath & "\" & fname(i) & " " & TrgtFName & "'"
                    FileCopy(LpBookingPath & "\" & fname(i), TrgtFName)
                    If Not IsFileOrDirValid(TrgtFName & "\" & fname(i), FileAttribute.Normal) Then
                        '  DocQ = DocQ - 1
                    End If
                    'If ExecSpAndReValue(LSql, Lpconstr) <> "0" Then
                    '    DocQ = DocQ - 1
                    'End If
                End If
            End If
        Next
        Return DocQ.ToString
    End Function
    Function ReFileExt(LpFname As String) As String
        Dim ext As String = ""
        Dim pos As Integer
        pos = InStr(LpFname, ".")
        If pos > 0 Then
            ext = Mid(LpFname, pos)
        End If
        Return ext
    End Function
    Function IsFileOrDirValid(ByVal LpDirName As String, ByVal Lptype As FileAttribute) As Boolean
        Dim vld As Boolean = False
        Try
            vld = (Dir(LpDirName, Lptype) <> "")
        Catch ex As Exception
            vld = False
        End Try
        Return vld
    End Function
    Function IsDirCreated(ByVal LpDirName As String) As Boolean
        Dim vld As Boolean = False
        Try
            MkDir(LpDirName)
            vld = (Dir(LpDirName, FileAttribute.Directory) <> "")
        Catch ex As Exception
            vld = False
        End Try
        Return vld
    End Function
    Function UPdateBlobWithByteArray(ByVal FileByteArray() As Byte, ByVal ImgFldName As String, ByVal Lpsql As String, ByVal LpDbConnect As String) As Boolean
        Dim Vld As Boolean
        ' Open the file
        'Dim fs As FileStream = New FileStream(FName, FileMode.Open, FileAccess.Read)
        ''Read the output in binary reader
        'Dim r As BinaryReader = New BinaryReader(fs)
        ''Declare a byte array to save the content of the file to be saved
        'Dim FileByteArray(fs.Length - 1) As Byte
        'r.Read(FileByteArray, 0, CInt(fs.Length))
        Using connection As New SqlConnection(LpDbConnect)
            Dim command As SqlCommand = connection.CreateCommand()
            command.CommandText = Lpsql
            Try
                command.Parameters.AddWithValue(ImgFldName, FileByteArray)
                connection.Open()
                command.ExecuteNonQuery()
                connection.Close()
                Vld = True
            Catch ex As Exception
                ' MsgBox("خطأ فى النظام" & " " & ex.Message & vbNewLine & Lpsql)
                Vld = False
            End Try
            command.Dispose()
            connection.Dispose()
        End Using

        UPdateBlobWithByteArray = Vld
    End Function
    Public Function CreateLoginTokn(LpMobile As String, Lpass As String, ByVal LpDbConnect As String) As String
        Dim Talkn As String = ""
        Dim i As Integer
        Dim wrd As String
        Dim ch As String = ""
        Dim Ntk As Double
        Dim mls As Double

        Ntk = Now.Ticks
        mls = Now.Millisecond
        Ntk = (Ntk * mls) * CLng(LpMobile)
        ' Ntk = (Ntk * mls)
        Talkn = StrReverse(Ntk.ToString)
        Talkn = IsFound("select dbo.ReEncStr('" & Talkn & "')", LpDbConnect)
        wrd = ""
        For i = 1 To Len(Talkn)
            ch = Hex(Asc(Mid(Talkn, i, 1)))
            If Len(ch) < 2 Then ch = "0" & ch
            wrd = wrd & ch
        Next
        Talkn = wrd
        Return Talkn
    End Function
    Function FillQueryHndlr() As DataTable
        Dim DQ As New DataTable
        DQ.TableName = "DataTableStatus"
        DQ.Columns.Add("DataTableResult", GetType(Boolean))
        DQ.Columns.Add("MsgCode", GetType(Integer))
        DQ.Columns.Add("MsgDesc", GetType(String))
        DQ.Columns.Add("MsgADesc", GetType(String))
        DQ.Rows.Add()
        DQ.Rows(0).Item(0) = True
        DQ.Rows(0).Item(1) = 1
        DQ.Rows(0).Item(2) = "Success"
        DQ.Rows(0).Item(3) = "تم"
        FillQueryHndlr = DQ
    End Function
    Function CreateDataTableStatus(DataTableStatus As String) As DataTable
        Dim DQ As New DataTable
        DQ.TableName = DataTableStatus
        ' DQ.Columns.Add("DataTableResult", GetType(Boolean))
        DQ.Columns.Add("statusCode", GetType(Integer))
        DQ.Columns.Add("MsgDesc", GetType(String))
        '  DQ.Columns.Add("MsgADesc", GetType(String))
        DQ.Rows.Add()
        'DQ.Rows(0).Item(0) = True
        DQ.Rows(0).Item(0) = 0
        DQ.Rows(0).Item(1) = "Success"
        '  DQ.Rows(0).Item(3) = "تم"
        CreateDataTableStatus = DQ
    End Function
    Sub SetQueryHndlr(ByRef Lpdt As DataTable, ByVal Msgcode As Integer, ByVal Msgdesc As String, ByVal MsgAdesc As String)
        Lpdt.Rows(0).Item(0) = False
        Lpdt.Rows(0).Item(1) = Msgcode
        Lpdt.Rows(0).Item(2) = Msgdesc
        Lpdt.Rows(0).Item(3) = MsgAdesc
    End Sub
    'Function GetCraPageQ(LpCra As Long, LpReqID As Long, LpUserID As Long, LpsrvcType As Integer, LpConStr As String, lpCol1 As Integer, LpCol2 As Integer, LpwsCon As String) As DataTable
    '    'v = tt.GetPageQ(60055964, 5, 1, 1, "Data Source=(local);Database=Cra00;Uid=sa;pwd=P@ssw0rd;", 1, 2, "Data Source=(local);Database=CraMService;Uid=sa;pwd=P@ssw0rd;")
    '    Dim PgsCls As New CraGetPages.GetPageQClas
    '    Dim Res As Array

    '    Dim Dt As New DataTable
    '    Dt.TableName = "CraPages"
    '    Dt.Columns.Add("Pages", GetType(Integer))
    '    Dt.Columns.Add("CraFee", GetType(Integer))
    '    Dt.Columns.Add("PostCost", GetType(String))
    '    Dt.Rows.Add()
    '    Dt.Rows(0).Item(0) = 0
    '    Dt.Rows(0).Item(1) = 0
    '    Dt.Rows(0).Item(2) = 0

    '    Res = PgsCls.GetPageQ(LpCra, LpReqID, LpUserID, LpsrvcType, LpConStr, lpCol1, LpCol2, LpwsCon)
    '    Dt.Rows(0).Item(0) = CInt(Res(0))
    '    Dt.Rows(0).Item(1) = CInt(Res(1))
    '    Dt.Rows(0).Item(2) = Res(2)

    '    Return Dt
    'End Function
    'Function GetCraPageQ(LpCra As Long, LpReqID As Long, LpUserID As Long, LpsrvcType As Integer, LpConStr As String, lpCol1 As Integer, LpCol2 As Integer, LpwsCon As String, LpUsetrac As Boolean, lpPath As String) As DataTable
    '    'v = tt.GetPageQ(60055964, 5, 1, 1, "Data Source=(local);Database=Cra00;Uid=sa;pwd=P@ssw0rd;", 1, 2, "Data Source=(local);Database=CraMService;Uid=sa;pwd=P@ssw0rd;")
    '    Dim PgsCls As New CraGetPages.GetPageQClas
    '    Dim Res As Array

    '    Dim Dt As New DataTable
    '    Dt.TableName = "CraPages"
    '    Dt.Columns.Add("Pages", GetType(Integer))
    '    Dt.Columns.Add("CraFee", GetType(Integer))
    '    Dt.Columns.Add("PostCost", GetType(String))
    '    Dt.Rows.Add()
    '    Dt.Rows(0).Item(0) = 0
    '    Dt.Rows(0).Item(1) = 0
    '    Dt.Rows(0).Item(2) = 0

    '    Res = PgsCls.GetPageQ(LpCra, LpReqID, LpUserID, LpsrvcType, LpConStr, lpCol1, LpCol2, LpwsCon, LpUsetrac, lpPath)
    '    Dt.Rows(0).Item(0) = CInt(Res(0))
    '    Dt.Rows(0).Item(1) = CInt(Res(1))
    '    Dt.Rows(0).Item(2) = Res(2)

    '    Return Dt
    'End Function
    Function ReLastkey(ByVal Tblname As String, ByVal LpDbConnect As String) As String
        Dim Qstr As String
        Dim IDNO As Short

        IDNO = 0

        Qstr = "select id from lastkey WHERE TablName='" & Tblname & "'"
        IDNO = CShort(0 & IsFound(Qstr, LpDbConnect))
        If IDNO = 0 Then
            Qstr = "Insert into lastkey (id,TablName) VALUES (1,'" & Tblname & "')"
        Else
            Qstr = "update lastkey SET  id = id + 1 where tablname= '" & Tblname & "'"
        End If
        If ExecInsertUpdateDelete(Qstr, LpDbConnect) Then
            IDNO = IDNO + 1
            ReLastkey = IDNO
        Else
            ReLastkey = ""
        End If
    End Function
    'Function sendSms(ByRef UserName As String, ByRef Password As String, ByRef msg As String, ByRef PhoneNumber As String, ByRef request_id As String, ByRef OperatornName As String) As String

    '    Dim strUrl As String = ""

    '    Select Case Left(LTrim(RTrim(PhoneNumber)), 4)
    '        Case "2010"
    '            strUrl = "http://bulksms.advansystelecom.com/Message_Request.aspx?username=" + RTrim(LTrim(UserName)) + "&password=" + RTrim(LTrim(Password)) + "&request_id=" + RTrim(LTrim(request_id)) + "&Mobile_No=" + RTrim(LTrim(PhoneNumber)) + "&type=2&message=" + msg + "&encoding=2&sender=Advansys&operator=Vodafone"
    '        Case "2011" Or "2015"
    '            strUrl = "http://bulksms.advansystelecom.com/Message_Request.aspx?username=" + RTrim(LTrim(UserName)) + "&password=" + RTrim(LTrim(Password)) + "&request_id=" + RTrim(LTrim(request_id)) + "&Mobile_No=" + RTrim(LTrim(PhoneNumber)) + "&type=2&message=" + msg + "&encoding=2&sender=Advansys&operator=Etisalat-WE"
    '        Case "2012"
    '            strUrl = "http://bulksms.advansystelecom.com/Message_Request.aspx?username=" + RTrim(LTrim(UserName)) + "&password=" + RTrim(LTrim(Password)) + "&request_id=" + RTrim(LTrim(request_id)) + "&Mobile_No=" + RTrim(LTrim(PhoneNumber)) + "&type=2&message=" + msg + "&encoding=2&sender=Advansys&operator=Mobinil"
    '    End Select

    '    Dim request As WebRequest = HttpWebRequest.Create(strUrl)
    '    Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
    '    Dim s As Stream = CType(response.GetResponseStream(), Stream)
    '    Dim readStream As StreamReader = New StreamReader(s)
    '    Dim dataString As String = readStream.ReadToEnd()

    '    response.Close()
    '    s.Close()
    '    readStream.Close()

    '    Return dataString


    'End Function
    <Obsolete>
    Public Shared Function Encrypt(ByVal inputText As String) As String
        Dim encryptionkey As String = "SAUW193BX628TD57"
        Dim keybytes As Byte() = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString())
        Dim rijndaelCipher As New RijndaelManaged()
        Dim plainText As Byte() = Encoding.Unicode.GetBytes(inputText)
        Dim pwdbytes As New PasswordDeriveBytes(encryptionkey, keybytes)
        Using encryptrans As ICryptoTransform = rijndaelCipher.CreateEncryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16))
            Using mstrm As New MemoryStream()
                Using cryptstm As New CryptoStream(mstrm, encryptrans, CryptoStreamMode.Write)
                    cryptstm.Write(plainText, 0, plainText.Length)
                    cryptstm.Close()
                    Return Convert.ToBase64String(mstrm.ToArray())
                End Using
            End Using
        End Using
    End Function

    <Obsolete>
    Public Shared Function Decrypt(ByVal encryptText As String) As String
        Dim encryptionkey As String = "SAUW193BX628TD57"
        Dim keybytes As Byte() = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString())
        Dim rijndaelCipher As New RijndaelManaged()
        Dim encryptedData As Byte() = Convert.FromBase64String(encryptText.Replace(" ", "+"))
        Dim pwdbytes As New PasswordDeriveBytes(encryptionkey, keybytes)
        Using decryptrans As ICryptoTransform = rijndaelCipher.CreateDecryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16))
            Using mstrm As New MemoryStream(encryptedData)
                Using cryptstm As New CryptoStream(mstrm, decryptrans, CryptoStreamMode.Read)
                    Dim plainText As Byte() = New Byte(encryptedData.Length - 1) {}
                    Dim decryptedCount As Integer = cryptstm.Read(plainText, 0, plainText.Length)
                    Return Encoding.Unicode.GetString(plainText, 0, decryptedCount)
                End Using
            End Using
        End Using
    End Function




End Class
