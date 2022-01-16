# Patika - Gelecek Varlik Bitirme Projesi

<!-- <p align="center">
    <img src="images/ekran.gif"/>
</p> -->

- Frontend tarafında React teknolojisi kullanılmıştır.
- Toplam 17 farklı sayfa bulunmaktadır.
- Başlangıçta anasayfa gözükmekte ve burada kullanıcının giriş yapıp yapmadığı kontrol edilmektedir.
- Giriş yapmamış bir kullanıcı menüden işlemlerine erişemez, bu sebepten dolayı sıkıntı yaşaması durumda etkileşime geçebileceği irtibat numaralarını görmektedir.

![Giriş yapılmadığı durumda anasayfa](prometheus-react-app/images/girissizanasayfa.png)

- Giriş yapıldıktan sonra kullanıcı admin değilse kendisi hakkındaki sayfalar hariç hiçbir sayfaya erişemez.

![Normal kullanıcının menüsü](prometheus-react-app/images/normal-kullanici.png)

- Kullanıcı adminse çeşitli sayfalara erişim hakkı vardır.

![Adminin menüsü](prometheus-react-app/images/admin.png)

- Normal kullanıcı mesajlarına, fatura veya aidat bilgilerine, kendi profil bilgilerine erişebilmektedir.

![Mesajlar](prometheus-react-app/images/mesajlarım.png)

- Admin ise kullanıcılara fatura ya da aidat atayabiliyor, kullanıcıları listeleyebiliyor, düzenleyebiliyor, silebiliyor, alacakları görebiliyor, konutları listeleyebiliyor, silebiliyor, konutlara kullanıcı ekleyebiliyor.

## Fatura Eklemeye Geçmek İçin Gerekli Sayfa
![Fatura Eklemek için gelen sayfa](prometheus-react-app/images/fatura-atama-1.png)

## Fatura Ekleme Sayfası
![Fatura Ekleme sayfası](prometheus-react-app/images/fatura-atama-2.png)

## Kullanıcıların Listelendiği Sayfa
![Fatura Ekleme sayfası](prometheus-react-app/images/kullanici-listesi.png)

- Fatura ödeme işlemi, kullanıcı fatura bilgilerine girdiğinde yapılabiliyor.

## Fatura Ödeme
![Fatura Ödeme sayfası](prometheus-react-app/images/fatura-odeme.png)

- Eğer kullanıcının sistemde kayıtlı kredi kartı yoksa ödeme yerine sisteme kredi kartı bilgisinin girilmesi isteniyor ve kredi kartı MongoDB'ye ekleniyor; sistemde kayıtlı kredi bulunuyor ise ödemeyi doğrudan gerçekleştirebiliyor.

## Kredi Kartı Yoksa
![Kartı Kartı Olmadığında](prometheus-react-app/images/kredi-karti-olmayan.png)

- Backend tarafında .Net Core teknolojisi kullanılmıştır.
- Api, service, model, db olarak toplam 4 katmanı bulunuyor.
- Kredi kartı için MongoDB, diğer tüm alanlar için ise MsSql kullanılmıştır.
- Kullanıcı oturumunun yönetilmesi için session ve jwt token kullanılmıştır.
- Kullanıcının token'ına bakılarak giriş yapmış olup olmadığı kontrol edilmekte ve ona göre menüdeki erişimi kısıtlanmaktadır.

## License
[MIT](https://www.mit.edu/)
