# แผนการปรับปรุงโครงสร้าง Database Models (Backend Architecture Refactoring)

จากการตรวจสอบไฟล์ C# Models ในโฟลเดอร์ `Models/` พบว่าโครงสร้างปัจจุบันมีปัญหาด้าน **Database Normalization (ความซ้ำซ้อนของข้อมูล)** และ **Scalability (การขยายระบบในอนาคต)** ซึ่งมักเกิดจากการแยกตารางสถานที่ท่องเที่ยว, ที่พัก, และร้านอาหารออกจากกันอย่างสิ้นเชิง

นี่คือสรุปปัญหาที่พบและสิ่งที่เรา "ต้องทำ" เพื่อแก้ปัญหาทั้งหมดครับ

## 🔴 ปัญหาของโครงสร้างปัจจุบัน (Bad Smells)

1. **Foreign Key ซ้ำซ้อน (Polymorphic Association Anti-pattern)**
   ตารางที่ใช้ร่วมกัน เช่น `Review`, `PlaceImage`, `Favorite`, และ `TripPlanItem` ต้องสร้าง Column มารองรับสถานที่ทุกประเภท:
   - `int? AttractionId`
   - `int? RestaurantId`
   - `int? AccommodationId`
   *ปัญหา:* ถ้าอนาคตเราเพิ่ม "Cafe" หรือ "Museum" เราจะต้องไปตามเพิ่ม Column ใหม่ใน 4-5 ตารางนี้ทั้งหมด ซึ่งผิดหลักการออกแบบฐานข้อมูลที่ดี
   
2. **ข้อมูลที่ซ้ำซ้อนกันในหลายตาราง (Duplicated Fields)**
   ตาราง `Attraction`, `Restaurant`, และ `Accommodation` มีฟิลด์ที่เหมือนกันเป๊ะซ้ำๆ กัน:
   - `Name`, `Description`
   - `LocationId`, `Latitude`, `Longitude`
   - `OpeningTime`, `ClosingTime`
   *ปัญหา:* เวลาระบบจะค้นหาสถานที่ใกล้เคียง (Nearby) ระบบต้อง Query แยก 3 ตารางแล้วเอามาต่อกัน ซึ่งทำงานช้าและเขียนโค้ดยากมาก

3. **Tags และ Categories ไม่ยืดหยุ่น**
   ปัจจุบันมี `AttractionTag` ทำให้เราติดแท็กได้แค่สถานที่ท่องเที่ยว ถ้าร้านอาหารอยากมีแท็กบ้าง ต้องสร้าง `RestaurantTag` เพิ่มอีก

---

## 🟢 สิ่งที่เราต้องทำ (Action Plan)

เราต้องใช้เทคนิคที่เรียกว่า **Table-Per-Hierarchy (TPH)** ของ Entity Framework Core โดยการยุบรวมสถานที่ทั้งหมดให้มี Base Class เดียวกันคือ `Place`

### 1. สร้าง Base Model `Place.cs`
สร้างคลาสแม่เพื่อเก็บข้อมูลส่วนกลางที่สถานที่ทุกประเภทมีเหมือนกัน
```csharp
public abstract class Place 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public TimeSpan? OpeningTime { get; set; }
    public TimeSpan? ClosingTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties ส่วนกลาง
    public ICollection<PlaceImage> Images { get; set; } = new List<PlaceImage>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<PlaceTag> Tags { get; set; } = new List<PlaceTag>();
}
```

### 2. ปรับ Models สถานที่ให้สืบทอด (Inherit) จาก `Place`
```csharp
public class Attraction : Place {
    public decimal? EntranceFee { get; set; }
}

public class Restaurant : Place {
    public FoodType FoodType { get; set; }
}

public class Accommodation : Place {
    public decimal? PricePerNight { get; set; }
}
```
*EF Core จะรวม 3 คลาสนี้เป็น 1 ตารางใน Database โดยอัตโนมัติ และใช้คอลัมน์ `Discriminator` แยกประเภทว่าแถวไหนเป็นอะไร*

### 3. รื้อตารางความสัมพันธ์ (Mapping Tables) ใหม่
ลบ FK ที่ซ้ำซ้อนออกให้หมด แล้วแทนที่ด้วย `PlaceId` ตัวเดียว:

**ใน `Review.cs`, `Favorite.cs`, `PlaceImage.cs`, `TripPlanItem.cs`:**
```csharp
// ลบ AttractionId, RestaurantId, AccommodationId ออกให้หมด
// แทนที่ด้วย:
public int PlaceId { get; set; }
public Place? Place { get; set; }
```

### 4. แก้ไขเรื่อง Tag
- ลบ `AttractionTag.cs` ทิ้ง
- สร้าง `PlaceTag.cs` แทน เพื่อให้ทั้งที่พัก ร้านอาหาร และที่เที่ยว สามารถติดแท็ก (เช่น "วิวภูเขา", "ที่จอดรถ") ร่วมกันได้

### 5. ปรับปรุง `AppDbContext.cs`
ลบ `DbSet<...>` เก่าที่ซ้ำซ้อนออก และกำหนด TPH:
```csharp
public DbSet<Place> Places { get; set; }
// DbSet ของ Attraction, Restaurant, Accommodation สามารถลบออกหรือเก็บไว้ก็ได้ EF จะรู้เองว่ามันสืบทอดมาจาก Place

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // กำหนด Table-per-Hierarchy
    modelBuilder.Entity<Place>()
        .HasDiscriminator<string>("PlaceType")
        .HasValue<Attraction>("Attraction")
        .HasValue<Restaurant>("Restaurant")
        .HasValue<Accommodation>("Accommodation");
        
    // ... ลบ/อัปเดต Relation ของเก่าที่เชื่อมกับ AttractionId เปลี่ยนเป็น PlaceId
}
```

### 6. สร้าง Migration ใหม่
หลังจากแก้โค้ดทั้งหมด ต้องรัน:
```bash
dotnet ef migrations add RefactorToPlaceTPH
dotnet ef database update
```
*(หมายเหตุ: การทำแบบนี้ Database เก่าอาจพัง ต้องเขียน Migration ลบตารางเก่าและย้ายข้อมูล หรือทำการ Seed ข้อมูลใหม่ทั้งหมด)*

---

### 💡 ผลลัพธ์ที่จะได้
- โค้ดใน Backend จะสั้นลง 30-40% 
- การ Search และ Map จะทำได้เร็วมาก เพราะ Query ตาราง `Places` แค่ตารางเดียว
- อนาคตถ้ามีเพิ่ม `Cafe` หรือ `SouvenirShop` แค่สร้าง Model ใหม่มาสืบทอดจาก `Place` โดยไม่ต้องแตะตาราง Review หรือ Image อีกเลย
