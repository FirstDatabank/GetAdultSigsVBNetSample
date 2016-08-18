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

End Class
