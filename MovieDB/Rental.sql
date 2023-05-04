CREATE TABLE [dbo].[Rental]
(
	[RentalId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[RentalDate] DATETIME NOT NULL,
	[CustomerId] INT NOT NULL,
	[MovieId] INT NOT NULL, 
    CONSTRAINT [FK_Rental_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([CustomerId]), 
    CONSTRAINT [FK_Rental_Movie] FOREIGN KEY ([MovieId]) REFERENCES [Movie]([MovieId]),
	
)
