Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Utils.Implementation

Namespace DevExpress.Utils
	Public NotInheritable Class Algorithms
		Private Sub New()
		End Sub
		Public Shared Function TopologicalSort(Of T)(ByVal sourceObjects As IList(Of T), ByVal comparer As IComparer(Of T)) As IList(Of T)
			Dim sorter As New TopologicalSorter(Of T)()
			Return sorter.Sort(sourceObjects, comparer)
		End Function
	End Class
End Namespace

Namespace DevExpress.Utils.Implementation
	#Region "TopologicalSorter"
Public Class TopologicalSorter(Of T)
	#Region "Node"
	Public Class Node
		Private refCount_Renamed As Integer
		Private next_Renamed As Node
		Public Sub New(ByVal refCount As Integer, ByVal [next] As Node)
			Me.refCount_Renamed = refCount
			Me.next_Renamed = [next]
		End Sub
		Public ReadOnly Property RefCount() As Integer
			Get
				Return refCount_Renamed
			End Get
		End Property
		Public ReadOnly Property [Next]() As Node
			Get
				Return next_Renamed
			End Get
		End Property
	End Class
	#End Region

	#Region "Fields"
	Private qLink_Renamed() As Integer
	Private nodes_Renamed() As Node
	Private sourceObjects_Renamed As IList(Of T)
	Private comparer_Renamed As IComparer(Of T)
	#End Region

	#Region "Properties"
	Protected Friend ReadOnly Property Nodes() As Node()
		Get
			Return nodes_Renamed
		End Get
	End Property
	Protected Friend ReadOnly Property QLink() As Integer()
		Get
			Return qLink_Renamed
		End Get
	End Property
	Protected ReadOnly Property Comparer() As IComparer(Of T)
		Get
			Return comparer_Renamed
		End Get
	End Property
	Protected Friend ReadOnly Property SourceObjects() As IList(Of T)
		Get
			Return sourceObjects_Renamed
		End Get
	End Property
	#End Region

	Protected Function GetComparer() As IComparer(Of T)
		If Comparer IsNot Nothing Then
			Return Comparer
		Else
			Return System.Collections.Generic.Comparer(Of T).Default
		End If
	End Function
	Protected Function IsDependOn(ByVal x As T, ByVal y As T) As Boolean
		Return GetComparer().Compare(x, y) > 0
	End Function
	Public Function Sort(ByVal sourceObjects As IList(Of T), ByVal comparer As IComparer(Of T)) As IList(Of T)
		Me.comparer_Renamed = comparer
		Return Sort(sourceObjects)
	End Function
	Public Function Sort(ByVal sourceObjects As IList(Of T)) As IList(Of T)
		Me.sourceObjects_Renamed = sourceObjects

		Dim n As Integer = sourceObjects.Count
		If n < 2 Then
			Return sourceObjects
		End If

		Initialize(n)
		CalculateRelations(sourceObjects)

		Dim r As Integer = FindNonRelatedNodeIndex()
		Dim result As IList(Of T) = ProcessNodes(r)
		If result.Count > 0 Then
			Return result
		Else
			Return sourceObjects
		End If

	End Function
	Protected Friend Sub Initialize(ByVal n As Integer)
		Dim count As Integer = n + 1
		Me.qLink_Renamed = New Integer(count - 1){}
		Me.nodes_Renamed = New Node(count - 1){}
	End Sub
	Protected Friend Sub CalculateRelations(ByVal sourceObjects As IList(Of T))
		Dim n As Integer = sourceObjects.Count
		For y As Integer = 0 To n - 1
			For x As Integer = 0 To n - 1
				If (Not IsDependOn(sourceObjects(y), sourceObjects(x))) Then
					Continue For
				End If
				Dim minIndex As Integer = x + 1
				Dim maxIndex As Integer = y + 1
				QLink(maxIndex) += 1
				Nodes(minIndex) = New Node(maxIndex, Nodes(minIndex))
			Next x
		Next y
	End Sub
	Protected Friend Function FindNonRelatedNodeIndex() As Integer
		Dim r As Integer = 0
		Dim n As Integer = SourceObjects.Count
		For i As Integer = 0 To n
			If QLink(i) = 0 Then
				QLink(r) = i
				r = i
			End If
		Next i
		Return r
	End Function
	Protected Overridable Function ProcessNodes(ByVal r As Integer) As IList(Of T)
		Dim n As Integer = sourceObjects_Renamed.Count
		Dim k As Integer = n

		Dim f As Integer = QLink(0)
		Dim result As New List(Of T)(n)
		Do While f > 0
			result.Add(sourceObjects_Renamed(f - 1))
			k -= 1

			Dim node As Node = Nodes(f)
			Do While node IsNot Nothing
				node = RemoveRelation(node, r)
			Loop
			f = QLink(f)
		Loop
		'Debug.Assert(k == 0);
		Return result

	End Function
	Private Function RemoveRelation(ByVal node As Node, ByRef r As Integer) As Node
		Dim suc_p As Integer = node.RefCount
		QLink(suc_p) -= 1

		If QLink(suc_p) = 0 Then
			QLink(r) = suc_p
			r = suc_p
		End If
		Return node.Next
	End Function
End Class
#End Region
End Namespace
