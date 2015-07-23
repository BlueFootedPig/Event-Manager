Public Class ProgramBlock
    Inherits BlockBase
    Implements IDisposable

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overrides Function IsThereALock(self As Object, objectToCheck As Object) As Boolean
        Return True
    End Function


End Class
