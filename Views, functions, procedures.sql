----------------VIEWS----------------
--12
CREATE VIEW allAssocManagers 
 
 AS

SELECT SU.username AS Username, SU.password AS Password, SAM.SAM_name AS Name
FROM Sports_Association_Manager SAM INNER JOIN S_User SU ON SAM.username = SU.username

GO

CREATE VIEW allClubRepresentatives
 
 AS
 
SELECT SU.username AS Username, SU.password AS Password, CR.CR_name AS Name, C.C_name AS Club
FROM Club_Rep CR INNER JOIN Club C ON CR.CR_id = C.CR_id INNER JOIN S_User SU ON CR.username = SU.username

GO

CREATE VIEW allCLubs 

 AS
 
SELECT C_name AS Name, C_location AS Location 
FROM Club

GO

CREATE VIEW allFans 

 AS

SELECT  SU.username AS Username, SU.password AS Password, F.F_name AS Name, F.F_national_id AS ID, F.F_birthdate AS Birthdate, F.F_status AS Status
FROM Fan F INNER JOIN S_User SU ON F.username = SU.username

GO

CREATE VIEW allMatches 

 AS

SELECT C1.C_name AS Host, C2.C_name AS Guest, M.M_start AS Start
FROM (Match M INNER JOIN Club C1 ON M.M_host_id = C1.C_id)
						INNER JOIN Club C2 ON M.M_guest_id = C2.C_id

GO

CREATE VIEW allRequests 

 AS

SELECT CR.username AS Club_Representative, SM.username AS Stadium_Representative, HR.HR_status AS Status
FROM Club_Rep CR INNER JOIN Host_Request HR ON CR.CR_id = HR.CR_id 
	             INNER JOIN Stadium_Manager SM ON SM.SM_id = HR.SM_id

GO

CREATE VIEW allStadiumManagers 
 
 AS

SELECT SU.username AS Username, SU.password AS Password, SM.SM_name AS Name, S.S_name AS Stadium 
FROM Stadium_Manager SM INNER JOIN Stadium S ON S.SM_ID = SM.SM_ID INNER JOIN S_User SU ON SM.username = SU.username

GO

CREATE VIEW allStadiums 

 AS

SELECT S_name AS Name, S_location AS Location, S_capacity AS Capacity, S_status AS Status
FROM Stadium

GO

CREATE VIEW allTickets 

 AS

SELECT C1.C_name AS Host, C2.C_name AS Guest, T_stadium AS Stadium, M.M_start AS Start
FROM (Ticket T INNER JOIN Match M ON T.M_id = M.M_id)
						INNER JOIN Club C1 ON M.M_host_id = C1.C_id
						INNER JOIN Club C2 ON M.M_guest_id = C2.C_id

GO

CREATE VIEW clubsNeverMatched

 AS

SELECT C1.C_name AS First_Club, C2.C_name AS Second_Club
FROM Club C1, Club C2
WHERE C1.C_name < C2.C_name AND NOT EXISTS
(
	SELECT M.M_host_id, M.M_guest_id
	FROM Match M
	WHERE ((C1.C_id = M.M_host_id AND C2.C_id = M.M_guest_id) OR (C2.C_id = M.M_host_id AND C1.C_id = M.M_guest_id)) AND (M_start <= CURRENT_TIMESTAMP)
)

GO

CREATE VIEW clubsWithNoMatches 

 AS 

SELECT C_name AS Club
FROM Club 
Where NOT EXISTS
(
	SELECT M_host_id, M_guest_id
	FROM Match
	WHERE M_guest_id = C_id OR M_host_id = C_id
)

GO

CREATE VIEW matchesPerTeam

 AS

SELECT C.C_name AS Club, count(M.M_id) AS Played
FROM Match M, Club C
WHERE (M.M_guest_id = C.C_id OR M.M_host_id = C.C_id) AND M.M_start <= CURRENT_TIMESTAMP
GROUP BY C.C_name

GO

CREATE VIEW playedMatches

 AS

SELECT C1.C_name AS Host, C2.C_name AS Guest, M.M_start AS Start_Time, M.M_end AS End_Time
FROM (Match M INNER JOIN Club C1 ON M.M_host_id = C1.C_id)
						INNER JOIN Club C2 ON M.M_guest_id = C2.C_id
WHERE M.M_start <= CURRENT_TIMESTAMP

GO

CREATE VIEW upcomingMatches

 AS

SELECT C1.C_name AS Host, C2.C_name AS Guest, M.M_start AS Start_Time, M.M_end AS End_Time
FROM (Match M INNER JOIN Club C1 ON M.M_host_id = C1.C_id)
						INNER JOIN Club C2 ON M.M_guest_id = C2.C_id
WHERE M.M_start > CURRENT_TIMESTAMP

GO

CREATE VIEW clubsNeverMatched_M3


 AS

SELECT C1.C_name AS First_Club, C2.C_name AS Second_Club
FROM Club C1, Club C2
WHERE C1.C_name < C2.C_name AND NOT EXISTS
(
	SELECT M.M_host_id, M.M_guest_id
	FROM Match M
	WHERE (C1.C_id = M.M_host_id AND C2.C_id = M.M_guest_id) OR (C2.C_id = M.M_host_id AND C1.C_id = M.M_guest_id)
)

GO



----------------PROCEDURES----------------
--23

CREATE PROC addAssociationManager @name VARCHAR(20) , @username VARCHAR(20) , @password VARCHAR(20)

 AS

INSERT INTO S_User VALUES(@username,@password)
INSERT INTO Sports_Association_Manager VALUES(@name,@username)

GO

CREATE PROC addClub @name VARCHAR(20),@location VARCHAR(20)

 AS

INSERT INTO Club VALUES (@name,@location,NULL)

GO

CREATE PROC addFan @name VARCHAR(20), @username VARCHAR(20), @password VARCHAR(20), @id VARCHAR(20), @birthdate DATETIME, @address VARCHAR(20), @phone_number INT

 AS

INSERT INTO S_User VALUES (@username, @password)
INSERT INTO Fan VALUES (@id, @name, @address, @phone_number, @birthdate, 1, @username)

GO

CREATE PROC addHostRequest @CLUB_name VARCHAR(20), @STAD_name VARCHAR(20), @Date DATETIME

 AS

DECLARE @CR_id AS INT, @C_id AS INT
SELECT @CR_id = CR_id, @C_id = C_id FROM Club WHERE @CLUB_name = C_name

DECLARE @SM_id AS INT
SELECT @SM_id = SM.SM_id FROM Stadium S INNER JOIN Stadium_Manager SM ON S.SM_id = SM.SM_id WHERE @STAD_name = S.S_name

DECLARE @M_id AS INT
SELECT @M_id = M_id FROM Match WHERE M_start = @Date AND S_id IS NULL AND @C_id = M_host_id

INSERT INTO Host_Request VALUES ('unhandled', @M_id, @CR_id, @SM_id)

GO

CREATE PROC addNewMatch @host VARCHAR(20), @guest VARCHAR(20), @start DATETIME, @end DATETIME

 AS

DECLARE @h_id AS INT, @g_id AS INT;


SELECT @h_id = C_id 
FROM Club
Where C_name = @host

SELECT @g_id = C_id 
FROM Club
Where C_name = @guest

INSERT INTO Match VALUES (@start, @end, NULL, NULL, @h_id, @g_id)

GO

CREATE PROC addRepresentative @REPname varchar(20), @CLUBname varchar(20), @username VARCHAR(20), @password VARCHAR(20)

 AS

INSERT INTO S_User VALUES (@username, @password)

INSERT INTO Club_Rep VALUES (@REPname, @username)

DECLARE @CR_id AS INT
SELECT @CR_id = CR_id FROM Club_Rep WHERE username = @username

UPDATE Club SET CR_id = @CR_id WHERE C_name = @CLUBname

GO

CREATE PROC addStadium @name VARCHAR(20), @location VARCHAR(20), @capacity INT

 AS

INSERT INTO Stadium VALUES (@name, @location, @capacity, 1, Null)

GO

CREATE PROC addStadiumManager @name VARCHAR(20), @STADname VARCHAR(20), @username VARCHAR(20), @password VARCHAR(20)

 AS
 
INSERT INTO S_User VALUES (@username, @password)

INSERT INTO Stadium_Manager VALUES (@name,@username)

DECLARE @SM_id AS INT
SELECT @SM_id = SM_id FROM Stadium_Manager WHERE username = @username

UPDATE Stadium SET SM_id = @SM_id WHERE S_name = @STADname

GO

CREATE PROC addTicket @host VARCHAR(20), @guest VARCHAR(20), @date DATETIME

 AS

DECLARE @hid AS INT
SELECT @hid = C_id
FROM Club
WHERE C_name = @host

DECLARE @gid AS INT
SELECT @gid = C_id
FROM Club
WHERE C_name = @guest

DECLARE @mid AS INT, @sid AS INT
SELECT @mid = M_id, @sid = S_id
FROM Match
WHERE M_host_id = @hid AND M_guest_id = @gid AND M_start = @date

DECLARE @sname AS VARCHAR(20)
SELECT @sname = S_name
FROM Stadium
WHERE S_id = @sid

INSERT INTO Ticket VALUES (@sname, 1, @mid, Null)

GO

CREATE PROC acceptRequest @SM_username VARCHAR(20), @HOSTname VARCHAR(20), @GUESTname VARCHAR(20), @MATCHdate DATETIME

 AS

DECLARE @SM_id AS INT
SELECT @SM_id = SM_id FROM Stadium_Manager WHERE username = @SM_username

DECLARE @GUESTid AS INT
SELECT @GUESTid = C_id FROM Club WHERE C_name = @GUESTname

DECLARE @HOSTid AS INT
SELECT @HOSTid = C_id FROM Club WHERE C_name = @HOSTname

DECLARE @STADid AS INT, @STADcap AS INT
SELECT @STADid = S_id, @STADcap = S_capacity FROM Stadium WHERE SM_id = @SM_id

DECLARE @CR_id AS INT
SELECT @CR_id = CR_id FROM Club WHERE C_id = @HOSTid

DECLARE @M_id AS INT
SELECT @M_id = M_id FROM Match
WHERE M_host_id = @HOSTid AND M_guest_id = @GUESTid AND M_start = @MATCHdate

UPDATE Match SET S_id = @STADid, M_attendees = @STADcap WHERE M_id = @M_id

DECLARE @count AS INT
SET @count = 0

WHILE @count < @STADcap
BEGIN
EXEC addTicket @HOSTname, @GUESTname, @MATCHdate
SET @count = @count +1
END

UPDATE Host_Request SET HR_status = 'accepted' WHERE SM_id = @SM_id AND CR_id = @CR_id AND M_id = @M_id AND HR_status = 'unhandled'

GO

CREATE PROC blockFan @id VARCHAR(20)

 AS

UPDATE Fan
SET F_status = 0
WHERE F_national_id = @id

GO

CREATE PROC clearAllTables

 AS

DELETE FROM Ticket
DELETE FROM Match
DELETE FROM Club
DELETE FROM Host_Request
DELETE FROM Stadium
DELETE FROM System_Admin
DELETE FROM Sports_Association_Manager
DELETE FROM Fan
DELETE FROM Club_Rep
DELETE FROM Stadium_Manager
DELETE FROM S_User

GO

CREATE PROC createAllTables

 AS
 
CREATE TABLE S_User(
    username VARCHAR(20),
    password VARCHAR(20),
    PRIMARY KEY(username)
)

CREATE TABLE Stadium_Manager(
    SM_id INT IDENTITY,
    SM_name VARCHAR(20),
    username VARCHAR(20),
    FOREIGN KEY(username) REFERENCES S_User ON UPDATE CASCADE ON DELETE CASCADE,
    PRIMARY KEY(SM_id)
)

CREATE TABLE Club_Rep(
    CR_id INT IDENTITY,
    CR_name VARCHAR(20),
    username VARCHAR(20),
    FOREIGN KEY(username) REFERENCES S_User ON UPDATE CASCADE ON DELETE CASCADE,
    PRIMARY KEY(CR_id)
)

CREATE TABLE Fan(
    F_national_id VARCHAR(20),
    F_name VARCHAR(20),
    F_address VARCHAR(20),
    F_phone INT,
    F_birthdate DATE,
    F_status BIT,
    username VARCHAR(20),
    FOREIGN KEY(username) REFERENCES S_User ON UPDATE CASCADE ON DELETE CASCADE,
    PRIMARY KEY(F_national_id)
)

CREATE TABLE Sports_Association_Manager(
    SAM_id INT IDENTITY,
    SAM_name VARCHAR(20),
    username VARCHAR(20),
    FOREIGN KEY(username) REFERENCES S_User ON UPDATE CASCADE ON DELETE CASCADE,
    PRIMARY KEY(SAM_id)
)

CREATE TABLE System_Admin(
    SA_id INT IDENTITY,
    SA_name VARCHAR(20),
    username VARCHAR(20),
    FOREIGN KEY(username) REFERENCES S_User ON UPDATE CASCADE ON DELETE CASCADE,
    PRIMARY KEY(SA_id)
)

CREATE TABLE Stadium(
    S_id INT IDENTITY,
    S_name VARCHAR(20),
    S_location VARCHAR(20),
    S_capacity INT,
    S_status BIT,
    PRIMARY KEY(S_id),
    SM_id INT,
    FOREIGN KEY(SM_id) REFERENCES Stadium_Manager ON UPDATE CASCADE ON DELETE CASCADE
)

CREATE TABLE Host_Request(
    HR_id INT IDENTITY,
    HR_status VARCHAR(20),
    M_id INT,
    PRIMARY KEY(HR_id),
    CR_id INT,
    FOREIGN KEY(CR_id) REFERENCES Club_Rep,
    SM_id INT,
    FOREIGN KEY(SM_id) REFERENCES Stadium_Manager
)

CREATE TABLE Club(
    C_id INT IDENTITY,
    C_name VARCHAR(20),
    C_location VARCHAR(20),
    PRIMARY KEY(C_id),
    CR_id INT,
    FOREIGN KEY(CR_id) REFERENCES Club_Rep ON UPDATE CASCADE ON DELETE CASCADE
)

CREATE TABLE Match(
    M_id INT IDENTITY,
    M_start DATETIME,
    M_end DATETIME,
    M_attendees INT,
    PRIMARY KEY(M_id),
    S_id INT,
    FOREIGN KEY(S_id) REFERENCES Stadium ON UPDATE CASCADE ON DELETE CASCADE,
	M_host_id INT,
	M_guest_id INT,
	FOREIGN KEY(M_host_id) REFERENCES Club,
	FOREIGN KEY(M_guest_id) REFERENCES Club
)

CREATE TABLE Ticket(
    T_id INT IDENTITY,
    T_stadium VARCHAR(20),
    T_status BIT,
    PRIMARY KEY(T_id),
    M_id INT,
    FOREIGN KEY(M_id) REFERENCES Match ON UPDATE CASCADE ON DELETE CASCADE,
    F_national_id VARCHAR(20),
    FOREIGN KEY(F_national_id) REFERENCES Fan
)

GO

CREATE PROC deleteClub @name VARCHAR(20)

 AS

DECLARE @id AS INT
SELECT @id = C_id
FROM Club
WHERE C_name = @name

DELETE FROM Match
WHERE M_host_id = @id OR M_guest_id = @id

DELETE FROM Club
WHERE C_name = @name

GO

CREATE PROC deleteMatch @host VARCHAR(20) ,@guest VARCHAR(20)

 AS 

DECLARE @hid AS INT, @gid AS INT

SELECT @hid = C_id 
FROM Club
Where C_name = @host

SELECT @gid = C_id 
FROM Club
Where C_name = @guest

DELETE FROM Match
WHERE M_host_id = @hid AND M_guest_id = @gid

GO

CREATE PROC deleteMatchesOnStadium @name VARCHAR(20)

 AS

DECLARE @id AS INT
SELECT @id = S_id
FROM Stadium
WHERE S_name = @name

DELETE FROM Match
WHERE S_id = @id AND M_start > CURRENT_TIMESTAMP

GO

CREATE PROC deleteStadium @name VARCHAR(20)

 AS

DELETE FROM Stadium
WHERE S_name = @name

GO

CREATE PROC dropAllProceduresFunctionsViews

 AS

DROP VIEW allAssocManagers
DROP VIEW allClubRepresentatives
DROP VIEW allCLubs
DROP VIEW allFans
DROP VIEW allMatches
DROP VIEW allRequests
DROP VIEW allStadiumManagers
DROP VIEW allStadiums
DROP VIEW allTickets
DROP VIEW clubsNeverMatched
DROP VIEW clubsWithNoMatches
DROP VIEW matchesPerTeam

DROP PROC acceptRequest
DROP PROC addAssociationManager
DROP PROC addClub
DROP PROC addFan
DROP PROC addHostRequest
DROP PROC addNewMatch
DROP PROC addRepresentative
DROP PROC addStadium
DROP PROC addStadiumManager
DROP PROC addTicket
DROP PROC blockFan
DROP PROC clearAllTables
DROP PROC createAllTables
DROP PROC deleteClub
DROP PROC deleteMatch
DROP PROC deleteMatchesOnStadium
DROP PROC deleteStadium
DROP PROC dropAllTables
DROP PROC purchaseTicket
DROP PROC rejectRequest
DROP PROC unblockFan
DROP PROC updateMatchHost

DROP FUNCTION allPendingRequests
DROP FUNCTION allUnassignedMatches
DROP FUNCTION availableMatchesToAttend
DROP FUNCTION clubsNeverPlayed
DROP FUNCTION matchesRankedByAttendance
DROP FUNCTION matchWithHighestAttendance
DROP FUNCTION requestsFromClub
DROP FUNCTION upcomingMatchesOfClub
DROP FUNCTION viewAvailableStadiumsOn

GO

CREATE PROC dropAllTables

 AS

DROP TABLE Ticket
DROP TABLE Match
DROP TABLE Club
DROP TABLE Host_Request
DROP TABLE Stadium
DROP TABLE System_Admin
DROP TABLE Sports_Association_Manager
DROP TABLE Fan
DROP TABLE Club_Rep
DROP TABLE Stadium_Manager
DROP TABLE S_User

GO

CREATE PROC purchaseTicket @fanid VARCHAR(20), @host VARCHAR(20), @guest VARCHAR(20), @date DATETIME

 AS

DECLARE @hid AS INT
SELECT @hid = C_id
FROM Club
WHERE C_name = @host

DECLARE @gid AS INT
SELECT @gid = C_id
FROM Club
WHERE C_name = @guest

DECLARE @mid AS INT, @sid AS INT
SELECT @mid = M_id, @sid = S_id
FROM Match
WHERE M_host_id = @hid AND M_guest_id = @gid AND M_start = @date

DECLARE @sname AS VARCHAR(20)
SELECT @sname = S_name
FROM Stadium
WHERE S_id = @sid

UPDATE TOP (1) Ticket
SET F_national_id = @fanid, T_status = 0
WHERE M_id = @mid AND T_status = 1

GO

CREATE PROC rejectRequest @SM_username VARCHAR(20), @HOSTname VARCHAR(20), @GUESTname VARCHAR(20), @MATCHdate DATETIME

 AS

DECLARE @SM_id AS INT
SELECT @SM_id = SM_id FROM Stadium_Manager WHERE username = @SM_username

DECLARE @GUESTid AS INT
SELECT @GUESTid = C_id FROM Club WHERE C_name = @GUESTname

DECLARE @HOSTid AS INT
SELECT @HOSTid = C_id FROM Club WHERE C_name = @HOSTname

DECLARE @STADid AS INT
SELECT @STADid = S_id FROM Stadium WHERE SM_id = @SM_id

DECLARE @CR_id AS INT
SELECT @CR_id = CR_id FROM Club WHERE C_id = @HOSTid

DECLARE @M_id AS INT
SELECT @M_id = M_id FROM Match
WHERE M_host_id = @HOSTid AND M_guest_id = @GUESTid AND M_start = @MATCHdate

UPDATE Host_Request SET HR_status = 'rejected' WHERE SM_id = @SM_id AND CR_id = @CR_id AND M_id = @M_id AND HR_status = 'unhandled'

GO

CREATE PROC unblockFan @id VARCHAR(20)

 AS

UPDATE Fan
SET F_status = 1
WHERE F_national_id = @id

GO

CREATE PROC updateMatchHost @host VARCHAR(20), @guest VARCHAR(20), @date DATETIME

 AS

DECLARE @hid AS INT
SELECT @hid = C_id
FROM Club
WHERE C_name = @host

DECLARE @gid AS INT
SELECT @gid = C_id
FROM Club
WHERE C_name = @guest

UPDATE Match
SET M_host_id = @gid, M_guest_id = @hid
WHERE M_start = @date AND M_host_id = @hid AND M_guest_id = @gid

GO

CREATE PROC purchaseTicket_M3 @fanid VARCHAR(20), @host VARCHAR(20), @guest VARCHAR(20)

 AS

DECLARE @hid AS INT
SELECT @hid = C_id
FROM Club
WHERE C_name = @host

DECLARE @gid AS INT
SELECT @gid = C_id
FROM Club
WHERE C_name = @guest

DECLARE @mid AS INT, @sid AS INT
SELECT @mid = M_id, @sid = S_id
FROM Match
WHERE M_host_id = @hid AND M_guest_id = @gid

DECLARE @sname AS VARCHAR(20)
SELECT @sname = S_name
FROM Stadium
WHERE S_id = @sid

UPDATE TOP (1) Ticket
SET F_national_id = @fanid, T_status = 0
WHERE M_id = @mid AND T_status = 1

GO

CREATE PROC sendRequest
@CRusername VARCHAR(20), @SMname VARCHAR(20), @date DATETIME

 AS


DECLARE @cname AS VARCHAR(20)
SELECT @cname = C_name
FROM Club_Rep CR INNER JOIN Club C ON C.CR_id = CR.CR_id
WHERE @CRusername = CR.username

DECLARE @sname AS VARCHAR(20)
SELECT @sname = S_name
FROM Stadium_Manager SM INNER JOIN Stadium S ON SM.SM_id = S.SM_id
WHERE @SMname = SM.SM_name

EXEC addHostRequest @cname, @sname, @date

GO

CREATE PROC stadiumManaged
@username VARCHAR(20)

 AS

SELECT S_name AS Name, S_location AS Location, S_capacity AS Capacity, S_status AS Status
FROM Stadium S INNER JOIN Stadium_Manager SM ON S.SM_id = SM.SM_id
WHERE SM.username = @username

GO

CREATE PROC userlogin
@username VARCHAR(20),
@password VARCHAR(20),
@success BIT OUTPUT,
@type INT OUTPUT

 AS

BEGIN

IF exists(
SELECT username, password
FROM S_User
WHERE username = @username AND password = @password)
BEGIN

	Set @success = 1;

	IF EXISTS(
	SELECT username
	FROM System_Admin
	WHERE username = @username
	)
	BEGIN
	SET @type = 1
	END

	ELSE IF EXISTS(
	SELECT username
	FROM Sports_Association_Manager
	WHERE username = @username
	)
	BEGIN
	SET @type = 2
	END

	ELSE IF EXISTS(
	SELECT username
	FROM Club_Rep
	WHERE username = @username
	)
	BEGIN
	SET @type = 3
	END

	ELSE IF EXISTS(
	SELECT username
	FROM Stadium_Manager
	WHERE username = @username
	)
	BEGIN
	SET @type = 4
	END

	ELSE IF EXISTS(
	SELECT username
	FROM Fan
	WHERE username = @username
	)
	BEGIN
	SET @type = 5
	END

END

ELSE
BEGIN

	Set @success = 0;

END

END

GO

CREATE PROC checkMatchExists
@host VARCHAR(20),
@guest VARCHAR(20),
@success BIT OUTPUT

 AS

IF EXISTS(
SELECT M_id
FROM Match M INNER JOIN Club C1 ON C1.C_id = M.M_host_id INNER JOIN Club C2 ON C2.C_id = M.M_guest_id
WHERE C1.C_name = @host AND C2.C_name = @guest
)
	SET @success = 1
ELSE
	SET @success = 0

GO

CREATE PROC checkUsernameDistinct
@username VARCHAR(20),
@success BIT OUTPUT

 AS

IF EXISTS(
SELECT username
FROM S_User
WHERE username = @username
)
	SET @success = 0
ELSE
	SET @success = 1

GO

CREATE PROC checkDateValid
@username VARCHAR(20),
@date DATETIME,
@success BIT OUTPUT

 AS

IF EXISTS(
SELECT M_id
FROM Match M INNER JOIN Club C ON M.M_host_id = C.C_id INNER JOIN Club_Rep CR ON C.CR_id = CR.CR_id
WHERE M_start = @date AND CR.username = @username
)
	SET @success = 1
ELSE
	SET @success = 0

GO

CREATE PROC checkNatidDistinct
@id VARCHAR(20),
@success BIT OUTPUT

 AS

IF EXISTS(
SELECT F_name
FROM Fan
WHERE F_national_id = @id
)
	SET @success = 0
ELSE
	SET @success = 1

GO

CREATE PROC checkMatchExistsAndNotStarted
@host VARCHAR(20),
@guest VARCHAR(20),
@success BIT OUTPUT

 AS

IF EXISTS(
SELECT M_id
FROM Match M INNER JOIN Club C1 ON C1.C_id = M.M_host_id INNER JOIN Club C2 ON C2.C_id = M.M_guest_id
WHERE C1.C_name = @host AND C2.C_name = @guest AND M_start > CURRENT_TIMESTAMP
)
	SET @success = 1
ELSE
	SET @success = 0

GO

CREATE PROC checkTicketAvailable
@host VARCHAR(20),
@guest VARCHAR(20),
@success BIT OUTPUT

 AS


IF EXISTS(
SELECT M_id
FROM Club C1 INNER JOIN Match M ON C1.C_id = M.M_host_id INNER JOIN Club C2 ON C2.C_id = M.M_guest_id
WHERE M.M_start > CURRENT_TIMESTAMP AND C1.C_name = @host AND C2.C_name = @guest AND EXISTS
(
SELECT T.T_id
FROM Ticket T
WHERE M.M_id = T.M_id AND T.T_status = 1
)
)
	SET @success = 1
ELSE
	SET @success = 0

GO

CREATE PROC checkStartToEnd
@start DATETIME,
@end DATETIME,
@success BIT OUTPUT

 AS

IF @end > @start
	SET @success = 1
ELSE
	SET @success = 0

GO

CREATE PROC checkTimeStamp
@date DATETIME,
@success BIT OUTPUT

 AS

IF @date > CURRENT_TIMESTAMP
	SET @success = 1
ELSE
	SET @success = 0

GO

CREATE PROC checkStadExists
@name VARCHAR(20),
@success BIT OUTPUT

 AS

IF EXISTS(
SELECT S_id
FROM Stadium
WHERE S_name = @name
)
	SET @success = 1
ELSE
	SET @success = 0

GO

CREATE PROC checkSMExists
@name VARCHAR(20),
@success BIT OUTPUT

 AS

IF EXISTS(
SELECT SM_id
FROM Stadium_Manager
WHERE SM_name = @name
)
	SET @success = 1
ELSE
	SET @success = 0

GO

CREATE PROC clubRepresented
@username VARCHAR(20)

 AS

SELECT C_name AS Name, C_location AS Location
FROM Club C INNER JOIN Club_Rep CR ON C.CR_id = CR.CR_id
WHERE CR.username = @username

GO

CREATE PROC checkClubExists
@name VARCHAR(20),
@success BIT OUTPUT

 AS

IF EXISTS(
SELECT C_id
FROM Club
WHERE C_name = @name
)
	SET @success = 1
ELSE
	SET @success = 0

GO

CREATE PROC checkRequestExists
@username VARCHAR(20),
@host VARCHAR(20),
@guest VARCHAR(20),
@date DATETIME,
@success BIT OUTPUT

 AS

IF EXISTS(
SELECT HR_id
FROM Host_Request HR INNER JOIN Stadium_Manager SM ON HR.SM_id = SM.SM_id 
					 INNER JOIN Match M ON M.M_id = HR.M_id
					 INNER JOIN Club C1 ON C1.C_id = M.M_host_id
					 INNER JOIN Club C2 ON C2.C_id = M.M_guest_id
WHERE SM.username = @username AND C1.C_name = @host AND C2.C_name = @guest AND M.M_start = @date
)
	SET @success = 1
ELSE
	SET @success = 0

GO

----------------FUNCTIONS----------------
--9

CREATE FUNCTION allPendingRequests
(@SM_username VARCHAR(20))
RETURNS @Requests TABLE (Club_Representative VARCHAR(20), Guest_Club VARCHAR(20), Start DATETIME)

 AS

BEGIN

DECLARE @SM_id AS INT
SELECT @SM_id = SM_id 
FROM Stadium_Manager 
WHERE username = @SM_username

INSERT INTO @Requests 
SELECT CR.CR_name, C.C_name, M_start 
FROM Host_Request HR INNER JOIN Club_Rep CR ON HR.CR_id = CR.CR_id INNER JOIN Match M ON M.M_id = HR.M_id INNER JOIN Club C ON M.M_guest_id = C.C_id 
WHERE HR.HR_status = 'unhandled' AND HR.SM_id = @SM_id

RETURN
END

GO

CREATE FUNCTION allUnassignedMatches
(@CLUBname VARCHAR(20))
RETURNS @Match TABLE (Guest_Club VARCHAR(20), Start DATETIME)

 AS

BEGIN

DECLARE @CLUBid AS INT
SELECT @CLUBid = C_id 
FROM Club 
WHERE C_name = @CLUBname

INSERT INTO @Match 
SELECT C.C_name, M.M_start 
FROM Match M INNER JOIN Club C ON C_id = M_guest_id 
WHERE M_host_id = @CLUBid AND S_id IS NULL

RETURN
END

GO

CREATE FUNCTION availableMatchesToAttend
(@date DATETIME)
RETURNS @RESULT TABLE(Host VARCHAR(20), Guest VARCHAR(20), Start DATETIME, Stadium VARCHAR(20))

 AS

BEGIN

INSERT INTO @RESULT
SELECT DISTINCT C1.C_name, C2.C_name, M.M_start, T.T_stadium
FROM Club C1 INNER JOIN Match M ON C1.C_id = M.M_host_id INNER JOIN Club C2 ON C2.C_id = M.M_guest_id INNER JOIN Ticket T ON T.M_id = M.M_id
WHERE M.M_start > @date AND EXISTS
(
SELECT T.T_id
FROM Ticket T
WHERE M.M_id = T.M_id AND T.T_status = 1
)

RETURN
END

GO

CREATE FUNCTION clubsNeverPlayed
(@CLUBname VARCHAR(20))
RETURNS @RESULT TABLE(Club VARCHAR(20))

 AS

BEGIN

DECLARE @ID AS INT
SELECT @ID = C_id
FROM Club
WHERE C_name = @CLUBname

INSERT INTO @RESULT
SELECT C_name
FROM Club
WHERE C_id != @ID AND NOT EXISTS
(
SELECT C_name
FROM Match M
WHERE ((M.M_host_id = @ID AND M.M_guest_id = C_id) OR (M.M_guest_id = @ID AND M.M_host_id = C_id)) AND (M_start <= CURRENT_TIMESTAMP)
)

RETURN
END

GO

CREATE FUNCTION matchesRankedByAttendance
()
RETURNS @RESULT TABLE(Host VARCHAR(20), Guest VARCHAR(20))

 AS

BEGIN

INSERT INTO @RESULT
SELECT C1.C_name, C2.C_name
FROM Ticket T INNER JOIN Match M ON T.M_id = M.M_id INNER JOIN Club C1 ON C1.C_id = M.M_host_id INNER JOIN Club C2 ON C2.C_id = M.M_guest_id
WHERE T.T_status = 0 AND M_start <= CURRENT_TIMESTAMP
GROUP BY C1.C_name, C2.C_name, M.M_id
ORDER BY count(T_id) DESC
OFFSET 0 ROWS

RETURN
END

GO

CREATE FUNCTION matchWithHighestAttendance
()
RETURNS @RESULT TABLE(Host VARCHAR(20), Guest VARCHAR(20))

 AS

BEGIN

INSERT INTO @RESULT
SELECT TOP 1 C1.C_name, C2.C_name
FROM Ticket T INNER JOIN Match M ON T.M_id = M.M_id INNER JOIN Club C1 ON C1.C_id = M.M_host_id INNER JOIN Club C2 ON C2.C_id = M.M_guest_id
WHERE T.T_status = 0
GROUP BY C1.C_name, C2.C_name, M.M_id
ORDER BY count(T_id) DESC

RETURN
END

GO

CREATE FUNCTION requestsFromClub 
(@STADname VARCHAR(20), @CLUBname VARCHAR(20))
RETURNS @RESULT TABLE(Host VARCHAR(20), Guest VARCHAR(20))

 AS

BEGIN

DECLARE @SMid AS INT
SELECT @SMid = SM_id
FROM Stadium
WHERE S_name = @STADname

DECLARE @CRid AS INT
SELECT @CRid = CR_id
FROM Club
WHERE C_name = @CLUBname

INSERT INTO @RESULT
SELECT C1.C_name AS Host, C2.C_name AS Guest
FROM Club C1 INNER JOIN Match M ON C1.C_id = M.M_host_id INNER JOIN Club C2 ON C2.C_id = M.M_guest_id
WHERE M_id IN
(
SELECT M_id
FROM Host_Request
WHERE CR_id = @CRid AND SM_id = @SMid
)

RETURN
END

GO

CREATE FUNCTION upcomingMatchesOfClub
(@CLUBname VARCHAR(20))
RETURNS @RESULT TABLE(Club VARCHAR(20), Competing_Club VARCHAR(20), Start DATETIME, Stadium VARCHAR(20))

 AS

BEGIN

DECLARE @cid AS INT
SELECT @cid = C_id
FROM Club
WHERE C_name = @CLUBname

INSERT INTO @RESULT
SELECT @CLUBname, C_name, M_start, S_name
FROM Match M INNER JOIN Club C ON C_id = M_host_id LEFT OUTER JOIN Stadium S ON S.S_id = M.S_id
WHERE M_start > CURRENT_TIMESTAMP AND M_guest_id = @cid

UNION

SELECT @CLUBname, C_name, M_start, S_name
FROM Match M INNER JOIN Club C ON C_id = M_guest_id LEFT OUTER JOIN Stadium S ON S.S_id = M.S_id
WHERE M_start > CURRENT_TIMESTAMP AND M_host_id = @cid

RETURN
END

GO

CREATE FUNCTION viewAvailableStadiumsOn
(@date DATETIME) 
RETURNS @Stadium TABLE (Name VARCHAR(20), Location VARCHAR(20), Capacity INT)

 AS

BEGIN

INSERT INTO @Stadium 
SELECT S.S_name, S.S_location, S.S_capacity 
FROM Stadium S
WHERE S.S_status = 1 AND NOT Exists
(
SELECT M.S_id 
FROM Match M
WHERE M.M_start = @date AND M.S_id = S.S_id
)

RETURN
END

GO

CREATE FUNCTION upcomingMatchesfunc
(@username VARCHAR(20))
RETURNS @RESULT TABLE(Host VARCHAR(20), Guest VARCHAR(20), Start_Time DATETIME, End_Time DATETIME, Stadium VARCHAR(20))

 AS


BEGIN

DECLARE @cid AS INT
SELECT @cid = C_id
FROM Club C INNER JOIN Club_Rep CR ON C.CR_id = CR.CR_id
WHERE CR.username = @username

INSERT INTO @RESULT
SELECT C1.C_name, C2.C_name, M.M_start, M.M_end, S_name
FROM (Match M INNER JOIN Club C1 ON M.M_host_id = C1.C_id)
						INNER JOIN Club C2 ON M.M_guest_id = C2.C_id LEFT OUTER JOIN Stadium S ON M.S_id = S.S_id
WHERE M.M_start > CURRENT_TIMESTAMP AND C1.C_id = @cid

UNION

SELECT C1.C_name, C2.C_name, M.M_start, M.M_end, S_name
FROM (Match M INNER JOIN Club C1 ON M.M_host_id = C1.C_id)
						INNER JOIN Club C2 ON M.M_guest_id = C2.C_id LEFT OUTER JOIN Stadium S ON M.S_id = S.S_id
WHERE M.M_start > CURRENT_TIMESTAMP AND C2.C_id = @cid

RETURN
END

GO

CREATE FUNCTION allRequestsfunc
(@username VARCHAR(20))
RETURNS @RESULT TABLE(Club_Representative VARCHAR(20), Host_Club VARCHAR(20), Guest_Club VARCHAR(20), Start_Time DATETIME, End_Time DATETIME, Status VARCHAR(20))

 AS

BEGIN

INSERT INTO @RESULT
SELECT CR.CR_name, C1.C_name, C2.C_name, M.M_start, M.M_end, HR.HR_status
FROM Club_Rep CR INNER JOIN Host_Request HR ON CR.CR_id = HR.CR_id 
	             INNER JOIN Stadium_Manager SM ON SM.SM_id = HR.SM_id
				 INNER JOIN Match M ON HR.M_id = M.M_id
				 INNER JOIN Club C1 ON M.M_host_id = C1.C_id
				 INNER JOIN Club C2 ON M.M_guest_id = C2.C_id
WHERE SM.username = @username

RETURN
END

GO

CREATE FUNCTION availableMatchesToAttend_M3rpoc
(@date DATETIME)
RETURNS @RESULT TABLE(Host VARCHAR(20), Guest VARCHAR(20), Stadium VARCHAR(20), Location VARCHAR(20))

 AS

BEGIN

INSERT INTO @RESULT
SELECT DISTINCT C1.C_name, C2.C_name, S.S_name, S.S_location
FROM Club C1 INNER JOIN Match M ON C1.C_id = M.M_host_id INNER JOIN Club C2 ON C2.C_id = M.M_guest_id INNER JOIN Stadium S ON S.S_id = M.S_id
WHERE M.M_start > @date AND EXISTS
(
SELECT T.T_id
FROM Ticket T
WHERE M.M_id = T.M_id AND T.T_status = 1
)

RETURN
END