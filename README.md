# MT2Config

Metin2 client'ı için `config.exe` ayar aracı. `metin2.cfg` dosyasını client'ın
`CPythonSystem::LoadConfig()` / `SaveConfig()` fonksiyonlarıyla birebir aynı kurallarla okur ve yazar.

*English summary below.*

## Özellikler

- **Client uyumlu `metin2.cfg`**
  - Client'ın yazdığı dosya olduğu gibi okunur, hiçbir zaman silinmez.
  - Aracın düzenlemediği anahtarlar (`BPP`, `SAVE_ID`, `PRE_LOADING_DELAY_TIME`, kendi client'ınıza eklediğiniz özel anahtarlar…) korunur.
  - Sayılar Windows dilinden bağımsız yazılır (`MUSIC_VOLUME 0.750`, `VOICE_VOLUME 0-5`).
  - Dosyada olmayan anahtarlar client'ın varsayılanlarıyla yorumlanır.
- **Gerçek ekran modları:** Çözünürlük ve yenileme hızı listeleri monitörün desteklediği modlardan (`EnumDisplaySettings`) gelir.
  - Listede olmayan bir değer silinmez, *(özel)* olarak gösterilir.
- **Türkçe ve İngilizce arayüz:**
  - Dil Windows'a göre seçilir ve pencereden değiştirilebilir.
  - Seçim `HKCU\Software\MT2Config` altında hatırlanır.
- **Ek seçenekler:**
  - Tüm gölge seviyeleri (0-5)
  - Görünmeyen nesneleri gizleme (culling)
  - Sıkıştırılmamış dokular
  - Sohbet / isim / hasar / pazar başlığı gösterimi
- **"Kaydet ve Oyna":** Client exe'si `config.exe` ile aynı klasördeyse görünür.
- **Tek dosya:** Oyunculara sadece `config.exe` dağıtılır.
  - Windows 7 SP1 ve üstünde çalışır.
  - Windows 10 (1903+) ve 11'de kurulum gerekmez.
  - DPI ölçeklemesinde bulanıklaşmaz.

## Gereksinimler

| | |
|---|---|
| Oyuncu | Windows 7 SP1+ ve .NET Framework 4.8 (Windows 10 1903+ / 11'de hazır gelir) |
| Geliştirici | Visual Studio 2026 (".NET masaüstü geliştirme" iş yükü) veya Windows'ta .NET SDK |

## Derleme

- **Visual Studio:** `MT2Config.slnx` dosyasını açın ve Release olarak derleyin.
- **Komut satırı:**

  ```
  dotnet build MT2Config.slnx -c Release
  ```

Çıktı `MT2Config\bin\Release\config.exe` dosyasıdır. Bu dosyayı client klasörüne kopyalayın (`metin2.cfg` ile aynı yere).
Oyunculara yalnızca `config.exe` dağıtmanız yeterlidir. `config.exe.config` isteğe bağlıdır: yanında dağıtılırsa
.NET Framework 4.8 yüklü olmayan sistemlerde Windows kurulum ister.

Her push'ta GitHub Actions da derleme yapar. `config.exe`, çalıştırmanın sayfasından indirilebilir.

## Sunucunuza göre özelleştirme

| Dosya | Ne değişir |
|---|---|
| `MT2Config/GameClient.cs` | Pencere başlığındaki oyun adı ve "Kaydet ve Oyna"nın arayacağı client exe isimleri |
| `MT2Config/ClientConfig.cs` | Varsayılan değerler. Client'ınızdaki `SetDefaultConfig()` ve `DEFAULT_VALUE_ALWAYS_SHOW_NAME` ile **aynı olmalıdır**, çünkü `SaveConfig()` varsayılan değerdeki `WINDOWED`, `VIEW_CHAT`, `ALWAYS_VIEW_NAME`, `SHOW_DAMAGE`, `SHOW_SALESTEXT` anahtarlarını dosyaya yazmaz. |
| `MT2Config/Language.cs` | Metinler. Yeni dil eklemek için bir örneği kopyalayıp çevirin ve `All` listesine ekleyin. |

### Yeni bir cfg anahtarı eklemek

Client'ın `LoadConfig/SaveConfig` fonksiyonlarına yeni bir anahtar eklediyseniz:

1. `ClientConfig.cs` dosyasında:
   - bir özellik ekleyin,
   - `ReadEntries()` ve `WriteEntries()` içine okuma/yazma satırını ekleyin,
   - anahtarı `SaveOrder` listesine ekleyin.
2. `MainForm`'a bir kontrol ekleyin ve onu `ShowConfig()` / `ReadConfig()` içinde bağlayın.
3. Metnini `Language.cs` içine ekleyin.

Arayüzde göstermeyecekseniz hiçbir şey yapmanıza gerek yok: araç bilinmeyen anahtarları zaten korur.

## Client uyumluluğu hakkında

- **Boş satır:** Client okumayı ilk boş satırda bırakır (`sscanf` → `EOF`). Araç da öyle okur ve yalnızca dosyanın sonuna boş satır yazar.
- **`MUSIC_VOLUME`:** İçinde `.` olmayan bir değer client'ta eski 0-5 ölçeği sayılır (`1` ≈ %16). Bu yüzden değer her zaman `%.3f` biçiminde yazılır.
- **`VOICE_VOLUME`:** `atoi` ile okunan 0-5 arası bir tam sayıdır.
- **Anahtar adları:** Büyük/küçük harf duyarsızdır (`stricmp`). Karşılaştırma Türkçe `i/İ` kurallarından etkilenmez.
- **BOM:** Dosyanın başındaki UTF-8 BOM yok sayılır (client'ta ilk ayarı bozuyordu) ve geri yazılmaz.

## Lisans

GPL-3.0. İlk sürüm Takuma (work.takuma@gmail.com) tarafından yazılmıştır.

---

## English

`config.exe` for the Metin2 client. It reads and writes `metin2.cfg` with the same rules as the client's
`CPythonSystem::LoadConfig()` / `SaveConfig()`.

- **Compatible `metin2.cfg` handling:**
  - Unknown keys are preserved and the file is never deleted.
  - Numbers are written culture-invariant.
  - Missing keys mean the client defaults.
- **Display modes:** Resolutions and refresh rates are queried from the monitor.
- **Languages:** Turkish and English UI, selectable at runtime.
- **"Save and Play":** Starts the client when its exe is next to `config.exe`.
- **Single file:** `config.exe` targets .NET Framework 4.8 and runs on Windows 7 SP1+, with nothing to install on Windows 10/11.

Build with Visual Studio 2026 (`MT2Config.slnx`) or `dotnet build MT2Config.slnx -c Release` on Windows.
Server-specific settings live in `GameClient.cs` (name, client exe), `ClientConfig.cs` (defaults, keep them in
sync with the client's `SetDefaultConfig()`) and `Language.cs` (texts).

Licensed under GPL-3.0, originally written by Takuma.
