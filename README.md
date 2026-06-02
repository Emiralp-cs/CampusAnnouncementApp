# CampusAnnouncement

Kampüs duyurularını yönetmek için geliştirilmiş bir web uygulamasıdır. Öğrenciler ve öğretmenler sisteme kayıt olabilir; sınav, etkinlik, yemekhane ve kütüphane gibi farklı kategorilerde duyurular oluşturulabilir. Duyuru yayınlandığında kayıtlı tüm kullanıcılar otomatik olarak bildirim alır.

---

## Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|---|---|
| C# / .NET 10 | Backend uygulama dili ve çalışma ortamı |
| ASP.NET Core Web API | RESTful API katmanı |
| Entity Framework Core | ORM — veritabanı işlemleri |
| SQLite | Yerel veritabanı (`campus.db`) |
| Swashbuckle / Swagger | API dokümantasyonu ve test arayüzü |
| HTML / CSS / JavaScript | Statik ön yüz (`wwwroot/index.html`) |

---

## Kullanılan Tasarım Desenleri

### Observer (Gözlemci)
Duyuru yayınlandığında sisteme kayıtlı tüm kullanıcılara otomatik bildirim gönderilir. `AnnouncementPublisher` yayıncı (subject), `StudentObserver` ve `TeacherObserver` sınıfları ise gözlemci (observer) rolündedir.

### Factory (Fabrika)
Farklı duyuru türleri (`Sinav`, `Etkinlik`, `Yemekhane`, `Kutuphane`) tek bir merkezi noktadan üretilir. `AnnouncementFactory.Create()` metodu, gelen türe göre uygun nesneyi oluşturur ve yanlış türler için hata fırlatır.

### Singleton (Tek Örnek)
Uygulama genelinde yalnızca tek bir `Logger` örneği bulunur. `Logger.GetInstance()` metodu, thread-safe çift kontrol kilidi (double-checked locking) ile bu garantiyi sağlar.

---

## Katmanlı Mimari

```
CampusAnnouncement/
├── Domain/          → Çekirdek iş mantığı: entity'ler, interface'ler, enum'lar
├── Application/     → Uygulama servisleri ve factory sınıfları
├── Infrastructure/  → EF Core DbContext, observer'lar, bildirim türleri, Logger
└── Controllers/     → HTTP isteklerini karşılayan Presentation katmanı
```

| Katman | Sorumluluk |
|---|---|
| **Domain** | `BaseAnnouncement`, `User`, `IObserver`, `IPublisher` gibi saf iş nesneleri. Dışarıya bağımlılığı yoktur. |
| **Application** | `AnnouncementPublisher` (Observer yönetimi) ve `AnnouncementFactory` (nesne üretimi). Yalnızca Domain'e bağımlıdır. |
| **Infrastructure** | Veritabanı bağlamı (`AppDbContext`), `StudentObserver`, `TeacherObserver`, bildirim sınıfları ve `Logger`. |
| **Presentation** | `UsersController` ve `AnnouncementsController` — HTTP katmanı. |

---

## Projeyi Çalıştırma

### Gereksinimler
- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Adımlar

```bash
# 1. Projeyi klonlayın
git clone <repo-url>
cd CampusAnnouncement/CampusAnnouncement

# 2. Bağımlılıkları yükleyin ve uygulamayı çalıştırın
dotnet run
```

Uygulama varsayılan olarak `http://localhost:5000` adresinde çalışır.

- Ön yüz arayüzü: `http://localhost:5000`
- Swagger dokümantasyonu: `http://localhost:5000/swagger`

> Veritabanı (`campus.db`) ilk çalıştırmada otomatik olarak oluşturulur; migration çalıştırmak gerekmez.

---

## API Endpoint'leri

### Kullanıcılar

| Metot | Endpoint | Açıklama |
|---|---|---|
| `GET` | `/api/users` | Tüm kullanıcıları listeler |
| `POST` | `/api/users` | Yeni kullanıcı oluşturur |
| `DELETE` | `/api/users/{id}` | Belirtilen kullanıcıyı siler |

**POST /api/users — İstek gövdesi:**
```json
{
  "name": "Ahmet Yılmaz",
  "userType": 0
}
```
> `userType`: `0` = Öğrenci, `1` = Öğretmen

---

### Duyurular

| Metot | Endpoint | Açıklama |
|---|---|---|
| `GET` | `/api/announcements` | Tüm duyuruları listeler |
| `POST` | `/api/announcements` | Yeni duyuru oluşturur ve kullanıcılara bildirim gönderir |
| `DELETE` | `/api/announcements/{id}` | Belirtilen duyuruyu siler |

**POST /api/announcements — İstek gövdesi:**
```json
{
  "title": "Vize Sınavı Duyurusu",
  "content": "Vize sınavları 10 Haziran'da başlayacaktır.",
  "type": 0
}
```
> `type`: `0` = Sınav, `1` = Etkinlik, `2` = Yemekhane, `3` = Kütüphane

---

## Proje Klasör Yapısı

```
CampusAnnouncement/
└── CampusAnnouncement/
    ├── Controllers/
    │   ├── AnnouncementsController.cs
    │   └── UsersController.cs
    ├── Application/
    │   ├── Factories/
    │   │   ├── AnnouncementFactory.cs
    │   │   └── NotificationFactory.cs
    │   └── Services/
    │       └── AnnouncementPublisher.cs
    ├── Domain/
    │   ├── Entities/
    │   │   ├── Announcement.cs
    │   │   ├── BaseAnnouncement.cs
    │   │   ├── User.cs
    │   │   ├── ExamAnnouncement.cs
    │   │   ├── EventAnnouncement.cs
    │   │   ├── FoodMenuAnnouncement.cs
    │   │   └── LibraryAnnouncement.cs
    │   ├── Enums/
    │   │   ├── AnnouncementType.cs
    │   │   ├── NotificationType.cs
    │   │   └── UserType.cs
    │   └── Interfaces/
    │       ├── IAnnouncement.cs
    │       ├── INotification.cs
    │       ├── IObserver.cs
    │       └── IPublisher.cs
    ├── Infrastructure/
    │   ├── Notifications/
    │   │   ├── EmailNotification.cs
    │   │   ├── PushNotification.cs
    │   │   └── SmsNotification.cs
    │   ├── Observers/
    │   │   ├── StudentObserver.cs
    │   │   └── TeacherObserver.cs
    │   ├── Persistence/
    │   │   └── AppDbContext.cs
    │   └── Logger.cs
    ├── wwwroot/
    │   └── index.html
    ├── campus.db
    ├── Program.cs
    └── CampusAnnouncement.csproj
```
