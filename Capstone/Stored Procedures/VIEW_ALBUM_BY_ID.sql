USE [CapstoneDB]
GO
/****** Object:  StoredProcedure [dbo].[VIEW_ALBUM_BY_ID]    Script Date: 9/12/2017 12:50:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[VIEW_ALBUM_BY_ID]
(
	@AlbumID bigint
)
AS
BEGIN
SELECT AlbumID,
	Name,
	Artist,
	Genre,
	ReleaseDate,
	PictureURL,
	NumberOfTracks,
	Duration,
	AlbumType,
	ReleaseType,
	Sales,
	GaonAwards,
	AudioLink
FROM Album
WHERE AlbumID = @AlbumID
END