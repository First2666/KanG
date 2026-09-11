-- =============================================
-- Seed REAL Images for Kanchanaburi Places
-- Uses Wikimedia Commons (free, public domain images)
-- =============================================

-- Attraction Images
INSERT INTO [PlaceImages] (ImageUrl, [Description], IsPrimary, AttractionId, RestaurantId, AccommodationId, CreatedAt) VALUES
-- 1. น้ำตกเอราวัณ
('https://upload.wikimedia.org/wikipedia/commons/thumb/2/2a/Erawan_Waterfall%2C_Kanchanaburi_Province%2C_Thailand_-_June_2004.jpg/1280px-Erawan_Waterfall%2C_Kanchanaburi_Province%2C_Thailand_-_June_2004.jpg', N'น้ำตกเอราวัณ ชั้น 7', 1, 1, NULL, NULL, GETUTCDATE()),
-- 2. สะพานข้ามแม่น้ำแคว
('https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Bridge_over_the_river_Kwai.jpg/1280px-Bridge_over_the_river_Kwai.jpg', N'สะพานข้ามแม่น้ำแคว', 1, 2, NULL, NULL, GETUTCDATE()),
-- 3. น้ำตกไทรโยคน้อย
('https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/SaiYokNoi.jpg/1024px-SaiYokNoi.jpg', N'น้ำตกไทรโยคน้อย', 1, 3, NULL, NULL, GETUTCDATE()),
-- 4. เขื่อนศรีนครินทร์
('https://upload.wikimedia.org/wikipedia/commons/thumb/8/82/Sri_Nagarindra_Dam.jpg/1280px-Sri_Nagarindra_Dam.jpg', N'เขื่อนศรีนครินทร์', 1, 4, NULL, NULL, GETUTCDATE()),
-- 5. วัดถ้ำเสือ
('https://upload.wikimedia.org/wikipedia/commons/thumb/1/17/Wat_Tham_Suea_06.jpg/1280px-Wat_Tham_Suea_06.jpg', N'พระพุทธรูปบนยอดเขา วัดถ้ำเสือ', 1, 5, NULL, NULL, GETUTCDATE()),
-- 6. สุสานสงคราม (ดอนรัก)
('https://upload.wikimedia.org/wikipedia/commons/thumb/3/3a/Kanchanaburi_War_Cemetery_2.jpg/1280px-Kanchanaburi_War_Cemetery_2.jpg', N'สุสานสงครามดอนรัก', 1, 6, NULL, NULL, GETUTCDATE()),
-- 7. ถ้ำกระแซ
('https://upload.wikimedia.org/wikipedia/commons/thumb/c/ce/Thailand_Burma_Railway_-_Bridge_on_the_River_Kwai_-_Tham_Krasae_Bridge.jpg/1280px-Thailand_Burma_Railway_-_Bridge_on_the_River_Kwai_-_Tham_Krasae_Bridge.jpg', N'สะพานถ้ำกระแซ', 1, 7, NULL, NULL, GETUTCDATE()),
-- 8. น้ำตกไทรโยคใหญ่
('https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Sai_Yok_Yai_waterfall.jpg/800px-Sai_Yok_Yai_waterfall.jpg', N'น้ำตกไทรโยคใหญ่', 1, 8, NULL, NULL, GETUTCDATE());

-- Accommodation Images (using free stock images that represent the vibe)
INSERT INTO [PlaceImages] (ImageUrl, [Description], IsPrimary, AttractionId, RestaurantId, AccommodationId, CreatedAt) VALUES
('https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/River_Kwai_Jungle_Rafts.jpg/1280px-River_Kwai_Jungle_Rafts.jpg', N'แพลอยน้ำแม่น้ำแคว', 1, NULL, NULL, 1, GETUTCDATE()),
('https://upload.wikimedia.org/wikipedia/commons/thumb/b/b3/River_Kwai_Village_Hotel.jpg/1024px-River_Kwai_Village_Hotel.jpg', N'ริเวอร์แคว วิลเลจ', 1, NULL, NULL, 2, GETUTCDATE()),
('https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/River_Kwai_Jungle_Rafts.jpg/1280px-River_Kwai_Jungle_Rafts.jpg', N'รีสอร์ทริมแม่น้ำแคว', 1, NULL, NULL, 3, GETUTCDATE()),
('https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/River_Kwai_Jungle_Rafts.jpg/1280px-River_Kwai_Jungle_Rafts.jpg', N'แพลอยน้ำไทรโยค', 1, NULL, NULL, 4, GETUTCDATE()),
('https://upload.wikimedia.org/wikipedia/commons/thumb/b/b3/River_Kwai_Village_Hotel.jpg/1024px-River_Kwai_Village_Hotel.jpg', N'X2 รีสอร์ท', 1, NULL, NULL, 5, GETUTCDATE()),
('https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/River_Kwai_Jungle_Rafts.jpg/1280px-River_Kwai_Jungle_Rafts.jpg', N'แคมป์ปิ้งเอราวัณ', 1, NULL, NULL, 6, GETUTCDATE());

-- Restaurant Images
INSERT INTO [PlaceImages] (ImageUrl, [Description], IsPrimary, AttractionId, RestaurantId, AccommodationId, CreatedAt) VALUES
('https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/Tom_Yum_Kung_Namkhon.jpg/1280px-Tom_Yum_Kung_Namkhon.jpg', N'ต้มยำกุ้งแม่น้ำ บ้านริมน้ำ', 1, NULL, 1, NULL, GETUTCDATE()),
('https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/Kaeng_som.jpg/1024px-Kaeng_som.jpg', N'แกงส้ม ครัวอารีย์ราย', 1, NULL, 2, NULL, GETUTCDATE()),
('https://upload.wikimedia.org/wikipedia/commons/thumb/2/2e/Boat_noodle.jpg/1280px-Boat_noodle.jpg', N'ก๋วยเตี๋ยวเรือป้าแดง', 1, NULL, 3, NULL, GETUTCDATE()),
('https://upload.wikimedia.org/wikipedia/commons/thumb/9/90/Pad_Thai.jpg/1280px-Pad_Thai.jpg', N'อาหาร Blue Rice', 1, NULL, 4, NULL, GETUTCDATE()),
('https://upload.wikimedia.org/wikipedia/commons/thumb/d/d5/Pla_pao.jpg/1280px-Pla_pao.jpg', N'ปลาเผาเกลือ แพอาหาร ริมแคว', 1, NULL, 5, NULL, GETUTCDATE()),
('https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Latte_art.jpg/1280px-Latte_art.jpg', N'กาแฟ Meena Cafe', 1, NULL, 6, NULL, GETUTCDATE());

PRINT 'Images seeded!'
