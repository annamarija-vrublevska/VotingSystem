The voting system project

The project is written in C# and for the database, SQLite is used.

For the demo purposes, when the project starts, all the data in the database is deleted and filled again with dummy data. Multiple voters and items are created. 

For project testing, you should be authorized, but to get a voter list request might be anonimous. 

![Voter list](img/VoterList.jpg)

It is done to get the list of possible users. For basic authorization, a correct user name (voter user name) is enough.

![Basic Auth](img/BasicAuth.jpg)

Items have specific data to present backend functionality. There are 3 items with already expired dates, so on all item list requests it is possible to see how states are changing. In the demo project, backend tasks are run multiple times per minute; in real life, it should be done once a day. 

![Item list](img/ItemList.jpg)