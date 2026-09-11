SET NOCOUNT ON;

-- 3. Insert PlaceImages
INSERT INTO PlaceImages (PlaceId, ImageUrl, Description, IsPrimary, CreatedAt)
VALUES
(6, N'https://upload.wikimedia.org/wikipedia/commons/3/3d/Floating_hotel_on_the_River_Kwai_Kanchanaburi_Province_Thailand.jpg', N'The Float House', 1, GETUTCDATE()),
(7, N'https://upload.wikimedia.org/wikipedia/commons/c/c9/U_Inchantree_Kanchanaburi.jpg', N'U Inchantree', 1, GETUTCDATE()),
(8, N'https://upload.wikimedia.org/wikipedia/commons/2/23/Thai_food_at_a_restaurant_in_Chiang_Mai.jpg', N'อาหารไทย', 1, GETUTCDATE()),
(9, N'https://upload.wikimedia.org/wikipedia/commons/9/90/Kanchanaburi_street_food.jpg', N'บรรยากาศคาเฟ่', 1, GETUTCDATE());

PRINT 'PlaceImages seeded successfully!';
