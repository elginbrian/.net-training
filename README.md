# 🎬 Cinema API - Sistem Informasi Bioskop

Cinema API adalah RESTful API yang dibangun menggunakan **.NET 10** dan **Entity Framework Core**. Proyek ini merupakan sistem *backend* untuk manajemen bioskop, mencakup pengelolaan studio, film, jadwal tayang, hingga sistem pemesanan tiket dengan pencegahan *double booking*.

## ✨ Fitur Utama

- **🔐 Autentikasi JWT & Role-based Access:** Sistem login dengan JWT. Pembagian hak akses antara `Admin` (mengelola data master) dan `User` (memesan tiket).
- **🛡️ Password Hashing:** Melindungi kredensial pengguna dengan algoritma enkripsi BCrypt.
- **🚫 Pencegahan Double Booking:** Logika bisnis yang ketat di lapisan *Service Layer* untuk memastikan satu kursi hanya bisa dipesan oleh satu orang pada jadwal yang sama.
- **⚙️ Global Exception Handling:** Menggunakan *Custom Middleware* untuk menangkap seluruh *error* aplikasi dan membungkusnya ke dalam respons JSON yang rapi dan seragam.
- **📚 API Documentation:** Integrasi dengan **Scalar** untuk eksplorasi dan pengujian API yang sangat interaktif dan mudah dipahami.
- **🧩 Loose Coupling Architecture:** Implementasi arsitektur relasi database modern (Token-based Identity) antara tabel User dan Tiket.

## 🛠️ Teknologi yang Digunakan

- **Framework:** .NET 10 (ASP.NET Core Web API)
- **Database:** Microsoft SQL Server (Express / LocalDB)
- **ORM:** Entity Framework (EF) Core 10
- **Security:** JWT Bearer Authentication, BCrypt.Net-Next
- **API UI:** Scalar.AspNetCore

## 🚀 Cara Menjalankan Proyek

### 1. Prasyarat
- Instal [.NET SDK terbaru](https://dotnet.microsoft.com/).
- Instal SQL Server (SQLEXPRESS) atau LocalDB.
- Instal `dotnet-ef` CLI tool (`dotnet tool install --global dotnet-ef`).

### 2. Konfigurasi Database
Buka file `CinemaAPI/appsettings.json` dan pastikan `DefaultConnection` sudah mengarah ke instans SQL Server Anda. Contoh untuk `SQLEXPRESS`:
```json
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=CinemaDb;Trusted_Connection=True;TrustServerCertificate=True"
```

### 3. Menyiapkan Database (Migration & Seeding)
Buka terminal di dalam folder proyek (`CinemaAPI`) dan jalankan perintah berikut untuk mengeksekusi migrasi dan menyuntikkan data awal (Data Seeding):
```bash
dotnet ef database update
```

### 4. Menjalankan Aplikasi
Jalankan perintah ini di terminal:
```bash
dotnet run
```

### 5. Mengakses Dokumentasi (Scalar)
Setelah server menyala, buka *browser* Anda dan kunjungi halaman Scalar (sesuaikan port-nya):
👉 `http://localhost:<port>/scalar/v1`

## 🧪 Panduan Testing dengan JWT

Database otomatis menyediakan 2 akun bawaan:
- **Akun Admin:** `admin@cinema.com` | Password: `admin123`
- **Akun User:** `user@cinema.com` | Password: `user123`

Langkah-langkah mencoba API yang terkunci (seperti `POST /api/Tikets`):
1. Panggil *endpoint* `POST /api/Auth/login` untuk mendapatkan string Token JWT.
2. Di halaman UI Scalar, pilih *endpoint* yang ingin diuji dan buka panel **Test Request**.
3. Di tab **Headers**, tambahkan data berikut:
   - Key: `Authorization`
   - Value: `Bearer <Token_JWT_Anda>`
4. Tekan **Send**!
