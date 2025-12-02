# ML Math Image Processing - Sayı Tanıma Uygulaması

Modern ve kullanıcı dostu bir Windows Forms uygulaması ile görüntü işleme ve makine öğrenmesi tabanlı sayı tanıma özellikleri sunan bir masaüstü uygulamasıdır.

## 📋 Özellikler

### 🔹 Görüntü İşleme
- **Görüntü Yükleme**: JPG, PNG, JPEG, BMP formatlarında görüntü dosyalarını yükleme
- **Gri Tonlama Dönüştürme**: Renkli görüntüleri gri tonlamaya dönüştürme

### 🔹 Makine Öğrenmesi ile Sayı Tanıma
- **Sayı Tanıma**: Görüntülerdeki sayıları otomatik olarak tanıma
- **Piksel Tabanlı Analiz**: 28x28 piksel standardına uygun görüntü işleme
- **Anlık Sonuç Gösterimi**: Tanıma sonuçlarını anında görüntüleme

### 🔹 Modern Kullanıcı Arayüzü
- **Renkli ve Modern Tasarım**: Profesyonel görünümlü arayüz
- **Kullanıcı Dostu Butonlar**: Kolay kullanım için optimize edilmiş kontroller
- **Anlık Geri Bildirim**: İşlem durumu ve sonuçlar için görsel geri bildirim

## 🛠️ Gereksinimler

### Sistem Gereksinimleri
- **İşletim Sistemi**: Windows 7 veya üzeri
- **.NET Framework**: 4.7.2 veya üzeri
- **Geliştirme Ortamı**: Visual Studio 2019/2022 (geliştirme için)

### NuGet Paketleri
- `Microsoft.ML` (v1.7.1)
- `Microsoft.ML.ImageAnalytics` (v1.7.1)

## 📦 Kurulum

### 1. Projeyi İndirin
```bash
git clone <repository-url>
cd ML_Math_Image_Processing
```

### 2. Visual Studio ile Açın
- Visual Studio'yu açın
- `File > Open > Project/Solution` menüsünden `MLMathImageProcessing.csproj` dosyasını seçin

### 3. NuGet Paketlerini Yükleyin
Visual Studio otomatik olarak NuGet paketlerini restore edecektir. Eğer restore edilmezse:

```bash
# Visual Studio Package Manager Console'da
Update-Package -reinstall
```

### 4. Projeyi Derleyin
- `Build > Build Solution` (Ctrl+Shift+B) ile projeyi derleyin
- Veya `Debug > Start Debugging` (F5) ile doğrudan çalıştırın

## 🚀 Kullanım Kılavuzu

### Adım 1: Görüntü Yükleme
1. Uygulamayı başlatın
2. **"📁 Görüntü Yükle"** butonuna tıklayın
3. Açılan dosya seçici penceresinden sayı içeren bir görüntü seçin
4. Görüntü ana ekranda görüntülenecektir

### Adım 2: (İsteğe Bağlı) Gri Tonlama
1. Görüntü yüklendikten sonra **"🎨 Gri Tonlama"** butonuna tıklayın
2. Görüntü gri tonlamaya dönüştürülecektir
3. Bu işlem sayı tanıma doğruluğunu artırabilir

### Adım 3: Sayı Tanıma
1. **"🔢 Sayı Tanı"** butonuna tıklayın
2. Uygulama görüntüyü analiz edecektir
3. Tanınan sayı hem alt panelde hem de açılan mesaj kutusunda gösterilecektir

## 📁 Proje Yapısı

```
ML_Math_Image_Processing/
│
├── MainForm.cs                 # Ana form ve kullanıcı arayüzü
├── DigitRecognitionService.cs  # Sayı tanıma servisi
├── Program.cs                  # Uygulama giriş noktası
├── MLMathImageProcessing.csproj # Proje yapılandırma dosyası
└── README.md                   # Bu dosya
```

## 🎨 Kullanıcı Arayüzü

- **Başlık Paneli**: Koyu mavi arka planlı başlık
- **Buton Paneli**: Üst kısımda işlem butonları
- **Görüntü Görüntüleme Alanı**: Orta kısımda büyük görüntü gösterimi
- **Sonuç Paneli**: Alt kısımda işlem sonuçları

## 🐛 Bilinen Sorunlar ve Çözümler

### Build Hatası: Dosya kilitli hatası
**Sorun**: `MSB3027` veya `MSB3021` hatası alıyorsunuz.

**Çözüm**:
1. Çalışan uygulama örneğini kapatın
2. Task Manager'dan `MLMathImageProcessing.exe` process'ini sonlandırın
3. Veya Visual Studio'da `Debug > Stop Debugging` (Shift+F5) yapın
4. Tekrar build edin

### Sayı Tanıma Doğruluğu
Mevcut algoritma basit bir piksel analizi kullanmaktadır. Daha iyi sonuçlar için:
- Temiz, yüksek kontrastlı görüntüler kullanın
- Sayıların görüntü merkezinde olduğundan emin olun
- Gri tonlama dönüştürmeyi kullanın

## 📝 Lisans

Bu proje eğitim ve kişisel kullanım amaçlıdır.

## 👨‍💻 Geliştirici

ML Math Image Processing projesi Visual Studio 2019/2022 ile geliştirilmiştir.

**Not**: Bu uygulama .NET Framework 4.7.2 ve ML.NET 1.7.1 kullanmaktadır.
