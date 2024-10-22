Bu proje koduna dayalı bir `README.md` dosyası hazırladım. Bu dosya, projenizin ne yaptığı, nasıl kurulduğu, kullanılan teknolojiler ve kullanım adımlarını içerir. İşte örnek içerik:

---

# AccessLibrary - Kütüphane Yönetim Sistemi

Bu proje, bir Access veritabanı kullanarak kütüphane yönetim işlemlerini gerçekleştirmek amacıyla geliştirilmiş bir **Windows Forms** uygulamasıdır. Kullanıcılar kitap ekleyebilir, silebilir, güncelleyebilir ve kitaplar üzerinde arama yapabilir. Proje, veritabanı işlemleri için **OleDb** kütüphanesini kullanmaktadır.

# Arayüz
![image](https://github.com/user-attachments/assets/e104854e-4175-4193-acdf-4b0feb7377a4)


## Özellikler
- **Kitap Ekleme**: Kullanıcılar yeni kitaplar ekleyebilir.
- **Kitap Güncelleme**: Var olan kitap bilgilerini düzenleyebilirsiniz.
- **Kitap Silme**: İstenmeyen kitapları veritabanından silebilirsiniz.
- **Kitap Arama**: Kitaplar, isimlerine göre anlık olarak aranabilir.
- **Kullanıcı Dostu Arayüz**: Tüm işlemler basit ve kullanışlı bir arayüzle yapılabilir.

## Kullanılan Teknolojiler
- **C# Windows Forms**: Arayüz geliştirme ve olay yönetimi.
- **Access Veritabanı (MDB)**: Kitap bilgilerini saklamak için kullanıldı.
- **OleDb**: Veritabanı bağlantıları ve sorgular.
- **ADO.NET**: Veritabanı işlemlerini gerçekleştirmek için kullanıldı.

## Kurulum

1. Bu projeyi bilgisayarınıza **klonlayın** veya indirin:
   ```bash
   git clone https://github.com/vahap-18/AccessLibrary.git
   ```

2. Visual Studio'da projeyi açın ve gerekli kütüphanelerin yüklü olduğundan emin olun.
   
3. Veritabanı dosyasının yolunu `OleDbConnection` nesnesi içindeki `connection` değişkenine doğru şekilde bağlayın:
   ```csharp
   OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=your-database-path.mdb");
   ```

4. Projeyi çalıştırmak için **Ctrl + F5** tuşlarına basarak başlatın.

## Kullanım

- **Kitap Ekleme**: "Ekle" butonuna tıklayarak kitap bilgilerini girebilir ve kitap ekleyebilirsiniz.
- **Kitap Güncelleme**: Bir kitabı seçip bilgilerini düzenleyerek "Güncelle" butonuna tıklayın.
- **Kitap Silme**: Silmek istediğiniz kitabı seçin ve "Sil" butonuna basın.
- **Kitap Arama**: Arama kutusuna kitap ismini yazdıkça, sonuçlar anlık olarak listelenecektir.

## Örnek Veritabanı Yapısı

| Id   | Name     | Author   | Type      | PageNumber | Station |
|------|----------|----------|-----------|------------|---------|
| 1    | Kitap Adı| Yazar Adı| Roman     | 300        | 1       |

- `Id`: Kitap numarası (otomatik artar).
- `Name`: Kitap adı.
- `Author`: Kitap yazarı.
- `Type`: Kitap türü.
- `PageNumber`: Sayfa sayısı.
- `Station`: Durumu (0 = ikinci el, 1 = sıfır ).

## Hata Ayıklama

Eğer proje sırasında herhangi bir hata ile karşılaşırsanız:
- **Bağlantı hatası**: Veritabanı dosyasının doğru dizinde olduğundan emin olun.
- **OleDb veritabanı hataları**: SQL sorgularını ve parametreleri dikkatlice kontrol edin.
- **Veritabanı izinleri**: Veritabanı dosyasının okuma/yazma izinlerini kontrol edin.

## Katkıda Bulunma

Her türlü katkıya açığız. Lütfen aşağıdaki adımları izleyin:
1. Bu projeyi forklayın.
2. Yeni bir dal (branch) oluşturun: `git checkout -b feature/kitap-arama`.
3. Yaptığınız değişiklikleri gönderin: `git commit -am 'Yeni özellik eklendi'`.
4. Dalınıza gönderin: `git push origin feature/kitap-arama`.
5. Bir **pull request** açın.
