USE [CapstoneDB]
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_ALBUM_BY_ID]    Script Date: 9/12/2017 12:51:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UPDATE_ALBUM_BY_ID]
(
	@AlbumID bigint,
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
UPDATE Album
SET Name = @Name,
	Artist = @Artist,
	Genre = @Genre,
	ReleaseDate = @ReleaseDate,
	PictureURL = @PictureURL,
	NumberOfTracks = @NumberOfTracks,
	Duration = @Duration,
	AlbumType = @AlbumType,
	ReleaseType = @ReleaseType,
	Sales = @Sales,
	GaonAwards = @GaonAwards,
	AudioLink = @AudioLink
WHERE AlbumID = @AlbumID
END