USE [CapstoneDB]
GO
/****** Object:  StoredProcedure [dbo].[DELETE_ALBUM_BY_ID]    Script Date: 9/12/2017 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[DELETE_ALBUM_BY_ID]
(
	@AlbumID bigint
)
AS
BEGIN
DELETE FROM Song
WHERE Album = @AlbumID

DELETE FROM Album
WHERE AlbumID = @AlbumID
END