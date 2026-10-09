-- SQL Script: Add 10 new places to KanG_Db (Attractions, Accommodations, Restaurants)
SET NOCOUNT ON;

DECLARE @Now DATETIME2 = SYSUTCDATETIME();

-- =========================================================================
-- 1. วัดถ้ำเสือ (Wat Tham Suea) [Attraction]
-- =========================================================================
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES (13.9538, 99.6053, N'ต.ม่วงชุม อ.ท่าม่วง จ.กาญจนบุรี 71110', N'034-655-383', N'');
DECLARE @Loc11 INT = SCOPE_IDENTITY();

INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, EntranceFee)
VALUES (N'วัดถ้ำเสือ (Wat Tham Suea)', 
        N'วัดสวยอันดับ 1 ของเมืองกาญจน์ โดดเด่นด้วยหลวงพ่อชินประทานพร พระพุทธรูปปางประทานพรที่ใหญ่ที่สุดในจังหวัดกาญจนบุรี ประดิษฐานอยู่บนยอดเขา พร้อมพระเจดีย์เกษแก้วมหาปราสาท ชมวิวทุ่งนาเขียวขจีแบบ 360 องศา',
        @Loc11, 13.9538, 99.6053, '07:30:00', '17:00:00', @Now, @Now, 'Attraction', 0.00);
DECLARE @Place11 INT = SCOPE_IDENTITY();

INSERT INTO PlaceImages (ImageUrl, Description, IsPrimary, PlaceId, CreatedAt)
VALUES (N'https://upload.wikimedia.org/wikipedia/commons/thumb/c/c2/Wat_Tham_Sua%2C_Tha_Muang%2C_Kanchanaburi_%28I%29.jpg/1280px-Wat_Tham_Sua%2C_Tha_Muang%2C_Kanchanaburi_%28I%29.jpg', N'วัดถ้ำเสือ ท่าม่วง', 1, @Place11, @Now),
       (N'https://images.unsplash.com/photo-1598971861713-54ad16a7e72e?q=80&w=1000', N'วิวทุ่งนาหลังวัดถ้ำเสือ', 0, @Place11, @Now);

INSERT INTO PlaceCategories (PlaceId, CategoryId) VALUES (@Place11, 1), (@Place11, 2);
INSERT INTO PlaceTags (PlaceId, TagId) VALUES (@Place11, 1), (@Place11, 2), (@Place11, 4), (@Place11, 7);

-- =========================================================================
-- 2. สะพานมอญ (Mon Bridge) [Attraction]
-- =========================================================================
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES (15.1432, 98.4503, N'ซอยสะพานไม้ ต.หนองลู อ.สังขละบุรี จ.กาญจนบุรี 71240', N'034-595-093', N'');
DECLARE @Loc12 INT = SCOPE_IDENTITY();

INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, EntranceFee)
VALUES (N'สะพานมอญ (Mon Bridge)', 
        N'สะพานไม้อุตตมานุสรณ์ สะพานไม้ที่ยาวที่สุดในประเทศไทยและยาวเป็นอันดับ 2 ของโลก ทอดข้ามแม่น้ำซองกาเลีย เชื่อมโยงวัฒนธรรมไทย-มอญ สัมผัสวิถีชีวิตชาวมอญ ตักบาตรยามเช้า และชมสายหมอกเหนือน้ำ',
        @Loc12, 15.1432, 98.4503, '06:00:00', '20:00:00', @Now, @Now, 'Attraction', 0.00);
DECLARE @Place12 INT = SCOPE_IDENTITY();

INSERT INTO PlaceImages (ImageUrl, Description, IsPrimary, PlaceId, CreatedAt)
VALUES (N'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/Mon_Bridge%2C_Sangkhlaburi%2C_Thailand.jpg/1280px-Mon_Bridge%2C_Sangkhlaburi%2C_Thailand.jpg', N'สะพานมอญ สังขละบุรี', 1, @Place12, @Now),
       (N'https://images.unsplash.com/photo-1508009603885-50cf7c579365?q=80&w=1000', N'บรรยากาศแม่น้ำซองกาเลีย', 0, @Place12, @Now);

INSERT INTO PlaceCategories (PlaceId, CategoryId) VALUES (@Place12, 1), (@Place12, 2);
INSERT INTO PlaceTags (PlaceId, TagId) VALUES (@Place12, 1), (@Place12, 3), (@Place12, 4), (@Place12, 7);

-- =========================================================================
-- 3. น้ำตกห้วยแม่ขมิ้น (Huay Mae Khamin Waterfall) [Attraction]
-- =========================================================================
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES (14.6381, 99.0433, N'อุทยานแห่งชาติเขื่อนศรีนครินทร์ ต.แม่กระบุง อ.ศรีสวัสดิ์ จ.กาญจนบุรี 71250', N'034-546-819', N'');
DECLARE @Loc13 INT = SCOPE_IDENTITY();

INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, EntranceFee)
VALUES (N'น้ำตกห้วยแม่ขมิ้น', 
        N'ม่านน้ำตกหินปูนที่งดงามที่สุดในอุทยานแห่งชาติเขื่อนศรีนครินทร์ มีทั้งหมด 7 ชั้น ไหลลดหลั่นเป็นชั้นหินปูนสวยงาม โดยเฉพาะชั้นที่ 4 ฉัตรแก้ว ที่ถือเป็นแลนด์มาร์คสำคัญ น้ำใสสะอาดท่ามกลางป่าเบญจพรรณที่อุดมสมบูรณ์',
        @Loc13, 14.6381, 99.0433, '08:00:00', '16:30:00', @Now, @Now, 'Attraction', 100.00);
DECLARE @Place13 INT = SCOPE_IDENTITY();

INSERT INTO PlaceImages (ImageUrl, Description, IsPrimary, PlaceId, CreatedAt)
VALUES (N'https://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Huay_Mae_Khamin_Waterfall_Kanchanaburi_01.jpg/1280px-Huay_Mae_Khamin_Waterfall_Kanchanaburi_01.jpg', N'น้ำตกห้วยแม่ขมิ้น ชั้นฉัตรแก้ว', 1, @Place13, @Now),
       (N'https://images.unsplash.com/photo-1546708973-b339540b5162?q=80&w=1000', N'ธรรมชาติผืนป่าห้วยแม่ขมิ้น', 0, @Place13, @Now);

INSERT INTO PlaceCategories (PlaceId, CategoryId) VALUES (@Place13, 1), (@Place13, 3);
INSERT INTO PlaceTags (PlaceId, TagId) VALUES (@Place13, 1), (@Place13, 2), (@Place13, 3), (@Place13, 4);

-- =========================================================================
-- 4. Z9 Resort [Accommodation]
-- =========================================================================
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES (14.4721, 99.1154, N'เขื่อนศรีนครินทร์ ต.ท่ากระดาน อ.ศรีสวัสดิ์ จ.กาญจนบุรี 71250', N'063-239-4459', N'https://z9resorts.com');
DECLARE @Loc14 INT = SCOPE_IDENTITY();

INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, AccommodationType, PricePerNight, MaxGuests)
VALUES (N'Z9 Resort', 
        N'ที่พักแพลอยน้ำสไตล์โมเดิร์นลักชัวรีสุดฮิตริมเขื่อนศรีนครินทร์ ดีไซน์กลมกลืนกับธรรมชาติแบบมินิมอล มีกิจกรรมพายเรือคายัค พายซับบอร์ด พร้อมวิวพระอาทิตย์ตกดินสุดโรแมนติก',
        @Loc14, 14.4721, 99.1154, '00:00:00', '23:59:00', @Now, @Now, 'Accommodation', 1, 4500.00, 2);
DECLARE @Place14 INT = SCOPE_IDENTITY();

INSERT INTO PlaceImages (ImageUrl, Description, IsPrimary, PlaceId, CreatedAt)
VALUES (N'https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?q=80&w=1000', N'Z9 Resort แพพักโมเดิร์นริมเขื่อน', 1, @Place14, @Now),
       (N'https://images.unsplash.com/photo-1566073771259-6a8506099945?q=80&w=1000', N'บรรยากาศยามเย็นริมผืนน้ำ', 0, @Place14, @Now);

INSERT INTO PlaceCategories (PlaceId, CategoryId) VALUES (@Place14, 1), (@Place14, 5);
INSERT INTO PlaceTags (PlaceId, TagId) VALUES (@Place14, 1), (@Place14, 3), (@Place14, 7), (@Place14, 8);

-- =========================================================================
-- 5. Cross River Kwai Resort [Accommodation]
-- =========================================================================
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES (14.0112, 99.4523, N'138 หมู่ 4 ต.หนองหญ้า อ.เมือง จ.กาญจนบุรี 71000', N'034-552-124', N'https://crossriverkwai.com');
DECLARE @Loc15 INT = SCOPE_IDENTITY();

INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, AccommodationType, PricePerNight, MaxGuests)
VALUES (N'Cross River Kwai Resort', 
        N'รีสอร์ทดีไซน์ระดับ 5 ดาวริมแม่น้ำแควน้อย สถาปัตยกรรมสุดชิคแนวโมเดิร์นอินดัสเทรียล ห้องพักหันหน้าสู่แม่น้ำทุกห้อง พร้อมสระว่ายน้ำระบบเกลือวิวแม่น้ำแบบ Panoramic',
        @Loc15, 14.0112, 99.4523, '00:00:00', '23:59:00', @Now, @Now, 'Accommodation', 1, 5500.00, 2);
DECLARE @Place15 INT = SCOPE_IDENTITY();

INSERT INTO PlaceImages (ImageUrl, Description, IsPrimary, PlaceId, CreatedAt)
VALUES (N'https://images.unsplash.com/photo-1540555700478-4be289fbecef?q=80&w=1000', N'Cross River Kwai สระว่ายน้ำวิวแม่น้ำ', 1, @Place15, @Now),
       (N'https://images.unsplash.com/photo-1571896349842-33c89424de2d?q=80&w=1000', N'ห้องพักลักชัวรีริมน้ำแควน้อย', 0, @Place15, @Now);

INSERT INTO PlaceCategories (PlaceId, CategoryId) VALUES (@Place15, 5);
INSERT INTO PlaceTags (PlaceId, TagId) VALUES (@Place15, 1), (@Place15, 2), (@Place15, 3), (@Place15, 8);

-- =========================================================================
-- 6. The Campster Kanchanaburi [Accommodation]
-- =========================================================================
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES (13.9876, 99.5211, N'287 ต.เกาะสำโรง อ.เมือง จ.กาญจนบุรี 71000', N'092-262-6999', N'');
DECLARE @Loc16 INT = SCOPE_IDENTITY();

INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, AccommodationType, PricePerNight, MaxGuests)
VALUES (N'The Campster Kanchanaburi', 
        N'ที่พักสไตล์แคมป์ปิ้งและรถบ้านสุดชิคริมแม่น้ำแคว บรรยากาศแคมป์ไฟใต้แสงดาว เต็นท์ติดแอร์สุดสบาย พร้อมกิจกรรมพายเรือคายัค และสระว่ายน้ำกลางแจ้ง',
        @Loc16, 13.9876, 99.5211, '00:00:00', '23:59:00', @Now, @Now, 'Accommodation', 4, 2800.00, 3);
DECLARE @Place16 INT = SCOPE_IDENTITY();

INSERT INTO PlaceImages (ImageUrl, Description, IsPrimary, PlaceId, CreatedAt)
VALUES (N'https://images.unsplash.com/photo-1510312305653-8ed496efae75?q=80&w=1000', N'The Campster แคมป์ปิ้งริมน้ำแคว', 1, @Place16, @Now),
       (N'https://images.unsplash.com/photo-1523987355523-c7b5b0dd90a7?q=80&w=1000', N'บรรยากาศเต็นท์พักผ่อนกลางธรรมชาติ', 0, @Place16, @Now);

INSERT INTO PlaceCategories (PlaceId, CategoryId) VALUES (@Place16, 1), (@Place16, 5);
INSERT INTO PlaceTags (PlaceId, TagId) VALUES (@Place16, 1), (@Place16, 3), (@Place16, 4), (@Place16, 6);

-- =========================================================================
-- 7. The Village Farm To Cafe [Restaurant]
-- =========================================================================
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES (13.9982, 99.4124, N'88/8 ม.4 ต.หนองหญ้า อ.เมือง จ.กาญจนบุรี 71000', N'034-540-599', N'');
DECLARE @Loc17 INT = SCOPE_IDENTITY();

INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, FoodType)
VALUES (N'The Village Farm To Cafe', 
        N'คาเฟ่ชื่อดังในโรงเรือนกระจกยักษ์ โอบล้อมด้วยภูเขาและป่าไผ่ธรรมชาติ มีทั้งอาหารคาว เบเกอรี่โฮมเมด และขนมหวานซิกเนเจอร์เมล่อนสดจากฟาร์ม บรรยากาศร่มรื่นมีมุมถ่ายรูปเพียบ',
        @Loc17, 13.9982, 99.4124, '09:00:00', '21:00:00', @Now, @Now, 'Restaurant', 2);
DECLARE @Place17 INT = SCOPE_IDENTITY();

INSERT INTO PlaceImages (ImageUrl, Description, IsPrimary, PlaceId, CreatedAt)
VALUES (N'https://images.unsplash.com/photo-1554118811-1e0d58224f24?q=80&w=1000', N'The Village Farm To Cafe โรงเรือนกระจก', 1, @Place17, @Now),
       (N'https://images.unsplash.com/photo-1559925393-8be0ec4767c8?q=80&w=1000', N'เบเกอรี่และกาแฟซิกเนเจอร์', 0, @Place17, @Now);

INSERT INTO PlaceCategories (PlaceId, CategoryId) VALUES (@Place17, 1), (@Place17, 4);
INSERT INTO PlaceTags (PlaceId, TagId) VALUES (@Place17, 1), (@Place17, 2), (@Place17, 4), (@Place17, 5);

-- =========================================================================
-- 8. ร้านอาหารพริกแกง (Prik Kaeng Restaurant) [Restaurant]
-- =========================================================================
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES (13.9785, 99.5891, N'ต.ลาดหญ้า อ.เมือง จ.กาญจนบุรี 71190', N'034-589-185', N'');
DECLARE @Loc18 INT = SCOPE_IDENTITY();

INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, FoodType)
VALUES (N'ร้านอาหารพริกแกง', 
        N'ร้านอาหารป่ารสจัดจ้านชื่อดังระดับตำนานเมืองกาญจน์ การันตีความแซ่บด้วยเมนูแกงป่าเนื้อสับ แกงคั่วหอยขม ปลากังผัดฉ่า รสชาติเครื่องแกงเข้มข้นถึงใจสูตรโบราณ',
        @Loc18, 13.9785, 99.5891, '10:30:00', '20:00:00', @Now, @Now, 'Restaurant', 0);
DECLARE @Place18 INT = SCOPE_IDENTITY();

INSERT INTO PlaceImages (ImageUrl, Description, IsPrimary, PlaceId, CreatedAt)
VALUES (N'https://images.unsplash.com/photo-1544025162-d76694265947?q=80&w=1000', N'อาหารป่ารสจัดจ้าน ร้านพริกแกง', 1, @Place18, @Now),
       (N'https://images.unsplash.com/photo-1555396273-367ea4eb4db5?q=80&w=1000', N'บรรยากาศร้านอาหารพื้นบ้าน', 0, @Place18, @Now);

INSERT INTO PlaceCategories (PlaceId, CategoryId) VALUES (@Place18, 4);
INSERT INTO PlaceTags (PlaceId, TagId) VALUES (@Place18, 2), (@Place18, 4), (@Place18, 7);

-- =========================================================================
-- 9. BARME Tea&Taste [Restaurant]
-- =========================================================================
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES (14.0045, 99.4978, N'วัดถ้ำเขาปูน ต.หนองหญ้า อ.เมือง จ.กาญจนบุรี 71000', N'092-493-5091', N'');
DECLARE @Loc19 INT = SCOPE_IDENTITY();

INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, FoodType)
VALUES (N'BARME Tea&Taste', 
        N'คาเฟ่วิวหลักล้านบนเนินเขาข้างวัดถ้ำเขาปูน มองเห็นโค้งแม่น้ำแควน้อย ทางรถไฟ และสะพานมรณะแบบพาโนรามา 180 องศา จิบชาหอมๆ ชิมขนมเค้กรับลมเย็นสบาย',
        @Loc19, 14.0045, 99.4978, '09:00:00', '18:00:00', @Now, @Now, 'Restaurant', 2);
DECLARE @Place19 INT = SCOPE_IDENTITY();

INSERT INTO PlaceImages (ImageUrl, Description, IsPrimary, PlaceId, CreatedAt)
VALUES (N'https://images.unsplash.com/photo-1501339847302-ac426a4a7cbb?q=80&w=1000', N'วิวโค้งแม่น้ำแควน้อย BARME Tea&Taste', 1, @Place19, @Now),
       (N'https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?q=80&w=1000', N'มุมจิบชากาแฟชมวิวพาโนรามา', 0, @Place19, @Now);

INSERT INTO PlaceCategories (PlaceId, CategoryId) VALUES (@Place19, 1), (@Place19, 4);
INSERT INTO PlaceTags (PlaceId, TagId) VALUES (@Place19, 1), (@Place19, 3), (@Place19, 5), (@Place19, 8);

-- =========================================================================
-- 10. ร้านอาหารแพมิตรสัมพันธ์ [Restaurant]
-- =========================================================================
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES (14.0412, 99.5041, N'ริมสะพานข้ามแม่น้ำแคว ต.ท่ามะขาม อ.เมือง จ.กาญจนบุรี 71000', N'034-511-975', N'');
DECLARE @Loc20 INT = SCOPE_IDENTITY();

INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, FoodType)
VALUES (N'ร้านอาหารแพมิตรสัมพันธ์', 
        N'ร้านอาหารแพริมน้ำแควที่เปิดมายาวนานกว่า 30 ปี ตั้งอยู่ติดกับสะพานข้ามแม่น้ำแคว ลิ้มรสอาหารไทยพื้นบ้าน ปลาคังทอดน้ำปลา ต้มยำปลาคัง และยำผักกูดกรอบอร่อย',
        @Loc20, 14.0412, 99.5041, '10:00:00', '21:30:00', @Now, @Now, 'Restaurant', 1);
DECLARE @Place20 INT = SCOPE_IDENTITY();

INSERT INTO PlaceImages (ImageUrl, Description, IsPrimary, PlaceId, CreatedAt)
VALUES (N'https://images.unsplash.com/photo-1552566626-52f8b828add9?q=80&w=1000', N'ร้านอาหารแพมิตรสัมพันธ์ ริมน้ำแคว', 1, @Place20, @Now),
       (N'https://images.unsplash.com/photo-1514933651103-005eec06c04b?q=80&w=1000', N'บรรยากาศแพอาหารริมสะพานข้ามแม่น้ำแคว', 0, @Place20, @Now);

INSERT INTO PlaceCategories (PlaceId, CategoryId) VALUES (@Place20, 4);
INSERT INTO PlaceTags (PlaceId, TagId) VALUES (@Place20, 1), (@Place20, 2), (@Place20, 3), (@Place20, 4), (@Place20, 7);

PRINT 'Successfully inserted 10 new places!';
