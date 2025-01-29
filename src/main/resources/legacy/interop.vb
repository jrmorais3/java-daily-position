
'------------------------------------------------------------------------------------------------------------------------------------
'INICIO DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------

Option Explicit

Dim BI                      As Worksheet
Dim BT                      As Worksheet
Dim ExtratoAtual            As Worksheet
Dim Data                    As String
Dim DataExtarto             As Date
Dim BDC                     As Worksheet
Dim Conta                   As String
Dim ContaPosicao            As String
Dim RRange                  As Range
Dim UltimaCell              As Long
Dim B                       As Integer
Dim V                       As Double

Sub FormatarExtratoBradesco()

Set BI = Worksheets("BASE IMPORTAÇÃO")
Set BDC = Worksheets("BD_Contas")
Set ExtratoAtual = ActiveSheet

On Error Resume Next
    ActiveSheet.ShowAllData
On Error GoTo 0

'------------------------------------------------------------------------------------------------------------------------------------
'Se o Extrato for Bradesco
'------------------------------------------------------------------------------------------------------------------------------------

If Mid(ActiveWorkbook.Name, 1, 8) = "BRADESCO" Then

Conta = Mid(Range("A7"), Application.WorksheetFunction.Search(" ", Range("A7"), Application.WorksheetFunction.Search("Conta", Range("A7"), 1)) + 1, Len(Range("A7")) - Application.WorksheetFunction.Search(" ", Range("A7"), Application.WorksheetFunction.Search("Conta", Range("A7"), 1)))

ActiveSheet.Shapes.Range(Array("Picture 1")).Select
Selection.Delete
Cells.UnMerge
Rows("1:8").Select
Selection.Delete

Range("A2").Select

Do While ActiveCell.Value <> "Total"

    ActiveCell.Offset(1, 0).Select

Loop

UltimaCell = ExtratoAtual.Cells(ExtratoAtual.Rows.Count, 1).End(xlUp).Row

Range(Selection, "A" & UltimaCell).Select
Selection.EntireRow.Delete

Range("A2").Select

Do While ActiveCell.Value <> ""
    
    If Format(ActiveCell.Value, "mm/dd/yyyy") > Format(Data, "mm/dd/yyyy") Then
        
        ActiveCell.EntireRow.Delete
    
    Else
    
        ActiveCell.Offset(1, 0).Select
    
    End If
    
Loop

ActiveCell.Value = Format(Data, "mm/dd/yyyy")
ActiveCell.Offset(0, 1).Value = "SALDO"
ActiveCell.Offset(0, 3).Value = ActiveCell.Offset(-1, 5).Value

Range("C:C").Select
Selection.ClearContents

Range("A2").Select

Do While ActiveCell.Value <> ""

    If ActiveCell.Value <> Data Then
    
        ActiveCell.EntireRow.Delete
        
    Else
    
        ActiveCell.Offset(1, 0).Select
    
    End If
    
Loop

Range("D2").Select

Do While ActiveCell.Offset(0, -3).Value <> ""

    If ActiveCell.Value = "" Then

        V = ActiveCell.Offset(0, 1).Value
        ActiveCell.Value = V
    
    End If
    
    ActiveCell.Offset(1, 0).Select

Loop

Range("E:F").Select
Selection.Delete

Range("A2").Select

'-----------------------------------------------------------------------------------
If ActiveCell.Value <> "" Then

    Set RRange = ExtratoAtual.Range(ExtratoAtual.Range("A2"), ExtratoAtual.Range("D" & ExtratoAtual.Rows.Count).End(xlUp))
                
        RRange.Copy Destination:=BI.Cells(BI.Rows.Count, 1).End(xlUp).Offset(1, 0)
                    
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
    
    BI.Select
    
        BI.Range("E" & BI.Cells(BI.Rows.Count, 5).End(xlUp).Row + 1, "E" & BI.Cells(BI.Rows.Count, 1).End(xlUp).Row).Select
        On Error Resume Next
            ContaPosicao = Application.WorksheetFunction.VLookup(Conta, BDC.Range("A:B"), 2, 0)
        If Err.Number Then
            
            Selection.Value = "-"
            
        Else
        
            Selection.Value = ContaPosicao
            
        End If
        
Else
        
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
        
End If

End If

End Sub


'------------------------------------------------------------------------------------------------------------------------------------
'FIM DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------

'------------------------------------------------------------------------------------------------------------------------------------
'INICIO DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------
Option Explicit

Dim BT                      As Worksheet
Dim ExtratoAtual            As Worksheet
Dim Data                    As String
Dim DataExtarto             As Date
Dim Conta                   As String
Dim ContaPosicao            As String
Dim RRange                  As Range
Dim UltimaCell              As Long
Dim B                       As Integer
Dim V                       As Double

Sub FormatarExtratoBBM()

Set ExtratoAtual = ActiveSheet

On Error Resume Next
    ActiveSheet.ShowAllData
On Error GoTo 0

'------------------------------------------------------------------------------------------------------------------------------------
'Se o Extrato for BBM
'------------------------------------------------------------------------------------------------------------------------------------

If Mid(ActiveSheet.Name, 1, 9) = "Banco BBM" Then

Conta = Right(Range("A1"), 8)

Cells.UnMerge
Rows("1:4").Select
Selection.Delete

For B = 1 To 2
    
    ExtratoAtual.Cells(ExtratoAtual.Rows.Count, 1).End(xlUp).Select
    ActiveCell.EntireRow.Delete
    
Next B

Range("A3").Select

Do While ActiveCell.Value <> ""
    
    If Format(ActiveCell.Value, "mm/dd/yyyy") > Format(Data, "mm/dd/yyyy") Then
        
        ActiveCell.EntireRow.Delete
    
    Else
    
        ActiveCell.Offset(1, 0).Select
    
    End If
    
Loop

Range("C2").Select

Do While ActiveCell.Offset(0, -2) <> ""
    
    V = ActiveCell.Value
    ActiveCell.Value = V
    ActiveCell.Offset(1, 0).Select

Loop

ExtratoAtual.Cells(ExtratoAtual.Rows.Count, 1).End(xlUp).Offset(1, 0).Select

ActiveCell.Value = Format(Data, "mm/dd/yyyy")
ActiveCell.Offset(0, 1).Value = "SALDO"
V = Application.WorksheetFunction.Sum(ExtratoAtual.Range("C2", "C" & ExtratoAtual.Cells(ExtratoAtual.Rows.Count, 1).End(xlUp).Row))

If Round(V, 2) = 0 Then

    ActiveCell.Offset(0, 2).Value = 0

Else

    ActiveCell.Offset(0, 2).Value = Round(V, 2)

End If

Range("C:C").Select
Selection.Insert Shift:=xlToRight

Range("A2").Select

Do While ActiveCell.Value <> ""

    If ActiveCell.Value <> Data Then
    
        ActiveCell.EntireRow.Delete
        
    Else
    
        ActiveCell.Offset(1, 0).Select
    
    End If
    
Loop

Range("A2").Select

'-----------------------------------------------------------------------------------
If ActiveCell.Value <> "" Then

    Set RRange = ExtratoAtual.Range(ExtratoAtual.Range("A2"), ExtratoAtual.Range("D" & ExtratoAtual.Rows.Count).End(xlUp))
                
        RRange.Copy Destination:=BI.Cells(BI.Rows.Count, 1).End(xlUp).Offset(1, 0)
                    
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
    
    BI.Select
    
        BI.Range("E" & BI.Cells(BI.Rows.Count, 5).End(xlUp).Row + 1, "E" & BI.Cells(BI.Rows.Count, 1).End(xlUp).Row).Select
        On Error Resume Next
            ContaPosicao = Application.WorksheetFunction.VLookup(Conta, BDC.Range("A:B"), 2, 0)
        If Err.Number Then
            
            Selection.Value = "-"
            
        Else
        
            Selection.Value = ContaPosicao
            
        End If
        
Else
        
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
        
End If

End If

End Sub

'------------------------------------------------------------------------------------------------------------------------------------
'FIM DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------

'------------------------------------------------------------------------------------------------------------------------------------
'INICIO DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------

Option Explicit

Dim BI                      As Worksheet
Dim BT                      As Worksheet
Dim ExtratoAtual            As Worksheet
Dim Data                    As String
Dim DataExtarto             As Date
Dim BDC                     As Worksheet
Dim Conta                   As String
Dim ContaPosicao            As String
Dim RRange                  As Range
Dim UltimaCell              As Long
Dim B                       As Integer
Dim V                       As Double

Sub FormatarExtratoSafra()

Set BI = Worksheets("BASE IMPORTAÇÃO")
Set BDC = Worksheets("BD_Contas")
Set ExtratoAtual = ActiveSheet

On Error Resume Next
    ActiveSheet.ShowAllData
On Error GoTo 0

'------------------------------------------------------------------------------------------------------------------------------------
'Se o Extrato for Safra
'------------------------------------------------------------------------------------------------------------------------------------
    
If Mid(ActiveWorkbook.Name, 1, 5) = "SAFRA" Then

Conta = Mid(Int(Separa(Right(Range("C3"), 10), 1)), 1, Len(Int(Separa(Right(Range("C3"), 10), 1))) - 1) & "-" & Right(Int(Separa(Right(Range("C3"), 10), 1)), 1)

Rows("1:11").Select
Selection.Delete
ActiveSheet.Shapes.Range(Array("Picture 1")).Select
Selection.Delete
Range("B:C,E:E").Select
Selection.Delete
Range("C:C").Select
Selection.ClearContents

Range("A2").Select

Do While ActiveCell.Value <> ""
    
    DataExtarto = ActiveCell.Value
    ActiveCell.Value = Format(DataExtarto, "mm/dd/yyyy")
    ActiveCell.Offset(1, 0).Select
    
Loop

Range("A2").Select

Do While ActiveCell.Value <> ""

    If ActiveCell.Value <> Data Then
    
        ActiveCell.EntireRow.Delete
    
    Else
    
        ActiveCell.Offset(1, 0).Select
    
    End If

Loop

Range("E2").Select

Do While ActiveCell.Offset(0, -4) <> ""

    If ActiveCell.Value <> "" And ActiveCell.Value <> 0 Then
        
        V = ActiveCell.Value
        ActiveCell.Offset(0, -1).Value = V
    
    End If
    
ActiveCell.Offset(1, 0).Select

Loop

Range("E:E").Select
Selection.Delete

Range("D2").Select

Do While ActiveCell.Offset(0, -3).Value <> ""

    If ActiveCell.Value = "" Then
    
        ActiveCell.EntireRow.Delete
    
    Else
    
        ActiveCell.Offset(1, 0).Select
    
    End If

Loop

Range("A2").Select

If ActiveCell.Value <> "" Then

    Set RRange = ExtratoAtual.Range(ExtratoAtual.Range("A2"), ExtratoAtual.Range("D" & ExtratoAtual.Rows.Count).End(xlUp))
                
        RRange.Copy Destination:=BI.Cells(BI.Rows.Count, 1).End(xlUp).Offset(1, 0)
                    
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
    
    BI.Select
    
        BI.Range("E" & BI.Cells(BI.Rows.Count, 5).End(xlUp).Row + 1, "E" & BI.Cells(BI.Rows.Count, 1).End(xlUp).Row).Select
        On Error Resume Next
            ContaPosicao = Application.WorksheetFunction.VLookup(Conta, BDC.Range("A:B"), 2, 0)
        If Err.Number Then
            
            Selection.Value = "-"
            
        Else
        
            Selection.Value = ContaPosicao
            
        End If
        
Else
        
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
        
End If

End If

End Sub
'------------------------------------------------------------------------------------------------------------------------------------
'FIM DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------
'------------------------------------------------------------------------------------------------------------------------------------
'INICIO DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------
Option Explicit

Dim BI                      As Worksheet
Dim BT                      As Worksheet
Dim ExtratoAtual            As Worksheet
Dim Data                    As String
Dim DataExtarto             As Date
Dim BDC                     As Worksheet
Dim Conta                   As String
Dim ContaPosicao            As String
Dim RRange                  As Range
Dim UltimaCell              As Long
Dim B                       As Integer
Dim V                       As Double

Sub FormatarExtratoSantander()

Set BI = Worksheets("BASE IMPORTAÇÃO")
Set BDC = Worksheets("BD_Contas")
Set ExtratoAtual = ActiveSheet

On Error Resume Next
    ActiveSheet.ShowAllData
On Error GoTo 0

'------------------------------------------------------------------------------------------------------------------------------------
'Se o Extrato for Santander
'------------------------------------------------------------------------------------------------------------------------------------
    
If Mid(ActiveSheet.Name, 1, 9) = "SANTANDER" Then

Conta = Mid(ActiveSheet.Name, 11, 11)

Rows("1:3").Select
Selection.Delete
Range("B:B,F:F").Select
Selection.Delete
Range("C:C").Select
Selection.ClearContents

Range("A2").Select

Do While ActiveCell.Value <> ""

    If ActiveCell.Value <> Data Then
    
        ActiveCell.EntireRow.Delete
    
    Else
    
        ActiveCell.Offset(1, 0).Select
    
    End If

Loop

Range("D2").Select

Do While ActiveCell.Offset(0, -3).Value <> ""

    If ActiveCell.Value = "" Then
    
        ActiveCell.EntireRow.Delete
    
    Else
    
        ActiveCell.Offset(1, 0).Select
    
    End If

Loop

Range("A2").Select

If ActiveCell.Value <> "" Then

    Set RRange = ExtratoAtual.Range(ExtratoAtual.Range("A2"), ExtratoAtual.Range("D" & ExtratoAtual.Rows.Count).End(xlUp))
                
        RRange.Copy Destination:=BI.Cells(BI.Rows.Count, 1).End(xlUp).Offset(1, 0)
                    
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
    
    BI.Select
    
        BI.Range("E" & BI.Cells(BI.Rows.Count, 5).End(xlUp).Row + 1, "E" & BI.Cells(BI.Rows.Count, 1).End(xlUp).Row).Select
        On Error Resume Next
            ContaPosicao = Application.WorksheetFunction.VLookup(Conta, BDC.Range("A:B"), 2, 0)
        If Err.Number Then
            
            Selection.Value = "-"
            
        Else
        
            Selection.Value = ContaPosicao
            
        End If
        
Else
        
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
        
End If

End If

End Sub

'------------------------------------------------------------------------------------------------------------------------------------
'FIM DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------
'------------------------------------------------------------------------------------------------------------------------------------
'INICIO DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------
Option Explicit

Dim BI                      As Worksheet
Dim BT                      As Worksheet
Dim ExtratoAtual            As Worksheet
Dim Data                    As String
Dim DataExtarto             As Date
Dim BDC                     As Worksheet
Dim Conta                   As String
Dim ContaPosicao            As String
Dim RRange                  As Range
Dim UltimaCell              As Long
Dim B                       As Integer
Dim V                       As Double

Sub FormatarExtratoVotorantim()

Set BI = Worksheets("BASE IMPORTAÇÃO")
Set BDC = Worksheets("BD_Contas")
Set ExtratoAtual = ActiveSheet

On Error Resume Next
    ActiveSheet.ShowAllData
On Error GoTo 0

'------------------------------------------------------------------------------------------------------------------------------------
'Se o Extrato for VOTORANTIM
'------------------------------------------------------------------------------------------------------------------------------------

If Mid(ActiveWorkbook.Name, 1, 10) = "VOTORANTIM" Then

Dim ContaVot       As String

ContaVot = Replace(Right(Range("B18"), Len(Range("B18")) - Application.WorksheetFunction.Search(" ", Range("B18"), 14)), ".", "")

Cells.UnMerge
Rows("1:19").Delete
Range("A:A,C:E,G:G,I:K,M:N,P:R,T:V").Delete
Range("C:C").ClearContents

UltimaCell = ExtratoAtual.Cells(ExtratoAtual.Rows.Count, 1).End(xlUp).Row

Range("E2").Select

Do While ActiveCell.Offset(0, -4).Value <> ""

    If ActiveCell.Value = "" Then
        
        ActiveCell.Value = ActiveCell.Offset(0, -1).Value * -1
        
    End If
    
    ActiveCell.Offset(1, 0).Select
    
Loop

Range("D:D").Delete

Range("A2").Select

For B = 1 To UltimaCell - 1

    If Not IsDate(ActiveCell) Then
    
        ActiveCell.EntireRow.Delete
        
    Else
    
        ActiveCell.Offset(1, 0).Select
    
    End If
    
Next B

ExtratoAtual.Cells(ExtratoAtual.Rows.Count, 1).End(xlUp).Offset(1, 0).Select
ActiveCell.Value = Format(Data, "mm/dd/yyyy")
ActiveCell.Offset(0, 1).Value = "SALDO"
ActiveCell.Offset(0, 3).Value = ActiveCell.Offset(-1, 4).Value
Range("E:E").Delete
Cells.EntireRow.AutoFit
Cells.EntireColumn.AutoFit

UltimaCell = ExtratoAtual.Cells(ExtratoAtual.Rows.Count, 1).End(xlUp).Row

Range("A2").Select

For B = 1 To UltimaCell - 1

    If ActiveCell.Value <> Data Then
    
        ActiveCell.EntireRow.Delete
        
    Else
    
        ActiveCell.Offset(1, 0).Select
    
    End If
    
Next B



Range("A2").Select

'-----------------------------------------------------------------------------------
If ActiveCell.Value <> "" Then

    Set RRange = ExtratoAtual.Range(ExtratoAtual.Range("A2"), ExtratoAtual.Range("D" & ExtratoAtual.Rows.Count).End(xlUp))
                
        RRange.Copy Destination:=BI.Cells(BI.Rows.Count, 1).End(xlUp).Offset(1, 0)
                    
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
    
    BI.Select
    
        BI.Range("E" & BI.Cells(BI.Rows.Count, 5).End(xlUp).Row + 1, "E" & BI.Cells(BI.Rows.Count, 1).End(xlUp).Row).Select
        On Error Resume Next
            ContaPosicao = Application.WorksheetFunction.VLookup(Conta, BDC.Range("A:B"), 2, 0)
        If Err.Number Then
            
            Selection.Value = "-"
            
        Else
        
            Selection.Value = ContaPosicao
            
        End If
        
Else
        
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
        
End If

End If

End Sub
'------------------------------------------------------------------------------------------------------------------------------------
'FIM DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------
'------------------------------------------------------------------------------------------------------------------------------------
'INICIO DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------
Option Explicit

Dim BI                      As Worksheet
Dim BT                      As Worksheet
Dim ExtratoAtual            As Worksheet
Dim Data                    As String
Dim DataExtarto             As Date
Dim BDC                     As Worksheet
Dim Conta                   As String
Dim ContaPosicao            As String
Dim RRange                  As Range
Dim UltimaCell              As Long
Dim B                       As Integer
Dim V                       As Double

Sub FormatarExtratoItaú()

Set BI = Worksheets("BASE IMPORTAÇÃO")
Set BDC = Worksheets("BD_Contas")
Set ExtratoAtual = ActiveSheet

On Error Resume Next
    ActiveSheet.ShowAllData
On Error GoTo 0

'------------------------------------------------------------------------------------------------------------------------------------
'Se o Extrato for Itaú
'------------------------------------------------------------------------------------------------------------------------------------
    
If Mid(ActiveSheet.Name, 1, 4) = "ITAÚ" Then

Conta = Mid(ActiveSheet.Name, 6, 7)

ActiveSheet.Shapes.Range(Array("Picture 1")).Select
Selection.Delete
Rows("1:6").Select
Selection.Delete
Columns("A:A").Select
Selection.Delete
Columns("B:C").Select
Selection.Delete
Columns("C:C").Select
Selection.ClearContents
Cells.Select
Cells.EntireColumn.AutoFit
      
Range("A2").Select

Do While ActiveCell.Value <> ""
                       
    If ActiveCell.Value <> Data Then
        
        Selection.EntireRow.Delete
                    
    Else
        
        ActiveCell.Offset(1, 0).Select
                    
    End If
                
Loop

UltimaCell = Cells(Rows.Count, 1).End(xlUp).Row

Range("E2").Select
        
For B = 1 To UltimaCell

    If ActiveCell.Value <> "" Then
        
        ActiveCell.Offset(0, -1).Value = ActiveCell.Value
    
    End If
    
    ActiveCell.Offset(1, 0).Select
    
Next B
        
Columns("E:E").Select
Selection.Delete
        
Range("D2").Select

Do While ActiveCell.Offset(0, -3).Value <> ""
               
    If ActiveCell.Value = "" Then
        
        Selection.EntireRow.Delete
                    
    Else
        
        ActiveCell.Offset(1, 0).Select
                    
    End If
               
Loop

ExtratoAtual.Range("A2", "A" & ExtratoAtual.Cells(Rows.Count, 1).End(xlUp).Row).Select
Selection.NumberFormat = "dd/mm/yyyy"
Range("A2").Select

If ActiveCell.Value <> "" Then

    Set RRange = ExtratoAtual.Range(ExtratoAtual.Range("A2"), ExtratoAtual.Range("D" & ExtratoAtual.Rows.Count).End(xlUp))
                
        RRange.Copy Destination:=BI.Cells(BI.Rows.Count, 1).End(xlUp).Offset(1, 0)
                    
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
    
    BI.Select
    
        BI.Range("E" & BI.Cells(BI.Rows.Count, 5).End(xlUp).Row + 1, "E" & BI.Cells(BI.Rows.Count, 1).End(xlUp).Row).Select
        On Error Resume Next
            ContaPosicao = Application.WorksheetFunction.VLookup(Conta, BDC.Range("A:B"), 2, 0)
        If Err.Number Then
            
            Selection.Value = "-"
            
        Else
        
            Selection.Value = ContaPosicao
            
        End If
        
Else
        
    Application.DisplayAlerts = False
        ActiveWorkbook.Close savechanges:=False
    Application.DisplayAlerts = False
        
End If

End If

End Sub
'------------------------------------------------------------------------------------------------------------------------------------
'FIM DO CÓDIGO
'------------------------------------------------------------------------------------------------------------------------------------




