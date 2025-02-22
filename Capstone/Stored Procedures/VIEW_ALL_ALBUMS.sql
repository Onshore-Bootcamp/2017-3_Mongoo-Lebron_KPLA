USE [CapstoneDB]
GO
/****** Object:  StoredProcedure [dbo].[VIEW_ALL_ALBUMS]    Script Date: 9/12/2017 12:50:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[VIEW_ALL_ALBUMS]
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
END