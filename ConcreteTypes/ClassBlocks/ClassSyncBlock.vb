Imports System.Reflection

Public Class ClassSyncBlock
    Inherits SignelExecutionBlockBase



    Public Sub New(objectToBlock As Object)
        MyBase.New()

        If objectToBlock Is Nothing Then Throw New ArgumentNullException("objectToBlock", "objectToBlock cannot be Null or Empty.")

        lockingObjects.Add(objectToBlock)
    End Sub

    Public Sub New(objectsToBlock As Object())
        MyBase.New()

        If objectsToBlock Is Nothing OrElse objectsToBlock.Count = 0 Then Throw New ArgumentNullException("objectToBlock", "objectsToBlock cannot be Null or Empty.")

        For Each item As Object In objectsToBlock
            lockingObjects.Add(item)
        Next
    End Sub

    Public Overrides Function IsThereALock(self As Object, objectToCheck As Object) As Boolean
        If lockingObjects.Equals(objectToCheck) Then
            Dim stackTrace As New StackTrace()
            Dim stackFrames As StackFrame() = stackTrace.GetFrames()
            AddCallingFunction(self, stackFrames.Skip(3).First().GetMethod())
            Return True
        End If
        Return False
    End Function


End Class
