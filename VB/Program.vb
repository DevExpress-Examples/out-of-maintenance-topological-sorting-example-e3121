Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace TopologicalSort
	Friend Class Program

		Shared Sub Main(ByVal args() As String)
			DoDXTopologicalSort()
		End Sub
		Private Shared Sub DoDXTopologicalSort()
			Console.WriteLine("DX Topological Sorter")
			Console.WriteLine(New String("="c, 21))
			Console.WriteLine("Nodes:")
			Dim list() As GraphNode = PrepareNodes()
			PrintNodes(list)

			Dim comparer As IComparer(Of GraphNode) = New GraphNodeComparer()

			Dim sortedNodes As IList(Of GraphNode) = DevExpress.Utils.Algorithms.TopologicalSort(Of GraphNode)(list, comparer)

			Console.WriteLine("Sorted nodes:")
			PrintNodes(sortedNodes)

			Console.Read()
		End Sub
		Private Shared Sub PrintNodes(ByVal list As IList(Of GraphNode))
			For i As Integer = 0 To list.Count - 1
				Dim s As String = String.Empty
				If i > 0 Then
					s = "->"
				End If
				s &= list(i).Id.ToString()
				Console.Write(s)
			Next i
			Console.WriteLine(Constants.vbCrLf)
		End Sub
		Private Shared Function PrepareNodes() As GraphNode()
			Dim nodeA As New GraphNode("A")
			Dim nodeB As New GraphNode("B")
			Dim nodeC As New GraphNode("C")
			Dim nodeD As New GraphNode("D")
			Dim nodeE As New GraphNode("E")

			nodeA.LinkedNodes.AddRange(New GraphNode() { nodeB, nodeC, nodeE })
			nodeB.LinkedNodes.Add(nodeD)
			nodeC.LinkedNodes.AddRange(New GraphNode() { nodeD, nodeE })
			nodeD.LinkedNodes.Add(nodeE)

			Return New GraphNode() { nodeD, nodeA, nodeC, nodeE, nodeB }
		End Function
	End Class
End Namespace
