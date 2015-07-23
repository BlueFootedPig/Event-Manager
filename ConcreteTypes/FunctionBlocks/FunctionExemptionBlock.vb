Imports System.Reflection

Public Class FunctionExemptionBlock
    Inherits BlockBase
    Implements IDisposable


    Public Delegate Sub MethodDelegate(sender As Object, e As EventArgs)
    Public Delegate Sub MethodDelegateNoParms()

    Public Sub New(methodsToBlock As MethodDelegateNoParms())
        MyBase.New()

        If methodsToBlock Is Nothing OrElse methodsToBlock.Count = 0 Then Throw New ArgumentNullException("methodsToBlock", "methodsToBlock cannot be Null or Empty.")

        For Each item As MethodDelegateNoParms In methodsToBlock
            lockingObjects.Add(item.Method)
        Next

    End Sub

    Public Sub New(methodToBlock As MethodDelegateNoParms)
        MyBase.New()

        If methodToBlock Is Nothing Then Throw New ArgumentNullException("methodToBlock", "methodToBlock cannot be Null or Empty.")

        lockingObjects.Add(methodToBlock.Method)
    End Sub

    Public Sub New(methodsToBlock As MethodDelegate())
        MyBase.New()
        For Each item As MethodDelegate In methodsToBlock
            lockingObjects.Add(item.Method)
        Next
    End Sub

    Public Sub New(methodToBlock As MethodDelegate)
        MyBase.New()
        lockingObjects.Add(methodToBlock.Method)
    End Sub

    Public Overrides Function IsThereALock(self As Object, objectToCheck As Object) As Boolean

        Dim returnValue As Boolean = False
        Dim stackTrace As New StackTrace()
        Dim stackFrames As StackFrame() = stackTrace.GetFrames()
        For Each StackFrame As StackFrame In stackFrames
            For Each lockCheck As Object In lockingObjects
                returnValue = returnValue OrElse lockingObjects.Equals(StackFrame.GetMethod())
            Next
        Next
        Return returnValue


    End Function


End Class
