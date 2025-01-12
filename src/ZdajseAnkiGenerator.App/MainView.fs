module MainView

open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Layout
open Types
open Api
open Store
    
[<RequireQualifiedAccess>]
module AppState =
    let selectedItems: IWritable<Subject list> = new State<_>([])

let subjectListView (item : Subject, selectedItems : IWritable<Subject list>) =
    Component.create(item.id, fun ctx -> 
        StackPanel.create [
            StackPanel.horizontalAlignment HorizontalAlignment.Left
            StackPanel.orientation Orientation.Horizontal
            StackPanel.children [
                CheckBox.create [
                    CheckBox.dock Dock.Left
                    CheckBox.verticalAlignment VerticalAlignment.Center
                    CheckBox.horizontalAlignment HorizontalAlignment.Center
                    CheckBox.onChecked (fun _ -> selectedItems.Set(item :: selectedItems.Current))
                    CheckBox.onUnchecked (fun _ -> selectedItems.Set(List.filter (fun x -> x.id <> item.id) selectedItems.Current))
                ]
                TextBlock.create [
                    TextBlock.dock Dock.Left
                    TextBlock.fontSize 16.0
                    TextBlock.verticalAlignment VerticalAlignment.Center
                    TextBlock.horizontalAlignment HorizontalAlignment.Center
                    TextBlock.text item.title 
                ]
            ]
        ] 
    )

let view (window : UserControl) =
    Component(fun ctx ->
        let selectedItems = ctx.usePassed AppState.selectedItems
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
                                    for item in state.Current.Value.pages do
                                        subjectListView (item, selectedItems)
                            ]
                        ]
                    )
                ]
                Button.create [
                    Button.content "Generate"
                    Button.onClick (fun _ ->
                       task {
                            let selected = selectedItems.Current
                                            |> List.map _.id
                            let! data = selected
                                        |> List.map fetchSubject
                                        |> Async.Parallel
                                        
                            let! stream = Generator.generateDeck(data[0])
                            if fileServiceInstance.IsSome then
                                do! fileServiceInstance.Value.SaveFile(stream.ToArray())
                       } |> ignore
                    )
                ]
            ]
        ]
    )
        
