Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization
Imports System.Web.Security
Imports System.Security.Cryptography

''Developed by Brian Averitt

Module modGateway

    Public Function validateCreditCardNumber(ByRef CC_Num As String, ByRef CC_Type_ID As Integer)

        Dim boolLengthCheck As Boolean = False
        Dim boolLengthCheckFound As Boolean = False
        Dim boolPrefixCheck As Boolean = False
        Dim boolPrefixFound As Boolean = False
        Dim boolLUHN_Check As Boolean = False
        Dim boolReturn As Boolean = False
        Dim boolContinue As Boolean = True

        Dim CC_stripped As String = Trim(CC_Num.Replace("-", "").Replace(" ", ""))

        ''Get the validation requirements for the credit card type
        Dim strSql As String = "SELECT prefix_chk, length_chk from mc_cc_types where enabled = 'true' and mc_cc_types_id = " & CC_Type_ID
        Dim tblResults As DataTable = g_IO_Execute_SQL(strSql, False)

        If tblResults.Rows.Count > 0 Then
            ''Credit Card Supported.  Begin Validation of the card number.

            '''''''''''''''''''check the length of the cc number''''''''''''''''''
            Dim strLength_chk As Array = tblResults.Rows(0)("Length_Chk").ToString.Split(",")

            For Each curLengthChk In strLength_chk
                If CC_stripped.Length = curLengthChk Then
                    boolLengthCheckFound = True
                    GoTo end_of_lengthChk_for
                End If
            Next

end_of_lengthChk_for:

            If boolLengthCheckFound = True Then
                boolLengthCheck = True
            Else
                boolContinue = False
            End If

            If boolContinue = True Then
                ''''''''''''''''''''Next, validate the prefix''''''''''''''''''''''
                Dim strPrefix_chk As Array = tblResults.Rows(0)("Prefix_Chk").ToString.Split(",")
                If strPrefix_chk.Length > 0 Then
                    ''Check for each of the prefixes found
                    For Each prefix In strPrefix_chk
                        Dim strPrefix As String = prefix.ToString
                        Dim strPrefixLength As Integer = strPrefix.Length
                        If strPrefix = CC_stripped.Substring(0, strPrefixLength) Then
                            boolPrefixFound = True
                            GoTo end_of_PrefixChk_for
                        End If
                    Next

end_of_PrefixChk_for:

                    If boolPrefixFound = False Then
                        boolPrefixCheck = False
                        boolContinue = False
                    Else
                        boolPrefixCheck = True
                    End If
                End If
            End If

            If boolContinue = True Then
                ''''''''''Use the LUHN Formula (Mod 10) for Validation of Primary Account Number
                Dim total As Integer = 0
                Dim counter As Integer = 1
                ''Iterate throguh each number in the CC string reversed.
                For Each number As String In CC_stripped.Reverse
                    Dim curDigit As Integer = 0
                    If (counter Mod 2) = 0 Then
                        ''Double this number and add the individual numbers to the overall total.
                        Dim ResultNum As String = (Convert.ToInt16(number) * 2).ToString
                        If Convert.ToInt16(ResultNum) > 9 Then
                            'For Each indNumber As character In ResultNum
                            For Each indNum In ResultNum
                                Dim i As Integer = CInt(indNum.ToString)
                                total += i
                            Next
                        Else
                            total += ResultNum
                        End If
                    Else
                        Dim i As Integer = CInt(number.ToString)
                        total += i
                    End If
                    counter += 1
                Next

                If total Mod 10 = 0 Then
                    boolLUHN_Check = True
                Else
                    boolContinue = False
                End If
            End If

        Else
            ''This credit card type is not supported
            boolReturn = False
        End If




        ''IS IT VALID??
        If boolLengthCheck = True And boolPrefixCheck = True And boolLUHN_Check = True Then
            ''It's a valid number, return true
            boolReturn = True
        Else
            boolReturn = False
        End If

        Return boolReturn
    End Function

    Partial Public Class gateway_transaction
        Dim response As String = ""
        'Dim transactionType As String = "Purchase"
        Dim dollarAmount As Decimal = "0.00"
        Dim cardHolderName As String = ""
        Dim address1 As String = ""
        Dim address2 As String = ""
        Dim city As String = ""
        Dim state As String = ""
        Dim zipCode As String = ""
        Dim cardNumber As String = ""
        Dim cardType As String = ""
        Dim expirationDate As String = ""
        Dim cvv As String = ""
        Dim OrderNumber As String = ""

        ''Set Trx Conenction Defaults
        Dim KeyID As String = "123456"
        Dim HMACkey As String = "5tb864q9fhxobcowlaksjd07asd087"
        ''The eXactID is also known as the GatewayID
        Dim gatewayID As String = "AI8874-69"
        Dim password As String = "5tb864q9fhxobcows1k3ilaksjd07asd087"
        Dim divisionID As String = "1957194643"
        Dim MID As String = "933410342"
        Dim demoURL As String = "http://api.demo.globalgatewaye4.firstdata.com"
        Dim urlversion As String = "/transaction/v19"
        Dim transArmorToken As String

        ''Properties
        Property setDollarAmount() As String
            Get
                Return dollarAmount
            End Get
            Set(value As String)
                dollarAmount = value
            End Set
        End Property

        Property setCardHolderName() As String
            Get
                Return cardHolderName
            End Get
            Set(value As String)
                cardHolderName = value
            End Set
        End Property

        Property setAddress1() As String
            Get
                Return address1
            End Get
            Set(value As String)
                address1 = value
            End Set
        End Property

        Property setAddress2() As String
            Get
                Return address2
            End Get
            Set(value As String)
                address2 = value
            End Set
        End Property

        Property setCity() As String
            Get
                Return city
            End Get
            Set(value As String)
                city = value
            End Set
        End Property

        Property setZipCode() As String
            Get
                Return zipCode
            End Get
            Set(value As String)
                zipCode = value
            End Set
        End Property

        Property setCardNumber() As String
            Get
                Return cardNumber
            End Get
            Set(value As String)
                cardNumber = value
            End Set
        End Property

        Property setCardType() As String
            Get
                Return cardType
            End Get
            Set(value As String)
                cardType = value
            End Set
        End Property

        Property setExpirationDate() As String
            Get
                Return expirationDate
            End Get
            Set(value As String)
                expirationDate = value
            End Set
        End Property

        Property setCVV() As String
            Get
                Return cvv
            End Get
            Set(value As String)
                cvv = value
            End Set
        End Property

        Property setOrderNumber() As String
            Get
                Return OrderNumber
            End Get
            Set(value As String)
                OrderNumber = value
            End Set
        End Property

        ''Private Functions

        Private Function createDataTableFromXMLNodes(ByRef nodeList As XmlNodeList)
            Dim resultTable As New DataTable

            For Each node As XmlNode In nodeList.ItemOf(0).ChildNodes
                resultTable.Columns.Add(node.Name, GetType(String))
            Next

            Return resultTable
        End Function

        Private Function createDatableForErrorResponse(ByVal responseString As String)
            Dim errorTable As New DataTable

            errorTable.Columns.Add("Transaction_Error", GetType(String))
            errorTable.Columns.Add("Transaction_Approved", GetType(String))
            errorTable.Columns.Add("Bank_Resp_Code", GetType(String))
            errorTable.Columns.Add("Exact_Resp_code", GetType(String))

            Dim newRow As DataRow = errorTable.NewRow
            newRow("Transaction_Error") = "TRUE"
            newRow("Transaction_Approved") = "FALSE"
            newRow("Bank_Resp_Code") = responseString
            newRow("Exact_Resp_code") = responseString
            errorTable.Rows.Add(newRow)
            Return errorTable
        End Function

        ''Methods

        Public Function commitPreAuthorization()
            Dim stringBuilder As New StringBuilder
            Dim cardToken As String = ""

            Dim settings As XmlWriterSettings = New XmlWriterSettings()
            Dim utf8noBOM As Encoding = New UTF8Encoding(False)
            settings.Indent = False
            settings.OmitXmlDeclaration = False
            settings.NewLineOnAttributes = False
            settings.Encoding = utf8noBOM
            Dim xml_string As String = ""
            Using xml_stream As MemoryStream = New MemoryStream()
                Using writer As XmlWriter = XmlWriter.Create(xml_stream, settings)
                    writer.WriteStartElement("Transaction")
                    writer.WriteElementString("ExactID", gatewayID)
                    writer.WriteElementString("Password", password)
                    writer.WriteElementString("Transaction_Type", "01") ''Authorization
                    writer.WriteElementString("Card_Number", cardNumber)
                    writer.WriteElementString("CVDCode", cvv)
                    'writer.WriteElementString("VerificationStr2", cvv)
                    writer.WriteElementString("CVD_Presence_Ind", 1)
                    'writer.WriteElementString("CVD_Presence_Ind", "1")
                    writer.WriteElementString("CardType", cardType)
                    writer.WriteElementString("Expiry_Date", expirationDate)
                    writer.WriteElementString("CardHoldersName", cardHolderName)
                    writer.WriteElementString("DollarAmount", "0.00")
                    writer.WriteElementString("Customer_Ref", OrderNumber)

                    'Customer_Ref
                    'writer.WriteStartElement("LineItem")
                    'writer.WriteElementString("LineNum", "1")
                    'writer.WriteElementString("DollarAmount", "1.00")
                    'writer.WriteEndElement()
                    writer.WriteEndElement()
                End Using
                xml_string = Encoding.UTF8.GetString(xml_stream.ToArray)
            End Using

            ''xml_string = stringBuilder.ToString

            ''SHA1 hash on XML string
            Dim encoder As ASCIIEncoding = New ASCIIEncoding
            Dim xml_byte() As Byte = encoder.GetBytes(xml_string)
            Dim sha1_crypto As SHA1CryptoServiceProvider = New SHA1CryptoServiceProvider
            Dim hash As String = BitConverter.ToString(sha1_crypto.ComputeHash(xml_byte)).Replace("-", "")
            Dim hashed_content As String = hash.ToLower

            Dim time As String = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
            Dim type As String = "application/xml"
            Dim method As String = "POST"


            Dim url As String = "https://api.demo.globalgatewaye4.firstdata.com" & urlversion

            '      string hash_data = method + type + "\n" + hashed_content + "\n" + time + "\n" + uri;
            Dim hash_data As String = method & vbLf & type & vbLf & hashed_content & vbLf & time & vbLf & urlversion

            'HMAC hmac_sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(key)); //key
            Dim hmac_sha1 As HMAC = New HMACSHA1(Encoding.UTF8.GetBytes(HMACkey))
            'byte[] hmac_data = hmac_sha1.ComputeHash(Encoding.UTF8.GetBytes(hash_data)); //data
            Dim hmac_data As Byte() = hmac_sha1.ComputeHash(Encoding.UTF8.GetBytes(hash_data))
            'string base64_hash = Convert.ToBase64String(hmac_data);
            Dim base64_hash = Convert.ToBase64String(hmac_data)


            'HttpWebRequest web_request = (HttpWebRequest)WebRequest.Create(url);
            Dim web_request As HttpWebRequest = WebRequest.Create(url)
            web_request.Method = "POST"
            web_request.ContentType = type
            web_request.Accept = "*/*"
            web_request.Headers.Add("x-gge4-date", time)
            web_request.Headers.Add("x-gge4-content-sha1", hashed_content)
            web_request.Headers.Add("Authorization", "GGE4_API " + KeyID + ":" + base64_hash)
            web_request.ContentLength = xml_string.Length

            'Write and send request Data
            Using Stream_writer As StreamWriter = New StreamWriter(web_request.GetRequestStream())
                Stream_writer.Write(xml_string)
            End Using

            ''Get Response and read into string
            Dim response_string As String = ""

            Dim resultDatatable As DataTable = Nothing

            Try
                Using web_response As HttpWebResponse = web_request.GetResponse()

                    Using response_stream As StreamReader = New StreamReader(web_response.GetResponseStream)
                        response_string = response_stream.ReadToEnd
                    End Using

                    ''laod XML
                    Dim xmldoc As XmlDocument = New XmlDocument()
                    xmldoc.LoadXml(response_string)
                    Dim nodelist As XmlNodeList = xmldoc.SelectNodes("TransactionResult")

                    'Create the datatable to return
                    resultDatatable = createDataTableFromXMLNodes(nodelist) 'Generates the datatable from the XML fields
                    Dim rowResult As DataRow = resultDatatable.NewRow

                    For Each node As XmlNode In nodelist.ItemOf(0).ChildNodes
                        rowResult(node.Name) = node.InnerText
                        response &= node.Name & " : " & node.InnerText
                        response &= "<br />"
                    Next

                    resultDatatable.Rows.Add(rowResult)

                    ''Insert result into Audit table.
                    ''insertIntoPNCGatewayResponseLog(response)

                    'transArmorToken = response
                    'transArmorToken = nodelist.ItemOf(0)("TransarmorToken").InnerText


                End Using

            Catch SoapHdrEx As SoapHeaderException
                response = SoapHdrEx.ToString
            Catch SoapEx As SoapException
                response = SoapEx.ToString
            Catch ex As WebException
                If Not IsNothing(ex.Response) Then
                    Using responseEX As HttpWebResponse = ex.Response
                        Using reader As StreamReader = New StreamReader(responseEX.GetResponseStream)
                            response = reader.ReadToEnd
                        End Using
                    End Using
                End If
                resultDatatable = createDatableForErrorResponse(response)

                response = ex.ToString
            Catch ex As Exception
                response = ex.ToString
            End Try

            Return resultDatatable

        End Function

        Public Function commitTrans()
            '09/22/15 added trim to cardnumber/transarmoroToken
            Dim stringBuilder As New StringBuilder

            Dim settings As XmlWriterSettings = New XmlWriterSettings()
            Dim utf8noBOM As Encoding = New UTF8Encoding(False)
            settings.Indent = False
            settings.OmitXmlDeclaration = False
            settings.NewLineOnAttributes = False
            settings.Encoding = utf8noBOM
            Dim xml_string As String = ""
            Using xml_stream As MemoryStream = New MemoryStream()
                Using writer As XmlWriter = XmlWriter.Create(xml_stream, settings)
                    writer.WriteStartElement("Transaction")
                    writer.WriteElementString("ExactID", gatewayID)
                    writer.WriteElementString("Password", password)
                    writer.WriteElementString("Transaction_Type", "00") ''Transaction
                    writer.WriteElementString("TransarmorToken", cardNumber.Trim.Replace(" ", ""))
                    writer.WriteElementString("CVDCode", cvv)
                    'writer.WriteElementString("VerificationStr2", cvv)
                    writer.WriteElementString("CVD_Presence_Ind", 1)
                    'writer.WriteElementString("CVD_Presence_Ind", "1")
                    writer.WriteElementString("CardType", cardType)
                    writer.WriteElementString("Expiry_Date", expirationDate)
                    writer.WriteElementString("CardHoldersName", cardHolderName)
                    writer.WriteElementString("DollarAmount", dollarAmount)
                    writer.WriteElementString("Customer_Ref", OrderNumber)


                    writer.WriteEndElement()
                End Using
                xml_string = Encoding.UTF8.GetString(xml_stream.ToArray)
            End Using

            ''xml_string = stringBuilder.ToString

            ''SHA1 hash on XML string
            Dim encoder As ASCIIEncoding = New ASCIIEncoding
            Dim xml_byte() As Byte = encoder.GetBytes(xml_string)
            Dim sha1_crypto As SHA1CryptoServiceProvider = New SHA1CryptoServiceProvider
            Dim hash As String = BitConverter.ToString(sha1_crypto.ComputeHash(xml_byte)).Replace("-", "")
            Dim hashed_content As String = hash.ToLower

            Dim time As String = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
            Dim type As String = "application/xml"
            Dim method As String = "POST"


            Dim url As String = "https://api.demo.globalgatewaye4.firstdata.com" & urlversion

            '      string hash_data = method + type + "\n" + hashed_content + "\n" + time + "\n" + uri;
            Dim hash_data As String = method & vbLf & type & vbLf & hashed_content & vbLf & time & vbLf & urlversion

            'HMAC hmac_sha1 = new HMACSHA1(Encoding.UTF8.GetBytes(key)); //key
            Dim hmac_sha1 As HMAC = New HMACSHA1(Encoding.UTF8.GetBytes(HMACkey))
            'byte[] hmac_data = hmac_sha1.ComputeHash(Encoding.UTF8.GetBytes(hash_data)); //data
            Dim hmac_data As Byte() = hmac_sha1.ComputeHash(Encoding.UTF8.GetBytes(hash_data))
            'string base64_hash = Convert.ToBase64String(hmac_data);
            Dim base64_hash = Convert.ToBase64String(hmac_data)


            'HttpWebRequest web_request = (HttpWebRequest)WebRequest.Create(url);
            Dim web_request As HttpWebRequest = WebRequest.Create(url)
            web_request.Method = "POST"
            web_request.ContentType = type
            web_request.Accept = "*/*"
            web_request.Headers.Add("x-gge4-date", time)
            web_request.Headers.Add("x-gge4-content-sha1", hashed_content)
            web_request.Headers.Add("Authorization", "GGE4_API " + KeyID + ":" + base64_hash)
            web_request.ContentLength = xml_string.Length

            'Write and send request Data
            Using Stream_writer As StreamWriter = New StreamWriter(web_request.GetRequestStream())
                Stream_writer.Write(xml_string)
            End Using

            ''Get Response and read into string
            Dim response_string As String = ""
            ''Create Table to be returned
            Dim resultDatatable As DataTable = Nothing
            Try
                Using web_response As HttpWebResponse = web_request.GetResponse()

                    Using response_stream As StreamReader = New StreamReader(web_response.GetResponseStream)
                        response_string = response_stream.ReadToEnd
                    End Using

                    ''laod XML
                    Dim xmldoc As XmlDocument = New XmlDocument()
                    xmldoc.LoadXml(response_string)
                    Dim nodelist As XmlNodeList = xmldoc.SelectNodes("TransactionResult")

                    'Create the datatable to return
                    resultDatatable = createDataTableFromXMLNodes(nodelist) 'Generates the datatable from the XML fields
                    Dim rowResult As DataRow = resultDatatable.NewRow

                    For Each node As XmlNode In nodelist.ItemOf(0).ChildNodes
                        If node.InnerText <> "" Then
                            rowResult(node.Name) = node.InnerText
                            response &= node.Name & " : " & node.InnerText
                            response &= "<br />"
                        End If

                    Next

                    ''Add row into the resultTable to be returned
                    resultDatatable.Rows.Add(rowResult)

                    ''Insert result into Audit table.
                    'insertIntoPNCGatewayResponseLog(response)
                End Using


            Catch SoapHdrEx As SoapHeaderException
                response = SoapHdrEx.ToString
            Catch SoapEx As SoapException
                response = SoapEx.ToString
            Catch ex As WebException
                If Not IsNothing(ex.Response) Then
                    Using responseEX As HttpWebResponse = ex.Response
                        Using reader As StreamReader = New StreamReader(responseEX.GetResponseStream)
                            response = reader.ReadToEnd
                        End Using
                    End Using
                End If
                resultDatatable = createDatableForErrorResponse(response)

                response = ex.ToString
            Catch ex As Exception
                response = ex.ToString
            End Try

            Return resultDatatable
        End Function

    End Class

    Public Sub insertIntoGatewayResponseLog(ByRef ResponseString As String)
        Dim strSQL As String = "Insert into GW_ResponseLog (response_string) values (' " & ResponseString.Replace("'", "''") & "')"
        g_IO_Execute_SQL(strSQL, False)
    End Sub

End Module