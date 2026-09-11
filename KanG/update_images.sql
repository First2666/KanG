SET NOCOUNT ON;

UPDATE PlaceImages SET ImageUrl = N'https://upload.wikimedia.org/wikipedia/commons/4/44/Bridge_over_River_Kwai.jpg' WHERE PlaceId = 1;
UPDATE PlaceImages SET ImageUrl = N'https://upload.wikimedia.org/wikipedia/commons/a/a6/Erawan_Waterfall.jpg' WHERE PlaceId = 2 AND IsPrimary = 1;
UPDATE PlaceImages SET ImageUrl = N'https://upload.wikimedia.org/wikipedia/commons/2/29/Erawan_National_Park_Kanchanaburi.jpg' WHERE PlaceId = 2 AND IsPrimary = 0;
UPDATE PlaceImages SET ImageUrl = N'https://upload.wikimedia.org/wikipedia/commons/f/fe/Death_Railway%2C_River_Khwae.jpg' WHERE PlaceId = 3;
UPDATE PlaceImages SET ImageUrl = N'https://upload.wikimedia.org/wikipedia/commons/4/44/Prasat_Muang_Sing_Historical_Park%2C_Thakilen%2C_Thailand_%28368971761%29.jpg' WHERE PlaceId = 4;
UPDATE PlaceImages SET ImageUrl = N'https://upload.wikimedia.org/wikipedia/commons/c/c1/Srinagarind_Dam.jpg' WHERE PlaceId = 5;

PRINT 'Updated Image URLs successfully!';
