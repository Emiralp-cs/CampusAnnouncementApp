using CampusAnnouncementApp.Domain.Entities;
using CampusAnnouncementApp.Domain.Enums;
using CampusAnnouncementApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=campus.db"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Users.Any())
    {
        db.Users.AddRange(
            new User { Name = "Ali Yılmaz",          Email = "ali@kampus.edu.tr",          UserType = UserType.Ogrenci, NotificationPreferences = "Email,SMS" },
            new User { Name = "Ayşe Kaya",            Email = "ayse@kampus.edu.tr",         UserType = UserType.Ogrenci, NotificationPreferences = "MobilBildirim" },
            new User { Name = "Mehmet Demir",         Email = "mehmet@kampus.edu.tr",       UserType = UserType.Ogrenci, NotificationPreferences = "Email" },
            new User { Name = "Zeynep Çelik",         Email = "zeynep@kampus.edu.tr",       UserType = UserType.Ogrenci, NotificationPreferences = "SMS,MobilBildirim" },
            new User { Name = "Can Özkan",            Email = "can@kampus.edu.tr",          UserType = UserType.Ogrenci, NotificationPreferences = "Email,SMS,MobilBildirim" },
            new User { Name = "Selin Arslan",         Email = "selin@kampus.edu.tr",        UserType = UserType.Ogrenci, NotificationPreferences = "Email" },
            new User { Name = "Burak Şahin",          Email = "burak@kampus.edu.tr",        UserType = UserType.Ogrenci, NotificationPreferences = "SMS" },
            new User { Name = "Elif Yıldız",          Email = "elif@kampus.edu.tr",         UserType = UserType.Ogrenci, NotificationPreferences = "MobilBildirim" },
            new User { Name = "Emre Kılıç",           Email = "emre@kampus.edu.tr",         UserType = UserType.Ogrenci, NotificationPreferences = "Email,MobilBildirim" },
            new User { Name = "Derya Aydın",          Email = "derya@kampus.edu.tr",        UserType = UserType.Ogrenci, NotificationPreferences = "Email,SMS,MobilBildirim" },
            new User { Name = "Prof. Dr. Ahmet Yıldız",  Email = "ahmet@kampus.edu.tr",        UserType = UserType.Ogretmen, NotificationPreferences = "Email" },
            new User { Name = "Doç. Dr. Fatma Şahin",    Email = "fatma@kampus.edu.tr",        UserType = UserType.Ogretmen, NotificationPreferences = "Email,SMS" },
            new User { Name = "Dr. Hasan Kılıç",         Email = "hasan@kampus.edu.tr",        UserType = UserType.Ogretmen, NotificationPreferences = "MobilBildirim" },
            new User { Name = "Prof. Dr. Elif Arslan",   Email = "elif.arslan@kampus.edu.tr",  UserType = UserType.Ogretmen, NotificationPreferences = "Email" },
            new User { Name = "Öğr. Gör. Burak Aydın",  Email = "burak.aydin@kampus.edu.tr",  UserType = UserType.Ogretmen, NotificationPreferences = "SMS" },
            new User { Name = "Doç. Dr. Serkan Çelik",  Email = "serkan@kampus.edu.tr",       UserType = UserType.Ogretmen, NotificationPreferences = "Email,MobilBildirim" },
            new User { Name = "Prof. Dr. Merve Demir",   Email = "merve@kampus.edu.tr",        UserType = UserType.Ogretmen, NotificationPreferences = "Email,SMS" },
            new User { Name = "Dr. Okan Yılmaz",         Email = "okan@kampus.edu.tr",         UserType = UserType.Ogretmen, NotificationPreferences = "MobilBildirim" },
            new User { Name = "Öğr. Gör. Canan Özkan",  Email = "canan@kampus.edu.tr",        UserType = UserType.Ogretmen, NotificationPreferences = "Email" },
            new User { Name = "Prof. Dr. Tarık Kaya",    Email = "tarik@kampus.edu.tr",        UserType = UserType.Ogretmen, NotificationPreferences = "SMS,MobilBildirim" }
        );
    }

    if (!db.Announcements.Any())
    {
        db.Announcements.AddRange(
            new Announcement { Title = "Vize Sınavı Tarihi Değişti",  Content = "Vize sınavları bir hafta ertelenmiştir.",                                              Type = AnnouncementType.Sinav,      CreatedAt = DateTime.Now },
            new Announcement { Title = "Bahar Şenliği",               Content = "Bahar şenliği bu hafta sonu kampüste düzenlenecektir.",                                Type = AnnouncementType.Etkinlik,   CreatedAt = DateTime.Now },
            new Announcement { Title = "Bugünün Yemek Menüsü",        Content = "Öğle: Mercimek çorbası, izgara köfte, pilav.",                                         Type = AnnouncementType.Yemekhane,  CreatedAt = DateTime.Now },
            new Announcement { Title = "Kütüphane Çalışma Saatleri",  Content = "Kütüphane hafta içi 08:00-22:00 arası açıktır.",                                       Type = AnnouncementType.Kutuphane,  CreatedAt = DateTime.Now },
            new Announcement { Title = "Final Sınav Programı",        Content = "Final sınav programı öğrenci bilgi sisteminde yayınlanmıştır.",                         Type = AnnouncementType.Sinav,      CreatedAt = DateTime.Now },
            new Announcement { Title = "Kariyer Günleri",             Content = "Bu hafta Perşembe saat 14:00de konferans salonunda kariyer günleri düzenlenecektir.",   Type = AnnouncementType.Etkinlik,   CreatedAt = DateTime.Now },
            new Announcement { Title = "Yarınki Yemek Menüsü",        Content = "Kahvaltı: Menemen, Öğle: Tavuk sote ve bulgur pilavı.",                                Type = AnnouncementType.Yemekhane,  CreatedAt = DateTime.Now },
            new Announcement { Title = "Kütüphane Yeni Kitaplar",     Content = "Kütüphaneye 200 adet yeni kitap eklenmiştir.",                                          Type = AnnouncementType.Kutuphane,  CreatedAt = DateTime.Now },
            new Announcement { Title = "Dönem Sonu Proje Teslimi",    Content = "Dönem sonu projeleri 15 Haziran Pazartesi saat 23:59a kadar yüklenmelidir.",            Type = AnnouncementType.Sinav,      CreatedAt = DateTime.Now },
            new Announcement { Title = "Mezuniyet Töreni",            Content = "Mezuniyet töreni 20 Haziran Cuma günü saat 10:00da düzenlenecektir.",                   Type = AnnouncementType.Etkinlik,   CreatedAt = DateTime.Now },
            new Announcement { Title = "Özel Menü",                   Content = "Bugün öğle yemeğinde özel kampüs menüsü sunulmaktadır.",                                Type = AnnouncementType.Yemekhane,  CreatedAt = DateTime.Now },
            new Announcement { Title = "Kütüphane Bakım",             Content = "Kütüphane 5 Haziran Cuma günü bakım nedeniyle kapalı olacaktır.",                      Type = AnnouncementType.Kutuphane,  CreatedAt = DateTime.Now },
            new Announcement { Title = "Bütünleme Sınavları",         Content = "Bütünleme sınavları 1-10 Temmuz tarihleri arasında yapılacaktır.",                     Type = AnnouncementType.Sinav,      CreatedAt = DateTime.Now },
            new Announcement { Title = "Spor Turnuvası",              Content = "Kampüs içi spor turnuvası kayıtları başlamıştır.",                                      Type = AnnouncementType.Etkinlik,   CreatedAt = DateTime.Now },
            new Announcement { Title = "Haftalık Yemek Listesi",      Content = "Bu haftanın yemek listesi öğrenci portalında yayınlanmıştır.",                          Type = AnnouncementType.Yemekhane,  CreatedAt = DateTime.Now }
        );
    }

    db.SaveChanges();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
