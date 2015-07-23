Public Class ClassBlock
    Inherits BlockBase
    Implements IDisposable

    Public Sub New(objectToBlock As Object)
        MyBase.New()

        If objectToBlock Is Nothing Then Throw New ArgumentNullException("objectToBlock", "objectToBlock cannot be Null or Empty.")

        lockingObjects.Add(objectToBlock)
    End Sub

    Public Sub New(objectsToBlock As Object())
        MyBase.New()

        If objectsToBlock Is Nothing OrElse objectsToBlock.Count = 0 Then Throw New ArgumentNullException("objectToBlock", "objectToBlock cannot be Null or Empty.")

        For Each item As Object In objectsToBlock
            lockingObjects.Add(item)
        Next
    End Sub

    Public Overrides Function IsThereALock(self As Object, objectToCheck As Object) As Boolean
        Return lockingObjects.Equals(objectToCheck)
    End Function



End Class
