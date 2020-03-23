Public Class Form1
    REM declaring main variables
    Dim b, d, Fcu As Single
    REM declaring shear variables
    Dim Qu_max, qact, q_uncracked, q_cracked, q_max, qst, a, Fy, S, n As Double
    Dim x As Integer
    REM declaring moment variables
    Dim Mu, Ku, um, us, Asm, As_m, As_s, dm, n_m, mkus, method, um100 As Double
    Dim c1, j, Fym As Double
    Dim dn As Integer
    Dim um_table As New Dictionary(Of Double, Integer)
    Dim us_table As New Dictionary(Of Double, Integer)

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 3
        ComboBox2.SelectedIndex = 1
        ComboBox3.SelectedIndex = 0
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 45 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 45 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 45 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 45 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 45 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Sub moment(ByVal dn, ByVal dm)
        If Ku < 0.8 Then
            um = um_table.Item(0.8) / 100
        End If
        If Ku >= 0.8 And Ku <= 5.0 Then
            um = um_table.Item(Ku) / 100
        End If
        If Ku > 5.0 Then
            um = um_table.Item(5.0) / 100
        End If
        Asm = (1.1 / 360) * b * d
        As_m = (um / 100) * b * d
        If As_m < Asm Then
            As_m = Asm
        End If
        n_m = Math.Ceiling(As_m / dm)
        If Ku < mkus Then
            us = 0.0
        End If
        If Ku >= mkus And Ku <= 5.0 Then
            us = us_table.Item(Ku) / 100
        End If
        If Ku > 5.0 Then
            us = us_table.Item(5.0) / 100
        End If
        As_s = (us / 100) * b * d
        REM rounding values for output
        As_m = Math.Round(As_m, 2)
        As_s = Math.Round(As_s, 2)
        If us = 0.0 Then
            MessageBox.Show("Ku= " & Ku & vbCrLf & "μ= " & um & vbCrLf & "μ'= " & us _
                        & vbCrLf & "As= " & As_m & vbCrLf & "As'= " & As_s _
                        & vbCrLf & "Main Reinforcment: " & n_m & "∅" & dn _
                        & vbCrLf & "No secondary reinforcment needed.", "Solution")
        Else
            MessageBox.Show("Ku= " & Ku & vbCrLf & "μ= " & um & vbCrLf & "μ'= " & us _
                        & vbCrLf & "As= " & As_m & vbCrLf & "As'= " & As_s _
                        & vbCrLf & "Main Reinforcment: " & n_m & "∅" & dn, "Solution")
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0
                dn = 10
                dm = 79
            Case 1
                dn = 12
                dm = 113
            Case 2
                dn = 14
                dm = 154
            Case 3
                dn = 16
                dm = 201
            Case 4
                dn = 18
                dm = 255
            Case 5
                dn = 20
                dm = 314
            Case 6
                dn = 22
                dm = 380
            Case 7
                dn = 25
                dm = 491
        End Select
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Select Case ComboBox2.SelectedIndex
            Case 0
                method = 0
            Case 1
                method = 1
            Case 2
                method = 2
            Case 3
                method = 3
            Case 4
                method = 4
        End Select
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" _
        Or TextBox4.Text = "" Then
            MessageBox.Show("Looks like you missed a required value there, Check again!", "Missing Values!")
        Else
            b = TextBox1.Text
            d = TextBox2.Text
            Fcu = TextBox3.Text
            Mu = TextBox4.Text
            If Fcu = 20 Or Fcu = 22.5 Or Fcu = 25 Or Fcu = 27.5 Or Fcu = 30 Then
                Mu = Math.Abs(Mu)
                If b <= 0 Or d <= 0 Or Fcu <= 0 Or Mu <= 0 Then
                    MessageBox.Show("Unexpected Value", "Error")
                Else
                    If method = 0 Then
                        c1 = 3.5
                        j = 0.78
                        Fym = 360
                        d = c1 * Math.Sqrt((Mu * 10 ^ 6) / (Fcu * b))
                        d = Math.Ceiling(d / 50) * 50
                        As_m = (Mu * 10 ^ 6) / (Fym * d * j)
                        n_m = Math.Ceiling(As_m / dm)
                        REM rounding values for output
                        As_m = Math.Round(As_m, 2)
                        MessageBox.Show("b= " & b & vbCrLf & "d= " & d & vbCrLf & "As= " & As_m _
                                        & vbCrLf & "Main Reinforcment: " & n_m & "∅" & dn, "Solution")
                    End If
                    If method = 1 Or method = 2 Or method = 3 Or method = 4 Then
                        If Fcu = 20 Then
                            um_table.Clear()
                            um_table.Add(0.8, 27)
                            um_table.Add(0.9, 30)
                            um_table.Add(1.0, 34)
                            um_table.Add(1.1, 38)
                            um_table.Add(1.2, 41)
                            um_table.Add(1.3, 45)
                            um_table.Add(1.4, 49)
                            um_table.Add(1.5, 53)
                            um_table.Add(1.6, 57)
                            um_table.Add(1.7, 61)
                            um_table.Add(1.8, 65)
                            um_table.Add(1.9, 69)
                            um_table.Add(2.0, 73)
                            um_table.Add(2.1, 78)
                            um_table.Add(2.2, 82)
                            um_table.Add(2.3, 87)
                            um_table.Add(2.4, 91)
                            um_table.Add(2.5, 96)
                            um_table.Add(2.6, 100)
                            um_table.Add(2.7, 104)
                            um_table.Add(2.8, 107)
                            um_table.Add(2.9, 110)
                            um_table.Add(3.0, 114)
                            um_table.Add(3.1, 117)
                            um_table.Add(3.2, 120)
                            um_table.Add(3.3, 124)
                            um_table.Add(3.4, 127)
                            um_table.Add(3.5, 130)
                            um_table.Add(3.6, 134)
                            um_table.Add(3.7, 137)
                            um_table.Add(3.8, 141)
                            um_table.Add(3.9, 144)
                            um_table.Add(4.0, 147)
                            um_table.Add(4.1, 151)
                            um_table.Add(4.2, 154)
                            um_table.Add(4.3, 157)
                            um_table.Add(4.4, 161)
                            um_table.Add(4.5, 164)
                            um_table.Add(4.6, 167)
                            um_table.Add(4.7, 171)
                            um_table.Add(4.8, 174)
                            um_table.Add(4.9, 177)
                            um_table.Add(5.0, 181)
                            um100 = 2.6
                            us_table.Clear()
                            us_table.Add(2.7, 4)
                            us_table.Add(2.8, 7)
                            us_table.Add(2.9, 10)
                            us_table.Add(3.0, 14)
                            us_table.Add(3.1, 17)
                            us_table.Add(3.2, 20)
                            us_table.Add(3.3, 24)
                            us_table.Add(3.4, 27)
                            us_table.Add(3.5, 30)
                            us_table.Add(3.6, 34)
                            us_table.Add(3.7, 37)
                            us_table.Add(3.8, 41)
                            us_table.Add(3.9, 44)
                            us_table.Add(4.0, 47)
                            us_table.Add(4.1, 51)
                            us_table.Add(4.2, 54)
                            us_table.Add(4.3, 57)
                            us_table.Add(4.4, 61)
                            us_table.Add(4.5, 64)
                            us_table.Add(4.6, 67)
                            us_table.Add(4.7, 71)
                            us_table.Add(4.8, 74)
                            us_table.Add(4.9, 77)
                            us_table.Add(5.0, 81)
                            mkus = 2.7
                        End If
                        If Fcu = 22.5 Then
                            um_table.Clear()
                            um_table.Add(0.8, 27)
                            um_table.Add(0.9, 30)
                            um_table.Add(1.0, 34)
                            um_table.Add(1.1, 37)
                            um_table.Add(1.2, 41)
                            um_table.Add(1.3, 44)
                            um_table.Add(1.4, 48)
                            um_table.Add(1.5, 52)
                            um_table.Add(1.6, 56)
                            um_table.Add(1.7, 60)
                            um_table.Add(1.8, 63)
                            um_table.Add(1.9, 68)
                            um_table.Add(2.0, 72)
                            um_table.Add(2.1, 76)
                            um_table.Add(2.2, 80)
                            um_table.Add(2.3, 85)
                            um_table.Add(2.4, 89)
                            um_table.Add(2.5, 94)
                            um_table.Add(2.6, 98)
                            um_table.Add(2.7, 103)
                            um_table.Add(2.8, 108)
                            um_table.Add(2.9, 112)
                            um_table.Add(3.0, 115)
                            um_table.Add(3.1, 119)
                            um_table.Add(3.2, 122)
                            um_table.Add(3.3, 125)
                            um_table.Add(3.4, 129)
                            um_table.Add(3.5, 132)
                            um_table.Add(3.6, 135)
                            um_table.Add(3.7, 139)
                            um_table.Add(3.8, 142)
                            um_table.Add(3.9, 145)
                            um_table.Add(4.0, 149)
                            um_table.Add(4.1, 152)
                            um_table.Add(4.2, 156)
                            um_table.Add(4.3, 159)
                            um_table.Add(4.4, 162)
                            um_table.Add(4.5, 166)
                            um_table.Add(4.6, 169)
                            um_table.Add(4.7, 172)
                            um_table.Add(4.8, 176)
                            um_table.Add(4.9, 179)
                            um_table.Add(5.0, 182)
                            um100 = 2.6
                            us_table.Clear()
                            us_table.Add(3.0, 3)
                            us_table.Add(3.1, 6)
                            us_table.Add(3.2, 9)
                            us_table.Add(3.3, 13)
                            us_table.Add(3.4, 16)
                            us_table.Add(3.5, 20)
                            us_table.Add(3.6, 23)
                            us_table.Add(3.7, 26)
                            us_table.Add(3.8, 30)
                            us_table.Add(3.9, 33)
                            us_table.Add(4.0, 36)
                            us_table.Add(4.1, 40)
                            us_table.Add(4.2, 43)
                            us_table.Add(4.3, 46)
                            us_table.Add(4.4, 50)
                            us_table.Add(4.5, 53)
                            us_table.Add(4.6, 56)
                            us_table.Add(4.7, 60)
                            us_table.Add(4.8, 63)
                            us_table.Add(4.9, 67)
                            us_table.Add(5.0, 70)
                            mkus = 3.0
                        End If
                        If Fcu = 25 Then
                            um_table.Clear()
                            um_table.Add(0.8, 27)
                            um_table.Add(0.9, 30)
                            um_table.Add(1.0, 34)
                            um_table.Add(1.1, 37)
                            um_table.Add(1.2, 41)
                            um_table.Add(1.3, 44)
                            um_table.Add(1.4, 48)
                            um_table.Add(1.5, 52)
                            um_table.Add(1.6, 56)
                            um_table.Add(1.7, 59)
                            um_table.Add(1.8, 63)
                            um_table.Add(1.9, 67)
                            um_table.Add(2.0, 71)
                            um_table.Add(2.1, 75)
                            um_table.Add(2.2, 79)
                            um_table.Add(2.3, 83)
                            um_table.Add(2.4, 88)
                            um_table.Add(2.5, 92)
                            um_table.Add(2.6, 96)
                            um_table.Add(2.7, 101)
                            um_table.Add(2.8, 105)
                            um_table.Add(2.9, 110)
                            um_table.Add(3.0, 114)
                            um_table.Add(3.1, 119)
                            um_table.Add(3.2, 124)
                            um_table.Add(3.3, 127)
                            um_table.Add(3.4, 130)
                            um_table.Add(3.5, 134)
                            um_table.Add(3.6, 137)
                            um_table.Add(3.7, 140)
                            um_table.Add(3.8, 144)
                            um_table.Add(3.9, 147)
                            um_table.Add(4.0, 150)
                            um_table.Add(4.1, 154)
                            um_table.Add(4.2, 157)
                            um_table.Add(4.3, 161)
                            um_table.Add(4.4, 164)
                            um_table.Add(4.5, 167)
                            um_table.Add(4.6, 171)
                            um_table.Add(4.7, 174)
                            um_table.Add(4.8, 177)
                            um_table.Add(4.9, 181)
                            um_table.Add(5.0, 184)
                            um100 = 2.7
                            us_table.Clear()
                            us_table.Add(3.3, 2)
                            us_table.Add(3.4, 5)
                            us_table.Add(3.5, 9)
                            us_table.Add(3.6, 12)
                            us_table.Add(3.7, 15)
                            us_table.Add(3.8, 19)
                            us_table.Add(3.9, 22)
                            us_table.Add(4.0, 25)
                            us_table.Add(4.1, 29)
                            us_table.Add(4.2, 32)
                            us_table.Add(4.3, 36)
                            us_table.Add(4.4, 39)
                            us_table.Add(4.5, 42)
                            us_table.Add(4.6, 46)
                            us_table.Add(4.7, 49)
                            us_table.Add(4.8, 52)
                            us_table.Add(4.9, 56)
                            us_table.Add(5.0, 59)
                            mkus = 3.3
                        End If
                        If Fcu = 27.5 Then
                            um_table.Clear()
                            um_table.Add(0.8, 27)
                            um_table.Add(0.9, 30)
                            um_table.Add(1.0, 34)
                            um_table.Add(1.1, 37)
                            um_table.Add(1.2, 41)
                            um_table.Add(1.3, 44)
                            um_table.Add(1.4, 48)
                            um_table.Add(1.5, 51)
                            um_table.Add(1.6, 55)
                            um_table.Add(1.7, 59)
                            um_table.Add(1.8, 63)
                            um_table.Add(1.9, 66)
                            um_table.Add(2.0, 70)
                            um_table.Add(2.1, 74)
                            um_table.Add(2.2, 78)
                            um_table.Add(2.3, 82)
                            um_table.Add(2.4, 86)
                            um_table.Add(2.5, 90)
                            um_table.Add(2.6, 95)
                            um_table.Add(2.7, 99)
                            um_table.Add(2.8, 103)
                            um_table.Add(2.9, 108)
                            um_table.Add(3.0, 112)
                            um_table.Add(3.1, 116)
                            um_table.Add(3.2, 121)
                            um_table.Add(3.3, 126)
                            um_table.Add(3.4, 130)
                            um_table.Add(3.5, 135)
                            um_table.Add(3.6, 139)
                            um_table.Add(3.7, 142)
                            um_table.Add(3.8, 145)
                            um_table.Add(3.9, 149)
                            um_table.Add(4.0, 152)
                            um_table.Add(4.1, 155)
                            um_table.Add(4.2, 159)
                            um_table.Add(4.3, 162)
                            um_table.Add(4.4, 165)
                            um_table.Add(4.5, 169)
                            um_table.Add(4.6, 172)
                            um_table.Add(4.7, 176)
                            um_table.Add(4.8, 179)
                            um_table.Add(4.9, 182)
                            um_table.Add(5.0, 186)
                            um100 = 2.7
                            us_table.Clear()
                            us_table.Add(3.6, 1)
                            us_table.Add(3.7, 4)
                            us_table.Add(3.8, 8)
                            us_table.Add(3.9, 11)
                            us_table.Add(4.0, 15)
                            us_table.Add(4.1, 18)
                            us_table.Add(4.2, 21)
                            us_table.Add(4.3, 25)
                            us_table.Add(4.4, 28)
                            us_table.Add(4.5, 31)
                            us_table.Add(4.6, 35)
                            us_table.Add(4.7, 38)
                            us_table.Add(4.8, 41)
                            us_table.Add(4.9, 45)
                            us_table.Add(5.0, 48)
                            mkus = 3.6
                        End If
                        If Fcu = 30 Then
                            um_table.Clear()
                            um_table.Add(0.8, 27)
                            um_table.Add(0.9, 30)
                            um_table.Add(1.0, 33)
                            um_table.Add(1.1, 37)
                            um_table.Add(1.2, 40)
                            um_table.Add(1.3, 44)
                            um_table.Add(1.4, 48)
                            um_table.Add(1.5, 51)
                            um_table.Add(1.6, 55)
                            um_table.Add(1.7, 58)
                            um_table.Add(1.8, 62)
                            um_table.Add(1.9, 66)
                            um_table.Add(2.0, 70)
                            um_table.Add(2.1, 74)
                            um_table.Add(2.2, 77)
                            um_table.Add(2.3, 81)
                            um_table.Add(2.4, 85)
                            um_table.Add(2.5, 89)
                            um_table.Add(2.6, 93)
                            um_table.Add(2.7, 98)
                            um_table.Add(2.8, 102)
                            um_table.Add(2.9, 106)
                            um_table.Add(3.0, 110)
                            um_table.Add(3.1, 115)
                            um_table.Add(3.2, 119)
                            um_table.Add(3.3, 123)
                            um_table.Add(3.4, 128)
                            um_table.Add(3.5, 132)
                            um_table.Add(3.6, 137)
                            um_table.Add(3.7, 142)
                            um_table.Add(3.8, 147)
                            um_table.Add(3.9, 150)
                            um_table.Add(4.0, 154)
                            um_table.Add(4.1, 157)
                            um_table.Add(4.2, 160)
                            um_table.Add(4.3, 164)
                            um_table.Add(4.4, 167)
                            um_table.Add(4.5, 170)
                            um_table.Add(4.6, 174)
                            um_table.Add(4.7, 177)
                            um_table.Add(4.8, 181)
                            um_table.Add(4.9, 184)
                            um_table.Add(5.0, 187)
                            um100 = 2.8
                            us_table.Clear()
                            us_table.Add(4.0, 4)
                            us_table.Add(4.1, 7)
                            us_table.Add(4.2, 10)
                            us_table.Add(4.3, 14)
                            us_table.Add(4.4, 17)
                            us_table.Add(4.5, 20)
                            us_table.Add(4.6, 24)
                            us_table.Add(4.7, 27)
                            us_table.Add(4.8, 31)
                            us_table.Add(4.9, 34)
                            us_table.Add(5.0, 37)
                            mkus = 4.0
                        End If
                    End If
                    If method = 1 Then
                        Ku = (Mu * 10 ^ 6) / (b * d ^ 2)
                        Ku = Math.Round(Ku, 1)
                        moment(dn, dm)
                    End If
                    If method = 2 Then
                        Ku = 5.0
                        d = Math.Sqrt((Mu * 10 ^ 6) / (b * Ku))
                        d = Math.Ceiling(d / 50) * 50
                        um = um_table.Item(Ku) / 100
                        us = us_table.Item(Ku) / 100
                        As_m = (um / 100) * b * d
                        As_s = (us / 100) * b * d
                        n_m = Math.Ceiling(As_m / dm)
                        REM rounding values for output
                        As_m = Math.Round(As_m, 2)
                        As_s = Math.Round(As_s, 2)
                        MessageBox.Show("b= " & b & vbCrLf & "d= " & d & vbCrLf & "Ku= " & Ku & vbCrLf & "μ= " & um _
                                        & vbCrLf & "μ'= " & us & vbCrLf & "As= " & As_m & vbCrLf & "As'= " & As_s _
                                        & vbCrLf & "Main Reinforcment: " & n_m & "∅" & dn, "Solution")
                    End If
                    If method = 3 Then
                        Ku = um100
                        d = Math.Sqrt((Mu * 10 ^ 6) / (b * Ku))
                        d = Math.Ceiling(d / 50) * 50
                        um = um_table.Item(Ku) / 100
                        us = 0.0
                        As_m = (um / 100) * b * d
                        As_s = 0.0
                        n_m = Math.Ceiling(As_m / dm)
                        REM rounding values for output
                        As_m = Math.Round(As_m, 2)
                        As_s = Math.Round(As_s, 2)
                        MessageBox.Show("b= " & b & vbCrLf & "d= " & d & vbCrLf & "Ku= " & Ku & vbCrLf & "μ= " & um _
                                        & vbCrLf & "μ'= " & us & vbCrLf & "As= " & As_m & vbCrLf & "As'= " & As_s _
                                        & vbCrLf & "Main Reinforcment: " & n_m & "∅" & dn _
                                        & vbCrLf & "No secondary reinforcment needed.", "Solution")
                    End If
                    If method = 4 Then
                        Ku = 0.8
                        d = Math.Sqrt((Mu * 10 ^ 6) / (b * Ku))
                        d = Math.Ceiling(d / 50) * 50
                        um = um_table.Item(Ku) / 100
                        us = 0.0
                        As_m = (um / 100) * b * d
                        As_s = 0.0
                        n_m = Math.Ceiling(As_m / dm)
                        REM rounding values for output
                        As_m = Math.Round(As_m, 2)
                        As_s = Math.Round(As_s, 2)
                        MessageBox.Show("b= " & b & vbCrLf & "d= " & d & vbCrLf & "Ku= " & Ku & vbCrLf & "μ= " & um _
                                        & vbCrLf & "μ'= " & us & vbCrLf & "As= " & As_m & vbCrLf & "As'= " & As_s _
                                        & vbCrLf & "Main Reinforcment: " & n_m & "∅" & dn _
                                        & vbCrLf & "No secondary reinforcment needed.", "Solution")
                    End If
                End If
            Else
                MessageBox.Show("Fcu must be one of these values {20,22.5,25,27.5,30}", "Unexpected value!")
            End If
        End If
    End Sub

    Sub shear(ByVal x, ByVal a, ByVal Fy)
        qst = qact - q_cracked
        S = (2 * a * (Fy / 1.15)) / (b * qst)
        n = 1000 / S
        n = Math.Ceiling(n)
        If n < 5 Then
            qact = Math.Round(qact, 3)
            qst = Math.Round(qst, 3)
            MessageBox.Show("qact= " & qact & vbCrLf & "qst= " & qst & vbCrLf & _
                            "Use minimum stirrups: 5∅" & x & "/m'", "Solution")
        End If
        If n >= 5 And n <= 10 Then
            qact = Math.Round(qact, 3)
            qst = Math.Round(qst, 3)
            MessageBox.Show("qact= " & qact & vbCrLf & "qst= " & qst & vbCrLf & _
                            "Use: " & n & "∅" & x & "/m'", "Solution")
        End If
        If n > 10 Then
            S = (4 * a * (Fy / 1.15)) / (b * qst)
            n = 1000 / S
            n = Math.Ceiling(n)
            If n < 5 Then
                qact = Math.Round(qact, 3)
                qst = Math.Round(qst, 3)
                MessageBox.Show("qact= " & qact & vbCrLf & "qst= " & qst & vbCrLf & _
                                "Use minimum stirrups: 5∅" & x & "/m' {4 branches}", "Solution")
            End If
            If n >= 5 And n <= 10 Then
                qact = Math.Round(qact, 3)
                qst = Math.Round(qst, 3)
                MessageBox.Show("qact= " & qact & vbCrLf & "qst= " & qst & vbCrLf & _
                                "Use: " & n & "∅" & x & "/m' {4 branches}", "Solution")
            End If
            If n > 10 Then
                If x = 8 Then
                    MessageBox.Show("Use ∅10 or Increase Dimensions", "Solution")
                Else
                    MessageBox.Show("Use bent bars or Increase Dimensions", "Solution")
                End If
            End If
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        Select Case ComboBox3.SelectedIndex
            Case 0
                x = 8
                a = 50.8
                Fy = 240
            Case 1
                x = 10
                a = 78.5
                Fy = 360
        End Select
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" _
        Or TextBox5.Text = "" Then
            MessageBox.Show("Looks like you missed a required value there, Check again!", "Missing Values!")
        Else
            b = TextBox1.Text
            d = TextBox2.Text
            Fcu = TextBox3.Text
            Qu_max = TextBox5.Text
            Qu_max = Math.Abs(Qu_max)
            If b <= 0 Or d <= 0 Or Fcu <= 0 Or Qu_max <= 0 Then
                MessageBox.Show("Unexpected Value", "Error")
            Else
                qact = (Qu_max * 1000) / (b * d)
                q_uncracked = 0.16 * (Fcu / 1.5) ^ 0.5
                q_cracked = 0.12 * (Fcu / 1.5) ^ 0.5
                q_max = 0.7 * (Fcu / 1.5) ^ 0.5
                If qact > q_max Then
                    MessageBox.Show("qact > qmax, Increase Dimensions", "Solution")
                End If
                If qact < q_uncracked Then
                    MessageBox.Show("Use minimum stirrups: 5∅8/m'", "Solution")
                End If
                If qact > q_uncracked And qact < q_max Then
                    shear(x, a, Fy)
                End If
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim abo1 As String
        abo1 = "This is a free software license and is compatible with the GNU GPL." & vbCrLf _
         & "Software was developed by/ Ibrahim A. Elshalaby"
        MessageBox.Show(abo1, "About")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim chng1 As String
        chng1 = "-v0.0 Beta:" & vbCrLf & " *Software released." & vbCrLf & " *Added:" & vbCrLf _
            & "  -You can solve shear only with an 8mm diameter steel." & vbCrLf _
            & "--------------------------------------------------------------------------------" & vbCrLf _
            & "-v0.1 Beta:" & vbCrLf & " *Added:" & vbCrLf & "  -Introducing GUI !" & vbCrLf _
            & "  -README file is now available to instruct you." & vbCrLf _
            & "  -You can solve moment only with a 16mm diameter steel." & vbCrLf _
            & "  -You can solve shear with a 10mm diameter steel also, but the software decides which diameter to use." _
            & vbCrLf & "--------------------------------------------------------------------------------" & vbCrLf _
            & "-v0.2 Beta:" & vbCrLf & " *Added:" & vbCrLf _
            & "  -You can choose a diameter from 16 and 18 when solving for moment." & vbCrLf _
            & "  -About and Changelog buttons added for more info about the software." & vbCrLf _
            & " *Changed:" & vbCrLf & "  -Changed the GUI's layout." & vbCrLf _
            & "  -Changed the icon to a more elegant one." & vbCrLf _
            & "  -No more recommendations for the secondary steel diameter." & vbCrLf _
            & " *Removed:" & vbCrLf & "  -No need for a README file anymore." & vbCrLf _
            & " *Fixed:" & vbCrLf & "  -Fixed a bug with results using 'or' when solving for shear." _
            & vbCrLf & "--------------------------------------------------------------------------------" & vbCrLf _
            & "-v0.3 Beta:" & vbCrLf & " *Added:" & vbCrLf _
            & "  -You can choose different diameters when solving for moment." & vbCrLf _
            & "  -You can choose different diameters when solving for shear." & vbCrLf _
            & " *Changed:" & vbCrLf & "  -We are now using VB.NET instead of Tkinter Python!" & vbCrLf _
            & "  -Changed the GUI's layout." & vbCrLf _
            & "  -Changed the way shear results are displayed." _
            & vbCrLf & "--------------------------------------------------------------------------------" & vbCrLf _
            & "-v0.4 [Stable]:" & vbCrLf & " *Added:" & vbCrLf _
            & "  -You can choose different methods when solving for moment." & vbCrLf _
            & " *Fixed:" & vbCrLf & "  -Software stops if an incorrect value is entered."
        MessageBox.Show(chng1, "Changelog")
    End Sub

End Class
