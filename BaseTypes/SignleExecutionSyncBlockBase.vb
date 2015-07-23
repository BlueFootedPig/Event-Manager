Imports System.Reflection

Public Class SignelExecutionBlockBase
    Implements IBlock
    Implements IDisposable

    Public Sub New()
        RegisterBlock(Me)
    End Sub

    Friend lockingObjects As New List(Of Object)
    Friend callingFunctions As New List(Of KeyValuePair(Of Object, MethodInfo))

    Public Overridable Function IsThereALock(self As Object, objectToCheck As Object) As Boolean Implements IBlock.IsThereALock
        Return True
    End Function

    Friend Sub AddCallingFunction(ob As Object, method As MethodInfo)
        Dim keyToAdd As New KeyValuePair(Of Object, MethodInfo)(ob, method)
        If callingFunctions.Contains(keyToAdd) Then
            callingFunctions.Remove(keyToAdd)
        End If
        callingFunctions.Add(keyToAdd)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                EventRegistry.UnRegisterBlock(Me)

                For Each method As KeyValuePair(Of Object, MethodInfo) In callingFunctions
                    method.Value.Invoke(method.Key, method.Value.GetParameters())
                Next

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
