# 🏙️ Beşiktaş Guest Book API

Beşiktaş ilçesinin mahallelerine yönelik bir ziyaretçi defteri REST API'si. Kullanıcılar kayıt olup giriş yaparak Beşiktaş'ın farklı mahallelerine not/mesaj ekleyebilir, düzenleyebilir ve arayabilirler.

---

## 🚀 Teknolojiler

| Teknoloji | Kullanım |
|---|---|
| **ASP.NET Core 8** | Web API framework |
| **Entity Framework Core** | ORM |
| **PostgreSQL** | Veritabanı (Npgsql sürücüsü) |
| **JWT Bearer** | Kimlik doğrulama |
| **AutoMapper** | Nesne eşleme |
| **FluentValidation** | Girdi doğrulama |
| **Serilog** | Loglama |
| **Swagger / OpenAPI** | API dokümantasyonu (geliştirme ortamı) |
| **Docker** | Konteynerizasyon |

---

## 📁 Proje Yapısı

Çözüm **Clean Architecture** prensiplerine göre 5 katmana ayrılmıştır:

```
BesiktasGuestBook/
├── Domain/               # Temel iş modelleri ve entity'ler
│   ├── Entities/         # User, Note, Mahalle (+ DTO'lar)
│   ├── Common/           # EntityBase (ortak alanlar)
│   └── Request/          # API istek modelleri
│
├── Application/          # İş mantığı servisleri
│   └── Service/          # IUserService, INoteService, IMahalleService + uygulamaları
│
├── Infrustructure/       # Yatay kesim kaygıları
│   ├── Token/            # JWT üretimi (TokenService, JwtOptions)
│   ├── PasswordHash/     # Şifre hashleme
│   ├── Mapper/           # AutoMapper profilleri
│   ├── Validators/       # FluentValidation doğrulayıcıları
│   ├── Middlewares/      # GlobalExceptionHandler
│   └── Exception/        # Özel exception tipleri
│
├── Persistence/          # Veri erişim katmanı
│   ├── Context/          # AppDbContext (EF Core)
│   └── Migrations/       # EF Core migration dosyaları
│
└── projemaksut/          # Web API giriş noktası
    ├── Controller/       # AuthController, UserController, NoteController, MahalleController
    ├── Program.cs        # DI kaydı, middleware, otomatik migration
    └── appsettings.json  # Uygulama yapılandırması
```

---

## 🔑 API Endpoint'leri

### Auth
| Method | Endpoint | Açıklama | Yetki |
|--------|----------|----------|-------|
| POST | `/api/Auth/Login` | Giriş yap, JWT token al | Herkese açık |

### User
| Method | Endpoint | Açıklama | Yetki |
|--------|----------|----------|-------|
| POST | `/api/User/Register` | Yeni kullanıcı kaydı | Herkese açık |
| GET | `/api/User/users` | Tüm kullanıcıları listele | JWT |
| GET | `/api/User/active/users` | Aktif kullanıcıları listele | JWT |
| GET | `/api/User/filtre` | Kullanıcıları filtrele (id, name, surname, email) | JWT |
| PUT | `/api/User/update` | Kendi profilini güncelle | JWT |
| DELETE | `/api/User/delete/{id}` | Kullanıcıyı soft delete yap | JWT |
| GET | `/api/User/adminUsers` | Tüm kullanıcı detayları | JWT (Admin) |
| DELETE | `/api/User/adminHardDelete/{id}` | Kullanıcıyı kalıcı sil | JWT (Admin) |

### Note
| Method | Endpoint | Açıklama | Yetki |
|--------|----------|----------|-------|
| POST | `/api/Note/CreateNote` | Not oluştur | JWT |
| GET | `/api/Note/GetUserNotesById` | Kullanıcıya ait notları getir | JWT |
| PUT | `/api/Note/UpdateNote` | Not güncelle (sadece kendi notun) | JWT |
| DELETE | `/api/Note/DeleteNote` | Not sil | JWT |
| GET | `/api/Note/FiltreNot` | Notları metin ile filtrele | JWT |

### Mahalle
| Method | Endpoint | Açıklama | Yetki |
|--------|----------|----------|-------|
| GET | `/api/Mahalle/AllMahalles` | Tüm mahalleleri listele | Herkese açık |
| GET | `/api/Mahalle/FiltreMahalle` | Mahalle adına göre filtrele | Herkese açık |

---

## ⚙️ Kurulum ve Çalıştırma

### Gereksinimler
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/)

### 1. Repoyu klonla

```bash
git clone https://github.com/maksutakg/BesiktasGuestBook.git
cd BesiktasGuestBook
```

### 2. Yapılandırmayı ayarla

`projemaksut/appsettings.json` dosyasını düzenle veya aşağıdaki ortam değişkenlerini tanımla:

```bash
POSTGRES_CONN="Host=localhost;Username=postgres;Password=sifre;Database=guestbookdotnet"
JWT_KEY="gizli-anahtar-buraya"
```

### 3. Bağımlılıkları yükle ve çalıştır

```bash
dotnet restore
cd projemaksut
dotnet run
```

> Uygulama başlarken bekleyen EF Core migration'ları ve mahalle seed verisi otomatik olarak uygulanır.

### 4. Swagger UI

Geliştirme ortamında aşağıdaki adresten API'yi test edebilirsiniz:
```
http://localhost:8080/swagger
```

---

## 🐳 Docker ile Çalıştırma

```bash
docker build -t besiktas-guestbook .
docker run -p 8080:8080 \
  -e POSTGRES_CONN="Host=host.docker.internal;Username=postgres;Password=sifre;Database=guestbookdotnet" \
  -e JWT_KEY="gizli-anahtar-buraya" \
  besiktas-guestbook
```

---

## 🌍 Ortam Değişkenleri

| Değişken | Açıklama | Varsayılan |
|---|---|---|
| `POSTGRES_CONN` | PostgreSQL bağlantı dizisi | `appsettings.json` içindeki değer |
| `JWT_KEY` | JWT imzalama anahtarı | `appsettings.json` içindeki değer |
| `PORT` | Dinlenecek port | `8080` |

---

## 🗺️ Veri Modeli

```
User ─── (1:N) ──► Note ◄── (N:1) ─── Mahalle
```

- **User**: Ad, soyad, e-posta (unique), hashlenmiş şifre
- **Note**: Metin, tarih/saat, kullanıcı ve mahalle ilişkileri
- **Mahalle**: Beşiktaş'taki 23 mahalle (seed verisi olarak yüklenir)

### Seed Mahalleler
Abbasağa, Akat, Arnavutköy, Balmumcu, Bebek, Cihannüma, Dikilitaş, Etiler, Gayrettepe, Konaklar, Kuruçeşme, Kültür, Levazım, Levent, Mecidiye, Muradiye, Nispetiye, Ortaköy, Sinanpaşa, Türkali, Ulus, Vişnezade, Yıldız

---

## 🔐 Kimlik Doğrulama

API, **JWT Bearer Token** kullanmaktadır.

1. `/api/Auth/Login` endpoint'ine mail ve şifre ile istek gönder
2. Dönen token'ı `Authorization: Bearer <token>` header'ı ile diğer isteklerde kullan

Token geçerlilik süresi: **99 dakika**

---

## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.
