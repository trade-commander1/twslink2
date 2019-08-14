
Public Class Conversions
    Private Sub Conversions()
    End Sub
    Public Function StrValid(ByRef stringParam As String) As Boolean
        If stringParam Is Nothing Or stringParam.Length = 0 Then
            StrValid = False
        Else
            StrValid = True
        End If
    End Function

    Public Function Str2Int(ByRef stringParam As String) As Integer
        Dim ret As Long
        ret = 0
        If StrValid(stringParam) = True Then
            Try
                ret = System.Convert.ToInt32(stringParam)
            Catch ex As System.IO.IOException
            End Try
        End If
        Str2Int = ret

    End Function
    Public Function Str2Double(ByRef stringParam As String) As Double
        Dim ret As Double
        ret = 0.0
        If StrValid(stringParam) = True Then
            Try
                ret = System.Convert.ToDouble(stringParam)
            Catch ex As System.IO.IOException
            End Try
        End If
        Str2Double = ret
    End Function
End Class

