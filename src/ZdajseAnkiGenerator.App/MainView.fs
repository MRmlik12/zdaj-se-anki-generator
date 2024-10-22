module MainView

open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Layout
open ZdajseAnkiGenerator.App.Api.Subjects

let subjectListView (item : Subject) =
    Component.create(item.Id, fun ctx -> 
        StackPanel.create [
            StackPanel.horizontalAlignment HorizontalAlignment.Left
            StackPanel.orientation Orientation.Horizontal
            StackPanel.children [
                CheckBox.create [
                    CheckBox.dock Dock.Left
                    CheckBox.verticalAlignment VerticalAlignment.Center
                    CheckBox.horizontalAlignment HorizontalAlignment.Center
                ]
                TextBlock.create [
                    TextBlock.dock Dock.Left
                    TextBlock.fontSize 16.0
                    TextBlock.verticalAlignment VerticalAlignment.Center
                    TextBlock.horizontalAlignment HorizontalAlignment.Center
                    TextBlock.text item.Title 
                ]
            ]
        ] 
    )

let view () =
    Component(fun ctx ->
        let state = ctx.useState<Index option> None 

        ctx.useEffect(
            handler = (fun _ ->
                async {
                    let! subject = fetchSubjects()
                    state.Set(Some(subject))
                } |> Async.Start),
            triggers = [ EffectTrigger.AfterInit ])
        
        StackPanel.create [
            StackPanel.orientation Orientation.Vertical
            StackPanel.children [
                TextBlock.create [
                    TextBlock.text "Zdaj.se Anki Generator"
                    TextBlock.fontSize 32.0
                    TextBlock.margin 40.0
                    TextBlock.horizontalAlignment HorizontalAlignment.Center
                ]
                ScrollViewer.create [
                    ScrollViewer.height 800.0
                    ScrollViewer.content (
                        StackPanel.create [
                            StackPanel.children [
                                if state.Current.IsSome then
                                    for item in state.Current.Value.Pages do
                                        subjectListView item
                            ]
                        ]
                    )
                ]
                Button.create [
                    Button.content "Generate"
                ]
            ]
        ]
    )
        
