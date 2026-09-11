-- Add a separate location for ก๋วยเตี๋ยวเรือป้าแดง
SET IDENTITY_INSERT [Locations] ON;
INSERT INTO [Locations] (Id, Latitude, Longitude, [Address], PhoneNumber, Website) VALUES
(19, 14.0368, 99.5190, N'ถ.แสงชูโต ตลาดเก่า ต.บ้านเหนือ อ.เมือง กาญจนบุรี', '', '');
SET IDENTITY_INSERT [Locations] OFF;

-- Now insert the missing restaurants
SET IDENTITY_INSERT [Restaurants] ON;
INSERT INTO [Restaurants] (Id, Name, [Description], FoodType, LocationId, OpeningTime, ClosingTime, Latitude, Longitude, CreatedAt) VALUES
(3, N'ก๋วยเตี๋ยวเรือป้าแดง', N'ร้านก๋วยเตี๋ยวเรือชื่อดังของกาญจนบุรี น้ำซุปเข้มข้น เนื้อเปื่อย ลูกชิ้นเด้ง ราคาชามละ 15-20 บาท คิวยาวทุกวัน เปิดมากว่า 20 ปี', 4, 19, '07:00', '15:00', 14.0368, 99.5190, GETUTCDATE());
SET IDENTITY_INSERT [Restaurants] OFF;

PRINT 'Fix done!'
