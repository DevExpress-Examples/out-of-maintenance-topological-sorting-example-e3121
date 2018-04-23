Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace TopologicalSort
	Public Class GraphNode
		Private linkedNodes_Renamed As New List(Of GraphNode)()
		Private id_Renamed As Object
		Public Sub New(ByVal id As Object)
			Me.id_Renamed = id
		End Sub
		Public ReadOnly Property LinkedNodes() As List(Of GraphNode)
			Get
				Return linkedNodes_Renamed
			End Get
		End Property
		Public ReadOnly Property Id() As Object
			Get
				Return id_Renamed
			End Get
		End Property
	End Class

	Public Class GraphNodeComparer
		Implements IComparer(Of GraphNode)
		#Region "IComparer<GraphNode> Members"

		Public Function Compare(ByVal x As GraphNode, ByVal y As GraphNode) As Integer Implements IComparer(Of GraphNode).Compare
			If x.LinkedNodes.Contains(y) Then
				Return -1
			End If
			If y.LinkedNodes.Contains(x) Then
				Return 1
			End If
			Return 0
		End Function

		#End Region
	End Class
End Namespace
