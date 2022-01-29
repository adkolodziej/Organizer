# Organizer
Simple WPF organizer app for University project

This application allow its user to Add, Filter, and Edit entries in the calendar.
It has been created using .Net 5.0, Windowos Presentation Foundation (WPF) and Entity Framework Core 5.0.10

For User
To launch the app download the repository, head to the bin/debug/net5.0-windows and launch Organizer.exe
The app provides three main columns. First contains three buttons and a combo box where you can choose already existing notes.
 1. New note - which creates new instance of a note and resets values in second column.
 2. Save changes - which adds the created note to the database or edits currently selected note.
 3. Delete note - which delets currently selected note.

Second column contains fields allowing the user to create a note. On top the date is being shown.
Below the user should provide a tag used for filtering the notes.
Then two fields that represent time of the activity in note.
And lastly the note content itself.

The last column consists mainly of the calendar. Clicking on a specific date allows the user to add notes and preview already existing ones.
Days that have Red border around them contain notes. If a day have also have a green border then it is a day chosen by a filter.
Below the calendar user can see Filter button that applies filter, combo box indicating the chosen filter type (Tag, Content, Start hour, End hour) and 3 other fields.
The empty field can be provided with a tag or content. And two hour fields to filter by hours. 

After every action that involves changing the database or filtering the whole calendar reloads to update the data. Chosen date is cleared after such reload.

For developer
To start the app you have to launch the .sln file using Visual Studio (2022 would be best)
