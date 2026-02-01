# Task Management System

Bu proje, .NET 9.0 Web API backend ve modern bir frontend arayüzünden oluşan kapsamlı bir görev yönetim sistemidir.

---

## 🛠 Kurulum ve Çalıştırma

### 1. Backend Hazırlığı (.NET Core 9.0)
Öncelikle API tarafını ve veritabanını ayağa kaldırmanız gerekmektedir.

* **Dizin:** `backend/TaskManagementSystem` klasörüne gidin.
* **Veritabanı İşlemleri:** Visual Studio içerisinde **Package Manager Console** üzerinden veya terminalden şu komutları çalıştırarak veritabanını localde oluşturun:
    ```powershell
    Add-Migration InitialCreate
    Update-Database
    ```
* **Çalıştırma:** Uygulamayı başlatın:
    ```bash
    dotnet run
    ```
* **Erişim:** API `http://localhost:5271` adresinde çalışacaktır.

---

### 2. Frontend Hazırlığı
Arayüzü çalıştırmak için Node.js yüklü olmalıdır.

* **Dizin:** `.\task-management-system\frontend\task-management-ui` klasörüne girin.
* **Bağımlılıklar:** Gerekli paketleri yükleyin:
    ```bash
    npm install
    ```
* **Çalıştırma:** Frontend'i başlatın:
    ```bash
    npm run dev
    ```
* **Erişim:** Tarayıcıdan `http://localhost:3000` adresine gidin.

---

## 🔐 Test Giriş Bilgileri

Sistemi aşağıdaki hazır hesaplarla test edebilirsiniz:

| Rol | E-posta | Şifre |
| :--- | :--- | :--- |
| **Yönetici (Admin)** | admin@company.com | admin123 |
| **Kullanıcı (Ayşe)** | ayse.kaya@company.com | ayse123 |

---

## 🚀 Teknolojiler
- **Backend:** .NET 9.0 Web API / Entity Framework Core
- **Frontend:** Next.JS
- **Portlar:** API (5271), UI (3000)
