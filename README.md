# Decibase
A music metadata database and tagging application

## Project Overview
This application will allow users to store and organize metadata for audio files. It will allow users to input information such as song title, artist, album name, genre, track and disk numbers, years, and so on. 

The application will store the information in a database with linked tables for song, artist and album information. The database table for song information will contain a link to each songs respective audio file, and as a stretch goal will also allow the application to read and write to the id3 tag embedded in the file.

## Sprint Breakdown

### Sprint 1
#### Sprint Goals
The goals for this sprint are to create model classes and use them to migrate an initial database, and then to write methods to execute CRUD functions upon the database.
#### Sprint Review
Only one user story was marked as "done" in this sprint due to unforseen complications in the setting up of the database, namely creating a many to many relationship between the artist and track tables - something that I had not done previously and had to learn during the sprint. The accesssor methods will be pushed into the next sprint along with some basic UI functionality.
#### Sprint Retrospective
The blocker fo this sprint was merely a lack of knowlege of Entity Framework, which lead to the first user story taking more time than was expected. However I was eventually able to overcome the blocker and managed to complete the user story. For the next sprint I am confident that I am more familiar with the tasks and so I am feeling optimistic with my ability to progress.
 
### Sprint 2
#### Sprint Goals
The goals for this sprint are to complete and write unit tests for the CRUD functions, and to construct an initial WPF file for the GUI.
#### Sprint Review
Once again, not all user stories were marked "done", due to my underestimation of time required for each task. 3 of the 4 CRUD functions were completed, as although it had been working previously, the update functionality was not functioning correctly at the time of the review. The GUI task was pushed back into the third sprint.
#### Sprint Retrospective
There were no large blockers in this sprint, a few obstacles here and there but all were overcome quickly. My main takeaway is that things take longer than I expect them to, and so I need to reel in my expectations of what I will manage to successfully implement before the deadline. I think I will be able to produce a functional GUI in the next sprint, allowing me time to polish and (possibly) look at my stretch goal in the fourth and final sprint.
