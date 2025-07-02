# NASA Bot Route App

## 📖 Proje Açıklaması

NASA Bot Route App, Mars yüzeyinde hareket eden rover robotları için bir navigasyon ve hareket kontrol sistemidir. Bu uygulama, rover robotlarının belirli bir alanda güvenli bir şekilde hareket etmelerini sağlar ve çarpışma kontrolü yapar.

## 🚀 Özellikler

- **Rover Robot Yönetimi**: Birden fazla rover robotunu sırayla yönetebilme
- **Hareket Kontrolü**: Robotların N (Kuzey), S (Güney), E (Doğu), W (Batı) yönlerinde hareket etmesi
- **Çarpışma Önleme**: Robotlar arasında çarpışmayı önleyen kontrol sistemi
- **Sınır Kontrolü**: Belirlenen alan sınırları içinde hareket garantisi
- **Real-time Konum Raporlama**: Robotların anlık konum ve yön bilgilerini görüntüleme

## 🛠 Teknolojiler

- **.NET 9.0**: Ana framework
- **C#**: Programlama dili
- **xUnit**: Unit test framework
- **Console Application**: Kullanıcı arayüzü

## 📁 Proje Yapısı

```
NasaBotRouteApp/
├── Models/
│   ├── Abstract/          # Soyut sınıflar ve arayüzler
│   │   ├── BaseRobot.cs
│   │   ├── IRobot.cs
│   │   └── BaseSurfaceRobot.cs
│   ├── Concrete/          # Somut model sınıfları
│   │   ├── CoordinateDirectionTypes.cs
│   │   ├── NasaMoveDirectionTypes.cs
│   │   └── RoverRobot.cs
│   └── Dtos/              # Veri transfer objeleri
│       └── RoverRobotValidationFilterData.cs
├── Service/               # İş mantığı servisleri
│   ├── INasaRoverMovementService.cs
│   └── NasaRoverMovementService.cs
├── Helpers/               # Yardımcı sınıflar
│   └── NasaMovementHelper.cs
├── Extensions/            # Uzantı metotları
│   └── DataExtensions.cs
└── Constants/             # Sabit değerler
    └── NasaConstants.cs
```

## 🎮 Kullanım

### Giriş Formatı

1. **Alan Boyutları**: `maxX maxY` (örn: `5 5`)
2. **Robot Konumu**: `x y direction` (örn: `1 2 N`)
3. **Hareket Komutları**: `LRMM...` formatında string

### Komutlar

- **L**: Sol döndür (90 derece)
- **R**: Sağ döndür (90 derece)
- **M**: İleri hareket et(1 birim)

### Yönler

- **N**: Kuzey (North)
- **E**: Doğu (East)
- **S**: Güney (South)
- **W**: Batı (West)

### Örnek Kullanım

```
5 5          // 5x5 boyutunda alan
1 2 N        // 1. robot: x=1, y=2, Kuzey yönünde
LMLMLMLMM    // 1. robot hareket komutları
3 3 E        // 2. robot: x=3, y=3, Doğu yönünde
MMRMMRMRRM   // 2. robot hareket komutları
```

**Beklenen Çıktı:**

```
1 3 N        // 1. robot final pozisyonu
5 1 E        // 2. robot final pozisyonu
```

## 🧪 Testler

Proje kapsamlı unit testlere sahiptir:

```bash
# Testleri çalıştırma
dotnet test

# Test kapsamı ile çalıştırma
dotnet test --collect:"XPlat Code Coverage"
```

### Test Senaryoları

- ✅ Robot oluşturma validasyonları
- ✅ Hareket komutları doğrulaması
- ✅ Çarpışma kontrol testleri
- ✅ Sınır kontrolü testleri
- ✅ Yön değiştirme testleri

## 🚦 Kurulum ve Çalıştırma

### Gereksinimler

- .NET 9.0 SDK
- Visual Studio 2022 veya VS Code

### Adımlar

1. **Repository'yi klonlayın:**

   ```bash
   git clone <repository-url>
   cd NasaBotRouteApp
   ```

2. **Uygulamayı çalıştırın:**

   ```bash
   dotnet run --project NasaBotRouteApp
   ```

3. **Testleri çalıştırın:**
   ```bash
   dotnet test NasaBotRouteTest
   ```

## 🏗 Mimari Tasarım

## 🔍 Önemli Sınıflar

### `NasaRoverMovementService`

Rover robotlarının hareket mantığını yöneten ana servis sınıfı.

### `RoverRobot`

Rover robotunu temsil eden model sınıfı, pozisyon ve yön bilgilerini içerir.

### `NasaMovementHelper`

Hareket hesaplamaları ve parsing işlemleri için yardımcı metotlar.
