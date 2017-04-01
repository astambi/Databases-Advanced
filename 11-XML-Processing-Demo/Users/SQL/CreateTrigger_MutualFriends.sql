CREATE TRIGGER t_UserFriends_Mutual ON UserFriends AFTER INSERT
AS
BEGIN
	INSERT INTO UserFriends (UserId, FriendId)
	SELECT FriendId, UserId
	FROM inserted
END