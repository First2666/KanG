-- Clear old broken images
DELETE FROM PlaceImages;

-- Re-insert with WORKING Unsplash URLs (free, reliable CDN)
INSERT INTO [PlaceImages] (ImageUrl, [Description], IsPrimary, AttractionId, RestaurantId, AccommodationId, CreatedAt) VALUES
-- Attractions
('https://images.unsplash.com/photo-1540956424564-9d5059d43527?w=800&auto=format&fit=crop', N'น้ำตกเอราวัณ', 1, 1, NULL, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1620300185987-bf5326588db5?w=800&auto=format&fit=crop', N'สะพานข้ามแม่น้ำแคว', 1, 2, NULL, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1504214208698-ea1916a2195a?w=800&auto=format&fit=crop', N'น้ำตกไทรโยคน้อย', 1, 3, NULL, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1506905925346-21bda4d32df4?w=800&auto=format&fit=crop', N'เขื่อนศรีนครินทร์', 1, 4, NULL, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1528181304800-259b08848526?w=800&auto=format&fit=crop', N'วัดถ้ำเสือ', 1, 5, NULL, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1555217851-6141535bd771?w=800&auto=format&fit=crop', N'สุสานสงคราม ดอนรัก', 1, 6, NULL, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1552465011-b4e21bf6e79a?w=800&auto=format&fit=crop', N'ถ้ำกระแซ', 1, 7, NULL, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=800&auto=format&fit=crop', N'น้ำตกไทรโยคใหญ่', 1, 8, NULL, NULL, GETUTCDATE()),

-- Accommodations
('https://images.unsplash.com/photo-1520250497591-112f2f40a3f4?w=800&auto=format&fit=crop', N'เดอะ โฟลทเฮ้าส์', 1, NULL, NULL, 1, GETUTCDATE()),
('https://images.unsplash.com/photo-1566073771259-6a8506099945?w=800&auto=format&fit=crop', N'ริเวอร์แคว วิลเลจ', 1, NULL, NULL, 2, GETUTCDATE()),
('https://images.unsplash.com/photo-1571896349842-33c89424de2d?w=800&auto=format&fit=crop', N'ไอยรา รีสอร์ท', 1, NULL, NULL, 3, GETUTCDATE()),
('https://images.unsplash.com/photo-1504280390226-c22502f6bc30?w=800&auto=format&fit=crop', N'โฟลทติ้ง รีสอร์ท', 1, NULL, NULL, 4, GETUTCDATE()),
('https://images.unsplash.com/photo-1618773928121-c32242e63f39?w=800&auto=format&fit=crop', N'X2 รีสอร์ท', 1, NULL, NULL, 5, GETUTCDATE()),
('https://images.unsplash.com/photo-1537905569824-f89f14cceb68?w=800&auto=format&fit=crop', N'แคมป์ปิ้ง เอราวัณ', 1, NULL, NULL, 6, GETUTCDATE()),

-- Restaurants
('https://images.unsplash.com/photo-1555396273-367ea4eb4db5?w=800&auto=format&fit=crop', N'บ้านริมน้ำ', 1, NULL, 1, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1567337710282-00832b415979?w=800&auto=format&fit=crop', N'ครัวอารีย์ราย', 1, NULL, 2, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1569058242567-93de6f36f8e6?w=800&auto=format&fit=crop', N'ก๋วยเตี๋ยวเรือป้าแดง', 1, NULL, 3, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1414235077428-338989a2e8c0?w=800&auto=format&fit=crop', N'Blue Rice', 1, NULL, 4, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1552566626-52f8b828add9?w=800&auto=format&fit=crop', N'แพอาหาร ริมแคว', 1, NULL, 5, NULL, GETUTCDATE()),
('https://images.unsplash.com/photo-1554118811-1e0d58224f24?w=800&auto=format&fit=crop', N'Meena Cafe', 1, NULL, 6, NULL, GETUTCDATE());

PRINT 'Images fixed with Unsplash URLs!'
