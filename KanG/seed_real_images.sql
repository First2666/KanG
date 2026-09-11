DELETE FROM PlaceImages;

-- Attractions
INSERT INTO [PlaceImages] (ImageUrl, [Description], IsPrimary, AttractionId, RestaurantId, AccommodationId, CreatedAt) VALUES
-- 1. น้ำตกเอราวัณ (Erawan Waterfall)
('https://tourismthailand.org/Media/Picture/Attraction/3261-20161202025114.jpg', N'น้ำตกเอราวัณ', 1, 1, NULL, NULL, GETUTCDATE()),
-- 2. สะพานข้ามแม่น้ำแคว (River Kwai Bridge)
('https://tourismthailand.org/Media/Picture/Attraction/3241-20161202022838.jpg', N'สะพานข้ามแม่น้ำแคว', 1, 2, NULL, NULL, GETUTCDATE()),
-- 3. น้ำตกไทรโยคน้อย (Sai Yok Noi)
('https://tourismthailand.org/Media/Picture/Attraction/3248-20161202023531.jpg', N'น้ำตกไทรโยคน้อย', 1, 3, NULL, NULL, GETUTCDATE()),
-- 4. เขื่อนศรีนครินทร์ (Srinagarind Dam)
('https://tourismthailand.org/Media/Picture/Attraction/3262-20161202025211.jpg', N'เขื่อนศรีนครินทร์', 1, 4, NULL, NULL, GETUTCDATE()),
-- 5. วัดถ้ำเสือ (Wat Tham Suea)
('https://tourismthailand.org/Media/Picture/Attraction/3257-20161202024734.jpg', N'วัดถ้ำเสือ', 1, 5, NULL, NULL, GETUTCDATE()),
-- 6. สุสานสงคราม ดอนรัก
('https://tourismthailand.org/Media/Picture/Attraction/3239-20161202022649.jpg', N'สุสานสงคราม ดอนรัก', 1, 6, NULL, NULL, GETUTCDATE()),
-- 7. ถ้ำกระแซ
('https://tourismthailand.org/Media/Picture/Attraction/3250-20161202023730.jpg', N'ถ้ำกระแซ', 1, 7, NULL, NULL, GETUTCDATE()),
-- 8. น้ำตกไทรโยคใหญ่
('https://tourismthailand.org/Media/Picture/Attraction/3251-20161202023812.jpg', N'น้ำตกไทรโยคใหญ่', 1, 8, NULL, NULL, GETUTCDATE());

-- Accommodations
INSERT INTO [PlaceImages] (ImageUrl, [Description], IsPrimary, AttractionId, RestaurantId, AccommodationId, CreatedAt) VALUES
('https://cf.bstatic.com/xdata/images/hotel/max1024x768/43743547.jpg?k=23071d3d63b2f5bdf7cdccb769f4c3f5922eb727ff065c71d37b12d1b702b881&o=&hp=1', N'เดอะ โฟลทเฮ้าส์', 1, NULL, NULL, 1, GETUTCDATE()),
('https://cf.bstatic.com/xdata/images/hotel/max1024x768/193026330.jpg?k=3b6d2e612f026a7e04b408cb960fcdbb25e22c069b7f5d688ffceb07047d1746&o=&hp=1', N'ริเวอร์แคว วิลเลจ', 1, NULL, NULL, 2, GETUTCDATE()),
('https://cf.bstatic.com/xdata/images/hotel/max1024x768/337190013.jpg?k=b0010915fbe87bbdb05915b81a171d31a550993540c76ce7ec3d92040b2db7ea&o=&hp=1', N'ไอยรา รีสอร์ท', 1, NULL, NULL, 3, GETUTCDATE()),
('https://cf.bstatic.com/xdata/images/hotel/max1024x768/193026330.jpg?k=3b6d2e612f026a7e04b408cb960fcdbb25e22c069b7f5d688ffceb07047d1746&o=&hp=1', N'โฟลทติ้ง รีสอร์ท', 1, NULL, NULL, 4, GETUTCDATE()),
('https://cf.bstatic.com/xdata/images/hotel/max1024x768/193026330.jpg?k=3b6d2e612f026a7e04b408cb960fcdbb25e22c069b7f5d688ffceb07047d1746&o=&hp=1', N'X2 รีสอร์ท', 1, NULL, NULL, 5, GETUTCDATE()),
('https://cf.bstatic.com/xdata/images/hotel/max1024x768/193026330.jpg?k=3b6d2e612f026a7e04b408cb960fcdbb25e22c069b7f5d688ffceb07047d1746&o=&hp=1', N'แคมป์ปิ้ง เอราวัณ', 1, NULL, NULL, 6, GETUTCDATE());

-- Restaurants
INSERT INTO [PlaceImages] (ImageUrl, [Description], IsPrimary, AttractionId, RestaurantId, AccommodationId, CreatedAt) VALUES
('https://img.wongnai.com/p/1920x0/2016/09/20/d1cdb82434f04c5e88863c0c008cf311.jpg', N'บ้านริมน้ำ', 1, NULL, 1, NULL, GETUTCDATE()),
('https://img.wongnai.com/p/1920x0/2016/09/20/d1cdb82434f04c5e88863c0c008cf311.jpg', N'ครัวอารีย์ราย', 1, NULL, 2, NULL, GETUTCDATE()),
('https://img.wongnai.com/p/1920x0/2016/09/20/d1cdb82434f04c5e88863c0c008cf311.jpg', N'ก๋วยเตี๋ยวเรือป้าแดง', 1, NULL, 3, NULL, GETUTCDATE()),
('https://img.wongnai.com/p/1920x0/2016/09/20/d1cdb82434f04c5e88863c0c008cf311.jpg', N'Blue Rice', 1, NULL, 4, NULL, GETUTCDATE()),
('https://img.wongnai.com/p/1920x0/2016/09/20/d1cdb82434f04c5e88863c0c008cf311.jpg', N'แพอาหาร ริมแคว', 1, NULL, 5, NULL, GETUTCDATE()),
('https://img.wongnai.com/p/1920x0/2016/09/20/d1cdb82434f04c5e88863c0c008cf311.jpg', N'Meena Cafe', 1, NULL, 6, NULL, GETUTCDATE());
