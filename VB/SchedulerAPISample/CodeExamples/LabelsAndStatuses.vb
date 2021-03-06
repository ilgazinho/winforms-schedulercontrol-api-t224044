﻿Imports DevExpress.XtraScheduler
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace SchedulerAPISample.CodeExamples
    Friend Class LabelsAndStatuses

        Shared Sub LabelInfoAction(ByVal scheduler As SchedulerControl)
            '			#Region "#LabelInfo"
            AddHandler scheduler.InitAppointmentDisplayText, AddressOf scheduler_InitAppointmentDisplayText
            scheduler.ActiveView.LayoutChanged()
            '			#End Region ' #LabelInfo
        End Sub

        ' #Region "#@LabelInfo"
        Public Shared Sub scheduler_InitAppointmentDisplayText(ByVal sender As Object, ByVal e As AppointmentDisplayTextEventArgs)
            Dim scheduler As SchedulerControl = TryCast(sender, SchedulerControl)
            Dim label As IAppointmentLabel = scheduler.DataStorage.Appointments.Labels.GetById(e.Appointment.LabelKey)
            e.Description = String.Format("Label Info:" & ControlChars.Lf & "DisplayName = '{0}'" & ControlChars.Lf & "ID = '{1}'", label.DisplayName, label.Id.ToString())
        End Sub
        ' #End Region ' #@LabelInfo


        Shared Sub StatusInfoAction(ByVal scheduler As SchedulerControl)
            '			#Region "#StatusInfo"
            AddHandler scheduler.InitAppointmentDisplayText, AddressOf scheduler_InitAppointmentDisplayText_1
            scheduler.ActiveView.LayoutChanged()
            '			#End Region ' #StatusInfo
        End Sub

        ' #Region "#@StatusInfo"
        Public Shared Sub scheduler_InitAppointmentDisplayText_1(ByVal sender As Object, ByVal e As AppointmentDisplayTextEventArgs)
            Dim scheduler As SchedulerControl = TryCast(sender, SchedulerControl)
            Dim status As IAppointmentStatus = scheduler.DataStorage.Appointments.Statuses.GetById(e.Appointment.StatusKey)
            e.Description = String.Format("Status Info:" & ControlChars.Lf & "DisplayName = '{0}'" & ControlChars.Lf & "ID = '{1}'", status.DisplayName, status.Id.ToString())
        End Sub
        ' #End Region ' #@StatusInfo

        Shared Sub ChangingLabelColorSchemeAction(ByVal scheduler As SchedulerControl)
            '			#Region "#ChangingLabelColorScheme"
            Dim labelStorage As IAppointmentLabelStorage = scheduler.DataStorage.Appointments.Labels
            For Each label As IAppointmentLabel In labelStorage
                Dim appLabel As AppointmentLabel = TryCast(label, AppointmentLabel)
                If appLabel IsNot Nothing Then
                    Dim skinElemName As String = AppointmentLabel.GetSkinElementName(appLabel.ColorId)
                    Dim skinColor As Color = DevExpress.Skins.SkinManager.Default.Skins("Office 2016 Colorful").GetSkin(DevExpress.Skins.SkinProductId.Scheduler).Colors.GetPrimaryColor(skinElemName, Color.Empty)
                    If skinColor = Color.Empty Then
                        skinColor = AppointmentLabel.GetDefaultColorByStringId(appLabel.ColorId)
                    End If
                    appLabel.SetColor(skinColor)
                End If
                scheduler.ActiveView.LayoutChanged()
            Next label
            '			#End Region ' #ChangingLabelColorScheme
        End Sub


        Private Shared Sub CustomLabelsAndStatusesAction(ByVal scheduler As SchedulerControl)
            '            #Region "#CustomLabelsAndStatuses"
            scheduler.DataStorage.Appointments.Clear()

            Dim IssueList() As String = {"Consultation", "Treatment", "X-Ray"}
            Dim IssueColorList() As Color = {Color.Ivory, Color.Pink, Color.Plum}
            Dim PaymentStatuses() As String = {"Paid", "Unpaid"}
            Dim PaymentColorStatuses() As Color = {Color.Green, Color.Red}


            Dim labelStorage As IAppointmentLabelStorage = scheduler.DataStorage.Appointments.Labels
            labelStorage.Clear()
            Dim count As Integer = IssueList.Length
            For i As Integer = 0 To count - 1
                Dim label As IAppointmentLabel = labelStorage.CreateNewLabel(i, IssueList(i))
                label.SetColor(IssueColorList(i))
                labelStorage.Add(label)
            Next i
            Dim statuses As IAppointmentStatusStorage = scheduler.DataStorage.Appointments.Statuses
            statuses.Clear()
            count = PaymentStatuses.Length
            For i As Integer = 0 To count - 1
                Dim status As IAppointmentStatus = statuses.CreateNewStatus(i, PaymentStatuses(i), PaymentStatuses(i))
                status.SetBrush(New SolidBrush(PaymentColorStatuses(i)))
                statuses.Add(status)
            Next i
            '            #End Region ' #CustomLabelsAndStatuses
        End Sub
    End Class
End Namespace
