# Ryanair TravelLabs - ROCS Hiring Test - Tycoon Factory

## API

The functionality is exposed via a web api which uses Swagger.  There are four endpoints, one to create an assignment and the others to read the data.

## DATA

The service pre-propulates the repos with worker and activities.

## Improvements
 
 I did not have as much time as I'd have liked to work on this.  I would have done the following if I had the time
 
 * Implement EF repos using SQL Server database - N.B. the entities would have needed to be classes not records
 * Checks when modifying/creating workers and activities to see if e.g. new rest time causes conflicts
 * Summary / Top 10 of week ahead
 * More tests. Some classes completely untested, e.g. AssignmentRepository

