Start with the existing database & import data

In Models => Add Journal & Publication
In PlanetHuntersContext => Add Journals & Publications

In Package Manager Console
	=> Enable-migrations
	=> Add-migration & update-database

Migrations:
1. AddJournalsPublications 
2. SeedJournalsPublications
	=> Up method: Seed the database with at least one Journal & for every Discovery, create a Publication with release date matching the announcement date of the Discovery.
	=> Down method: Restore DateMade for Discoveries.
3. RemoveDiscoveriesDateMadeCol
	=> Up method: Drop Column DateMade from Discoveries.
	=> Down method: Add Column DateMade (of type "date") to Discoveries.