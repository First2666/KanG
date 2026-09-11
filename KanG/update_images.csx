using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KanG.Data;
using KanG.Models;

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer("Server=LAPTOP-HAPT6HNM;Database=KanG_Db;TrustServerCertificate=True;Trusted_Connection=True;");
var db = new AppDbContext(optionsBuilder.Options);

var place5Images = db.PlaceImages.Where(img => img.PlaceId == 5).OrderBy(img => img.Id).ToList();

if (place5Images.Count >= 3) {
    place5Images[0].ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/da/Srinakarin_Dam_No.5.jpg/1280px-Srinakarin_Dam_No.5.jpg";
    place5Images[1].ImageUrl = "https://thailandtourismdirectory.go.th/images/attraction/4207/attraction_4207_2102191544391645260279.jpg";
    place5Images[2].ImageUrl = "https://tourismproduct.b-cdn.net/file-storage/8535/606c4b22c608f-222.jpg";
    
    // Add one more image to make it 4 images for a full gallery!
    db.PlaceImages.Add(new PlaceImage {
        PlaceId = 5,
        ImageUrl = "https://cms.dmpcdn.com/travel/2021/01/21/51525280-5bbf-11eb-9df0-7d727b10bb92_original.jpg",
        IsPrimary = false,
        CreatedAt = DateTime.UtcNow
    });

    db.SaveChanges();
    Console.WriteLine("Successfully updated Place 5 images!");
} else {
    Console.WriteLine("Place 5 images not found or count < 3.");
}
