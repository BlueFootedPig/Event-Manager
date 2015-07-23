Imports System.Reflection

Public Class FunctionSyncBlock
    Inherits SignelExecutionBlockBase
    Implements IDisposable

    Private method As MethodInfo
    Public Delegate Sub MethodDelegate(sender As Object, e As EventArgs)

    Public Delegate Sub MethodDelegateNoParms()

    Public Sub New(methodsToBlock As MethodDelegateNoParms())
        MyBase.New()

        If methodsToBlock Is Nothing OrElse methodsToBlock.Count = 0 Then Throw New ArgumentNullException("methodsToBlock", "methodsToBlock cannot be Null or Empty.")

        For Each methodToBlock As MethodDelegateNoParms In methodsToBlock
            lockingObjects.Add(methodToBlock.Method)
        Next

    End Sub

    Public Sub New(methodsToBlock As MethodDelegate())
        MyBase.New()

        If methodsToBlock Is Nothing OrElse methodsToBlock.Count = 0 Then Throw New ArgumentNullException("methodsToBlock", "methodsToBlock cannot be Null or Empty.")

        For Each methodToBlock As MethodDelegate In methodsToBlock
            lockingObjects.Add(methodToBlock.Method)
        Next

    End Sub

    Public Sub New(methodToBlock As MethodDelegateNoParms)
        MyBase.New()

        If methodToBlock Is Nothing Then Throw New ArgumentNullException("methodToBlock", "methodToBlock cannot be Null or Empty.")

        lockingObjects.Add(methodToBlock.Method)
    End Sub

    Public Sub New(methodToBlock As MethodDelegate)
        MyBase.New()

        If methodToBlock Is Nothing Then Throw New ArgumentNullException("methodToBlock", "methodToBlock cannot be Null or Empty.")

        lockingObjects.Add(methodToBlock.Method)
    End Sub

    Public Overrides Function IsThereALock(self As Object, objectToCheck As Object) As Boolean


        Dim stackTrace As New StackTrace()
        Dim stackFrames As StackFrame() = stackTrace.GetFrames()
        For Each StackFrame As StackFrame In stackFrames
            If lockingObjects.Equals(StackFrame.GetMethod()) Then
                AddCallingFunction(self, StackFrame.GetMethod())
                Return True
            End If
        Next

        Return False

    End Function



End Class
