﻿Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class TestDB
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function Test(ByVal testdata As String) As Boolean
        Dim x As New DbClass
        Dim str As String
        Dim con As String = ConfigurationManager.ConnectionStrings("DQ").ConnectionString
        str = " INSERT INTO [dbo].[TestDB]
           ([sss])
     VALUES
           ('" & testdata & "')"
        Return x.ExecInsertUpdateDelete(Str, con)
    End Function

End Class