CREATE VIEW vw_mowlist AS 
select distinct
	mow.MyOwnWordID AS MyOwnWordID,
	mow.MyOwnWordUID AS MyOwnWordUID,
	mow.WordName AS WordName,
	mow.Translate AS Translate,
	CASE WHEN p.PhotoUID != '' THEN
		1
	ELSE 
		0
	END AS IsExistsPhoto,
	CASE WHEN s.SentenceUID != '' THEN
		1
	ELSE 
		0
	END AS IsExistsSentence,
	CASE WHEN r.RecordingUID != '' THEN
		1
	ELSE 
		0
	END AS IsExistsRecording,
	mow.UserID as UserID
from MyOwnWord as mow
left join Photo as p on mow.MyOwnWordID = p.MyOwnWordID
left join Sentence as s on mow.MyOwnWordID = s.MyOwnWordID
left join Recording as r on mow.MyOwnWordID = r.MyOwnWordID