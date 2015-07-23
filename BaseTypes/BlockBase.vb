Public Class BlockBase
    Implements IBlock
    Implements IDisposable

    Friend lockingObjects As New List(Of Object)

    Public Sub New()
        RegisterBlock(Me)
    End Sub

    Public Overridable Function IsThereALock(self As Object, objectToCheck As Object) As Boolean Implements IBlock.IsThereALock
        Return True
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                EventRegistry.UnRegisterBlock(Me)
            End If

        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
