USE [CapstoneDB]
GO
/****** Object:  StoredProcedure [dbo].[ADD_NEW_ALBUM]    Script Date: 9/12/2017 12:28:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ADD_NEW_ALBUM]
(
	@Name nvarchar(30),
	@Artist bigint,
	@Genre nvarchar(200) = NULL,
	@ReleaseDate date,
	@PictureURL ntext = NULL,
	@NumberOfTracks smallint,
	@Duration time(7),
	@AlbumType varchar(13),
	@ReleaseType varchar(13),
	@Sales int = NULL,
	@GaonAwards nvarchar(MAX) = NULL,
	@AudioLink ntext = NULL
)
AS
BEGIN
INSERT INTO Album (Name, Artist, Genre, ReleaseDate, PictureURL, NumberOfTracks, 
	Duration, AlbumType, ReleaseType, Sales, GaonAwards, AudioLink)
VALUES			(@Name, @Artist, @Genre, @ReleaseDate, @PictureURL, @NumberOfTracks,
	@Duration, @AlbumType, @ReleaseType, @Sales, @GaonAwards, @AudioLink)
END