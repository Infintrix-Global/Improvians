
Partial Class gti_jobcard
    Inherits System.Web.UI.Page

    Private conn As dconn
    Private ssql As String
    Private m As modules
    Dim jb As String
    Dim ct As Int64

#Region "Pages"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("appuser") = 223

        If Session("appuser") = 0 Then
            Response.Redirect(dconn.ht)
        End If

        If Not Page.IsPostBack Then
            m = New modules

            m.Dispose()
            m = Nothing

        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        If m Is Nothing Then
        Else
            m.Dispose()
            m = Nothing
        End If

        'If rep Is Nothing Then
        'Else
        '    rep.Close()
        '    rep.Dispose()
        '    rep = Nothing
        'End If

    End Sub

#End Region

#Region "GridView Events"

    Private Sub DGHead02_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DGHead02.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'get plant age 
            e.Row.Cells(3).Text = DateDiff(DateInterval.Day, CDate(DGHead02.DataKeys(e.Row.RowIndex).Values("seeddt")), Now.Date)
        End If

    End Sub

    Private Sub DGTasks_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DGTasks.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If CDate(DGTasks.DataKeys(e.Row.RowIndex).Values("compdate")) < CDate("1/1/2000") Then
                e.Row.Cells(2).Text = ""
            Else
                e.Row.Cells(2).Text = CDate(DGTasks.DataKeys(e.Row.RowIndex).Values("compdate"))
            End If
        End If
    End Sub

    Private Sub DGInventory_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles DGInventory.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            ct += CLng(DGInventory.DataKeys(e.Row.RowIndex).Values("trays"))
        End If

    End Sub

#End Region

#Region "Functions"

    Function ValidateJob() As Boolean

        jb = Txtjob.Text

        Return True

    End Function

    Sub FillDGHeader01()
        ssql = "select j.No_ jobcode, j.[Shortcut Property 1 Value] germpct, j.[Bill-to Name] cname, j.[Item No_] itemno, j.[Item Description] itemdescp, " &
                "sum(t.Quantity) trays, j.[Delivery Date] ready_date, m.[Production Phase] pphase, " &
                "j.[Source No_] + '-' + convert(nvarchar,j.[Source Line No_]/1000) solines, j.[Variant Code] ts, j.[Source No_] sono,j.[Source Line No_] soline, " &
                "j.[Genus Code] crop, j.[Shortcut Property 10 Value] overage, " &
                "CASE WHEN m.[Closed at Date] < '2000-01-01' THEN m.[Posting Date] ELSE m.[Closed at Date] END seeddt, " &
                "CASE WHEN j.[Shortcut Property 2 Value] = 'Yes' THEN 'Yes' ELSE 'NO' END org " &
                "from [GTI$IA Job Tracking Entry] t, [GTI$Job] j " &
                "LEFT OUTER JOIN [GTI$IA Job Mutation Entry] m ON j.No_ = m.[Job No_] and m.[Production Phase] in ('SEEDING','RETURNS') " &
                "where j.No_ = t.[Job No_] And j.No_ = '" & jb & "' " &
                "group by j.No_, j.[Shortcut Property 2 Value], j.[Shortcut Property 1 Value], j.[Bill-to Name], j.[Item No_], j.[Item Description], " &
                "j.[Delivery Date], m.[Closed at Date], m.[Production Phase], m.[Posting Date], j.[Source No_], j.[Source Line No_], j.[Variant Code], j.[Genus Code], " &
                "j.[Shortcut Property 10 Value]"
        conn.OpenDB2Reader(ssql)

        DGHead01.DataSource = conn.dr
        DGHead01.DataBind()

        conn.CloseDB2()

        FillDGHeader02()

    End Sub

    Sub FillDGHeader02()
        Dim sql1 As String
        Dim cn1 As New dconn

        sql1 = "select j.No_ jobcode, j.[Shortcut Property 1 Value] germpct, j.[Bill-to Name] cname, j.[Item No_] itemno, j.[Item Description] itemdescp, " &
                "sum(t.Quantity) trays, j.[Delivery Date] ready_date, m.[Production Phase] pphase, " &
                "j.[Source No_] + '-' + convert(nvarchar,j.[Source Line No_]/1000) solines, j.[Variant Code] ts, j.[Source No_] sono,j.[Source Line No_] soline, " &
                "j.[Genus Code] crop, j.[Shortcut Property 10 Value] overage, " &
                "CASE WHEN m.[Closed at Date] < '2000-01-01' THEN m.[Posting Date] ELSE m.[Closed at Date] END seeddt, " &
                "CASE WHEN j.[Shortcut Property 2 Value] = 'Yes' THEN 'Yes' ELSE 'NO' END org " &
                "from [GTI$IA Job Tracking Entry] t, [GTI$Job] j " &
                "LEFT OUTER JOIN [GTI$IA Job Mutation Entry] m ON j.No_ = m.[Job No_] and m.[Production Phase] in ('SEEDING','RETURNS') " &
                "where j.No_ = t.[Job No_] And j.No_ = '" & jb & "' " &
                "group by j.No_, j.[Shortcut Property 2 Value], j.[Shortcut Property 1 Value], j.[Bill-to Name], j.[Item No_], j.[Item Description], " &
                "j.[Delivery Date], m.[Closed at Date], m.[Production Phase], m.[Posting Date], j.[Source No_], j.[Source Line No_], j.[Variant Code], j.[Genus Code], " &
                "j.[Shortcut Property 10 Value]"
        cn1.OpenDB2Reader(sql1)

        DGHead02.DataSource = cn1.dr
        DGHead02.DataBind()

        cn1.CloseDB2()

        FillDGSeeds()

    End Sub

    Sub FillDGSeeds()
        Dim sql2 As String
        Dim cn2 As New dconn

        sql2 = "select le.[Item No_] seed, le.[Lot No_] lot, le.Quantity qty " &
                "from [GTI$Item Ledger Entry] le " &
                "where le.[Job No_] = '" & jb & "' and le.[Item Category Code] = 'SEED'"
        cn2.OpenDB2Reader(sql2)

        DGSeeds.DataSource = cn2.dr
        DGSeeds.DataBind()

        cn2.CloseDB2()

        FillDGTasks()
    End Sub

    Sub FillDGTasks()
        Dim sql3 As String
        Dim cn3 As New dconn

        sql3 = "select w.[Activity Code] act, w.Date assigndate, w.[Actual Date] compdate " &
                "from [GTI$IA Job Activity Scheme Line] w " &
                "where w.[Job No_] = '" & jb & "' and w.Type = 2 order by w.Date"
        cn3.OpenDB2Reader(sql3)

        DGTasks.DataSource = cn3.dr
        DGTasks.DataBind()

        cn3.CloseDB2()

        FillDGInventory()

    End Sub

    Sub FillDGInventory()
        Dim sql4 As String
        Dim cn4 As New dconn

        sql4 = "select t.[Job No_] jobno, t.[Location Code] loc, t.[Position Code] bench, sum(t.Quantity) trays " &
                "from [GTI$IA Job Tracking Entry] t " &
                "where t.[Job No_] = '" & jb & "' " &
                "group by t.[Job No_], t.[Location Code], t.[Position Code]"
        cn4.OpenDB2Reader(sql4)
        DGInventory.DataSource = cn4.dr
        DGInventory.DataBind()

        cn4.CloseDB2()

        Lblinvct.Text = ct & " TRAYS"

        FillDGHealth()

    End Sub

    Sub FillDGHealth()
        Dim sql5 As String
        Dim cn5 As New dconn

        sql5 = "select h.Date dt, l.[Category Code] cat, l.Description descp, l.Remark " &
                "from [GTI$IA Obs_ Inspection Header] h, [GTI$IA Obs_ Inspection Line] l " &
                "where h.No_ = l.No_ and h.[Source Document No_] = '" & jb & "'"
        cn5.OpenDB2Reader(sql5)
        DGHealth.DataSource = cn5.dr
        DGHealth.DataBind()

        If DGHealth.Rows.Count > 0 Then
            Pnlhealth.Visible = True
        End If

        cn5.CloseDB2()
    End Sub

#End Region

#Region "Commands"

    Private Sub BTrun_Click(sender As Object, e As EventArgs) Handles BTrun.Click
        If Not ValidateJob() Then
            lblerr.Text = "Invalid Job NUmber"
            Lbljob.Focus()
            Exit Sub
        End If

        conn = New dconn

        FillDGHeader01()

    End Sub

#End Region

End Class
