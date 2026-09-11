SET NOCOUNT ON;

-- เคลียร์ข้อมูลเก่า (ถ้ามี)
DELETE FROM PlaceImages;
DELETE FROM Places;
DELETE FROM Locations;

-- รีเซ็ต Identity
DBCC CHECKIDENT ('Locations', RESEED, 0);
DBCC CHECKIDENT ('Places', RESEED, 0);
DBCC CHECKIDENT ('PlaceImages', RESEED, 0);

-- Insert Locations
INSERT INTO Locations (Latitude, Longitude, Address, PhoneNumber, Website)
VALUES 
(14.0416, 99.5036, N'ต.ท่ามะขาม อ.เมืองกาญจนบุรี จ.กาญจนบุรี 71000', N'034-511-200', N''),
(14.3761, 99.1444, N'อุทยานแห่งชาติเอราวัณ อ.ศรีสวัสดิ์ จ.กาญจนบุรี 71250', N'034-574-222', N''),
(14.1030, 99.1678, N'ต.ลุ่มสุ่ม อ.ไทรโยค จ.กาญจนบุรี 71150', N'', N''),
(14.0410, 99.2435, N'อุทยานประวัติศาสตร์เมืองสิงห์ อ.ไทรโยค จ.กาญจนบุรี 71150', N'034-528-456', N''),
(14.4072, 99.1246, N'ต.ท่ากระดาน อ.ศรีสวัสดิ์ จ.กาญจนบุรี 71250', N'034-574-001', N'');

-- Insert Places (Attractions)
INSERT INTO Places (Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, EntranceFee)
VALUES
(N'สะพานข้ามแม่น้ำแคว', N'แลนด์มาร์คสำคัญทางประวัติศาสตร์ของกาญจนบุรี สร้างขึ้นในสมัยสงครามโลกครั้งที่ 2 เป็นส่วนหนึ่งของเส้นทางรถไฟสายมรณะ', 1, 14.0416, 99.5036, '06:00:00', '18:00:00', GETUTCDATE(), GETUTCDATE(), 'Attraction', 0),
(N'น้ำตกเอราวัณ', N'น้ำตกที่สวยงามที่สุดแห่งหนึ่งของไทย มีทั้งหมด 7 ชั้น น้ำใสสีเขียวมรกต สามารถเล่นน้ำได้และมีปลาพลวงอาศัยอยู่จำนวนมาก', 2, 14.3761, 99.1444, '08:00:00', '16:30:00', GETUTCDATE(), GETUTCDATE(), 'Attraction', 100),
(N'ถ้ำกระแซ (ทางรถไฟสายมรณะ)', N'จุดชมวิวทางรถไฟสายมรณะที่สวยและน่าหวาดเสียวที่สุด ทางรถไฟเลาะไปตามหน้าผาสูงชันริมแม่น้ำแควน้อย', 3, 14.1030, 99.1678, '06:00:00', '18:00:00', GETUTCDATE(), GETUTCDATE(), 'Attraction', 0),
(N'ปราสาทเมืองสิงห์', N'โบราณสถานศิลปะขอมเพียงแห่งเดียวในภาคตะวันตกของประเทศไทย มีสถาปัตยกรรมที่งดงามและเก่าแก่', 4, 14.0410, 99.2435, '08:00:00', '16:30:00', GETUTCDATE(), GETUTCDATE(), 'Attraction', 20),
(N'เขื่อนศรีนครินทร์', N'เขื่อนอเนกประสงค์ขนาดใหญ่ บรรยากาศร่มรื่น วิวภูเขาและทะเลสาบสวยงามมาก มีกิจกรรมทางน้ำมากมาย', 5, 14.4072, 99.1246, '08:00:00', '18:00:00', GETUTCDATE(), GETUTCDATE(), 'Attraction', 0);

-- Insert Images
INSERT INTO PlaceImages (PlaceId, ImageUrl, IsPrimary, CreatedAt)
VALUES
(1, N'https://images.unsplash.com/photo-1601009892694-b2b93475f3a0?q=80&w=1200&auto=format&fit=crop', 1, GETUTCDATE()),
(2, N'https://images.unsplash.com/photo-1621379434455-8dcb4860d268?q=80&w=1200&auto=format&fit=crop', 1, GETUTCDATE()),
(2, N'https://images.unsplash.com/photo-1549429402-999335352c3c?q=80&w=1200&auto=format&fit=crop', 0, GETUTCDATE()),
(3, N'https://images.unsplash.com/photo-1582239474718-d9d9685a44cc?q=80&w=1200&auto=format&fit=crop', 1, GETUTCDATE()),
(4, N'https://images.unsplash.com/photo-1616186419749-8c9f5647565e?q=80&w=1200&auto=format&fit=crop', 1, GETUTCDATE()),
(5, N'https://images.unsplash.com/photo-1577701726071-70e0f3177699?q=80&w=1200&auto=format&fit=crop', 1, GETUTCDATE());

PRINT '✅ Successfully seeded real Kanchanaburi attractions.';
