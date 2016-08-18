Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim svc As OkAmbClient2.OKAmbulatoryServiceClient
        Dim request As OkAmbClient2.GetAdultSigsByPrescribableDrugRequest
        Dim loScope As System.ServiceModel.OperationContextScope
        Dim lsAuthHeaderValue As String = ""
        Dim loHttpRequestProperty As System.ServiceModel.Channels.HttpRequestMessageProperty
        Dim response As OkAmbClient2.GetAdultSigsByPrescribableDrugResponse

        Const CLIENT_ID As String = ""
        Const SECRET As String = ""

        Try
            svc = New OkAmbClient2.OKAmbulatoryServiceClient
            loScope = New System.ServiceModel.OperationContextScope(svc.InnerChannel)

            lsAuthHeaderValue = String.Format("{0} {1}:{2}", "SHAREDKEY", CLIENT_ID, SECRET)
            loHttpRequestProperty = New System.ServiceModel.Channels.HttpRequestMessageProperty()
            loHttpRequestProperty.Headers.Add(System.Net.HttpRequestHeader.Authorization, lsAuthHeaderValue)
            System.ServiceModel.OperationContext.Current.OutgoingMessageProperties.Add(loHttpRequestProperty.Name, loHttpRequestProperty)
            request = New OkAmbClient2.GetAdultSigsByPrescribableDrugRequest

            request.CallContext = New OkAmbClient2.CallContext
            request.CallContext.CallSystemName = "xyz pqr"

            request.PrescribableDrugID = "233200"
            request.SigTypeCode = 2

            request.Limit = 10
            request.Offset = 1

            response = svc.GetAdultSigsByPrescribableDrug(request)

            Debug.WriteLine(String.Format("Returned {0} of {1} items", response.Items.Count, response.TotalResultCount))
            MessageBox.Show(String.Format("Returned {0} of {1} items", response.Items.Count, response.TotalResultCount))

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            MessageBox.Show(ex.Message)
        End Try
    End Sub





    'Public Shared Function AllergenPicklistItems(ByVal search As String, Optional ByVal phonetics As Boolean = True, Optional ByVal simple As Boolean = False) As List(Of allergyInfo)
    '    Dim loFDBClient As FDBDrugAllergyService.DrugAllergyServiceClient
    '    Dim loScope As System.ServiceModel.OperationContextScope
    '    Dim lsAuthHeaderValue As String = ""
    '    Dim loHttpRequestProperty As System.ServiceModel.Channels.HttpRequestMessageProperty
    '    Dim loRequest As FDBDrugAllergyService.SearchAllergenPicklistRequest
    '    Dim loResults As List(Of allergyInfo)
    '    Dim loAllergyInfo As allergyInfo
    '    Dim loExtRequest As FDBDrugAllergyService.GetExternalAllergenTargetRequest
    '    Dim loExtResponse As FDBDrugAllergyService.GetExternalAllergenTargetResponse
    '    Dim loAllergenConcepts() As FDBDrugAllergyService.FDBAllergenConcept

    '    Try
    '        If Common.convertToString(search, Common.outputType.stringType).Length > 0 Then
    '            loFDBClient = New FDBDrugAllergyService.DrugAllergyServiceClient
    '            loScope = New System.ServiceModel.OperationContextScope(loFDBClient.InnerChannel)

    '            lsAuthHeaderValue = String.Format("{0} {1}:{2}", ihcSettings.appSettings.fdbAuthScheme, ihcSettings.appSettings.fdbClientID, ihcSettings.appSettings.fdbSecret)

    '            loHttpRequestProperty = New System.ServiceModel.Channels.HttpRequestMessageProperty()

    '            loHttpRequestProperty.Headers.Add(System.Net.HttpRequestHeader.Authorization, lsAuthHeaderValue)

    '            System.ServiceModel.OperationContext.Current.OutgoingMessageProperties.Add(loHttpRequestProperty.Name, loHttpRequestProperty)

    '            loRequest = New FDBDrugAllergyService.SearchAllergenPicklistRequest

    '            loRequest.CallContext = New FDBDrugAllergyService.CallContext

    '            loRequest.CallContext.CallSystemName = "Allergy PickList Search"

    '            loRequest.Limit = 10
    '            loRequest.Offset = 1

    '            If phonetics Then
    '                loRequest.PhoneticSearch = "Phonetic"
    '            Else
    '                loRequest.PhoneticSearch = "NoPhonetic"
    '            End If

    '            If simple Then
    '                loRequest.SearchType = "TextEqual"
    '            Else
    '                loRequest.SearchType = "StartsWith"
    '            End If

    '            loRequest.SearchText = search

    '            For Each loPickList As FDBDrugAllergyService.AllergenPicklist In loFDBClient.SearchAllergenPicklist(loRequest).Items

    '                If loResults Is Nothing Then
    '                    loResults = New List(Of allergyInfo)
    '                End If

    '                loAllergyInfo = New allergyInfo
    '                loAllergyInfo.HL7ObjectIdentifier = loPickList.HL7ObjectIdentifier
    '                loAllergyInfo.HL7ObjectIdentifierType = loPickList.HL7ObjectIdentifierType
    '                loAllergyInfo.PicklistDesc = loPickList.PicklistDesc
    '                loAllergyInfo.PicklistID = loPickList.PicklistID

    '                Select Case loPickList.PicklistConceptType.ToUpper.Trim
    '                    Case "INGREDIENT"
    '                        loAllergyInfo.PicklistConceptType = "Ingredient"
    '                        loAllergyInfo.AllergyGroupID = "6"
    '                    Case "DRUGNAME"
    '                        loAllergyInfo.PicklistConceptType = "Drug Name"
    '                        loAllergyInfo.AllergyGroupID = "2"
    '                    Case "ALLERGENGROUP"
    '                        loAllergyInfo.PicklistConceptType = "Allergen Group"
    '                        loAllergyInfo.AllergyGroupID = "1"
    '                End Select

    '                loExtRequest = New FDBDrugAllergyService.GetExternalAllergenTargetRequest

    '                loExtRequest.CallContext = New FDBDrugAllergyService.CallContext
    '                loExtRequest.CallContext.CallSystemName = "Get External Allergen Info"


    '                ReDim loAllergenConcepts(0)
    '                loAllergenConcepts(0) = New FDBDrugAllergyService.FDBAllergenConcept
    '                loAllergenConcepts(0).ConceptID = loPickList.PicklistID
    '                loAllergenConcepts(0).ConceptType = loPickList.PicklistConceptType

    '                loExtRequest.FDBAllergenConcepts = loAllergenConcepts

    '                loExtRequest.Limit = 10
    '                loExtRequest.Offset = 1

    '                loExtResponse = loFDBClient.TranslateFDBAllergenToExternalAllergen(loExtRequest)

    '                For Each loExtAllergenTarget As FDBDrugAllergyService.ExternalAllergenTarget In loExtResponse.Items
    '                    If String.Compare(loExtAllergenTarget.HL7ObjectIdentifier, "2.16.840.1.113883.6.88", True) = 0 Then
    '                        loAllergyInfo.RxNorm = loExtAllergenTarget.RxCUI
    '                        Exit For
    '                    End If
    '                Next

    '                loResults.Add(loAllergyInfo)

    '                loAllergyInfo = Nothing
    '            Next

    '            Return loResults
    '        Else
    '            Return Nothing
    '        End If
    '    Catch ex As Exception
    '        logic.doh(ex)
    '        Return Nothing
    '    End Try
    'End Function

End Class
