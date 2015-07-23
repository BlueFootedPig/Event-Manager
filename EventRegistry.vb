Imports System.Runtime.CompilerServices

Public Module EventRegistry
    Private ReadOnly registry As New Dictionary(Of Object, Integer)
    Private lockingObject As New Object()

    Private locks As New List(Of IBlock)

    ''' <summary>
    ''' Checks if there are any locks targeting calling function.
    ''' </summary>
    ''' <param name="sender">Sender from the event.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsThereALock(self As Object, sender As Object) As Boolean

        If self Is Nothing Then Throw New ArgumentNullException("self", "self cannot be null in case a synclock is in place.")

        For Each lock As IBlock In locks.OfType(Of ExemptionBlockBase)()
            If lock.IsThereALock(self, sender) Then Return False
        Next

        For Each lock As IBlock In locks
            If lock.IsThereALock(self, sender) Then Return True
        Next

        Return False

    End Function

    Friend Sub RegisterBlock(lock As IBlock)
        SyncLock (lockingObject)
            locks.Add(lock)
        End SyncLock
    End Sub

    Friend Sub UnRegisterBlock(lock As IBlock)
        SyncLock (lockingObject)
            locks.Remove(lock)
        End SyncLock

    End Sub


End Module

