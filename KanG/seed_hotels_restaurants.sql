SET NOCOUNT ON;

-- 1. Insert Locations
SET IDENTITY_INSERT Locations ON;

INSERT INTO Locations (Id, Latitude, Longitude, Address, PhoneNumber, Website)
VALUES 
(6, 14.2833, 99.0000, N'The FloatHouse River Kwai, 55 Moo 5 Wangkrajae, Sai Yok, Kanchanaburi', N'02-642-5497', N'www.thefloathouseriverkwai.com'),
(7, 14.0416, 99.5036, N'U Inchantree Kanchanaburi, 443 Mae Nam Khwae Road, Tha Makham, Kanchanaburi', N'034-521-584', N'www.uhotelsresorts.com'),
(8, 14.0450, 99.5050, N'Keeree Tara, 431/1 Mae Nam Khwae Road, Tha Makham, Kanchanaburi', N'034-513-855', N'www.keereetara.com'),
(9, 13.9666, 99.5666, N'Meena Cafe, 75/18 Moo 3, Tha Muang, Kanchanaburi', N'085-681-8187', N'');

SET IDENTITY_INSERT Locations OFF;

-- 2. Insert Places (2 Accommodations, 2 Restaurants)
SET IDENTITY_INSERT Places ON;

INSERT INTO Places (Id, Name, Description, LocationId, Latitude, Longitude, OpeningTime, ClosingTime, CreatedAt, UpdatedAt, PlaceType, AccommodationType, PricePerNight, MaxGuests, FoodType)
VALUES
(6, N'The FloatHouse River Kwai', N'รีสอร์ทลอยน้ำระดับหรูสไตล์บูติคบนแม่น้ำแควน้อย สัมผัสธรรมชาติอย่างใกล้ชิด', 6, 14.2833, 99.0000, '00:00:00', '23:59:59', GETUTCDATE(), GETUTCDATE(), 'Accommodation', 1, 4500.00, 2, NULL),
(7, N'U Inchantree Kanchanaburi', N'โรงแรมบูติคริมฝั่งแม่น้ำแควใหญ่ ใกล้สะพานข้ามแม่น้ำแคว ร่มรื่นด้วยต้นจันทรีขนาดใหญ่', 7, 14.0416, 99.5036, '00:00:00', '23:59:59', GETUTCDATE(), GETUTCDATE(), 'Accommodation', 0, 2500.00, 2, NULL),
(8, N'คีรีธารา (Keeree Tara)', N'ร้านอาหารริมแม่น้ำแควใหญ่ บรรยากาศโรแมนติก เสิร์ฟอาหารไทยและซีฟู้ดรสจัดจ้าน', 8, 14.0450, 99.5050, '11:00:00', '23:00:00', GETUTCDATE(), GETUTCDATE(), 'Restaurant', NULL, NULL, NULL, 1),
(9, N'มีนา คาเฟ่ (Meena Cafe)', N'คาเฟ่สุดฮิตกลางทุ่งนา มองเห็นวิววัดถ้ำเสือได้อย่างชัดเจน มุมถ่ายรูปเพียบ', 9, 13.9666, 99.5666, '08:30:00', '19:00:00', GETUTCDATE(), GETUTCDATE(), 'Restaurant', NULL, NULL, NULL, 2);

SET IDENTITY_INSERT Places OFF;

-- 3. Insert PlaceImages
INSERT INTO PlaceImages (PlaceId, ImageUrl, Description, IsPrimary)
VALUES
(6, N'https://upload.wikimedia.org/wikipedia/commons/3/3d/Floating_hotel_on_the_River_Kwai_Kanchanaburi_Province_Thailand.jpg', N'The Float House', 1),
(7, N'https://upload.wikimedia.org/wikipedia/commons/c/c9/U_Inchantree_Kanchanaburi.jpg', N'U Inchantree', 1),
(8, N'https://upload.wikimedia.org/wikipedia/commons/2/23/Thai_food_at_a_restaurant_in_Chiang_Mai.jpg', N'อาหารไทย', 1),
(9, N'https://upload.wikimedia.org/wikipedia/commons/9/90/Kanchanaburi_street_food.jpg', N'บรรยากาศคาเฟ่', 1);

PRINT 'Restaurants and Accommodations seeded successfully!';
